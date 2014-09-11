using UnityEngine;
using System.Collections;

public class bombscript : MonoBehaviour {
	public GameObject bomb;
	GameObject timer;

	// Use this for initialization
	void Start () {

		timer = GameObject.Instantiate (bomb) as GameObject;
		timer.SendMessage ("setParent", this.gameObject as GameObject);
		//timer.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+10.0f,this.transform.position.z);
	
	}
	
	// Update is called once per frame
	void Update () {

		timer.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+5.0f,this.transform.position.z);
	
	}
}
