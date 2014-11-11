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
	private GameObject particle;

	void Start()
	{
//		string ability = this.gameObject.transform.parent.gameObject.name;
//		player = GameObject.Find (ability).GetComponent<Player>();
		particle = gameObject.transform.FindChild("SFX_Tackle_Explosion").gameObject;

		currentAbilityLevel = 0;
		particle.SetActive(false);
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
			particle.SetActive(false);
			cooldownCounter += 1.0f * Time.deltaTime;
		}
		
		if (cooldownCounter >= cooldownPeriod[currentAbilityLevel]) {
			particle.transform.FindChild("Particle System").particleSystem.enableEmission = false;
			startCooldown = false;
			cooldownCounter = 0;
		}

		if (isCharge) {
			particle.SetActive(true);
			chargeCounter += 1.0f * Time.deltaTime;
			particle.transform.FindChild("Particle System").particleSystem.enableEmission = true;
			// Send message to parent turning Charge on with the duration
			gameObject.transform.parent.SendMessage ("Charge", chargeDuration[currentAbilityLevel]);

			isCharge = false;
		}
	}
}