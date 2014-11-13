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
		renderer.material.SetFloat ("_Cutoff", Mathf.InverseLerp (Screen.width, 0, Time.time*damping));

		this.transform.rotation= Quaternion.LookRotation (this.transform.position - Camera.main.transform.position);

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
