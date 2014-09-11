using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SupportHealthRegen : MonoBehaviour 
{
	public float regenAmount;
	public float regenCounter;
	public float regenAmountUpgrade;
	public float regenDurationUpgrade = 10.0f;
	public float regenRadiusUpgrade = 0.5f;
	public float regenDuration;
	public int currentAbilityLevel;
	
	public bool isRegen;
	public bool hasChanged;

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
		// Initiate counter and health regeneration upon hitting 'E' while regenFlag or selfHeal equals true
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
			
			player.SendMessage("HealthRegen", currentAbilityLevel);
			
			foreach (GameObject teammates in BlueInRadius) {
				teammates.SendMessage ("HealthRegen", currentAbilityLevel);
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
			Debug.Log ("Checking Stats...");
			CheckStats();
		}
	}

	void RecieveRegenAmount(int _amount)
	{
		regenAmount = _amount;
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