using UnityEngine;
using System.Collections;

/* Player Manager Script - controls health, ammo, speed, and exerperience points for the player. */

public class Player : MonoBehaviour 
{
	public enum Class {
		Trooper,
		Enforcer,
		Assassin
	};

	public float health;
	public float ammo;
	public int maxAmmo;
	public float speed;
	public int totalXP; // Total amount of XP
	public int xp;
	public int skillPoints; // Used for spending on upgrades
	
	public int kills; // Total player kills
	public int captures;
	public int assists;
	public int deaths; // Total player deaths
	public int level; // Current player level

	public float defaultHealth;
	public int[] maxHealth; // Max health depends on player class
	public float[] defaultSpeed; // Max speed depends on player class (Troper & Scout = 100, Heavy = 90)
	public Class currentClass;

	public GameObject abilityOne;
	public GameObject abilityTwo;

	public bool isShooting;
	private XPTracker xptracker;
	private CharacterController controller;

	void Awake()
	{
		// Set the this game object's tag to "Player"
		this.gameObject.tag = "Player";
	}

	void Start()
	{
		// Check what class the player has chosen - this will dictate max health, speed, and other things
		CheckPlayerClass ();

		ammo = 0; 
		maxAmmo = 500;
		totalXP = 0;
		skillPoints = 0;

		kills = 0;
		captures = 0;
		assists = 0;
		deaths = 0;
		level = 1;

		xptracker = GameObject.FindGameObjectWithTag ("XPTracker").GetComponent<XPTracker> ();
		controller = GetComponent<CharacterController> ();

		// When player logs in, send a message to XPTracker with this players name to be stored into a team list
		ArrayList playerInfo = new ArrayList();
		playerInfo.Add (this.gameObject.name);
		playerInfo.Add (this.gameObject.tag);

		xptracker.SendMessage ("StorePlayer", playerInfo);
	}

	void Update()
	{
		CheckPlayerClass ();

		if(Input.GetMouseButtonDown(0)){
			// DUCK SECTION
			RaycastHit duckInfo;
			// TODO Last parameter should be actual weapon's range. 10 for testing
			if(Physics.Raycast(transform.position, Vector3.forward, out duckInfo, 10.0f)) {
				if (duckInfo.transform.tag == "Duck") { 
					// Apply damage to the duck, and send the name of this player
					duckInfo.transform.gameObject.SendMessage("WhoKillMe", this.gameObject.name); 
					// TODO Should be actual weapon's damage. 10 for testing
					duckInfo.transform.gameObject.SendMessage("Damage", 10.0f); 
					Debug.Log ("Duck! " + duckInfo.transform.name + " " + duckInfo.transform.gameObject.GetComponent<DuckScript>().health);
				}
			} 
		}

		// Simulating when player scores a kill (TESTING ONLY)
		if (Input.GetKeyDown (KeyCode.J)) {
			OnKill ();
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			OnCapture ();
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			OnAssist ();
		}
//
//		// Test Character Controls (TESTING ONLY)
//		if (Input.GetKey (KeyCode.W)) {
//			transform.Translate (Vector3.forward * speed * Time.deltaTime);
//		}
//		if (Input.GetKey (KeyCode.S)) {
//			transform.Translate (-Vector3.forward * speed * Time.deltaTime);
//		}
//		if (Input.GetKey (KeyCode.A)) {
//			transform.Translate (-Vector3.right * speed * Time.deltaTime);
//		}
//		if (Input.GetKey (KeyCode.D)) {
//			transform.Translate (Vector3.right * speed * Time.deltaTime);
//		}
	}

	void CheckPlayerClass()
	{
		// Selecting one class turns on that class' abilities, and turns off the others
		if (currentClass == Class.Trooper) {
			health = maxHealth[0];
			defaultHealth = maxHealth[0];
			speed = defaultSpeed[0];

			abilityOne = this.gameObject.transform.FindChild ("Restock").gameObject;
			abilityTwo = this.gameObject.transform.FindChild ("PulseRadar").gameObject;

			gameObject.transform.FindChild ("Shield").gameObject.SetActive(false);
			gameObject.transform.FindChild ("Tackle").gameObject.SetActive(false);
			gameObject.transform.FindChild ("Cloak").gameObject.SetActive(false);
			gameObject.transform.FindChild ("ChainShot").gameObject.SetActive(false);

			abilityOne.SetActive(true);
			abilityTwo.SetActive(true);
		}
		if (currentClass == Class.Enforcer) {
			health = maxHealth[1];
			defaultHealth = maxHealth[1];
			speed = defaultSpeed[1];

			abilityOne = this.gameObject.transform.FindChild ("Shield").gameObject;
			abilityTwo = this.gameObject.transform.FindChild ("Tackle").gameObject;

			gameObject.transform.FindChild ("Restock").gameObject.SetActive(false);
			gameObject.transform.FindChild ("PulseRadar").gameObject.SetActive(false);
			gameObject.transform.FindChild ("Cloak").gameObject.SetActive(false);
			gameObject.transform.FindChild ("ChainShot").gameObject.SetActive(false);
			
			abilityOne.SetActive(true);
			abilityTwo.SetActive(true);
		}
		if (currentClass == Class.Assassin) {
			health = maxHealth[2];
			defaultHealth = maxHealth[2];
			speed = defaultSpeed[2];

			abilityOne = this.gameObject.transform.FindChild ("Cloak").gameObject;
			abilityTwo = this.gameObject.transform.FindChild ("ChainShot").gameObject;

			gameObject.transform.FindChild ("Restock").gameObject.SetActive(false);
			gameObject.transform.FindChild ("PulseRadar").gameObject.SetActive(false);
			gameObject.transform.FindChild ("Shield").gameObject.SetActive(false);
			gameObject.transform.FindChild ("Tackle").gameObject.SetActive(false);
			
			abilityOne.SetActive(true);
			abilityTwo.SetActive(true);
		}
	}

