using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFlower : MonoBehaviour {

    ValueCalculator Calculator;

    private void Start()
    {
        Calculator = new NonFlowerValueCalculator();
    }
    private void OnGUI()
    {
        if (GUILayout.Button("CalcGoldFlower ()"))
        {
            CalcGoldFlower();
        }
        if (GUILayout.Button("CalcGoldFlowerFix ()"))
        {
            CalcGoldFlowerFix();
        }
    }
    void CalcGoldFlower () {
        LimitedPlayerProvider limitedPlayerProvider = new LimitedPlayerProvider();
        PlayerComparator playerComparator = new PlayerComparator(Calculator);
        List<Player> players;
        players = (limitedPlayerProvider.getPlayers(17));
        playerComparator.sortRegularPlayers(players);
        //playerComparator.sortUnRegularPlayers(players);
        foreach (var p in players)
        {
            Debug.Log($"{p.Cards[0].getNumber()}-{p.Cards[0].getFlower()}" +
                $"-{p.Cards[1].getNumber()}-{p.Cards[1].getFlower()}" +
                $"-{p.Cards[2].getNumber()}-{p.Cards[2].getFlower()}" +
                $"-{p.Value}");
            //foreach (var c in p.Cards)
            //{
            //    Debug.Log(c.getNumber()+"____"+c.getFlower());
            //}
        }
    }

    void CalcGoldFlowerFix()
    {
        LimitedPlayerProvider limitedPlayerProvider = new LimitedPlayerProvider();
        PlayerComparator playerComparator = new PlayerComparator(Calculator);
        List<Player> players;
        //players = (limitedPlayerProvider.getPlayers(17));
        players = new List<Player>();
        players.Add(new Player(new Card(0, 14), new Card(1, 2), new Card(2,3)));
        players.Add(new Player(new Card(0, 4), new Card(1, 2), new Card(2, 3)));
        playerComparator.sortUnRegularPlayers(players);
        foreach (var p in players)
        {
            Debug.Log($"{p.Cards[0].getNumber()}-{p.Cards[0].getFlower()}" +
                $"-{p.Cards[1].getNumber()}-{p.Cards[1].getFlower()}" +
                $"-{p.Cards[2].getNumber()}-{p.Cards[2].getFlower()}" +
                $"-{p.Value}");
            //foreach (var c in p.Cards)
            //{
            //    Debug.Log(c.getNumber()+"____"+c.getFlower());
            //}
        }
    }
}
