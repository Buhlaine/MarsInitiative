using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OffenseCamo : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float cooldownCounter;
	public float cooldownPeriod;
	public float decreasedSpeed;
	public float camoCounter;
	public float[] camoDuration;
	public string target;
	
	public bool isCamo;
	public bool startCooldown;

	private Player player;
	private SphereCollider sphereCollider;

	void Start()
	{
		currentAbilityLevel = 0;
		cooldownPeriod = 26.0f;

		sphereCollider = this.gameObject.transform.GetComponent<SphereCollider> ();
		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();

		float sixpercent = player.defaultSpeed * 0.06f;
		decreasedSpeed = player.defaultSpeed - sixpercent;

		player.SendMessage ("AbilityOne", this.gameObject.name);
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.E) && !startCooldown && !isCamo) {
			isCamo = true;
		}

		RaycastHit hitInfo;
		
		// Fire a raycast in front of the player and gather hit info
		if(currentAbilityLevel >= 1) {
			if(Physics.Raycast(transform.position, Vector3.forward, out hitInfo, 5.0f)) {
				if(hitInfo.transform.tag == "Teammate") {
					// Assign the target to variable marked
					target = hitInfo.transform.name;
				}
			}
			else if(!isCamo) {
				target = null;
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

			player.SendMessage("PersonalCamo", decreasedSpeed);

			if(target != null && currentAbilityLevel >= 1) {
				GameObject.Find(target).SendMessage("PersonalCamo", decreasedSpeed);
				Debug.Log (target + " is invisible now.");
			}
		}

		if(!isCamo) {
			player.SendMessage ("PersonalCamoOff");

			if(target != null && currentAbilityLevel >= 1) {
				GameObject.Find(target).SendMessage("PersonalCamoOff");
				Debug.Log (target + " is visible now.");
			}
		}
	}
	
	void Changed()
	{
		currentAbilityLevel += 1;
	}
}