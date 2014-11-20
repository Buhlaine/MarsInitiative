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

	private Player player;
	private SphereCollider sphereCollider;
	private List<GameObject> EnemyInRadius = new List<GameObject>();
	private GameObject particle;

	void Awake()
	{
		sphereCollider = this.gameObject.transform.GetComponent<SphereCollider> ();
		player = this.gameObject.transform.parent.GetComponent<Player>();
		particle = transform.FindChild("Radar").gameObject;
	}

	void Start()
	{
		currentAbilityLevel = 0;
		cooldownPeriod = 16.0f;
		particle.transform.FindChild("Radar_Effect").gameObject.SetActive(false);
		particle.transform.FindChild("Burst").gameObject.SetActive(false);
	}

	[RPC]
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
			particle.transform.FindChild("Radar_Effect").gameObject.SetActive(false);
			particle.transform.FindChild("Burst").gameObject.SetActive(false);
			cooldownCounter += 1.0f * Time.deltaTime;
		}
		
		if (cooldownCounter >= cooldownPeriod) {
			startCooldown = false;
			cooldownCounter = 0;
		}

		if (isPulse) {
			pulseCounter += 1.0f * Time.deltaTime;
			particle.transform.FindChild("Radar_Effect").gameObject.SetActive(true);
			particle.transform.FindChild("Burst").gameObject.SetActive(true);

			// Marking enemies on the minimap
			foreach (var enemy in EnemyInRadius) {
				networkView.RPC("PulseRadar", enemy.networkView.owner);
				particle.transform.FindChild("Radar_Effect").gameObject.SetActive(true);
				particle.transform.FindChild("Burst").gameObject.SetActive(true);

				damageCounter += 1.0f * Time.deltaTime;
				if (damageCounter >= 3) {
					damageCounter = 0;
					networkView.RPC("PulseDamage", enemy.networkView.owner, pulseDamage[currentAbilityLevel]);
				}
			}
		}

		if (!isPulse) {
			damageCounter = 0;
		}
	}

	void Changed()
	{
		currentAbilityLevel += 1;
	}

	void OnTriggerEnter(Collider other)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (player.enemy);
		// Adding enemies to a list if they're within range
		foreach (var enemy in enemies) {
			if (other.gameObject.tag == player.enemy) {
				EnemyInRadius.Add(other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		// Removing enemies from a list if they're not within range
		foreach (var enemy in EnemyInRadius) {
			if (other.gameObject.tag == player.enemy) { 
				EnemyInRadius.Remove(other.gameObject);
			}
		}
	}
}