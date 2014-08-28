using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefenseAmmoRegen : MonoBehaviour 
{
	public float regenCounter;
	public float regenAmount;
	public int regenDuration;
	public int currentAbilityLevel;

	public bool isRegen;

	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> BlueInRadius = new List<GameObject>();	

	void Start()
	{
		currentAbilityLevel = 1;

		sphereCollider = transform.GetComponent<SphereCollider> ();
		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();
	}

	void Update()
	{
		if (currentAbilityLevel == 1) {
			regenDuration = 10;
			regenAmount = player.GetComponent<Player>().maxAmmo * 0.06f;
			sphereCollider.radius = 0.5f;
		}
		if (currentAbilityLevel == 2) {
			regenDuration = 15;
			regenAmount = player.GetComponent<Player>().maxAmmo * 0.12f;
			sphereCollider.radius = 2.0f;
		}
		if (currentAbilityLevel == 3) {
			regenDuration = 30;
			regenAmount = player.GetComponent<Player>().maxAmmo * 0.24f;
			sphereCollider.radius = 5.0f;
		}

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
			
			player.SendMessage("AmmoRegen", regenAmount);
			
			foreach (GameObject teammates in BlueInRadius) {
				teammates.SendMessage ("AmmoRegen", regenAmount);
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