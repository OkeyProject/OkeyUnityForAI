using UnityEngine;
using System.Collections;

public class CardStack {

	//Private Variable
	private card[] cardStack;
	private int size = 104;

	//Init
	public CardStack() {
		this.cardStack = new card[104];
		for (int i = 0; i < 104; i++) {
			Color tmp = Color.black;
			if (i < 26)
				tmp = Color.black;
			if (i < 52)
				tmp = Color.blue;
			if (i < 78)
				tmp = Color.red;
			if (i < 104)
				tmp = Color.yellow;
			this.cardStack [i] = new card ((i % 13) + 1, tmp);
		}
		this.shuffle ();
	}

	private void shuffle() {
		
		for (int i = 0; i < 500000; i++) {
			int a = Random.Range(1,10000) % 104;
			int b = Random.Range(1,10000) % 104;
			card tmp = cardStack [a];
			cardStack [a] = cardStack [b];
			cardStack [b] = tmp;
		}
	}

	public card draw() {
		if (size <= 0) {
			Debug.LogError ("CardStack Empty");
		} else {
			size--;
			return cardStack[size];
		}
		return cardStack[0];
	}
}

