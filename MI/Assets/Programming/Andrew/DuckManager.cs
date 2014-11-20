using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DuckManager : MonoBehaviour {
	
	private int duckState = 0;
	
	
	
	private float seconds = 0;
	
	bool thing = false;
	
	private List<Vector3> spawns;
	
	void Start(){
		seconds = Random.Range(60f,120f);
		this.renderer.enabled = false;
		foreach(Transform i in GameObject.Find("DUCK_SPANWNS").GetComponentsInChildren<Transform>()){
			spawns.Add(i.gameObject.transform.position);
			i.gameObject.renderer.enabled = false;
		}
		
	}
	
	void Update(){
		
		switch(duckState){
		
		//0 = notspawned
		case 0:
			seconds -= Time.deltaTime;
			if(seconds <= 0){
				this.renderer.enabled = true;
				duckState++;
				this.transform.position = spawns[(int)(Random.value%spawns.Count)];
				this.transform.localRotation = Quaternion.Euler(0,Random.value%360,0);
			}
			break;
			
		//1 = firstalive
		case 1:
			
			break;
			
		//2 = firstdead
		case 2:
			//condition for respawn
			if(thing){
				this.renderer.enabled = true;
				duckState++;
				this.transform.position = spawns[(int)(Random.value%spawns.Count)];
				this.transform.localRotation = Quaternion.Euler(0,Random.value%360,0);
			}
			break;
		
		//3 = respawned
		case 3:
			
			break;
		}
		
		
	}
	
	void ApplyDamage(float lol){
		if(duckState == 1 || duckState == 3){
			this.renderer.enabled = false;
			duckState++;
		}
	}
		
	
}
