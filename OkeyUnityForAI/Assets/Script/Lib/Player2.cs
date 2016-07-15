using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Player2 : Player{

	private struct pair {
		public int color;
		public int number;

		public pair( int _color , int _number ){
			color = _color;
			number = _number;
		}
	}



	public Player2(){
		//Debug.Log("Player1 create");
	}

	public override bool GetCard (Card card)
	{
		//Debug.Log("Player1");
		Debug.Log(">>>>>>>>>>>>>>>>>>>");

		if ( card != null) {
			Debug.Log (card.color);
			Debug.Log (card.number);	
			return HAND.TAKE;

		}
		else 
			return base.GetCard (card);


	}

	public override Card[,] ThrowCard (Card card){

		Card[,] tmp = AI ();
		return base.ThrowCard (card);
	}

	private Card[,] AI (  ) {

		//Variable
		int[,] tableA = new int[4,14];
		int[,] tableB = new int[4,14] ;
		int num;
		Color col;

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

		Debug.Log ("Initial table is");
		this.showTable (tableA);

		/******************************************************/
		/**********AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA***********/
		/******************************************************/ 
		//		Color First
		//Get Color Done
		int sum = 0;
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
						if (tableA [i, k] > 0)
							sum += 1;
						else if (k == 13) {
							if ( tableA[i, 1] > 0 )
								sum += 1;	
						}
						else
							break;

				}

				if (sum >= 3) {

					for (; j < 14; j++)
						if (tableA [i, j] > 0) {
							tableA [i, j] -= 1;
							doneA [doneAsz].Add (new pair (i, j));
						} 
						else if (j == 13) {
							if (tableA [i, 1] > 0) {
								tableA [i, 1] -= 1;
								doneA [doneAsz].Add (new pair( i,1 ) );
							}
						}
						else
							break;
					doneAsz += 1;
				}

			}
		}


		//** ---------------------------------------------------------**//
		//** ---------------------------------------------------------**//
		//** ---------------------------------------------------------**//

		//Get Color Wait
		sum = 0;
		for (int i = 1; i < 14; i++) {
			sum = 0;
			for (int j = 0; j < 4; j++) {
				if (tableA [j, i] > 0)
					sum += 1;
			}
			if (sum >= 2) {

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
						if (tableA [i, k] > 0)
							sum += 1;
						else if (k == 13) {
							if ( tableA[i, 1] > 0 )
								sum += 1;	
						}
						else
							break;

				}

				if (sum >= 2) {

					for (; j < 14; j++)
						if (tableA [i, j] > 0) {
							tableA [i, j] -= 1;
							waitA [waitAsz].Add (new pair (i, j));
						} 
						else if (j == 13) {
							if (tableA [i, 1] > 0) {
								tableA [i, 1] -= 1;
								waitA [waitAsz].Add (new pair( i,1 ) );
							}
						}
						else
							break;
					waitAsz += 1;
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
		int totalsz = 0 , padding = 0;
		for (int i = 0; i < doneAsz; i++) {
			for (int j = 0; j < doneA [i].Count; j++) {
				hand [totalsz / 12, totalsz % 12].color = this.deconvert (doneA[i][j].color);
				hand [totalsz / 12, totalsz % 12].number = doneA [i] [j].number;
				totalsz += 1;
			}

		}

		for (int i = 0; i < waitAsz; i++) {
			for (int j = 0; j < waitA [i].Count; j++) {
				hand [totalsz / 12, totalsz % 12].color = this.deconvert (waitA[i][j].color);
				hand [totalsz / 12, totalsz % 12].number = waitA [i] [j].number;
				totalsz += 1;
			}
		}

		for (int i = 0 ; i < trashA.Count ; i++) {
			if (totalsz == 14)
				break;
			hand [totalsz / 12, totalsz % 12].color = this.deconvert (trashA[i].color);
			hand [totalsz / 12, totalsz % 12].number = trashA[i].number;
			totalsz += 1;
		}


		//-----------------------------------------------------------------------------------------------------

		///******************************************************/
		///**********BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB***********/
		///******************************************************/ 
		////Number First
		//	//Get Number Done
		//		sum = 0;
		//		for (int i = 0; i < 4; i++) {
		//			for (int j = 1; j < 14; j++) {
		//				sum = 0;
		//				if (tableB [i, j] > 0) { 
		//					for (int k = j; k < 14; k++)
		//						if (tableB [i, k] > 0)
		//							sum += 1;
		//						else if (k == 13) {
		//							if ( tableB[i, 1] > 0 )
		//								sum += 1;	
		//						}
		//						else
		//							break;
		//
		//				}
		//
		//				if (sum >= 3) {
		//
		//					for (; j < 14; j++)
		//						if (tableB [i, j] > 0) {
		//							tableB [i, j] -= 1;
		//							doneB [doneBsz].Add (new pair (i, j));
		//						} 
		//						else if (j == 13) {
		//							if (tableB [i, 1] > 0) {
		//								tableB [i, 1] -= 1;
		//								doneB [doneBsz].Add (new pair( i,1 ) );
		//							}
		//						}
		//						else
		//							break;
		//					doneBsz += 1;
		//				}
		//
		//			}
		//		}
		//
		//		//Get Color Done
		//		sum = 0;
		//		for (int i = 1; i < 14; i++) {
		//			sum = 0;
		//			for (int j = 0; j < 4; j++) {
		//				if (tableB [j, i] > 0)
		//					sum += 1;
		//			}
		//			if (sum >= 3) {
		//
		//				for (int j = 0; j < 4; j++)
		//					if (tableB [j, i] > 0) {
		//						tableB [j, i] -= 1;
		//						doneB [doneBsz].Add ( new pair(j,i) );
		//					}
		//				doneBsz += 1;
		//			}
		//		}
		//
		//
		////** ---------------------------------------------------------**//
		////** ---------------------------------------------------------**//
		////** ---------------------------------------------------------**//
		//
		//		//Get Number Wait
		//		sum = 0;
		//		for (int i = 0; i < 4; i++) {
		//			for (int j = 1; j < 14; j++) {
		//				sum = 0;
		//				if (tableB [i, j] > 0) { 
		//					for (int k = j; k < 14; k++)
		//						if (tableB [i, k] > 0)
		//							sum += 1;
		//						else if (k == 13) {
		//							if ( tableB[i, 1] > 0 )
		//								sum += 1;	
		//						}
		//						else
		//							break;
		//
		//				}
		//
		//				if (sum >= 2) {
		//
		//					for (; j < 14; j++)
		//						if (tableB [i, j] > 0) {
		//							tableB [i, j] -= 1;
		//							waitB [waitBsz].Add (new pair (i, j));
		//						} 
		//						else if (j == 13) {
		//							if (tableB [i, 1] > 0) {
		//								tableB [i, 1] -= 1;
		//								waitB [waitBsz].Add (new pair( i,1 ) );
		//							}
		//						}
		//						else
		//							break;
		//					waitBsz += 1;
		//				}
		//
		//			}
		//		}
		//
		//		//Get Color Wait
		//		sum = 0;
		//		for (int i = 1; i < 14; i++) {
		//			sum = 0;
		//			for (int j = 0; j < 4; j++) {
		//				if (tableB [j, i] > 0)
		//					sum += 1;
		//			}
		//			if (sum >= 2) {
		//
		//				for (int j = 0; j < 4; j++)
		//					if (tableB [j, i] > 0) {
		//						tableB [j, i] -= 1;
		//						waitB [waitBsz].Add ( new pair(j,i) );
		//					}
		//				waitBsz += 1;
		//			}
		//		}

		//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		//////DEGUG FOR CARD CHOOSING//////
		//		Debug.Log ("A done and wait are: >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
		//		Debug.Log (doneAsz);
		//		Debug.Log (waitAsz);
		//		for (int i = 0; i < doneAsz; i++) {
		//			Debug.Log ("The doneA Pair Color Number is");
		//			for (int j = 0; j < doneA [i].Count; j++) {
		//				Debug.Log (doneA[i][j].color);
		//				Debug.Log (doneA[i][j].number);
		//			}
		//		}
		//
		//		for (int i = 0; i < waitAsz; i++) {
		//			Debug.Log ("The waitA Pair Color Number is");
		//			for (int j = 0; j < waitA [i].Count; j++) {
		//				Debug.Log (waitA[i][j].color);
		//				Debug.Log (waitA[i][j].number);
		//			}
		//		}
		//		Debug.Log ("B done and wait are :  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
		//		Debug.Log (doneBsz);
		//		Debug.Log (waitBsz);
		//		for (int i = 0; i < doneBsz; i++) {
		//			Debug.Log ("The doneB Pair Color Number is");
		//			for (int j = 0; j < doneB [i].Count; j++) {
		//				Debug.Log (doneB[i][j].color);
		//				Debug.Log (doneB[i][j].number);
		//			}
		//		}
		//
		//		for (int i = 0; i < waitBsz; i++) {
		//			Debug.Log ("The waitB Pair Color Number is");
		//			for (int j = 0; j < waitB [i].Count; j++) {
		//				Debug.Log (waitB[i][j].color);
		//				Debug.Log (waitB[i][j].number);
		//			}
		//		}




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
}
