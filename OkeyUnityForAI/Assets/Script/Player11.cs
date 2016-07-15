using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Player11 : Player{

	private struct pair {
		public int color;
		public int number;

		public pair( int _color , int _number ){
			color = _color;
			number = _number;
		}
	}
		
	public Player11(){
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
		int num , sum = 0 , rsum = 0;
		Color col;
		Card[,] handA = new Card[2, 12];
		Card[,] handB = new Card[2, 12];

//		Color First Pair
		List< pair >[] doneA = new List<pair>[10];
		List< pair >[] waitA = new List<pair>[10];
		List< pair >  trashA = new List<pair> ();

//		Number First Pair
		List< pair >[] doneB = new List<pair>[10];
		List< pair >[] waitB = new List<pair>[10];
		List< pair >  trashB = new List<pair> ();

		int doneAsz = 0, waitAsz = 0 , doneBsz = 0 , waitBsz = 0; 

//		Init
		for (int i = 0; i < 10; i++) {
			doneA [i] = new List<pair> ();
			waitA [i] = new List<pair> ();
			doneB [i] = new List<pair> ();
			waitB [i] = new List<pair> ();
		}



//		Convert hand to table
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 12; j++) {
				if (hand [i, j] ==  null)
					continue;
				num = hand [i, j].number;
				col = hand [i, j].color;
				tableA [convert (col), num] += 1;
				tableB [convert (col), num] += 1;
			}
		}

		tableA [convert (card.color), card.number] += 1;
		tableB [convert (card.color), card.number] += 1;
//		Debug.Log ("Initial table is");
//		this.showTable (tableA);

///******************************************************/
///**********BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB***********/
///******************************************************/ 
//Number First
	//Get Number Done
		sum = 0;
		for (int i = 0; i < 4; i++) {
			if (doneBsz + waitBsz >= 3)
				break;
			for (int j = 1; j < 14; j++) {
				if (doneBsz + waitBsz >= 3)
					break;
				sum = 0;
				if (tableB [i, j] > 0) { 
					for (int k = j; k < 14; k++)
						if (tableB [i, k] > 0) {
							sum += 1;
							if (k == 13) {
								if (tableB [i, 1] > 0) {
									sum += 1;
								}
							}
						} else
							break;

				}

				if (sum >= 3) {

					for (; j < 14; j++)
						if (tableB [i, j] > 0) {
							tableB [i, j] -= 1;
							doneB [doneBsz].Add (new pair (i, j));
							if (j == 13 && tableB [i, 1] > 0) {
								tableB [i, 1] -= 1;
								doneB [doneBsz].Add (new pair (i, 1));
							}
						} else
							break;
					doneBsz += 1;

				}

			}
		}

		//Get Color Done
		sum = 0;
		for (int i = 1; i < 14; i++) {
			if (doneBsz + waitBsz >= 3)
				break;
			sum = 0;
			for (int j = 0; j < 4; j++) {
				if (tableB [j, i] > 0)
					sum += 1;
			}
			if (sum >= 3) {

				for (int j = 0; j < 4; j++)
					if (tableB [j, i] > 0) {
						tableB [j, i] -= 1;
						doneB [doneBsz].Add ( new pair(j,i) );
					}
				doneBsz += 1;
			}
		}


