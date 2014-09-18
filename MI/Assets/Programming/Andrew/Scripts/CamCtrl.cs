using UnityEngine;
using System.Collections;

public class CamCtrl : MonoBehaviour {

	public Transform Near;
	public Transform Far;
	
	public int side = 1;
	
	private GameObject Buffer;
	private Transform Vert;
	
	public float snapSpeed = 0.5f;
	public float snapDist = 0.1f;

	public float vertSpeed = 5;

	public float vertValue = 0;
	public float vertMax = 90;
	public float vertMin = -90;
	
	void Start(){
		Buffer = this.transform.parent.transform.parent.gameObject;
		Buffer.transform.parent = null;
		Vert = this.transform.parent;
		
		Near = GameObject.FindGameObjectWithTag("CamTarget_near").transform;
		Far = GameObject.FindGameObjectWithTag("CamTarget_far").transform;
		
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.E)){
			Flip();
		}
		if(Input.GetMouseButtonDown(0)){
			Screen.lockCursor = true;
		}
		
		Buffer.transform.rotation = Near.transform.rotation;
		float dist = (Buffer.transform.position-Far.position).magnitude;
		if(dist > snapDist){
			Buffer.transform.position = Vector3.Lerp(Buffer.transform.position,Far.position,Time.deltaTime*snapSpeed);
		}
		else{
			Buffer.transform.position = Far.position;
		}

		vertValue -= Input.GetAxis("Mouse Y")*vertSpeed;
		vertValue = Mathf.Clamp(vertValue,vertMin,vertMax);
		Vert.localEulerAngles = new Vector3(vertValue,0,0);
		
		
		
	}

	private void Flip(){
		Near.localPosition = Mirror(Near.localPosition,Vector3.left);
		Far.localPosition = Mirror(Far.localPosition,Vector3.left);
		side = side*(-1);
	}
	
	private Vector3 Mirror(Vector3 _A, Vector3 _B){
		return _A - (2 * _B * (Vector3.Dot(_A,_B)));
	}
}
