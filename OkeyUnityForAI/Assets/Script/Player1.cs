﻿using UnityEngine;
using System.Collections;

public class Player1 : Player{

	public Player1(){
		//Debug.Log("Player1 create");
	}

	public override bool GetCard (Card card)
	{
		//Debug.Log("Player1");
		return base.GetCard (card);
	}

	public override Card[,] ThrowCard (Card card)
	{
		return base.ThrowCard (card);
	}
}
