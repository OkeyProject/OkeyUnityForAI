using UnityEngine;
using System.Collections;

public static class DECK{

	public static Vector3 CARD_SIZE {get{return new Vector3(0.2f, 0.05f, 0.3f);}}

	public static Vector3[] CARD_POS_1 {
		get{
			Vector3[] positionCal = new Vector3[16];
			for (int i=0; i<8; i++){
				positionCal[i] = new Vector3((-1.8f+0.4f*i), -2.7f, -0.08f);
				positionCal[i+8] = new Vector3((-1.8f+0.4f*i), -2.1f, -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3[] CARD_POS_2 {
		get{
			Vector3[] positionCal = new Vector3[16];
			for (int i=0; i<8; i++){
				positionCal[i] = new Vector3(2.7f, (-1.8f+0.4f*i), -0.08f);
				positionCal[i+8] = new Vector3(2.1f, (-1.8f+0.4f*i), -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3[] CARD_POS_3 {
		get{
			Vector3[] positionCal = new Vector3[16];
			for (int i=0; i<8; i++){
				positionCal[i] = new Vector3((1.8f-0.4f*i), 2.7f, -0.08f);
				positionCal[i+8] = new Vector3((1.8f-0.4f*i), 2.1f, -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3[] CARD_POS_4 {
		get{
			Vector3[] positionCal = new Vector3[16];
			for (int i=0; i<8; i++){
				positionCal[i] = new Vector3(-2.7f, (1.8f-0.4f*i), -0.08f);
				positionCal[i+8] = new Vector3(-2.1f, (1.8f-0.4f*i), -0.08f);
			}
			return positionCal;
		}
	}

	public static Quaternion CARD_ROTATION_POSITIVE1 {get{return Quaternion.Euler (new Vector3(270, 180, 0));}}
	public static Quaternion CARD_ROTATION_NEGETIVE1 {get{return Quaternion.Euler (new Vector3(270, 0, 0));}}

	public static Quaternion CARD_ROTATION_POSITIVE2 {get{return Quaternion.Euler (new Vector3(0, 270, 270));}}
	public static Quaternion CARD_ROTATION_NEGETIVE2 {get{return Quaternion.Euler (new Vector3(0, 270, 90));}}

	public static Quaternion CARD_ROTATION_POSITIVE3 {get{return Quaternion.Euler (new Vector3(90, 0, 0));}}
	public static Quaternion CARD_ROTATION_NEGETIVE3 {get{return Quaternion.Euler (new Vector3(90, 180, 0));}}

	public static Quaternion CARD_ROTATION_POSITIVE4 {get{return Quaternion.Euler (new Vector3(0, 90, 90));}}
	public static Quaternion CARD_ROTATION_NEGETIVE4 {get{return Quaternion.Euler (new Vector3(0, 90, 270));}}
	

}
