using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {
	
	private int side = 1;
	
	public float lerpSpeed = 0.5f;
	
	private float camLerp = 0;
	
	private Vector3 ogAngleR;
	private Vector3 ogAngleL;
	private Vector3 farDistR;
	private Vector3 farDistL;
	private float vertAngle = 0;
	
	private Transform positionFar;
	private Transform positionNear;
	private Transform positionReference;
	
	
	void Start () {
		positionFar = GameObject.FindGameObjectWithTag("CamTarget_far").transform;
		positionNear = GameObject.FindGameObjectWithTag("CamTarget_near").transform;
		positionReference = GameObject.FindGameObjectWithTag("CamReference").transform;
		
		farDistR = SolveDist();
		SolveRotation();
		ogAngleR = positionNear.localEulerAngles;
		
		CamSwap();
		farDistR = SolveDist();
		SolveRotation();
		ogAngleL = positionNear.localEulerAngles;
		
		CamSwap();
		
	}
	
	
	void Update () {
		
		vertAngle += Input.GetAxis("Mouse Y");
		vertAngle = Mathf.Clamp(vertAngle,-90f,90f);
		
		Vector3 rv = positionNear.eulerAngles;
		positionNear.eulerAngles = new Vector3(SolveAngleSide().x + vertAngle,rv.y,rv.z);
		
		positionFar.localRotation = Quaternion.LookRotation(positionNear.localPosition-positionFar.localPosition);
		positionFar.localPosition = positionNear.localPosition+(positionNear.localRotation*SolveDistSide());
		
		
		this.transform.position = Vector3.Lerp(this.transform.position,
	    	Vector3.Lerp(positionFar.position,positionNear.position,camLerp),
	    	lerpSpeed);
		this.transform.rotation = positionFar.rotation;
		
		if(Input.GetKeyDown(KeyCode.E)){
			CamSwap();
			Screen.lockCursor = true;
		}
		
		
	}
	
	void OnDrawGizmos(){
		//Gizmos.DrawLine(this.transform.position,
	}
	
	//SENDMESSAGE RECEIVERS
	void CamSwap(){
		side = side*(-1);
		positionNear.localPosition = new Vector3(-positionNear.localPosition.x,positionNear.localPosition.y,positionNear.localPosition.z);
		positionFar.localPosition = new Vector3(-positionFar.localPosition.x,positionFar.localPosition.y,positionFar.localPosition.z);
	}
	
	void SolveRotation(){
		positionNear.localRotation = Quaternion.LookRotation(positionFar.position-positionNear.position);
	}
	Vector3 SolveDist(){
		return positionNear.localPosition-positionFar.localPosition;
	}
	
	Vector3 SolveDistSide(){
		if (side > 0){
			return farDistR;
		}
		else{
			return farDistL;
		}
	}
	
	Vector3 SolveAngleSide(){
		if (side > 0){
			return ogAngleR;
		}
		else{
			return ogAngleL;
		}
	}
	
	
}
