using UnityEngine;
using System.Collections;

public static class DECK{

	public static Vector3 CARD_SIZE {get{return new Vector3(0.2f, 0.05f, 0.3f);}}

	public static Vector3[] CARD_POS_0 {
		get{
			Vector3[] positionCal = new Vector3[26];
			for (int i=0; i<13; i++){
				positionCal[i] = new Vector3(-27 + i*0.45f, -1.25f, -0.08f);
				positionCal[i+13] = new Vector3(-27 + i*0.45f, -1.92f, -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3[] CARD_POS_1 {
		get{
			Vector3[] positionCal = new Vector3[26];
			for (int i=0; i<13; i++){
				positionCal[i] = new Vector3(4.8f, -1.6f + i*0.45f, -0.08f);
				positionCal[i+13] = new Vector3(4.15f,-1.6f + i*0.45f, -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3[] CARD_POS_2 {
		get{
			Vector3[] positionCal = new Vector3[26];
			for (int i=0; i<13; i++){
				positionCal[i] = new Vector3(4.8f, 3.8f - i*0.45f, -0.08f);
				positionCal[i+13] = new Vector3(4.15f, 3.8f - i*0.45f, -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3[] CARD_POS_3 {
		get{
			Vector3[] positionCal = new Vector3[26];
			for (int i=0; i<13; i++){
				positionCal[i] = new Vector3(27 - i*0.45f, 4.08f, -0.08f);
				positionCal[i+13] = new Vector3(27 - i*0.45f, 3.41f, -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3 CARD_ROTATION_POSITIVE_0 {get{return new Vector3(270, 180, 0);}}
	public static Vector3 CARD_ROTATION_POSITIVE_1 {get{return new Vector3(0, 270, 270);}}
	public static Vector3 CARD_ROTATION_POSITIVE_2 {get{return new Vector3(0, 90, 90);}}
	public static Vector3 CARD_ROTATION_POSITIVE_3 {get{return new Vector3(0, 90, 90);}}
	

}
