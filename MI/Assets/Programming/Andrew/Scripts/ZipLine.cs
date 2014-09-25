﻿using UnityEngine;
using System.Collections;

public class ZipLine : MonoBehaviour {

	public GameObject target;
	
	public float travelTime = 1f;
	
	void Start () {
		this.renderer.enabled = false;
	}
	
	void Update () {
		
	}
	
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine(this.transform.position,target.transform.position);
	}
	
	void OnTriggerStay(Collider collider){
		if (Input.GetKeyDown(KeyCode.R)){
			Debug.Log("R");
			if(collider.gameObject.tag == "Player"){
				Debug.Log("Plyaer");
				//collider.gameObject.SendMessage("lockMovement",collider.gameObject);
				StartCoroutine(Zip(collider.gameObject));
			}
		}
	}
	
	IEnumerator Zip(GameObject _player){
	 	float progress = 0;
		while(progress < travelTime){
			_player.transform.position = Vector3.Lerp(this.transform.position,target.transform.position,progress/travelTime);
			progress += Time.deltaTime;
			Debug.Log(progress.ToString());
			yield return null;
		}
	}
	
}
