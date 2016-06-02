using UnityEngine;
using System.Collections;

public class PlayerHandler{
	private Player[] players;
	private int dealOrder;

	public PlayerHandler(){
		players = new Player[4];
		dealOrder = 0;

		for(int i =0; i< 4; i++)
			players[i] = new Player1();

	}



	public bool PlayerGet(int playerID){
		return players[playerID].GetCard();
	}

	public void PlayerThrow(int playerID){
		players[playerID].ThrowCard();
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
}
