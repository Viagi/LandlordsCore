using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_GamerPlayCard_ReqHandler : AMActorRpcHandler<Gamer, Actor_GamerPlayCard_Req, Actor_GamerPlayCard_Ack>
    {
        protected override async Task Run(Gamer gamer, Actor_GamerPlayCard_Req message, Action<Actor_GamerPlayCard_Ack> reply)
        {
            Actor_GamerPlayCard_Ack response = new Actor_GamerPlayCard_Ack();
            try
            {
                Room room = Game.Scene.GetComponent<RoomComponent>().Get(gamer.RoomID);

                GameControllerComponent gameController = room.GetComponent<GameControllerComponent>();
                DeskCardsCacheComponent deskCardsCache = room.GetComponent<DeskCardsCacheComponent>();
                OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();

                //检测是否符合出牌规则
                if (CardsHelper.PopEnable(message.Cards, out CardsType type))
                {
                    //当前出牌牌型是否比牌桌上牌型的权重更大
                    bool isWeightGreater = CardsHelper.GetWeight(message.Cards, type) > deskCardsCache.GetTotalWeight();
                    //当前出牌牌型是否和牌桌上牌型的数量一样
                    bool isSameCardsNum = message.Cards.Length == deskCardsCache.GetAll().Length;
                    //当前出牌玩家是否是上局最大出牌者
                    bool isBiggest = orderController.Biggest == orderController.CurrentAuthority;
                    //当前牌桌牌型是否是顺子
                    bool isStraight = deskCardsCache.Rule == CardsType.Straight || deskCardsCache.Rule == CardsType.DoubleStraight || deskCardsCache.Rule == CardsType.TripleStraight;
                    //当前出牌牌型是否和牌桌上牌型一样
                    bool isSameCardsType = type == deskCardsCache.Rule;

                    if (isBiggest ||    //先手出牌玩家
                        type == CardsType.JokerBoom ||  //王炸
                        type == CardsType.Boom && isWeightGreater ||    //更大的炸弹
                        isSameCardsType && isStraight && isSameCardsNum && isWeightGreater ||   //更大的顺子
                        isSameCardsType && isWeightGreater)     //更大的同类型牌
                    {
                        if (type == CardsType.JokerBoom)
                        {
                            //王炸翻4倍
                            gameController.Multiples *= 4;
                            room.Broadcast(new Actor_SetMultiples_Ntt() { Multiples = gameController.Multiples });
                        }
                        else if (type == CardsType.Boom)
                        {
                            //炸弹翻2倍
                            gameController.Multiples *= 2;
                            room.Broadcast(new Actor_SetMultiples_Ntt() { Multiples = gameController.Multiples });
                        }
                    }
                    else
                    {
                        response.Error = ErrorCode.ERR_PlayCardError;
                        reply(response);
                        return;
                    }
                }
                else
                {
                    response.Error = ErrorCode.ERR_PlayCardError;
                    reply(response);
                    return;
                }

                //如果符合将牌从手牌移到出牌缓存区
                deskCardsCache.Clear();
                deskCardsCache.Rule = type;
                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                foreach (var card in message.Cards)
                {
                    handCards.PopCard(card);
                    deskCardsCache.AddCard(card);
                }

                //转发玩家出牌消息
                room.Broadcast(new Actor_GamerPlayCard_Ntt() { UserID = gamer.UserID, Cards = message.Cards });

                if (handCards.CardsCount == 0)
                {
                    //当前出牌者手牌数为0时游戏结束，当前最大出牌者为赢家
                    Identity winnerIdentity = room.Get(orderController.Biggest).GetComponent<HandCardsComponent>().AccessIdentity;
                    Dictionary<long, long> gamersScore = new Dictionary<long, long>();

                    //游戏结束所有玩家摊牌
                    foreach (var _gamer in room.GetAll())
                    {
                        _gamer.RemoveComponent<TrusteeshipComponent>();
                        gamersScore.Add(_gamer.UserID, gameController.GetScore(_gamer, winnerIdentity));

                        if (_gamer.UserID != gamer.UserID)
                        {
                            Card[] _gamerCards = _gamer.GetComponent<HandCardsComponent>().GetAll();
                            room.Broadcast(new Actor_GamerPlayCard_Ntt() { UserID = _gamer.UserID, Cards = _gamerCards });
                        }
                    }

                    //游戏结束结算
                    gameController.GameOver(gamersScore);

                    //广播游戏结束消息
                    room.Broadcast(new Actor_Gameover_Ntt()
                    {
                        Winner = winnerIdentity,
                        BasePointPerMatch = gameController.BasePointPerMatch,
                        Multiples = gameController.Multiples,
                        GamersScore = gamersScore
                    });
                }
                else
                {
                    //轮到下位玩家出牌
                    orderController.Biggest = gamer.UserID;
                    orderController.Turn();
                    room.Broadcast(new Actor_AuthorityPlayCard_Ntt() { UserID = orderController.CurrentAuthority, IsFirst = false });
                }

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }

            await Task.CompletedTask;
        }
    }
}
