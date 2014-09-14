using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OffenseCamo : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float decreasedSpeed;
	public float camoCounter;
	public float camoDuration = 0.0f;
	public float camoDurationUpdrade = 10.0f;
	public float camoRadiusUpgrade = 1.0f;
	
	public bool isCamo;
	public bool hasChanged;

	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> BlueInRadius = new List<GameObject>();
	// Store boosted players into a second list, so that they can keep the camo outside of the player's range
	private List<GameObject> BlueCamo = new List<GameObject>();

	void Start()
	{
		currentAbilityLevel = 1;

		sphereCollider = this.gameObject.transform.GetComponent<SphereCollider> ();
		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();

		float sixpercent = player.defaultSpeed * 0.06f;
		decreasedSpeed = player.defaultSpeed - sixpercent;

		player.SendMessage ("AbilityOne", this.gameObject.name);

		CheckStats ();
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.E)) {
			isCamo = true;
		}

		// Reset Counter
		if(camoCounter >= camoDuration) {
			camoCounter = 0;
			isCamo = false;
		}

		if(isCamo) {
			camoCounter += 1.0f * Time.deltaTime;

			player.SendMessage("PersonalCamo", decreasedSpeed);

			foreach (GameObject teammates in BlueInRadius) {
				BlueCamo.Add (teammates);
				teammates.SendMessage("PersonalCamo", decreasedSpeed);
			}
		}

		if(!isCamo) {
			player.SendMessage ("PersonalCamoOff");
			
			foreach (GameObject teammates in BlueCamo) {
				teammates.SendMessage ("PersonalCamoOff");
			}
			// Clear 
			BlueCamo.Clear();
		}
	}

	void CheckStats()
	{
		if (currentAbilityLevel == 1) {
			camoDuration = camoDurationUpdrade;
			sphereCollider.radius = camoRadiusUpgrade;
		}
		if (currentAbilityLevel == 2) {
			camoDuration += camoDurationUpdrade;
			sphereCollider.radius += camoRadiusUpgrade;
		}
		if (currentAbilityLevel == 3) {
			camoDuration += camoDurationUpdrade;
			sphereCollider.radius += camoRadiusUpgrade;
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
		GameObject[] teammates = GameObject.FindGameObjectsWithTag ("Teammate");
		// Adding teammates within the set radius
		foreach (var str in teammates) {
			if (other.gameObject == str) { 
				BlueInRadius.Add(other.gameObject);
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		GameObject[] teammates = GameObject.FindGameObjectsWithTag ("Teammate");
		// Delete the teammates who are not within range
		foreach (var str in teammates) {
			if (other.gameObject == str) { 
				BlueInRadius.Remove(other.gameObject);
			}
		}
	}
}