//** ---------------------------------------------------------**//
//** ---------------------------------------------------------**//
//** ---------------------------------------------------------**//

		//Get Number Wait
		sum = 0;
		for (int i = 0; i < 4; i++) {
			if (doneBsz + waitBsz >= 3)
				break;
			for (int j = 1; j < 14; j++) {
				if (doneBsz + waitBsz >= 3)
					break;
				sum = 0;
				if (tableB [i, j] > 0) { 
					for (int k = j; k < 14; k++)
						if (tableB [i, k] > 0) {
							sum += 1;
							if (k == 13) {
								if (tableB [i, 1] > 0) {
									sum += 1;
								}
							}
						} else
							break;

				}

				if (sum == 1 ) {


					if (j == 12 ) {
						if (this.remTable [i, j + 1] < 2 && tableB [i, 1] > 0) {
							tableB [i, 12] -= 1;
							tableB [i, 1] -= 1;
							waitB [waitBsz].Add (new pair (i, 12));
							waitB [waitBsz].Add (new pair (i, 1));
							j += 2;
							waitBsz += 1;
						}
					} 
					else if( j == 13 ) {

						break;
					}
					else if (this.remTable [i, j + 1] < 2 && tableB [i, j + 2] > 0) {
						tableB [i, j] -= 1;
						tableB [i, j + 2] -= 1;
						waitB [waitBsz].Add (new pair (i, j));
						waitB [waitBsz].Add (new pair (i, j + 2));
						j += 2;
						waitBsz += 1;
					} 

					else
						break;

				}

				if (sum == 2) {
					if ( ( j == 1 && this.remTable [i, j + 2] < 2 ) || (j == 12 && (this.remTable [i, j - 1] < 2 || this.remTable [i, 1] < 2)) 
						|| (j == 13 && this.remTable [i, j - 1] < 2) || (this.remTable [i, j - 1] < 2 || this.remTable [i, j + 2] < 2) ) {
						for (; j < 14; j++)
							if (tableB [i, j] > 0) {
								tableB [i, j] -= 1;
								waitB [waitBsz].Add (new pair (i, j));
								if (j == 13 && tableB [i, 1] > 0) {
									tableB [i, 1] -= 1;
									waitB [waitBsz].Add (new pair (i, 1));
								}
							} else
								break;
						waitBsz += 1;
					} 

				}

			}
		}

		//Get Color Wait
		sum = rsum = 0;
		for (int i = 1; i < 14; i++) {
			sum = 0;
			for (int j = 0; j < 4; j++) {
				if (tableB [j, i] > 0)
					sum += 1;
				else if (this.remTable [j,i] > 1) {
					rsum += 1;
				}
			}

			if (sum == 2 && rsum < 2 ) {

				for (int j = 0; j < 4; j++)
					if (tableB [j, i] > 0) {
						tableB [j, i] -= 1;
						waitB [waitBsz].Add ( new pair(j,i) );
					}
				waitBsz += 1;
			}
		}

		//Get TrashB
		for (int i = 0; i < 4; i++) {
			for (int j = 1; j < 14; j++) {
				while (tableB [i, j] > 0) {
					trashB.Add (new pair (i, j));
					tableB [i, j] -= 1;
				}
			}
		}

		//Rebuild Hand
		int totalsz = 0 ;
		for (int i = 0; i < doneBsz; i++) {
			for (int j = 0; j < doneB [i].Count; j++) {
//				Debug.Log ("The Card is ");
//				Debug.Log (doneB[i][j].color);
//				Debug.Log (doneB[i][j].number);
				handB [totalsz / 12, totalsz % 12] = new Card (doneB[i][j].number,this.deconvert (doneB[i][j].color));
//				handB [totalsz / 12, totalsz % 12].color = this.deconvert (doneB[i][j].color);
//				handB [totalsz / 12, totalsz % 12].number = doneB [i] [j].number;
				totalsz += 1;
			}

		}

		for (int i = 0; i < waitBsz; i++) {

			for (int j = 0; j < waitB [i].Count; j++) {

				if (totalsz > 13)
					break;
//				Debug.Log ("The Card is ");
//				Debug.Log (waitB[i][j].color);
//				Debug.Log (waitB[i][j].number);
				handB [totalsz / 12, totalsz % 12] = new Card (waitB[i][j].number,this.deconvert (waitB[i][j].color));
//				handB [totalsz / 12, totalsz % 12].color = this.deconvert (waitB[i][j].color);
//				handB [totalsz / 12, totalsz % 12].number = waitB [i] [j].number;
				totalsz += 1;
			}
		}

		for (int i = 0 ; i < trashB.Count ; i++) {
			if (totalsz > 13)
				break;
//			Debug.Log ("The Card is ");
//			Debug.Log (trashB[i].color);
//			Debug.Log (trashB[i].number);
			handB [totalsz / 12, totalsz % 12] = new Card (trashB[i].number,this.deconvert (trashB[i].color));
//			handB [totalsz / 12, totalsz % 12].color = this.deconvert (trashB[i].color);
//			handB [totalsz / 12, totalsz % 12].number = trashB[i].number;
			totalsz += 1;
		}

		if ( waitBsz == 0 && trashB.Count <= 1)
			this.isWin = true;
		
			return handB;
				
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
		int[,] tableB = new int[4,14] ;
		int num , sum , rsum , totalsz;
		Color col;

		//		Color First Pair
		List< pair >[] doneA = new List<pair>[10];
		List< pair >[] waitA = new List<pair>[10];
