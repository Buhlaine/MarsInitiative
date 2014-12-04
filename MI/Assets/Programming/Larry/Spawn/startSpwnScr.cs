using UnityEngine;
using System.Collections;

public class startSpwnScr : MonoBehaviour {

	void OnTriggerEnter(Collider colInfo)
	{
		if(colInfo.tag == "Player" || colInfo.tag == "Red" || colInfo.tag == "Blue")
		{
			Debug.LogWarning ("bleh");
			this.gameObject.SetActive (false);
		}
	}
}
