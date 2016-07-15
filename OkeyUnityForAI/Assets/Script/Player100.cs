using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Player100 : Player{

	private struct pair {
		public int color;
		public int number;

		public pair( int _color , int _number ){
			color = _color;
			number = _number;
		}
	}
		
	public Player100(){
		//Debug.Log("Player1 create");
	}



	public override bool GetCard (Card card)
	{
//		Debug.Log(">>>>>>>>>>>>>>>>>>>");

		//Remember Card I Get


		if ( card != null) {
//			Debug.Log (card.color);
//			Debug.Log (card.number);	
			this.remTable [convert (card.color), card.number] += 1;
			if (shouldTake (card))
				return HAND.TAKE;
			else
				return HAND.DRAW;
			
		}
		else 
			return HAND.DRAW;


	}

	public override Card[,] ThrowCard (Card card){
		
		Card[,] tmp = AI ( card );
		hand = tmp;
		return base.ThrowCard (card);
	}


	private Card[,] AI ( Card card ) {

		int flg = Random.Range (1, 10000);
		if (flg > 5000) {
			int sz = flg % 12;
			if (flg % 2 == 1) {
				hand [flg % 2, sz % 2] = card;
			} else
				hand [flg % 2, sz] = card;
		}

		return hand;

	}

	private int convert( Color color ) {
		if (color == Color.black)
			return 0;
		else if (color == Color.blue)
			return 1;
		else if (color == Color.red)
			return 2;
		else if (color == Color.yellow)
			return 3;
		else
			return -1;
	}

	private Color deconvert( int color ) {
		if (color == 0)
			return Color.black;
		else if (color == 1)
			return Color.blue;
		else if (color == 2)
			return Color.red;
		else if (color == 3)
			return Color.yellow;
		else
			return Color.white;
	}

	private void showTable( int[,] table ){
		string str = "";
		Debug.Log ("Pring Table");
		for (int i = 0; i < 4; i++) {
			str = "";
			for (int j = 1; j < 14; j++) {
				str = str + table[i, j] + " ";
			}
			Debug.Log (str);
		}	
	}





//~~~~~~~~~~~~~~!@~!@~!!!!!!!!!!!!!!!!!!!@~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~!!!!!!!!!!!!!!!!!!!!!!!!
//~~~~~~~~~~~~~~!@~!@~!!!!!!!!!!!!!!!!!!!@~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~!!!!!!!!!!!!!!!!!!!!!!!!
//~~~~~~~~~~~~~~!@~!@~!!!!!!!!!!!!!!!!!!!@~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~!!!!!!!!!!!!!!!!!!!!!!!!
//~~~~~~~~~~~~~~!@~!@~!!!!!!!!!!!!!!!!!!!@~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~!!!!!!!!!!!!!!!!!!!!!!!!
//~~~~~~~~~~~~~~!@~!@~!!!!!!!!!!!!!!!!!!!@~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~!!!!!!!!!!!!!!!!!!!!!!!!


	private bool shouldTake( Card card ){

		int flg = Random.Range(1,10000) % 2;
		if (flg == 1)
			return false;
		else
			return true;

	}




}
