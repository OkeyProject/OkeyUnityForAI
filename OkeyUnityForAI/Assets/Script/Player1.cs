using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Player1 : Player{

	private struct pair {
		public int color;
		public int number;

		public pair( int _color , int _number ){
			color = _color;
			number = _number;
		}
	}

	public Player1(){
		//Debug.Log("Player1 create");
	}

	public override bool GetCard (Card card)
	{
		//Debug.Log("Player1");
		return base.GetCard (card);
	}

	public override Card[,] ThrowCard (Card card){
		
		Card[,] tmp = AI ();
		return base.ThrowCard (card);
	}

	private Card[,] AI () {

		//Variable
		int[,] table = new int[4,14];
		int num;
		Color col;

		List< pair >[] done = new List<pair>[10];
		List< pair >[] wait = new List<pair>[10];

		int donesz = 0, waitsz = 0; 

		//Init
		for (int i = 0; i < 10; i++) {
			done [i] = new List<pair> ();
			wait [i] = new List<pair> ();
		}

		//Convert hand to table
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 12; j++) {
				if (hand [i, j] ==  null)
					continue;
				num = hand [i, j].number;
				col = hand [i, j].color;
				table [convert (col), num] += 1;
			}
		}

//		this.showTable (table);

		//Color First
		//Get Color Done
		int sum = 0;
		for (int i = 1; i < 14; i++) {
			sum = 0;
			for (int j = 0; j < 4; j++) {
				if (table [j, i] > 0)
					sum += 1;
			}
			if (sum >= 3) {
				
				for (int j = 0; j < 4; j++)
					if (table [j, i] > 0) {
						table [j, i] -= 1;
						done [donesz].Add ( new pair(j,i) );
					}
				donesz += 1;
			}
		}

		this.showTable (table);
			
		//Get Number Done
		sum = 0;
		for (int i = 0; i < 4; i++) {
			for (int j = 1; j < 14; j++) {
				sum = 0;
				if (table [i, j] > 0) { 
					Debug.Log ("pair is ");
					Debug.Log (i);
					Debug.Log (j);
					for (int k = j; k < 14; k++)
						if (table [i, k] > 0)
							sum += 1;
						else if (k == 13) {
							if ( table[i, 1] > 0 )
								sum += 1;	
						}
						else
							break;
					
				}
//				Debug.Log ("sum is :");
//				Debug.Log (sum);
				if (sum >= 3) {

					for (; j < 14; j++)
						if (table [i, j] > 0) {
							table [i, j] -= 1;
							done [donesz].Add (new pair (i, j));
						} 
						else if (j == 13) {
							if (table [i, 1] > 0) {
								table [i, 1] -= 1;
								done [donesz].Add (new pair( i,1 ) );
							}
						}
						else
							break;
					donesz += 1;
				}

			}
		}

		this.showTable (table);
		Debug.Log ("finish number");
		Debug.Log (donesz);


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

	private void showTable( int[,] table ){
		string str = "";
		Debug.Log ("Pring Table");
		for (int i = 0; i < 4; i++) {
			str = "";
			for (int j = 1; j < 15; j++) {
				str = str + table[i, j] + " ";
			}
			Debug.Log (str);
		}	
	}
}
