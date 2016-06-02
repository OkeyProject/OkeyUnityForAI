using UnityEngine;
using System.Collections;

public class Player1 : Player{

	public Player1(){
		Debug.Log("Player1 create");
	}

	public override bool GetCard ()
	{
		Debug.Log("Player1");
		return base.GetCard ();
	}

	public override void ThrowCard (Card card)
	{
		base.ThrowCard (card);
	}
}
