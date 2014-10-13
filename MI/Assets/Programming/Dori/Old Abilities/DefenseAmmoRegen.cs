using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefenseAmmoRegen : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float regenCounter;
	public float cooldownCounter;
	public float cooldownPeriod;
	public float[] regenAmount;
	public float[] regenDuration;
	public float[] regenRadius;
//	public float regenAmountUpgrade = 0.6f;
//	public float regenDurationUpgrade = 5.0f;
//	public float regenRadiusUpgrade = 0.5f;
	
	public bool isRegen;
//	public bool hasChanged;
	public bool startCooldown;
	
	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> BlueInRadius = new List<GameObject>();	

	void Start()
	{
		currentAbilityLevel = 0;
		cooldownPeriod = 18.0f;

		sphereCollider = transform.GetComponent<SphereCollider> ();
		string parent = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (parent).GetComponent<Player>();

		player.SendMessage ("AbilityTwo", this.gameObject.name);

//		CheckStats ();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && !isRegen && !startCooldown) {
			isRegen = true;
			sphereCollider.radius = regenRadius[currentAbilityLevel];
		}
		
		// Reset counter
		if (regenCounter >= regenDuration[currentAbilityLevel]) {
			startCooldown = true;
			isRegen = false;
			regenCounter = 0;
		}

		if(startCooldown) {
			cooldownCounter += 1.0f * Time.deltaTime;
		}
		
		if (cooldownCounter >= cooldownPeriod) {
			startCooldown = false;
			cooldownCounter = 0;
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

//	void CheckStats()
//	{
//		if (currentAbilityLevel == 1) {
//			regenDuration = regenDurationUpgrade;
//			sphereCollider.radius = regenRadiusUpgrade;
//		}
//		if (currentAbilityLevel == 2) {
//			regenDuration += regenDurationUpgrade;
//			sphereCollider.radius += regenRadiusUpgrade;
//		}
//		if (currentAbilityLevel == 3) {
//			regenDuration = regenDuration + regenDurationUpgrade;
//			sphereCollider.radius += regenRadiusUpgrade;
//		}
//
//		hasChanged = false;
//	}

	void Changed()
	{
//		hasChanged = true;
		currentAbilityLevel += 1;

//		if (hasChanged) {
//			CheckStats();
//		}
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