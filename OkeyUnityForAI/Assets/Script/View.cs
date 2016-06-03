using UnityEngine;
using System.Collections;

public class View : MonoBehaviour {

	public static void ShowView(int id, Card[,] hand){
		string tag = "P"+(id+1).ToString();
		GameObject[] removeObjs = GameObject.FindGameObjectsWithTag(tag);
		for(int i=0; i<removeObjs.Length; i++){
			Destroy(removeObjs[i]);
		}

		for(int i=0;i<2;i++){
			for(int j=0;j<12;j++){
				if (hand[i,j] != null){
					Vector3 location;
					Quaternion rotation;
					switch(id){
					case 0:
						location = HAND.CARD_POS1[i,j];
						rotation = HAND.CARD_ROTATION_POSITIVE1;
						break;
					case 1:
						location = HAND.CARD_POS2[i,j];
						rotation = HAND.CARD_ROTATION_POSITIVE2;
						break;
					case 2:
						location = HAND.CARD_POS3[i,j];
						rotation = HAND.CARD_ROTATION_POSITIVE3;
						break;
					case 3:
						location = HAND.CARD_POS4[i,j];
						rotation = HAND.CARD_ROTATION_POSITIVE4;
						break;
					default:
						location = HAND.CARD_POS1[i,j];
						rotation = HAND.CARD_ROTATION_POSITIVE1;
						break;
					}

					string objname = "C"+hand[i,j].number;
					GameObject newObj = Instantiate(Resources.Load(objname), location, rotation) as GameObject;
					newObj.tag = tag;

					Renderer newRenderer = newObj.GetComponent<Renderer>();
					newRenderer.materials[1].SetColor("_Color", hand[i,j].color);
				}
			}
		}
	}

	public static void ShowDiscard(int id){
		string tag = "D"+(id+1).ToString();
		GameObject[] removeObjs = GameObject.FindGameObjectsWithTag(tag);
		for(int i=0; i<removeObjs.Length; i++){
			Destroy(removeObjs[i]);
		}

		for(int i=0; i<Discard.discard[id].Count; i++){
			Vector3 location;
			Quaternion rotation;

			switch(id){
			case 0:
				location = DECK.CARD_POS_1[i];
				rotation = DECK.CARD_ROTATION_POSITIVE1;
				break;
			case 1:
				location = DECK.CARD_POS_2[i];
				rotation = DECK.CARD_ROTATION_POSITIVE2;
				break;
			case 2:
				location = DECK.CARD_POS_3[i];
				rotation = DECK.CARD_ROTATION_POSITIVE3;
				break;
			case 3:
				location = DECK.CARD_POS_4[i];
				rotation = DECK.CARD_ROTATION_POSITIVE4;
				break;
			default:
				location = DECK.CARD_POS_1[i];
				rotation = DECK.CARD_ROTATION_POSITIVE1;
				break;
			}

			string objname = "C"+Discard.discard[id][i].card.number;
			GameObject newObj = Instantiate(Resources.Load(objname), location, rotation) as GameObject;
			newObj.tag = tag;
			newObj.transform.localScale = DECK.CARD_SIZE;

			Renderer newRenderer = newObj.GetComponent<Renderer>();
			newRenderer.materials[1].SetColor("_Color", Discard.discard[id][i].card.color);
		}
	}

}
