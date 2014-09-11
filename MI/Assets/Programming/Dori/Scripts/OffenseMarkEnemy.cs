using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OffenseMarkEnemy : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float markDuration = 0.0f;
	public float markCounter;
	public string marked;
	public float markDurationUpgrade = 10.0f;

	public bool isMarking;
	public bool hasChanged;

	void Start()
	{
		currentAbilityLevel = 1;

		CheckStats ();
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
			markDuration += markDurationUpgrade;
		}
		if(currentAbilityLevel == 2) {
			markDuration += markDurationUpgrade;
		}
		if(currentAbilityLevel == 3) {
			markDuration += markDurationUpgrade;
		}

		// Start Counter
		if(Input.GetKeyDown(KeyCode.Q) && marked != null) {
			isMarking = true;
		}

		if (isMarking) {
			markCounter += 1.0f * Time.deltaTime;

			Debug.Log("Sending " + marked + " to GUI");
			// Send message to GUI with the marked enemies information (transform.name) so that the enemy can be followed on the minimap ... don't know if will work
			// GUI.SendMessage("MarkEnemy", marked);
		}

		if(markCounter >= markDuration) {
			markCounter = 0;
			isMarking = false;
			marked = null;
		}
	}

	void CheckStats()
	{
		if (currentAbilityLevel == 1) {
			markDuration = markDurationUpgrade;
		}
		if (currentAbilityLevel == 2) {
			markDuration += markDurationUpgrade;
		}
		if (currentAbilityLevel == 3) {
			markDuration += markDurationUpgrade;
		}
		
		hasChanged = false;
	}
	
	void Changed()
	{
		hasChanged = true;
		
		if (hasChanged) {
			Debug.Log ("Checking Stats...");
			CheckStats();
		}
	}
}