using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefenseAmmoRegen : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float regenCounter;
	public float regenAmount = 0.0f;
	public float regenDuration = 0.0f;
	public float regenAmountUpgrade = 0.6f;
	public float regenDurationUpgrade = 5.0f;
	public float regenRadiusUpgrade = 0.5f;
	
	public bool isRegen;
	public bool hasChanged;
	
	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> BlueInRadius = new List<GameObject>();	

	void Start()
	{
		currentAbilityLevel = 1;

		sphereCollider = transform.GetComponent<SphereCollider> ();
		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();

		CheckStats ();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && !isRegen) {
			isRegen = true;
		}
		
		// Reset counter
		if (regenCounter >= regenDuration) {
			regenCounter = 0;
			isRegen = false;
		}
		
		// Start counter
		if (isRegen) {
			regenCounter += 1 * Time.deltaTime;

			player.SendMessage("AmmoRegen", currentAbilityLevel);
			
			foreach (GameObject teammates in BlueInRadius) {
				teammates.SendMessage ("AmmoRegen", currentAbilityLevel);
			}
		}
	}

	void CheckStats()
	{
		if (currentAbilityLevel == 1) {
			regenDuration = regenDurationUpgrade;
			sphereCollider.radius = regenRadiusUpgrade;
		}
		if (currentAbilityLevel == 2) {
			regenDuration += regenDurationUpgrade;
			sphereCollider.radius += regenRadiusUpgrade;
		}
		if (currentAbilityLevel == 3) {
			regenDuration = regenDuration + regenDurationUpgrade;
			sphereCollider.radius += regenRadiusUpgrade;
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