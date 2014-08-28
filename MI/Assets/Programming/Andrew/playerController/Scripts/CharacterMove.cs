using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
	
	public float runForce = 50f;
	public float strafeForce = 30f;
	public float turnSpeed = 5f;
	public float jumpForce = 10;
	
	private bool canJump = true;
	
	void Start () {
		
	}
	
	void Update () {
		this.transform.RotateAround(this.transform.position,Vector3.up,Input.GetAxis("Mouse X")*turnSpeed);
		
		this.rigidbody.AddForce(this.transform.forward*Input.GetAxis("Vertical")*runForce);
		this.rigidbody.AddForce(this.transform.right*Input.GetAxis("Horizontal")*strafeForce);
		
		if (Input.GetKeyDown(KeyCode.Space) && canJump){
			Vector3 v = this.rigidbody.velocity;
			this.rigidbody.velocity = new Vector3(v.x,jumpForce,v.y);
			canJump = false;
		}
		
	}
	
	void JumpLand(){
		canJump = true;
	}
}
