using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SupportPulseRadar : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float pulseCounter;
	public float cooldownCounter;
	public float cooldownPeriod;
	public float[] pulseDamage;
	public float[] pulseDuration;
	public float[] pulseRadius;
	
	public bool isPulse;
	public bool startCooldown;

	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> EnemyInRadius = new List<GameObject>();

	void Start()
	{
		sphereCollider = this.gameObject.transform.GetComponent<SphereCollider> ();
		string ability = this.gameObject.transform.parent.gameObject.name;
		player = GameObject.Find (ability).GetComponent<Player>();
		
		currentAbilityLevel = 0;
		cooldownPeriod = 16.0f;
		
		player.SendMessage ("AbilityTwo", this.gameObject.name);
	}

	void Update()
	{
		sphereCollider.radius = pulseRadius [currentAbilityLevel];

		if (Input.GetKeyDown (KeyCode.Q) && !isPulse && !startCooldown) {
			isPulse = true;
		}

		// Stop counter
		if (pulseCounter >= pulseDuration[currentAbilityLevel]) {
			startCooldown = true;
			isPulse = false;
			pulseCounter = 0;
		}

		// Reset and start cool down
		if(startCooldown) {
			cooldownCounter += 1.0f * Time.deltaTime;
		}
		
		if (cooldownCounter >= cooldownPeriod) {
			startCooldown = false;
			cooldownCounter = 0;
		}

		if (isPulse) {
			pulseCounter += 1.0f * Time.deltaTime;

			// Marking enemies on the minimap
			foreach (var enemy in EnemyInRadius) {
				enemy.SendMessage("PulseRadar", true);
				// Sending damage in "pulses"
//				for(float i = 0; i < 10; i += 1.0f * Time.deltaTime) {
//					if (i%2 == 0) {
//						Debug.Log ("Pulse Damage!" + i);
//						enemy.SendMessage("PulseDamage", pulseDamage[currentAbilityLevel]);
//					}
//				}
			}
		}

		if (!isPulse) {
			foreach (var enemy in EnemyInRadius) {
				enemy.SendMessage("PulseRadar", false);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("Boom");
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		// Adding enemies to a list if they're within range
		foreach (var enemy in enemies) {
			if (other.gameObject == enemy) { 
				Debug.Log ("Added: " + other.gameObject);
				EnemyInRadius.Add(other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		// Removing enemies from a list if they're not within range
		foreach (var enemy in enemies) {
			if (other.gameObject == enemy) { 
				EnemyInRadius.Remove(other.gameObject);
			}
		}
	}
}