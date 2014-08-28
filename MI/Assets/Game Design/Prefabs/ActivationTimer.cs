using UnityEngine;
using System.Collections;

public class ActivationTimer : MonoBehaviour {

	public float seconds;
	
	public GameObject item;
	
	void Update(){
		seconds -= Time.deltaTime;
		if((seconds <= 0) && (item != null)){
			item.SetActive(true);
			Destroy(this);
		}
	}

}
