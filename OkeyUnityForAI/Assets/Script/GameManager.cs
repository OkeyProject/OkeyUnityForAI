using UnityEngine;
using System.Collections;



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
			yield return new WaitForSeconds(1f);
			bool getType = playerhandler.PlayerGet(currentPlayer%4);
			Card got;

			if (getType == HAND.DRAW){
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
}
