using UnityEngine;
using System.Collections;

public class bombscript : MonoBehaviour {
	public GameObject bomb;
	public GameObject box;
	GameObject timer;
	Vector3 TimerScreenLoc;
	Vector3 ScreenLoc;

	// Use this for initialization
	void Start () {
		ScreenLoc=Camera.main.ScreenToWorldPoint(new Vector3(0f,0f));
		timer = GameObject.Instantiate (bomb) as GameObject;
		timer.SendMessage ("setParent", this.gameObject as GameObject);
		//timer.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+10.0f,this.transform.position.z);
	
	}
	
	// Update is called once per frame
	void Update () {
		TimerScreenLoc = Camera.main.WorldToScreenPoint(new Vector3(box.transform.position.x, box.transform.position.y));
		Debug.Log ("x"+TimerScreenLoc.x);
		Debug.Log ("y"+TimerScreenLoc.y);

		timer.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+5.0f,this.transform.position.z);
	}

	void OnDrawGizmosSelected() {
		Vector3 p = camera.ScreenToWorldPoint(new Vector3(100, 100, camera.nearClipPlane));
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(p, 10.0F);
	}



}
