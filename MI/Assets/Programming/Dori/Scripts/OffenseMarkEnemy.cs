using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OffenseMarkEnemy : MonoBehaviour 
{
	public float markDuration;
	public float markCounter;
	public int currentAbilityLevel;
	public string marked;

	public bool isMarking;

	void Start()
	{
		currentAbilityLevel = 1;
	}
	
	void Update()
	{
		RaycastHit hitInfo;

		// Fire a raycast in front of the player and gather hit info
		if(Physics.Raycast(transform.position, Vector3.forward, out hitInfo, 5.0f)) {
			if(hitInfo.transform.tag == "Red") {
				// Assign the target to variable marked
				marked = hitInfo.transform.name;
			}
		}
		else if(!isMarking) {
			marked = null;
		}

		// Set duration of mark depending on ability level
		if(currentAbilityLevel == 1) {
			markDuration = 10.0f;
		}
		if(currentAbilityLevel == 2) {
			markDuration = 15.0f;
		}
		if(currentAbilityLevel == 3) {
			markDuration = 30.0f;
		}

		// Start Counter
		if(Input.GetKeyDown(KeyCode.Q) && marked != null) {
			isMarking = true;
		}

		if (isMarking) {
			markCounter += 1.0f * Time.deltaTime;

			Debug.Log("Sending " + marked + " to GUI");
			// GUI.SendMessage("MarkEnemy", marked);
		}

		if(markCounter >= markDuration) {
			markCounter = 0;
			isMarking = false;
			marked = null;
		}
	}
}