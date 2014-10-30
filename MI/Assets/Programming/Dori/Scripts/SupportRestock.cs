﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* This ability restocks both health and ammo. 
 * Ability Level 1 = Duration(4) Radius(Self)
 * Ability Level 2 = Duration(4) Radius(8)
 * Ability Level 3 = Duration(5) Radius(8)
 * Cooldown = 15 seconds at all three levels
 * Regen amount per second = 10
 * 
 * This script initially only heals the player, but at higher levels - will store other 
 * teammates into a list and apply the same restock perks to those stored in the list (BlueInRadius)
*/

public class SupportRestock : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float regenCounter;
	public float cooldownCounter;
	public float cooldownPeriod;
	public float[] regenAmount;
	public float[] regenDuration;
	public float[] regenRadius;
	
	public bool isRegen;
	public bool startCooldown;
	
	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> BlueInRadius = new List<GameObject>();	
	
	void Start()
	{
		// Create references and set values
		sphereCollider = this.gameObject.transform.GetComponent<SphereCollider> ();
		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();
		
		currentAbilityLevel = 0;
		cooldownPeriod = 15.0f;
	}
	
	void Update()
	{
		// Set the radius of the sphere collider to the pre-determined amount
		sphereCollider.radius = regenRadius [currentAbilityLevel];

		// Initiate counter and health/ammo regeneration
		if (Input.GetKeyDown(KeyCode.E) && !isRegen && !startCooldown) {
			isRegen = true;
		}

		// Reset Counter and start cool down
		if (regenCounter >= regenDuration[currentAbilityLevel]) {
			startCooldown = true;
			isRegen = false;
			regenCounter = 0;
		}

		// Start the cooldown timer once startCooldown is set to true
		if(startCooldown) {
			cooldownCounter += 1.0f * Time.deltaTime;
		}

		// Once the cooldownCounter is greater than or equal to the cooldownPeriod 
		// then reset the cooldownCounter and set startCooldown to false
		if (cooldownCounter >= cooldownPeriod) {
			startCooldown = false;
			cooldownCounter = 0;
		}
		
		// Reset restock counter
		if (regenCounter >= regenDuration[currentAbilityLevel]) {
			regenCounter = 0;
			isRegen = false;
			startCooldown = true;
		}
		
		// Start restock counter
		if (isRegen) {
			regenCounter += 1 * Time.deltaTime;

			// Send a message to the player to restock
			player.SendMessage("Restock", regenAmount[currentAbilityLevel]);

			// Send message to restock to each of the teammates stored in the list BlueInRadius
			foreach (var teammates in BlueInRadius) {
				teammates.SendMessage ("Restock", regenAmount[currentAbilityLevel]);
				Debug.Log("Restocking: " + teammates);
			}
		}
	}

	// If called by the player because of a player level up, then this ability has been chosen for a level up
	void Changed()
	{
		currentAbilityLevel += 1;
	}
	
	void OnTriggerEnter(Collider other) 
	{
		// Finding GameObjects with a tag that matches "Teammate"
		GameObject[] teammates = GameObject.FindGameObjectsWithTag ("Teammate");

		// Checking for whether there are teammates within the set radius
		foreach (var teammate in teammates) {
			if (other.tag == "Teammate") { 
				BlueInRadius.Add(other.gameObject);
				Debug.Log ("Adding: " + teammate.gameObject + " Count: " + BlueInRadius.Count);
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		// Delete the teammates who are not within the radius of the player
		if (other.tag == "Teammate") { 
			Debug.Log ("Removing: " + other.gameObject);
			BlueInRadius.Remove(other.gameObject);
		}
	}
}