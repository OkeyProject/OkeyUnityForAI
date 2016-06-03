using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class Player {
	protected Card[,] previous;
	protected Card[,] hand;
	protected int id;

	protected Player(){
		hand = new Card[2, 12];
		//Debug.Log ("player created");
	}

	public void SetID(int id){
		this.id = id;
	}

	public virtual bool GetCard(){
		//Debug.Log ("Get");
		return HAND.DRAW;
	}

	public virtual Card[,] ThrowCard(Card card){
		//Debug.Log ("Throw");
		Card[,] newhand = hand;
		newhand[0,0] = card;
		return newhand;
	}

	public void Deal(int order, Card card){
		
		if (order < 12){
			hand[0, order] = card;
		} else {
			hand[1, order-12] = card;
		}
		previous = hand;
	}

	public bool ThrowCheck(Card[,] newhand, Card previousGet){ 
		
		List<Card> oldHand = new List<Card>();
		List<Card> throwHand = new List<Card>();

		oldHand.AddRange(Enumerable.Range(0, 24).Select(i => previous[i/12, i%12]).ToArray());
		oldHand.Add(previousGet);
		throwHand.AddRange(Enumerable.Range(0,24).Select(i => newhand[i/12, i%12]).ToArray());

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
				if(a == null && oldHand[i]==null){
					oldHand.RemoveAt(i);
					flag = false;
					break;
				} else if(a.number == oldHand[i].number && a.color == oldHand[i].color){
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

		hand = newhand;
		Debug.Log("Remain: "+oldHand[0].number+" "+oldHand[0].ColorToString());
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
		Debug.Log(str);
	}

	public void ShowView(){
		View.ShowView(this.id, hand);

	}
}
