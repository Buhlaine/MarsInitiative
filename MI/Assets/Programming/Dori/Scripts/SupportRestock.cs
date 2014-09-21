using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		sphereCollider = this.gameObject.transform.GetComponent<SphereCollider> ();
		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();
		
		currentAbilityLevel = 0;
		cooldownPeriod = 15.0f;
		
		player.SendMessage ("AbilityOne", this.gameObject.name);
	}
	
	void Update()
	{
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
		
		if(startCooldown) {
			cooldownCounter += 1.0f * Time.deltaTime;
		}
		
		if (cooldownCounter >= cooldownPeriod) {
			startCooldown = false;
			cooldownCounter = 0;
		}
		
		// Reset counter
		if (regenCounter >= regenDuration[currentAbilityLevel]) {
			regenCounter = 0;
			isRegen = false;
			startCooldown = true;
		}
		
		// Start counter
		if (isRegen) {
			regenCounter += 1 * Time.deltaTime;
			
			player.SendMessage("Restock", regenAmount[currentAbilityLevel]);
			
			foreach (GameObject teammates in BlueInRadius) {
				teammates.SendMessage ("Restock", regenAmount[currentAbilityLevel]);
			}
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