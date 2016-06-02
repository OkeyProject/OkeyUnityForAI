using UnityEngine;
using System.Collections;



public class GameManager : MonoBehaviour {
	private int currentPlayer = 0;
	private bool DRAW = false;
	private bool TAKE = true;
	// Use this for initialization
	void Start () {
		currentPlayer = 0;
		CardStack cardStack = new CardStack();
		PlayerHandler playerhandler = new PlayerHandler();
		for(int i=0; i<56; i+=1){
			playerhandler.Deal(cardStack.draw());	
		}
		for(int i=0; i<4; i++){
			playerhandler.Show(i);
		}

		while(true){
			bool getType = playerhandler.PlayerGet(currentPlayer%4);
			Card got;

			if (getType == DRAW){
				got = cardStack.draw();
			} else{
				got = new Card(0, Color.white);
			}

			playerhandler.PlayerThrow(currentPlayer%4, got);

			currentPlayer += 1;

			if(currentPlayer > 10)
				break;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
