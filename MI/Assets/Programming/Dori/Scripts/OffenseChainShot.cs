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

	private bool hasShot;
	private Player player;
	private List<GameObject> listofHits = new List<GameObject>();

	RaycastHit hitInfo;

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
		}

		// Reset Counter and start cool down period
		if (hasShot) {
			startCooldown = true;
		}
		
		if (startCooldown) {
			cooldownCounter += 1.0f * Time.deltaTime;
		}
		
		if (cooldownCounter >= cooldownPeriod[currentAbilityLevel]) {
			startCooldown = false;
			hasShot = false;
			cooldownCounter = 0;
		}

		if (chainShotActive) {
			player.SendMessage ("ChainShot", gameObject.name);
		}
	}

	void Off()
	{
		hasShot = true;
		chainShotActive = false;
	}
}