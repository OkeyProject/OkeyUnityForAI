using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
	private int currentPlayer = 0;
	// Use this for initialization
	void Start () {
		StartCoroutine(Game());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Game(){
		currentPlayer = 0;
		CardStack cardStack = new CardStack();
		PlayerHandler playerhandler = new PlayerHandler();
		for(int i=0; i<56; i+=1){
			playerhandler.Deal(cardStack.draw());	
		}
		for(int i=0; i<4; i++){
			//playerhandler.Show(i);
			playerhandler.ShowObject(i);
		}
			
		while(true){
			yield return new WaitForSeconds(0.5f);
			bool getType = playerhandler.PlayerGet(currentPlayer%4, Discard.PreviousDiscard(currentPlayer%4));
			Card got;

			if (getType == HAND.DRAW){
				got = cardStack.draw();
				//Debug.Log("Drawed: "+got.number+" "+got.ColorToString());
				if (got.number == 0){
					Debug.Log("Game Over!!!");
					break;
				}
			} else{
				got = Discard.Take(currentPlayer%4);
				View.ShowDiscard ((currentPlayer + 3) % 4);
			}

			playerhandler.PlayerThrow(currentPlayer%4, got);


			View.ShowDiscard(currentPlayer%4);
//			Discard.ShowDebug();
			if (playerhandler.playerIsWin (currentPlayer % 4)) {
				int winner = currentPlayer % 4;
				string str = "Player " + winner.ToString () + " IS Win";

				Debug.Log (str);
				break;
			}

			currentPlayer += 1;

			//if(currentPlayer > 10)
			//	break;
		}
	}
}
