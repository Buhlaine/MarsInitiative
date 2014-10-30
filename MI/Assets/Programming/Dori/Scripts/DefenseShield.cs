using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* This ability should block all projectiles for the player and teammates within the set radius. 
 * Sets the player and teammate layer to "Ignore Raycast"
 * Ability Level 1 = Duration(3) Radius(4)
 * Ability Level 2 = Duration(3) Radius(8)
 * Ability Level 3 = Duration(4) Radius(8)
 * Cooldown Period = 25 seconds at all three levels
 * 
 * This ability should stay active on all teammates that are within the player's radius when the player
 * activates the ability (i.e if they leave the radius after the shield has been activated - they keep 
 * the shield even if they leave the player's radius. This is in contrast to Restock, where teammates
 * must stay within the player's radius to keep getting restocked.
 * 
 * This is done by storing teammates within the player's radius inside one list, and teammates whose shields
 * are active in another list.
*/

public class DefenseShield : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float shieldCounter;
	public float cooldownCounter;
	public float cooldownPeriod;
	public float[] shieldDuration;
	public float[] shieldRadius;

	public bool isShielded;
	public bool startCooldown;

//	private Player player;
	private SphereCollider sphereCollider;

	// Create two lists to store teammates in player's radius, and teammates with active shields
	private List<GameObject> BlueInRadius = new List<GameObject>();	
	private List<GameObject> BlueShielded = new List<GameObject>();

	void Start()
	{
		// Create references and set values
		currentAbilityLevel = 0;
		cooldownPeriod = 24.0f;

		sphereCollider = this.transform.GetComponent<SphereCollider> ();
//		string parent = this.gameObject.transform.parent.gameObject.name;
//		player = GameObject.Find (parent).GetComponent<Player>();
	}

	void Update()
	{
		// Set the radius of the sphere collider to the pre-determined amount
		sphereCollider.radius = shieldRadius [currentAbilityLevel];

		if (Input.GetKeyDown (KeyCode.Q) && !isShielded && !startCooldown) {
			isShielded = true;
		}

		// Reset Counter and start cool down
		if (shieldCounter >= shieldDuration[currentAbilityLevel]) {
			startCooldown = true;
			isShielded = false;
			shieldCounter = 0;
		}

		if(startCooldown) {
			cooldownCounter += 1.0f * Time.deltaTime;
		}

		if (cooldownCounter >= cooldownPeriod) {
			startCooldown = false;
			cooldownCounter = 0;
		}
		
		// Start counter
		if (isShielded) {
			shieldCounter += 1.0f * Time.deltaTime;

			// Turn sphere colliders on (they are on layermask "Ignore Raycast")
			sphereCollider.enabled = true;
			gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			
			foreach (GameObject teammates in BlueInRadius) {
				// Store players within range into a second list 
				BlueShielded.Add (teammates);
			}

			foreach (GameObject teammates in BlueShielded) {
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

	void Changed()
	{
		currentAbilityLevel += 1;
	}
	
	void OnTriggerEnter(Collider other) 
	{
		GameObject[] teammates = GameObject.FindGameObjectsWithTag ("Teammate");
		// Checking for whether there are teammates within the set radius
		foreach (var teammate in teammates) {
			if(other.tag == "Teammate") {
				BlueInRadius.Add(other.gameObject);
				Debug.Log ("Adding: " + teammate.gameObject.name + " | " + other.gameObject.name);
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		// Delete the teammates who are not within the radius of the player
		foreach (var str in BlueInRadius) {
			if(other.gameObject == str) {
				BlueInRadius.Remove(other.gameObject);
			}
		}
	}
}