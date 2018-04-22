


using System.Collections.Generic;
using UnityEngine;
/**
* 无限制的发牌器，将产生指定个玩家数量的牌数，这里不限制玩家的数量，不考虑不同的玩家出现完全同样的牌
* 
* @author Leon
*
*/
public class UnlimitedPlayerProvider : PlayerProvider {

	//private Random random = new Random();

	//@Override
	public Player getSinglePlayer() {
		Player player = new Player();
		for (int j = 0; j < 3; j++) {
			Card card = new Card();
			// 以下防止同一副牌中出现花色和大小都相同的牌
			int cardFlower = getRandomFlower();
			int cardNumber = getRandomNumber();
			if (j == 0) {
				card.setFlower(cardFlower);
				card.setNumber(cardNumber);
			} else if (j == 1) {
				while (cardFlower == player.Cards[0].getFlower() && cardNumber == player.Cards[0].getNumber()) {
					cardFlower = getRandomFlower();
					cardNumber = getRandomNumber();
				}
				card.setFlower(cardFlower);
				card.setNumber(cardNumber);
			} else {
				while ((cardFlower == player.Cards[0].getFlower() && cardNumber == player.Cards[0].getNumber())
						|| (cardFlower == player.Cards[1].getFlower() && cardNumber == player.Cards[1].getNumber())) {
					cardFlower = getRandomFlower();
					cardNumber = getRandomNumber();
				}
				card.setFlower(cardFlower);
				card.setNumber(cardNumber);
			}
			player.Cards[j] = card;
		}
		PlayerUtil.sortPlayerByNumber(player);
		return player;
	}

	//@Override
	public List<Player> getPlayers(int number) {
        List<Player> players = new List<Player>();
		for (int i = 0; i < number; i++) {
			players.Add(getSinglePlayer());
		}
		return players;
	}

	private int getRandomFlower() {
		return Random.Range(0,4);
	}

	private int getRandomNumber() {
		return 2 + Random.Range(0,13);
	}

	//无限制模式下，发单张牌是如此的肆无忌惮
	//@Override
	public Card getCard() {
		return new Card(getRandomFlower(), getRandomNumber());
	}
	
	//@Override
	public void shuffle() {
		// 非限制玩家数的情况，不需要洗牌
	}
}