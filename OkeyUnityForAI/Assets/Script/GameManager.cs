using UnityEngine;
using System.Collections;



public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CardStack cardStack = new CardStack();
		PlayerHandler playerhandler = new PlayerHandler();
		for(int i=0; i<56; i+=1){
			playerhandler.Deal(cardStack.draw());	
		}
		for(int i=0; i<4; i++){
			playerhandler.Show(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
