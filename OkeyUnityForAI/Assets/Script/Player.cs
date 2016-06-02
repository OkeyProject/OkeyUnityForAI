using UnityEngine;
using System.Collections;

public abstract class Player {

	protected card[,] hand;

	protected Player(){
		hand = new card[2, 12];
		Debug.Log ("player created");
	}

	public virtual bool GetCard(){
		Debug.Log ("Get");
		return false;
	}

	public virtual void ThrowCard(){
		Debug.Log ("Throw");
	}
	
}
