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

	void Start()
	{
		currentAbilityLevel = 0;

		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Q) && !startCooldown && !chainShotActive) {
			chainShotActive = true;

			// Send a raycast forward. If it hits an enemy while the chainshot is active, then that enemy is dead.
			RaycastHit enemy;
			if(Physics.Raycast(transform.position, Vector3.forward, out enemy)) {
				if(enemy.transform.tag == "Enemy") {
					target = enemy.transform.gameObject;
				}
			}
		}

		// Reset Counter and start cool down period
		if (hasShot) {
			startCooldown = true;
		}

		// The cooldown timer stars. Player cannot use this ability while the cooldown is active.
		if (startCooldown) {
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
			player.SendMessage ("ChainShot", target.name);
		}
	}

	void Off()
	{
		hasShot = true;
		chainShotActive = false;
	}
}