using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// /////////////////
/// COLOR FIRST
/// /////////////////
/// </summary>

public class Player3 : Player{

	private struct pair {
		public int color;
		public int number;

		public pair( int _color , int _number ){
			color = _color;
			number = _number;
		}
	}

	public Player3(){
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

		//Variable
		int[,] tableA = new int[4,14];
		int[,] tableB = new int[4,14] ;
		int num;
		Color col;
		Card[,] handA = new Card[2, 12];
		Card[,] handB = new Card[2, 12];

		//		Color First Pair
		List< pair >[] doneA = new List<pair>[10];
		List< pair >[] waitA = new List<pair>[10];
		List< pair >  trashA = new List<pair> ();

		//		Number First Pair
//		List< pair >[] doneB = new List<pair>[10];
//		List< pair >[] waitB = new List<pair>[10];
//		List< pair >  trashB = new List<pair> ();

		int doneAsz = 0, waitAsz = 0 , doneBsz = 0 , waitBsz = 0; 

		//		Init
		for (int i = 0; i < 10; i++) {
			doneA [i] = new List<pair> ();
			waitA [i] = new List<pair> ();
//			doneB [i] = new List<pair> ();
//			waitB [i] = new List<pair> ();
		}



		//		Convert hand to table
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 12; j++) {
				if (hand [i, j] ==  null)
					continue;
				num = hand [i, j].number;
				col = hand [i, j].color;
				tableA [convert (col), num] += 1;
//				tableB [convert (col), num] += 1;
			}
		}

		tableA [convert (card.color), card.number] += 1;
