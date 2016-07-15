using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class Player {
	protected Card[,] previous;
	protected Card[,] hand;
	public int[,] remTable;
	public bool isWin = false;
	protected int id;

	protected Player(){
		hand = new Card[2, 12];
		previous = new Card[2,12];
		remTable = new int[4,14];
		for (int i = 0; i < 4; i++) {
			for (int j = 1; j < 14; j++) {
				remTable [i, j] = 0;
			}
		}
		//Debug.Log ("player created");
	}

	public void SetID(int id){
		this.id = id;
	}

	public virtual bool GetCard(Card card){
		//Debug.Log ("Get");
		return HAND.DRAW;
	}

	public virtual Card[,] ThrowCard(Card card){
		//hand[0,0] = card;
		return hand;
	}

	public void Deal(int order, Card card){
		
		if (order < 12){
			hand[0, order] = card;
		} else {
			hand[1, order-12] = card;
		}

		this.remTable [convert (card.color), card.number] += 1;

		previous = (Card[,])hand.Clone();
	}

	public bool ThrowCheck(Card[,] newhand, Card previousGet){ 
		
		List<Card> oldHand = new List<Card>();
		List<Card> throwHand = new List<Card>();

		//Debug.Log(previous[0,0].number);
		oldHand.AddRange(Enumerable.Range(0, 24).Select(i => previous[i/12, i%12]).ToArray());
		oldHand.Add(previousGet);
		throwHand.AddRange(Enumerable.Range(0,24).Select(i => newhand[i/12, i%12]).ToArray());

		ThrowDebug(oldHand);
		ThrowDebug(throwHand);

		if(oldHand.Count != 25){
			Debug.LogError("Wrong hand length");
			return false;
		}

		foreach(Card a in throwHand){
			/*if (a == null)
				Debug.Log("Null");
			else
				Debug.Log(a.number+" "+a.ColorToString());*/
			bool flag = true;
			for(int i=0; i<oldHand.Count; i++){
				
//				Debug.Log(oldHand[i]);
				if(a == null && oldHand[i]==null){
					oldHand.RemoveAt(i);
					flag = false;
					break;
				}
				else if(oldHand[i] == null || a == null){
					continue;
				} else if( a.number == oldHand[i].number && a.color == oldHand[i].color){
					oldHand.RemoveAt(i);
					flag = false;
					break;
				}
			}

			if(flag){
				Debug.LogError("Illegal card");
				return false;
			}

		}

		if (oldHand.Count != 1){
			Debug.LogError("Illegal card");
			return false;
		}

		Discard.ThrowCard(this.id, oldHand[0]);
		hand = (Card[,])newhand.Clone();
		previous = (Card[,])hand.Clone();
		//Debug.Log("Remain: "+oldHand[0].number+" "+oldHand[0].ColorToString());
		return true;
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
		//Debug.Log(str);
	}

	public void ShowView(){
		View.ShowView(this.id, hand);

	}

	public void ThrowDebug(List<Card> hand){
		string str = "";
		foreach(Card a in hand){
			if(a == null)
				str += " null ";
			else
				str += "("+a.number+" "+a.ColorToString()+")";
		}

		//Debug.Log(str);
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

}
