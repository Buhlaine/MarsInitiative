using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SupportHealthRegen : MonoBehaviour 
{
	public float regenAmount;
	public float regenCounter;
	public float regenDurationUpgrade = 10.0f;
	public float regenRadiusUpgrade = 0.5f;
	public float regenDuration;
	public int currentAbilityLevel;
	
	public bool isRegen;

	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> BlueInRadius = new List<GameObject>();	

	void Start()
	{
		sphereCollider = this.gameObject.transform.GetComponent<SphereCollider> ();
		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();
	
		currentAbilityLevel = 1;
	}

	void Update()
	{
		// Sphere Collider radius and regenAmount changes depending on player level
		if (currentAbilityLevel == 1) {
			regenDuration += regenDurationUpgrade;
			sphereCollider.radius += regenRadiusUpgrade;
			regenAmount = player.GetComponent<Player>().maxHealth * 0.06f;
		}
		if (currentAbilityLevel == 2) {
			regenDuration += regenDurationUpgrade;
			sphereCollider.radius += regenRadiusUpgrade;
			regenAmount = player.GetComponent<Player>().maxHealth * 0.12f;
		}
		if (currentAbilityLevel == 3) {
			regenDuration += regenDurationUpgrade;
			sphereCollider.radius += regenRadiusUpgrade;
			regenAmount = player.GetComponent<Player>().maxHealth * 0.24f;
		}

		// Initiate counter and health regeneration upon hitting 'E' while regenFlag or selfHeal equals true
		if (Input.GetKeyDown(KeyCode.E)) {
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
			
			player.SendMessage("HealthRegen", regenAmount);
			
			foreach (GameObject teammates in BlueInRadius) {
				teammates.SendMessage ("HealthRegen", regenAmount);
			}
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