//		tableB [convert (card.color), card.number] += 1;
		//		Debug.Log ("Initial table is");
		//		this.showTable (tableA);

		/******************************************************/
		/**********AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA***********/
		/******************************************************/ 
		//		Color First
		//Get Color Done
		int sum = 0 , rsum = 0;
		for (int i = 1; i < 14; i++) {
			if (doneAsz >= 3)
				break;
			sum = 0;
			for (int j = 0; j < 4; j++) {
				if (tableA [j, i] > 0)
					sum += 1;
			}
			if (sum >= 3) {

				for (int j = 0; j < 4; j++)
					if (tableA [j, i] > 0) {
						tableA [j, i] -= 1;
						doneA [doneAsz].Add ( new pair(j,i) );
					}
				doneAsz += 1;
			}
		}

		//Get Number Done
		sum = 0;
		for (int i = 0; i < 4; i++) {
			if (doneAsz >= 3)
				break;
			for (int j = 1; j < 14; j++) {
				if (doneAsz >= 3)
					break;
				sum = 0;
				if (tableA [i, j] > 0) { 
					for (int k = j; k < 14; k++)
						if (tableA [i, k] > 0) {
							sum += 1;
							if (k == 13) {
								if (tableA [i, 1] > 0) {
									sum += 1;
								}
							}
						} else
							break;

				}

				if (sum >= 3) {

					for (; j < 14; j++)
						if (tableA [i, j] > 0) {
							tableA [i, j] -= 1;
							doneA [doneAsz].Add (new pair (i, j));
							if (j == 13 && tableA [i, 1] > 0) {
								tableA [i, 1] -= 1;
								doneA [doneAsz].Add (new pair (i, 1));
							}
						} else
							break;
					doneAsz += 1;

				}

			}
		}


		//** ---------------------------------------------------------**//
		//** ---------------------------------------------------------**//
		//** ---------------------------------------------------------**//

		//Get Color Wait
		sum = rsum = 0;

		for (int i = 1; i < 14; i++) {
			if (doneAsz + waitAsz >= 3)
				break;
			sum = 0;
			for (int j = 0; j < 4; j++) {
				if (tableA [j, i] > 0)
					sum += 1;
				else if (this.remTable [j, i] > 1) {
					rsum += 1;
				}
			}

			if (sum == 2 && rsum < 2) {

				for (int j = 0; j < 4; j++)
					if (tableA [j, i] > 0) {
						tableA [j, i] -= 1;
						waitA [waitAsz].Add (new pair (j, i));
					}
				waitAsz += 1;
			}
		}

		//Get Number Wait
		sum = 0;

		for (int i = 0; i < 4; i++) {
			if (doneAsz + waitAsz >= 3)
				break;
			for (int j = 1; j < 14; j++) {
				if (doneAsz + waitAsz >= 3)
					break;
				sum = 0;
				if (tableA [i, j] > 0) { 
					for (int k = j; k < 14; k++)
						if (tableA [i, k] > 0) {
							sum += 1;
							if (k == 13) {
								if (tableA [i, 1] > 0) {
									sum += 1;
								}
							}
						} else
							break;

				}

				if (sum == 1) {


					if (j == 12) {
						if (this.remTable [i, j + 1] < 2 && tableA [i, 1] > 0) {
							tableA [i, 12] -= 1;
							tableA [i, 1] -= 1;
							waitA [waitAsz].Add (new pair (i, 12));
							waitA [waitAsz].Add (new pair (i, 1));
							j += 2;
							waitAsz += 1;
						}
					} else if (j == 13) {

						break;
					} else if (this.remTable [i, j + 1] < 2 && tableA [i, j + 2] > 0) {
						tableA [i, j] -= 1;
						tableA [i, j + 2] -= 1;
						waitA [waitAsz].Add (new pair (i, j));
						waitA [waitAsz].Add (new pair (i, j + 2));
						j += 2;
						waitAsz += 1;
					} else
						break;

				}

				if (sum == 2) {
					if ((j == 1 && this.remTable [i, j + 2] < 2) || (j == 12 && (this.remTable [i, j - 1] < 2 || this.remTable [i, 1] < 2))
						|| (j == 13 && this.remTable [i, j - 1] < 2) || (this.remTable [i, j - 1] < 2 || this.remTable [i, j + 2] < 2)) {
						for (; j < 14; j++)
							if (tableA [i, j] > 0) {
								tableA [i, j] -= 1;
								waitA [waitAsz].Add (new pair (i, j));
								if (j == 13 && tableA [i, 1] > 0) {
									tableA [i, 1] -= 1;
									waitA [waitAsz].Add (new pair (i, 1));
								}
							} else
								break;
						waitAsz += 1;
					} 

				}

			}
		}


		//Get TrashA
		for (int i = 0; i < 4; i++) {
			for (int j = 1; j < 14; j++) {
				while (tableA [i, j] > 0) {
					trashA.Add (new pair (i, j));
					tableA [i, j] -= 1;
				}
			}
		}

		//Rebuild Hand
		int totalsz = 0 ;
		for (int i = 0; i < doneAsz; i++) {
			for (int j = 0; j < doneA [i].Count; j++) {
				handA [totalsz / 12, totalsz % 12] = new Card (doneA[i][j].number,this.deconvert (doneA[i][j].color));
				//				handA [totalsz / 12, totalsz % 12].color = this.deconvert (doneA[i][j].color);
				//				handA [totalsz / 12, totalsz % 12].number = doneA [i] [j].number;
				totalsz += 1;
			}

		}

		for (int i = 0; i < waitAsz; i++) {

			for (int j = 0; j < waitA [i].Count; j++) {
				if (totalsz > 13)
					break;
				handA [totalsz / 12, totalsz % 12] = new Card (waitA[i][j].number,this.deconvert (waitA[i][j].color));
				//				handA [totalsz / 12, totalsz % 12].color = this.deconvert (waitA[i][j].color);
				//				handA [totalsz / 12, totalsz % 12].number = waitA [i] [j].number;
				totalsz += 1;
			}
		}

		for (int i = 0 ; i < trashA.Count ; i++) {
			if (totalsz > 13)
				break;
			handA [totalsz / 12, totalsz % 12] = new Card (trashA[i].number,this.deconvert (trashA[i].color));
			//			handA [totalsz / 12, totalsz % 12].color = this.deconvert (trashA[i].color);
			//			handA [totalsz / 12, totalsz % 12].number = trashA[i].number;
			totalsz += 1;
		}


			return handA;
		
		//		return hand;

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

		//Variable
		int[,] tableA = new int[4,14];
//		int[,] tableB = new int[4,14] ;
		int num;
		Color col;

		//		Color First Pair
		List< pair >[] doneA = new List<pair>[10];
		List< pair >[] waitA = new List<pair>[10];
		//		List< pair >  trashA = new List<pair> ();

		//		Number First Pair
//		List< pair >[] doneB = new List<pair>[10];
//		List< pair >[] waitB = new List<pair>[10];
		//		List< pair >  trashB = new List<pair> ();

		int doneAsz = 0, waitAsz = 0 , doneBsz = 0 , waitBsz = 0; 

		//		Init
		for (int i = 0; i < 10; i++) {
			doneA [i] = new List<pair> ();
			waitA [i] = new List<pair> ();
//			doneB [i] = new List<pair> ();
//			waitB [i] = new List<pair> ();
		}

		//		Convert hand to table
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 12; j++) {
				if (hand [i, j] ==  null)
					continue;
				num = hand [i, j].number;
				col = hand [i, j].color;
				tableA [convert (col), num] += 1;
//				tableB [convert (col), num] += 1;
			}
		}

		int goalcol = convert (card.color);
		int goalnum = card.number;
		tableA [goalcol, goalnum] += 1;
