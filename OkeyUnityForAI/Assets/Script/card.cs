using UnityEngine;
using System.Collections;

public class card  {

	public int number;
	public Color color;

	public card(int number, Color color){
		this.number = number;
		this.color = color;
	}

	public string colorToString(){
		if( this.color == Color.black){
			return "Black";
		} else if ( this.color == Color.yellow){
			return "Yellow";
		} else if ( this.color == Color.red){
			return "Red";
		} else if ( this.color == Color.blue){
			return "Blue";
		} else{
			return "Unknown";
		}
	}

}
