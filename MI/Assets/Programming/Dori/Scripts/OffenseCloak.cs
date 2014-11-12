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
	private GameObject particle;

	void Awake()
	{
		player = this.gameObject.transform.parent.GetComponent<Player>();
		particle = gameObject.transform.FindChild("SFX_Cloaking").gameObject;
	}

	void Start()
	{
		currentAbilityLevel = 0;
		cooldownPeriod = 26.0f;
		particle.SetActive (false);

		Reset ();
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
				if(hitInfo.transform.tag == player.teammate) {
					// Assign the target to variable marked
					target = hitInfo.transform.name;
				} 
			}
			else {
				Reset();
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

			player.SendMessage("CloakOn");
			particle.SetActive(true);

			if(target != null && currentAbilityLevel >= 1) {
				GameObject.Find(invisible).SendMessage("CloakOn");
			}
		}

		if(!isCamo) {
			player.SendMessage ("CloakOff");
			particle.SetActive (false);

			if(invisible != null && currentAbilityLevel >= 1) {
				GameObject.Find(invisible).SendMessage("CloakOff");
				Reset ();
			}
		}
	}

	void Reset()
	{
		invisible = null;
		target = null;
	}
	
	void Changed()
	{
		currentAbilityLevel += 1;
	}
}