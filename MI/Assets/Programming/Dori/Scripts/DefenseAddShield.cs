using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefenseAddShield : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float shieldCounter;
	public float shieldDuration = 0.0f;
	public float shieldRadius;
	public float shieldUpgradeAmount = 10.0f;
	public float shieldUpgradeRadius = 1.0f;
	
	public bool isShielded;
	public bool hasChanged;

	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> BlueInRadius = new List<GameObject>();	
	// Second list to store shielded players
	private List<GameObject> BlueShielded = new List<GameObject>();

	void Start()
	{
		currentAbilityLevel = 1;

		sphereCollider = this.transform.GetComponent<SphereCollider> ();
		string parent = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (parent).GetComponent<Player>();

		player.SendMessage ("AbilityOne", this.gameObject.name);

		CheckStats ();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Q) && !isShielded) {
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
			gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			
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
			gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
			
			foreach (GameObject teammates in BlueShielded) {
				sphereCollider.enabled = false;
				teammates.gameObject.layer = LayerMask.NameToLayer("Default");
			}
			// Clear the second list for a new batch of players in range
			BlueShielded.Clear();
		}
	}

	void CheckStats()
	{
		if (currentAbilityLevel == 1) {
			shieldDuration = shieldUpgradeAmount;
			sphereCollider.radius = shieldUpgradeRadius;
			shieldRadius = sphereCollider.radius;
		}
		if (currentAbilityLevel == 2) {
			shieldDuration += shieldUpgradeAmount;
			sphereCollider.radius += shieldUpgradeRadius;
			shieldRadius = sphereCollider.radius;
		}
		if (currentAbilityLevel == 3) {
			shieldDuration += shieldUpgradeAmount;
			sphereCollider.radius += shieldUpgradeRadius;
			shieldRadius = sphereCollider.radius;
		}

		hasChanged = false;
	}

	void Changed()
	{
		hasChanged = true;
		
		if (hasChanged) {
			CheckStats();
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		GameObject[] teammates = GameObject.FindGameObjectsWithTag ("Teammate");
		// Checking for whether there are teammates within the set radius
		foreach (var str in teammates) {
			if (other.gameObject == str) { 
				BlueInRadius.Add(other.gameObject);
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		GameObject[] teammates = GameObject.FindGameObjectsWithTag ("Teammate");
		// Delete the teammates who are not within the radius of the player
		foreach (var str in teammates) {
			if (other.gameObject == str) { 
				BlueInRadius.Remove(other.gameObject);
			}
		}
	}
}