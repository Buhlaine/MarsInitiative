using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefenseAddShield : MonoBehaviour 
{
	public float shieldCounter;
	public float shieldDuration;
	public int currentAbilityLevel;
	
	public bool isShielded;
	
	private SphereCollider sphereCollider;
	private List<GameObject> BlueInRadius = new List<GameObject>();	
	// Second list to store shielded players
	private List<GameObject> BlueShielded = new List<GameObject>();

	void Start()
	{
		currentAbilityLevel = 1;

		sphereCollider = this.transform.GetComponent<SphereCollider> ();
	}

	void Update()
	{
		// Duration of shield is based on player level
		if (currentAbilityLevel == 1) {
			shieldDuration = 10.0f;
			sphereCollider.radius = 0.5f;
		}
		if (currentAbilityLevel == 2) {
			shieldDuration = 15.0f;
			sphereCollider.radius = 2.0f;
		}
		if (currentAbilityLevel == 3) {
			shieldDuration = 30.0f;
			sphereCollider.radius = 5.0f;
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			isShielded = true;
		}

		// Reset Counter
		if (shieldCounter >= shieldDuration) {
			shieldCounter = 0;
			isShielded = false;
		}
		
		// Start counter
		if (isShielded) {
			shieldCounter += 1 * Time.deltaTime;

			// Turn sphere colliders on (they are on layermask "Ignore Raycast")
			sphereCollider.enabled = true;
			gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			
			foreach (GameObject teammates in BlueInRadius) {
				// Store players within range into a second list 
				BlueShielded.Add (teammates);
				sphereCollider.enabled = true;
				teammates.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			}
		}

		// Reset and turn off sphere collider
		if (!isShielded) {
			sphereCollider.enabled = false;
			gameObject.layer = LayerMask.NameToLayer("Default");
			
			foreach (GameObject teammates in BlueShielded) {
				sphereCollider.enabled = false;
				teammates.gameObject.layer = LayerMask.NameToLayer("Default");
			}
			// Clear the second list for a new batch of players in range
			BlueShielded.Clear();
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		GameObject[] teammates = GameObject.FindGameObjectsWithTag ("Blue");
		// Checking for whether there are teammates within the set radius
		foreach (var str in teammates) {
			if (other.gameObject == str) { 
				BlueInRadius.Add(other.gameObject);
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		GameObject[] teammates = GameObject.FindGameObjectsWithTag ("Blue");
		// Delete the teammates who are not within the radius of the player
		foreach (var str in teammates) {
			if (other.gameObject == str) { 
				BlueInRadius.Remove(other.gameObject);
			}
		}
	}
}