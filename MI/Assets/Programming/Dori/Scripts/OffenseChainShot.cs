using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OffenseChainShot : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float cooldownCounter;
	public float[] cooldownPeriod;
	public float[] chainShotRadius;
	public bool chainShotActive;
	public bool startCooldown;
	public GameObject target;

	private bool hasShot;
	private Player player;
	private GameObject particle;

	void Awake()
	{
		player = this.gameObject.transform.parent.GetComponent<Player>();
		particle  = gameObject.transform.FindChild("SFX_Chain_Shot_Barrel").gameObject;
	}

	void Start()
	{
		currentAbilityLevel = 0;
		particle.SetActive(false);
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Q) && !startCooldown && !chainShotActive) {
			chainShotActive = true;
			particle.SetActive(true);
			hasShot = true;
		} 

		// Reset Counter and start cool down period
		if (hasShot) {
			startCooldown = true;
		}

		// The cooldown timer stars. Player cannot use this ability while the cooldown is active.
		if (startCooldown) {
			particle.SetActive(false);
			cooldownCounter += 1.0f * Time.deltaTime;
		}

		// Cooldown period is set according to this abilities level.
		if (cooldownCounter >= cooldownPeriod[currentAbilityLevel]) {
			startCooldown = false;
			hasShot = false;
			cooldownCounter = 0;
		}

		// Send the name of the short enemy to the weapons
		if (chainShotActive) {
			player.SendMessage("sniperSkillActivate", SendMessageOptions.DontRequireReceiver);
		}
	}
}