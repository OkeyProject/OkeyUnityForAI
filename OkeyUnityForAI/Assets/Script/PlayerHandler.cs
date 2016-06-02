using UnityEngine;
using System.Collections;

public class PlayerHandler{
	private Player[] players;


	public PlayerHandler(){
		players = new Player[4];

		for(int i =0; i< 4; i++)
			players[i] = new Player1();

	}



	public bool PlayerGet(int playerID){
		return players[playerID].GetCard();
	}

	public void PlayerThrow(int playerID){
		return players[playerID].ThrowCard();
	}
}
