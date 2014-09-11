using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SupportSpeedBoost : MonoBehaviour 
{
	public float boostCounter;
	public float boostDuration;
	public float boostDurationUpgrade = 10.0f;
	public float boostRadiusUpgrade = 0.5f;
	public int currentAbilityLevel;

	public bool isBoosted;
	public bool hasChanged;

	public Player player;
	public SphereCollider sphereCollider;
	private List<GameObject> BlueInRadius = new List<GameObject>();
	// Store boosted players into a second list, so that they can keep the boost outside of the player's range
	private List<GameObject> BlueBoosted = new List<GameObject>();

	void Start()
	{
		currentAbilityLevel = 1;
		boostCounter = 0;
		sphereCollider.enabled = true;

		sphereCollider = this.gameObject.transform.GetComponent<SphereCollider> ();
		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Q) && !isBoosted) {
			isBoosted = true;
		}

		// Reset counter
		if (boostCounter >= boostDuration) {
			boostCounter = 0;
			isBoosted = false;
		}

		// Start counter
		if (isBoosted) {
			boostCounter += 1 * Time.deltaTime;

			player.SendMessage("ApplySpeedBoost", currentAbilityLevel);

			foreach (GameObject teammates in BlueInRadius) {
				// Store players within range into a second list 
				BlueBoosted.Add (teammates);
				teammates.SendMessage ("ApplySpeedBoost", currentAbilityLevel);
			}
		}
		// Reset speed to default speed
		if (!isBoosted) {
			player.SendMessage ("DefaultSpeed");

			foreach (GameObject teammates in BlueBoosted) {
				teammates.SendMessage ("DefaultSpeed");
			}
			// Clear the second list for a new batch of players in range
			BlueBoosted.Clear();
		}
	}

	void CheckStats()
	{
		if (currentAbilityLevel == 1) {
			boostDuration = boostDurationUpgrade;
			sphereCollider.radius = boostRadiusUpgrade;
		}
		if (currentAbilityLevel == 2) {
			boostDuration += boostDurationUpgrade;
			sphereCollider.radius += boostRadiusUpgrade;
		}
		if (currentAbilityLevel == 3) {
			boostDuration += boostDurationUpgrade;
			sphereCollider.radius += boostRadiusUpgrade;
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

	void OnTriggerEnter(Collider other) 
	{
		// Adding players within the range to a list 
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
		// Delete the teammates who are not within range
		GameObject[] teammates = GameObject.FindGameObjectsWithTag ("Blue");
		// Checking for whether there are teammates within the set radius
		foreach (var str in teammates) {
			if (other.gameObject == str) { 
				BlueInRadius.Remove(other.gameObject);
			}
		}
	}
}