//		List< pair >  trashA = new List<pair> ();

		//		Number First Pair
		List< pair >[] doneB = new List<pair>[10];
		List< pair >[] waitB = new List<pair>[10];
//		List< pair >  trashB = new List<pair> ();

		int doneAsz = 0, waitAsz = 0 , doneBsz = 0 , waitBsz = 0; 

		//		Init
		for (int i = 0; i < 10; i++) {
			doneA [i] = new List<pair> ();
			waitA [i] = new List<pair> ();
			doneB [i] = new List<pair> ();
			waitB [i] = new List<pair> ();
		}

		//		Convert hand to table
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 12; j++) {
				if (hand [i, j] ==  null)
					continue;
				num = hand [i, j].number;
				col = hand [i, j].color;
				tableA [convert (col), num] += 1;
				tableB [convert (col), num] += 1;
			}
		}

		int goalcol = convert (card.color);
		int goalnum = card.number;
		tableA [goalcol, goalnum] += 1;
		tableB [goalcol, goalnum] += 1;

		//		Debug.Log ("Initial table is");
		//		this.showTable (tableA);

		//Get Number Done
		sum = 0;
		for (int i = 0; i < 4; i++) {
			for (int j = 1; j < 14; j++) {
				sum = 0;
				if (tableB [i, j] > 0) { 
					for (int k = j; k < 14; k++)
						if (tableB [i, k] > 0)
							sum += 1;
						else if (k == 13) {
							if ( tableB[i, 1] > 0 )
								sum += 1;	
						}
						else
							break;

				}

				if (sum >= 3) {

					for (; j < 14; j++)
						if (tableB [i, j] > 0) {
							tableB [i, j] -= 1;
							doneB [doneBsz].Add (new pair (i, j));
						} 
						else if (j == 13) {
							if (tableB [i, 1] > 0) {
								tableB [i, 1] -= 1;
								doneB [doneBsz].Add (new pair( i,1 ) );
							}
						}
						else
							break;
					doneBsz += 1;
				}

			}
		}

		//Get Color Done
		sum = 0;
		for (int i = 1; i < 14; i++) {
			sum = 0;
			for (int j = 0; j < 4; j++) {
				if (tableB [j, i] > 0)
					sum += 1;
			}
			if (sum >= 3) {

				for (int j = 0; j < 4; j++)
					if (tableB [j, i] > 0) {
						tableB [j, i] -= 1;
						doneB [doneBsz].Add ( new pair(j,i) );
					}
				doneBsz += 1;
			}
		}


//** ---------------------------------------------------------**//
//** ---------------------------------------------------------**//
//** ---------------------------------------------------------**//

		//Get Number Wait
		sum = 0;
		for (int i = 0; i < 4; i++) {
			for (int j = 1; j < 14; j++) {
				sum = 0;
				if (tableB [i, j] > 0) { 
					for (int k = j; k < 14; k++)
						if (tableB [i, k] > 0)
							sum += 1;
						else if (k == 13) {
							if ( tableB[i, 1] > 0 )
								sum += 1;	
						}
						else
							break;

				}

				if (sum >= 2) {

					for (; j < 14; j++)
						if (tableB [i, j] > 0) {
							tableB [i, j] -= 1;
							waitB [waitBsz].Add (new pair (i, j));
						} 
						else if (j == 13) {
							if (tableB [i, 1] > 0) {
								tableB [i, 1] -= 1;
								waitB [waitBsz].Add (new pair( i,1 ) );
							}
						}
						else
							break;
					waitBsz += 1;
				}

			}
		}

		//Get Color Wait
		sum = 0;
		for (int i = 1; i < 14; i++) {
			sum = 0;
			for (int j = 0; j < 4; j++) {
				if (tableB [j, i] > 0)
					sum += 1;
			}
			if (sum >= 2) {

				for (int j = 0; j < 4; j++)
					if (tableB [j, i] > 0) {
						tableB [j, i] -= 1;
						waitB [waitBsz].Add ( new pair(j,i) );
					}
				waitBsz += 1;
			}
		}

		if (tableB [goalcol, goalnum] > 0) {
			return false;
		}
		return true;

	}




}
