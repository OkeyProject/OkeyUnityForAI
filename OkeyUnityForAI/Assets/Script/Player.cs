using UnityEngine;
using System.Collections;

public abstract class Player {

	protected Card[,] hand;

	protected Player(){
		hand = new Card[2, 12];
		Debug.Log ("player created");
	}

	public virtual bool GetCard(){
		Debug.Log ("Get");
		return false;
	}

	public virtual void ThrowCard(){
		Debug.Log ("Throw");
	}

	public void Deal(int order, Card card){
		
		if (order < 12){
			hand[0, order] = card;
		} else {
			hand[1, order-12] = card;
		}
	}

	public void DebugShow(){
		string str = "";
		for(int i=0; i<2;i++){
			for(int j=0; j<12;j++){
				if(hand[i,j] == null){
					str += " null ";
				} else{
					str += "("+hand[i,j].number+" "+hand[i,j].ColorToString()+")";
				}
			}
		}
		Debug.Log(str);
	}
	
}
