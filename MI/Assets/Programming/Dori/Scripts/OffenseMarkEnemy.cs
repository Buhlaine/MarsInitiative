using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OffenseMarkEnemy : MonoBehaviour 
{
	public int currentAbilityLevel;
	public float markCounter;
	public float cooldownCounter;
	public float cooldownPeriod;
	public string marked;
	public float[] markDuration;
	public float[] markRadius;

	public bool isMarking;
	public bool startCooldown;

	private Player player;
	private SphereCollider sphereCollider;
	public List<GameObject> EnemiesInRadius = new List<GameObject>();
	public List<GameObject> EnemiesMarked = new List<GameObject>();

	void Awake()
	{
		player = this.gameObject.transform.parent.GetComponent<Player>();
		sphereCollider = GetComponent<SphereCollider> ();
	}

	void Start()
	{
		currentAbilityLevel = 0;
		cooldownPeriod = 16.0f;
	}
	
	void Update()
	{
		sphereCollider.radius = markRadius [currentAbilityLevel];

		// Start Counter
		if(Input.GetKeyDown(KeyCode.Q) && !startCooldown) {
			isMarking = true;
		}

		if (isMarking) {
			markCounter += 1.0f * Time.deltaTime;

			foreach (GameObject enemies in EnemiesInRadius) {
				EnemiesMarked.Add (enemies);
			}
			foreach (GameObject enemies in EnemiesMarked) {
				enemies.SendMessage("Marked", true);
			}
		}

		// Start ability counter
		if(markCounter >= markDuration[currentAbilityLevel]) {
			markCounter = 0;
			isMarking = false;
			marked = null;
			startCooldown = true;

			foreach (GameObject enemies in EnemiesMarked) {
				enemies.SendMessage ("Marked", false);
			}
			// Clear 
			EnemiesMarked.Clear();
		}

		// Start cool down
		if(startCooldown) {
			cooldownCounter += 1.0f * Time.deltaTime;
		}
		
		if (cooldownCounter >= cooldownPeriod) {
			startCooldown = false;
			cooldownCounter = 0;
		}
	}

	void Changed()
	{
		currentAbilityLevel += 1;
	}

	void OnTriggerEnter(Collider other) 
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (player.enemy);
		// Adding enemies within the set radius
		foreach (var str in enemies) {
			if (other.gameObject == str) { 
				EnemiesInRadius.Add(other.gameObject);
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (player.enemy);
		// Delete the enemies who are not within range
		foreach (var str in enemies) {
			if (other.gameObject == str) { 
				EnemiesInRadius.Remove(other.gameObject);
			}
		}
	}
}