using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OffenseCloak : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float cooldownCounter;
	public float cooldownPeriod;
	public float camoCounter;
	public float[] camoDuration;
	public string target;
	public string invisible;
	
	public bool isCamo;
	public bool startCooldown;

	private Player player;
	private SphereCollider sphereCollider;

	void Start()
	{
		// Set references and values
		currentAbilityLevel = 0;
		cooldownPeriod = 26.0f;

		sphereCollider = this.gameObject.transform.GetComponent<SphereCollider> ();
		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.E) && !startCooldown && !isCamo) {
			isCamo = true;
			invisible = target;
		}

		RaycastHit hitInfo;
		// Fire a raycast in front of the player and gather hit info
		if(currentAbilityLevel >= 1 && !isCamo) {
			if(Physics.Raycast(transform.position, Vector3.forward, out hitInfo, 50.0f)) {
				if(hitInfo.transform.tag == "Teammate") {
					// Assign the target to variable marked
					target = hitInfo.transform.name;
				}
				else {
					target = null;
				}
			}
		}

		// Reset Counter
		if(camoCounter >= camoDuration[currentAbilityLevel]) {
			startCooldown = true;
			isCamo = false;
			camoCounter = 0;
		}

		if(startCooldown) {
			cooldownCounter += 1.0f * Time.deltaTime;
		}
		
		if (cooldownCounter >= cooldownPeriod) {
			startCooldown = false;
			cooldownCounter = 0;
		}

		if(isCamo) {
			camoCounter += 1.0f * Time.deltaTime;

			player.SendMessage("PersonalCamo");

			if(target != null && currentAbilityLevel >= 1) {
				GameObject.Find(invisible).SendMessage("PersonalCamo");
				Debug.Log (invisible + " is invisible now.");
			}
		}

		if(!isCamo && target == null) {
			player.SendMessage ("PersonalCamoOff");

			if(invisible != null && currentAbilityLevel >= 1) {
				GameObject.Find(invisible).SendMessage("PersonalCamoOff");
				Debug.Log (invisible + " is visible now.");
				invisible = null;
			}
		}
	}
	
	void Changed()
	{
		currentAbilityLevel += 1;
	}
}