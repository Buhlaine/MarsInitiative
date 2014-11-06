using UnityEngine;
using System.Collections;

public class dotDmgCtrl : MonoBehaviour {

	private float creationTime;

	void OnTriggerExit(Collider other)
	{

		other.gameObject.SendMessage ("endDotDamage", SendMessageOptions.DontRequireReceiver);
	}

	void OnTriggerEnter(Collider other)
	{
		creationTime = Time.time;
		other.gameObject.SendMessageUpwards ("ApplyDotDamage",creationTime, SendMessageOptions.DontRequireReceiver);
	}
}
