using UnityEngine;
using System.Collections;

public class PlayerHandler{
	private Player[] players;
	private int dealOrder;

	public PlayerHandler(){
		players = new Player[4];
		dealOrder = 0;

		for(int i =0; i< 4; i++){
			players[i] = new Player1();
			players[i].SetID(i);
		}
	}



	public bool PlayerGet(int playerID){
		return players[playerID].GetCard();
	}

	public void PlayerThrow(int playerID, Card card){
		Card[,] newHand = players[playerID].ThrowCard(card);
		//Debug.LogWarning(newHand[0,0].number);

		if(!players[playerID].ThrowCheck(newHand, card)){
			Debug.LogError("Illegal Throw");
		} else{
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
}
