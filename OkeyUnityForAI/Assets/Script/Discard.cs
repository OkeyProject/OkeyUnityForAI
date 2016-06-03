using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Discard {
	public class Deck{
		public Card card;
		public bool taken;
		public Deck(Card card){
			this.card = card;
			this.taken = false;
		}
	}

	public static List<Deck>[] discard;

	static Discard(){
		discard = new List<Deck>[4];
		for(int i=0; i<4; i++){
			discard[i] = new List<Deck>();
		}
	}

	public static void ThrowCard(int playerID, Card card){
		discard[playerID].Add(new Deck(card));
	}

	public static Card PreviousDiscard(int playerID){
		if (playerID == 0)
			playerID = 3;
		else
			playerID -= 1;

		if (discard[playerID].Count == 0){
			Debug.Log("No card discarded yet");
			return null;
		}

		return discard[playerID][discard[playerID].Count-1].card;
	}

	public static Card Take(int playerID){
		if (playerID == 0)
			playerID = 3;
		else
			playerID -= 1;

		if (discard[playerID].Count == 0){
			Debug.LogError("No card to taken");
			return null;
		}

		discard[playerID][discard[playerID].Count].taken = true;
		return discard[playerID][discard[playerID].Count].card;
	}

	public static void ShowDebug(){
		string str = "";
		for(int i=0; i<4; i++){
			str+="P"+i+" ";
			for(int j=0; j<discard[i].Count; j++){
				str += "("+discard[i][j].card.number+" "+discard[i][j].card.ColorToString()+")";
			}
			str += "\n";
		}

		Debug.Log(str);
	}
}
