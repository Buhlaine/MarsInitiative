using UnityEngine;
using System.Collections;

public class bombtimer : MonoBehaviour {
	GameObject followPos;
	float angle;
	public float damping;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.SetFloat ("_Cutoff", Mathf.InverseLerp (0, Screen.width, Time.time*damping));
		//Debug.Log (Input.mousePosition.x);
		//this.transform.position = followPos.transform.position + new Vector3(0, 3, 0);
		//angle = Vector3.Angle (Camera.main.transform.forward, this.transform.forward);
		//this.transform.forward = Quaternion.AngleAxis (angle, Vector3.up) * this.transform.forward;
		//Debug.Log ("dd"+ transform.InverseTransformPoint(Camera.main.transform.position));
		//Debug.Log ("ddt"+ Camera.main.transform.position);
		//this.transform.LookAt(transform.InverseTransformDirection(Camera.main.transform.position));
		//transform.InverseTransformDirection(this.transform.forward);
		//this.transform.LookAt(Camera.main.transform.forward, Vector3.down);
		//Vector3 screenloc = Camera.main.WorldToScreenPoint (this.transform.position);
		this.transform.rotation= Quaternion.LookRotation (this.transform.position - Camera.main.transform.position);
		//Vector3 screenloc = Camera.main.WorldToScreenPoint (this.transform.position);
		//this.transform.position = new Vector3 (Mathf.Clamp (screenloc.x, 0, Screen.width), Mathf.Clamp (screenloc.y, 0, Screen.height), this.transform.position.z);
		//Debug.Log (this.transform.forward);
	}

	void setParent(GameObject parent)
	{
//		angle = Vector3.Angle (Camera.main.transform.forward, this.transform.forward);
//		Debug.Log (this.transform.forward);
//		this.transform.forward = (Quaternion.AngleAxis (angle, Vector3.up) * this.transform.forward);
//		Debug.Log (this.transform.forward);
//		followPos = parent;
	}
}
