using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* This ability sends out a pulse that locates enemies on the minimap and applies damage.
 * Ability Level 1 = Duration(5) Radius for damage(8) Radius for radar(50)
 * Ability Level 2 = Duration(7) Radius for damage(8) Radius for radar(50)
 * Ability Level 3 = Duration(9) Radius for damage(8) Radius for radar(100)
 * Cooldown = 16 seconds at all three levels
 * Regen amount per second = 10
 * 
 * This script needs to send out damage in "waves" and not continually.
*/

public class SupportPulseRadar : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float pulseCounter;
	public float cooldownCounter;
	public float cooldownPeriod;
	public float[] pulseDamage;
	public float[] pulseDuration;
	public float[] pulseRadius;

	public float damageCounter;
	
	public bool isPulse;
	public bool startCooldown;

//	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> EnemyInRadius = new List<GameObject>();

	void Start()
	{
		// Create references and set values
		sphereCollider = this.gameObject.transform.GetComponent<SphereCollider> ();
//		string ability = this.gameObject.transform.parent.gameObject.name;
//		player = GameObject.Find (ability).GetComponent<Player>();
		
		currentAbilityLevel = 0;
		cooldownPeriod = 16.0f;
	}

	void Update()
	{
		// Set the radius of the sphere collider to the pre-determined amount
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

			ArrayList data = new ArrayList();

			// Marking enemies on the minimap
			foreach (var enemy in EnemyInRadius) {
				enemy.SendMessage("PulseRadar", enemy.name);
				// Sending damage in "pulses"
				damageCounter += 1.0f * Time.deltaTime;
				if (damageCounter >= 3) {
					damageCounter = 0;
					enemy.SendMessage("PulseDamage", pulseDamage[currentAbilityLevel]);
				}
			}
		}

		if (!isPulse) {
			damageCounter = 0;

			foreach (var enemy in EnemyInRadius) {
				enemy.SendMessage("PulseRadar", false);
			}
		}
	}

	// If called by the player because of a player level up, then this ability has been chosen for a level up
	void Changed()
	{
		currentAbilityLevel += 1;
	}

	void OnTriggerEnter(Collider other)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		// Adding enemies to a list if they're within range
		foreach (var enemy in enemies) {
			if (other.gameObject.tag == "Enemy") {
				EnemyInRadius.Add(other.gameObject);
				Debug.Log ("Adding: " + enemy.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		// Removing enemies from a list if they're not within range
		foreach (var enemy in EnemyInRadius) {
			if (other.gameObject.tag == "Enemy") { 
				EnemyInRadius.Remove(other.gameObject);
				Debug.Log ("Removing: " + enemy.gameObject);
			}
		}
	}
}