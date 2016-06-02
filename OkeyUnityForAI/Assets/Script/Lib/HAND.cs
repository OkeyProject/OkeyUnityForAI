using UnityEngine;
using System.Collections;

public static class HAND {
	public static Vector3 CARD_SCALE {get{return new Vector3(0.25f, 0.05f, 0.375f);}}

	public static Vector3[,] CARD_POS1 {	
		get{
			Vector3[,] positionCal = new Vector3[2, 12];
			for (int i = 0;i < 12;i++){
				positionCal[0, i] = new Vector3((-2.75f+0.5f*i), -4f, -0.08f);
				positionCal[1, i] = new Vector3((-2.75f+0.5f*i), -4.75f, -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3[,] CARD_POS2 {	
		get{
			Vector3[,] positionCal = new Vector3[2, 12];
			for (int i = 0;i < 12;i++){
				positionCal[0, i] = new Vector3(4f, (-2.75f+0.5f*i), -0.08f);
				positionCal[1, i] = new Vector3(4.75f, (-2.75f+0.5f*i), -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3[,] CARD_POS3 {	
		get{
			Vector3[,] positionCal = new Vector3[2, 12];
			for (int i = 0;i < 12;i++){
				positionCal[0, i] = new Vector3((2.75f-0.5f*i), 4f, -0.08f);
				positionCal[1, i] = new Vector3((2.75f-0.5f*i), 4.75f, -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3[,] CARD_POS4 {	
		get{
			Vector3[,] positionCal = new Vector3[2, 12];
			for (int i = 0;i < 12;i++){
				positionCal[0, i] = new Vector3(-4f, (2.75f-0.5f*i), -0.08f);
				positionCal[1, i] = new Vector3(-4.75f, (2.75f-0.5f*i), -0.08f);
			}
			return positionCal;
		}
	}

	public static Vector3 CARD_ROTATION_POSITIVE1 {get{return new Vector3(270, 180, 0);}}
	public static Vector3 CARD_ROTATION_NEGETIVE1 {get{return new Vector3(270, 0, 0);}}

	public static Vector3 CARD_ROTATION_POSITIVE2 {get{return new Vector3(0, 270, 270);}}
	public static Vector3 CARD_ROTATION_NEGETIVE2 {get{return new Vector3(0, 270, 90);}}

	public static Vector3 CARD_ROTATION_POSITIVE3 {get{return new Vector3(90, 0, 0);}}
	public static Vector3 CARD_ROTATION_NEGETIVE3 {get{return new Vector3(90, 180, 0);}}

	public static Vector3 CARD_ROTATION_POSITIVE4 {get{return new Vector3(0, 90, 90);}}
	public static Vector3 CARD_ROTATION_NEGETIVE4 {get{return new Vector3(0, 90, 270);}}

}