//		tableB [goalcol, goalnum] += 1;

		//		Debug.Log ("Initial table is");
		//		this.showTable (tableA);

/******************************************************/
/**********AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA***********/
/******************************************************/ 
		//		Color First
		//Get Color Done
		int sum = 0 , rsum = 0;
		for (int i = 1; i < 14; i++) {
			sum = 0;
			for (int j = 0; j < 4; j++) {
				if (tableA [j, i] > 0)
					sum += 1;
			}
			if (sum >= 3) {

				for (int j = 0; j < 4; j++)
					if (tableA [j, i] > 0) {
						tableA [j, i] -= 1;
						doneA [doneAsz].Add ( new pair(j,i) );
					}
				doneAsz += 1;
			}
		}

		//Get Number Done
		sum = 0;
		for (int i = 0; i < 4; i++) {
			for (int j = 1; j < 14; j++) {
				sum = 0;
				if (tableA [i, j] > 0) { 
					for (int k = j; k < 14; k++)
						if (tableA [i, k] > 0) {
							sum += 1;
							if (k == 13) {
								if (tableA [i, 1] > 0) {
									sum += 1;
								}
							}
						} else
							break;

				}

				if (sum >= 3) {

					for (; j < 14; j++)
						if (tableA [i, j] > 0) {
							tableA [i, j] -= 1;
							doneA [doneAsz].Add (new pair (i, j));
							if (j == 13 && tableA [i, 1] > 0) {
								tableA [i, 1] -= 1;
								doneA [doneAsz].Add (new pair (i, 1));
							}
						} else
							break;
					doneAsz += 1;

				}

			}
		}


//** ---------------------------------------------------------**//
//** ---------------------------------------------------------**//
//** ---------------------------------------------------------**//

		//Get Color Wait
		sum = rsum = 0;

		for (int i = 1; i < 14; i++) {
			sum = 0;
			for (int j = 0; j < 4; j++) {
				if (tableA [j, i] > 0)
					sum += 1;
				else if (this.remTable [j,i] > 1) {
					rsum += 1;
				}
			}

			if (sum == 2 && rsum < 2 ) {

				for (int j = 0; j < 4; j++)
					if (tableA [j, i] > 0) {
						tableA [j, i] -= 1;
						waitA [waitAsz].Add ( new pair(j,i) );
					}
				waitAsz += 1;
			}
		}

		//Get Number Wait
		sum = 0;
		for (int i = 0; i < 4; i++) {
			for (int j = 1; j < 14; j++) {
				sum = 0;
				if (tableA [i, j] > 0) { 
					for (int k = j; k < 14; k++)
						if (tableA [i, k] > 0) {
							sum += 1;
							if (k == 13) {
								if (tableA [i, 1] > 0) {
									sum += 1;
								}
							}
						} else
							break;

				}

				if (sum == 1 ) {


					if (j == 12 ) {
						if (this.remTable [i, j + 1] < 2 && tableA [i, 1] > 0) {
							tableA [i, 12] -= 1;
							tableA [i, 1] -= 1;
							waitA [waitAsz].Add (new pair (i, 12));
							waitA [waitAsz].Add (new pair (i, 1));
						}
					} 
					else if( j == 13 ) {

						break;
					}

					else if (this.remTable [i, j + 1] < 2 && tableA [i, j + 2] > 0) {
						tableA [i, j] -= 1;
						tableA [i, j + 2] -= 1;
						waitA [waitAsz].Add (new pair (i, j));
						waitA [waitAsz].Add (new pair (i, j + 2));
					} 

					else
						break;
					j += 2;
					waitAsz += 1;
				}

				if (sum == 2) {
					if ( ( j == 1 && this.remTable [i, j + 2] < 2 ) || (j == 12 && (this.remTable [i, j - 1] < 2 || this.remTable [i, 1] < 2)) 
						|| (j == 13 && this.remTable [i, j - 1] < 2) || (this.remTable [i, j - 1] < 2 || this.remTable [i, j + 2] < 2) ) {
						for (; j < 14; j++)
							if (tableA [i, j] > 0) {
								tableA [i, j] -= 1;
								waitA [waitAsz].Add (new pair (i, j));
								if (j == 13 && tableA [i, 1] > 0) {
									tableA [i, 1] -= 1;
									waitA [waitAsz].Add (new pair (i, 1));
								}
							} else
								break;
						waitAsz += 1;
					} 

				}

			}
		}


		if (tableA [goalcol, goalnum] > 0 ) {
			return false;
		}
		return true;

	}




}