	// XP SECTION
	void OnKill()
	{
		Debug.Log ("Kill");
		string player = this.gameObject.name;
		int isKill = 0;
		ArrayList data = new ArrayList();

		// Store data into an array - sent to XPTracker
		data.Add (player);
		data.Add (xp);
		data.Add (isKill);

		xptracker.SendMessage ("UpdateXP", data);
		kills += 1;
	}

	void OnCapture()
	{
		Debug.Log ("Capture");
		string player = this.gameObject.name;
		int isCap = 1;
		ArrayList data = new ArrayList();
		
		// Store data into an array - sent to XPTracker
		data.Add (player);
		data.Add (xp);
		data.Add (isCap);
		
		xptracker.SendMessage ("UpdateXP", data);
		captures += 1;
	}

	void OnAssist()
	{
		Debug.Log ("Assist");
		string player = this.gameObject.name;
		int isAss = 2;
		ArrayList data = new ArrayList();
		
		// Store data into an array - sent to XPTracker
		data.Add (player);
		data.Add (xp);
		data.Add (isAss);
		
		xptracker.SendMessage ("UpdateXP", data);
		assists += 1;
	}

	void OnAbility()
	{
		Debug.Log ("Ability");
		string player = this.gameObject.name;
		int isAbility = 3;
		ArrayList data = new ArrayList();
		
		// Store data into an array - sent to XPTracker
		data.Add (player);
		data.Add (xp);
		data.Add (isAbility);
		
		xptracker.SendMessage ("UpdateXP", data);
	}

	void OnKillDuck()
	{
		// If this player killed the duck - send info of this player that killed the duck
		string player = this.gameObject.name;
		ArrayList data = new ArrayList ();

		data.Add (player);
		data.Add (xp);

		xptracker.SendMessage ("CompleteLevel", data);
	}

	void OnDeath()
	{
		deaths += 1;
	}

	void KillYourself()
	{
		Debug.Log (this.gameObject.name + " has died.");
		health = 0;
		OnDeath ();
	}

	void AddXP(int _xp)
	{
		xp += _xp;
	}

	void ReceiveLevelUp()
	{
		level += 1;
		skillPoints += 1;

		int levelUpEvent = 1; // TODO Double Check with Ramone

		ArrayList data = new ArrayList();

		data.Add (gameObject.name);
		data.Add (levelUpEvent);

		gameObject.SendMessage ("UpperCornerEvent", data); // TODO Message has no receiver
	}

	// Golden Functions HERE
	void AbilityOneLevelUp()
	{
		// Sending a level up to ability one
		gameObject.transform.Find (abilityOne.name).SendMessage ("Changed");
	}

	void AbilityTwoLevelUp()
	{
		// Sending a level up to ability two
		gameObject.transform.Find (abilityTwo.name).SendMessage ("Changed");
	}

	void StatsLevelUp()
	{
		// TODO Sending a level up for stats so that it can be chosen by player
		gameObject.SendMessage ("StatLevelUp");
	}

	void ReceiveStatLevelUp(string _stat)
	{
		// TODO level up chosen stat
	}

	void RestartXPCounter(int _leftOverXP)
	{
		// Left over XP is calculated
		xp = 0;
		xp += _leftOverXP;
	}

	// SUPPORT CLASS SECTION
	void PulseRadar(bool _onoroff)
	{
		// TODO Marking the enemy
		Debug.Log ("Marking!");
	}
	
	void PulseDamage(float _damage)
	{
		Debug.Log ("Ping! Pulse damage taken!");
		// Damaging the enemy in "pulses". Pulses controlled in ability script.
		health -= _damage;
	}

	// Ammo and health restocking
	void Restock(int _regenIndex)
	{
		if (health >= defaultHealth) {
			health = defaultHealth;
		}
		else {
			health += _regenIndex * Time.deltaTime;
		}

		if (ammo >= maxAmmo) {
			ammo = maxAmmo;
		}
		else {
			ammo += _regenIndex * Time.deltaTime;
		}
	}

	// TEST DEFENSE SECTION
	void Charge(int _duration) 
	{
		RaycastHit hit;
		for (float i = 0.0f; i <= _duration; i += 1.0f * Time.deltaTime) {
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			controller.Move ( forward * speed * 1.5f * Time.deltaTime);

			if(Physics.SphereCast(transform.position, controller.height / 2, transform.forward, out hit, 0.5f)) {
				GameObject enemy = hit.transform.gameObject; 
				if(enemy.transform.tag == "Enemy") {
					enemy.SendMessage("KillYourself"); //TODO inappropriate geez
				}
			}
		}
	}

	// TEST OFFENSE SECTION
	void CloakOn()
	{
		// Turn on "invisible" shader
		this.gameObject.renderer.material.shader = Shader.Find ("Camo");
	}
	
	void CloakOff()
	{
		// Change shader back to default
		this.gameObject.renderer.material.shader = Shader.Find("Diffuse");
		this.gameObject.renderer.material.SetColor("Color", Color.blue); // For testing
	}

	void ChainShot(string _chainShotSender)
	{
		if (Input.GetButtonDown("Fire1")) {
			GameObject.Find (_chainShotSender).SendMessage ("Off");
		}
	}
}