using UnityEngine;
using System.Collections;

public class DefenseTackle : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float chargeCounter;
	public float cooldownCounter;
	public float[] cooldownPeriod;
	public float[] chargeDuration;
	
	public bool isCharge;
	public bool startCooldown;
	
//	private Player player;

	void Start()
	{
//		string ability = this.gameObject.transform.parent.gameObject.name;
//		player = GameObject.Find (ability).GetComponent<Player>();
		currentAbilityLevel = 0;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.E) && !isCharge && !startCooldown) {
			isCharge = true;
		}
		
		// Stop counter
		if (chargeCounter >= chargeDuration[currentAbilityLevel]) {
			startCooldown = true;
			isCharge = false;
			chargeCounter = 0;
		}
		
		// Reset and start cool down
		if(startCooldown) {
			cooldownCounter += 1.0f * Time.deltaTime;
		}
		
		if (cooldownCounter >= cooldownPeriod[currentAbilityLevel]) {
			startCooldown = false;
			cooldownCounter = 0;
		}

		if (isCharge) {
			chargeCounter += 1.0f * Time.deltaTime;
			// Send message to parent turning Charge on with the duration
			gameObject.transform.parent.SendMessage ("Charge", chargeDuration[currentAbilityLevel]);

			isCharge = false;
		}
	}
}