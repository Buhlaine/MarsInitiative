using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {
	
	private GameObject parent;
	
	// Use this for initialization
	void Start () {
		parent = this.transform.parent.gameObject;
	}
	
	void OnTriggerEnter(Collider collider){
		parent.SendMessage("JumpLand");
	}
	
}
