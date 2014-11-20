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

	void Awake()
	{
		particle = gameObject.transform.FindChild("SFX_Tackle_Explosion").gameObject;
//		player = this.gameObject.transform.parent.GetComponent<Player>();
	}

	void Start()
	{
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
			isCharge = false;
			startCooldown = true;
			chargeCounter = 0;
		}
		
		// Reset and start cool down
		if(startCooldown) {
			cooldownCounter += 1.0f * Time.deltaTime;
			particle.SetActive(false);
		}
		
		if (cooldownCounter >= cooldownPeriod[currentAbilityLevel]) {
			startCooldown = false;
			cooldownCounter = 0;
		}

		if (isCharge) {
			particle.SetActive(true);
			chargeCounter += 1.0f * Time.deltaTime;
			// Send message to parent turning Charge on with the duration
			gameObject.transform.parent.SendMessage ("Charge", chargeDuration[currentAbilityLevel]);
		}
	}
}