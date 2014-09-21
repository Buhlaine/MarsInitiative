using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefenseAddShield : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float shieldCounter;
	public float cooldownCounter;
	public float cooldownPeriod;
	public float[] shieldDuration;
	public float[] shieldRadius;

	public bool isShielded;
	public bool startCooldown;

	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> BlueInRadius = new List<GameObject>();	
	// Second list to store shielded players
	private List<GameObject> BlueShielded = new List<GameObject>();

	void Start()
	{
		currentAbilityLevel = 0;
		cooldownPeriod = 20.0f;

		sphereCollider = this.transform.GetComponent<SphereCollider> ();
		string parent = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (parent).GetComponent<Player>();

		player.SendMessage ("AbilityOne", this.gameObject.name);
	}

	void Update()
	{
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
		foreach (var str in teammates) {
			if(other.gameObject == str) {
				BlueInRadius.Add(other.gameObject);
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		GameObject[] teammates = GameObject.FindGameObjectsWithTag ("Teammate");
		// Delete the teammates who are not within the radius of the player
		foreach (var str in teammates) {
			if(other.gameObject == str) {
				BlueInRadius.Remove(other.gameObject);
			}
		}
	}
}