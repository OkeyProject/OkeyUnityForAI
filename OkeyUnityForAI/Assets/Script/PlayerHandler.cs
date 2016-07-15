using UnityEngine;
using System.Collections;

public class PlayerHandler{
	private Player[] players;
	private int dealOrder;

	public PlayerHandler(){
		players = new Player[4];
		dealOrder = 0;

//		for(int i =0; i< 4; i++){
//			players[i] = new Player1();
//			players[i].SetID(i);
//		}
		players[0] = new Player1();
		players [0].SetID (0);

		players[1] = new Player10();
		players [1].SetID (1);

		players[2] = new Player11();
		players [2].SetID (2);

		players[3] = new Player100();
		players [3].SetID (3);
	}



	public bool PlayerGet(int playerID, Card card){
		return players[playerID].GetCard(card);
	}

	public void PlayerThrow(int playerID, Card card){
		Card[,] newHand = players[playerID].ThrowCard(card);
		//Debug.LogWarning(newHand[0,0].number);


		if(!players[playerID].ThrowCheck(newHand, card)){
			Debug.LogError("Illegal Throw");
		} else{

//			Remember Card Enemy Throw
			for (int i = 0; i < 4; i++) {
				if (i == playerID)
					continue;
				this.players [i].remTable [convert (card.color), card.number] += 1;
			}
			players[playerID].ShowView();
		}
	}

	public void Deal(Card card){
		if (dealOrder > 56){
			Debug.LogError("Over Dealed");
		}
		players[dealOrder%4].Deal(dealOrder/4, card);
		dealOrder += 1;
	}

	public void Show(int playerID){
		players[playerID].DebugShow();
	}

	public void ShowObject(int playerID){
		players[playerID].ShowView();
	}

	public bool playerIsWin( int playerID ){
		return players [playerID].isWin;
	}

	private int convert( Color color ) {
		if (color == Color.black)
			return 0;
		else if (color == Color.blue)
			return 1;
		else if (color == Color.red)
			return 2;
		else if (color == Color.yellow)
			return 3;
		else
			return -1;
	}
}
