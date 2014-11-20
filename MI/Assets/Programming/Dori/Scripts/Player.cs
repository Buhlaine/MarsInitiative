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

	public string team; // Player team
	public float health;
	public float ammo;
	public float stamina;
	public int maxAmmo;
	public float speed;
	public int totalXP; // Total amount of XP
	public int xp;
	public int skillPoints; // Used for spending on upgrades
	public int classIndex;
	
	public int kills; // Total player kills
	public int captures;
	public int assists;
	public int deaths; // Total player deaths
	public int level; // Current player level

	public float defaultHealth;
	public float sprintSpeed;
	public int[] maxHealth; // Max health depends on player class
	public float[] defaultSpeed; // Max speed depends on player class (Troper & Scout = 100, Heavy = 90)
	public float[] maxStamina;
	public Class currentClass;
	public GameObject abilityOne;
	public GameObject abilityTwo;
	public GameObject boostAbility;
	public bool isShooting;

	private XPTracker xptracker;
	private CharacterController controller;
	private GameObject particle;
	private GameObject spawnManager;

	// Team Variables
	public string enemy;
	public string teammate;

	void Start()
	{
		// Check what class the player has chosen - this will dictate max health, speed, and other things
		CheckPlayerClass ();
		SetTeam ();

		xptracker = GameObject.FindGameObjectWithTag ("XPTracker").GetComponent<XPTracker> ();
		controller = GetComponent<CharacterController> ();
		particle = gameObject.transform.FindChild ("Fist_Bump").gameObject;
		spawnManager = GameObject.FindGameObjectWithTag ("spwnManager");

		ammo = 0; 
		maxAmmo = 500;
		totalXP = 0;
		skillPoints = 0;

		kills = 0;
		captures = 0;
		assists = 0;
		deaths = 0;
		level = 0;

		sprintSpeed = 10;
		ReceiveLevelUp ();

		// When player logs in, send a message to XPTracker with this players name to be stored into a team list
		ArrayList playerInfo = new ArrayList();
		playerInfo.Add (this.gameObject.name);
		playerInfo.Add (this.gameObject.tag);

		xptracker.SendMessage ("StorePlayer", playerInfo);
		particle.SetActive(false);
	}

	void Update()
	{
		CheckPlayerClass ();

		// SHOOTING THE DUCK (TEST ONLY)
		if(Input.GetMouseButtonDown(0)){
			// DUCK SECTION
			RaycastHit duckInfo;
			if(Physics.Raycast(transform.position, Vector3.forward, out duckInfo, 10.0f)) {
				if (duckInfo.transform.tag == "Duck") { 
					// Insta-Kill Duck
					duckInfo.transform.gameObject.SendMessage("KillDuck", this.gameObject.name); 
				}
			} 
		}

		// Health
		if(health <= 0) {
			OnDeath ();
		}

		// Fist Bump
		if (Input.GetKeyDown (KeyCode.F)) {
			particle.SetActive (true);
		}

		// Sprint
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			speed = sprintSpeed;
			stamina -= 1 * Time.deltaTime;
		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			speed = defaultSpeed[classIndex];
			stamina += 1 * Time.deltaTime;
		}
	}

	void SetTeam()
	{
		if (team == "Blue") {
			teammate = "Blue";
			enemy = "Red";
		}
		if (team == "Red") {
			teammate = "Red";
			enemy = "Blue";
		}
	}

	[RPC]
	void setPosition(Vector3 _pos)
	{
		this.transform.position = _pos;
	}

	// Synchronizing variables across the network
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		float syncHealth = 0;
		float syncAmmo = 0;
		float syncSpeed = 0;
		int syncKills = 0;
		int syncAssists = 0;
		int syncCaptures = 0;
		int syncDeaths = 0;
		int syncLevel = 0;

		if(stream.isWriting)
		{
			syncHealth = health;
			syncAmmo = ammo;
			syncSpeed = speed;
			syncKills = kills;
			syncAssists = assists;
			syncCaptures = captures;
			syncDeaths = deaths;
			syncLevel = level;

			stream.Serialize(ref syncHealth);
			stream.Serialize(ref syncAmmo);
			stream.Serialize(ref syncSpeed);
			stream.Serialize(ref syncKills);
			stream.Serialize(ref syncAssists);
			stream.Serialize(ref syncCaptures);
			stream.Serialize(ref syncDeaths);
			stream.Serialize(ref syncLevel);
		} else {
			stream.Serialize(ref syncHealth);
			stream.Serialize(ref syncAmmo);
			stream.Serialize(ref syncSpeed);
			stream.Serialize(ref syncKills);
			stream.Serialize(ref syncAssists);
			stream.Serialize(ref syncCaptures);
			stream.Serialize(ref syncDeaths);
			stream.Serialize(ref syncLevel);

			health = syncHealth;
			ammo = syncAmmo;
			speed = syncSpeed;
			kills = syncKills;
			assists = syncAssists;
			captures = syncCaptures;
			deaths = syncDeaths;
			level = syncLevel;
		}
	}

	void CheckPlayerClass()
	{
		// Selecting one class turns on that class' abilities, and turns off the others
		if (currentClass == Class.Trooper) {
			health = maxHealth[0];
			defaultHealth = maxHealth[0];
			speed = defaultSpeed[0];
			stamina = maxStamina[0];
			classIndex = 0;

			abilityOne = this.gameObject.transform.FindChild ("Restock").gameObject;
			abilityTwo = this.gameObject.transform.FindChild ("PulseRadar").gameObject;
			boostAbility = this.gameObject.transform.FindChild ("TrooperBoost").gameObject;

			gameObject.transform.FindChild ("Shield").gameObject.SetActive(false);
			gameObject.transform.FindChild ("Tackle").gameObject.SetActive(false);
			gameObject.transform.FindChild ("Cloak").gameObject.SetActive(false);
			gameObject.transform.FindChild ("ChainShot").gameObject.SetActive(false);
			gameObject.transform.FindChild ("AssassinBoost").gameObject.SetActive(false);
			gameObject.transform.FindChild ("EnforcerBoost").gameObject.SetActive(false);

			abilityOne.SetActive(true);
			abilityTwo.SetActive(true);
			boostAbility.SetActive(true);
		}
		if (currentClass == Class.Enforcer) {
			health = maxHealth[1];
			defaultHealth = maxHealth[1];
			speed = defaultSpeed[1];
			stamina = maxStamina[1];
			classIndex = 1;

			abilityOne = this.gameObject.transform.FindChild ("Shield").gameObject;
			abilityTwo = this.gameObject.transform.FindChild ("Tackle").gameObject;
			boostAbility = this.gameObject.transform.FindChild ("EnforcerBoost").gameObject;

			gameObject.transform.FindChild ("Restock").gameObject.SetActive(false);
			gameObject.transform.FindChild ("PulseRadar").gameObject.SetActive(false);
			gameObject.transform.FindChild ("Cloak").gameObject.SetActive(false);
			gameObject.transform.FindChild ("ChainShot").gameObject.SetActive(false);
			gameObject.transform.FindChild ("TrooperBoost").gameObject.SetActive(false);
			gameObject.transform.FindChild ("AssassinBoost").gameObject.SetActive(false);
			
			abilityOne.SetActive(true);
			abilityTwo.SetActive(true);
			boostAbility.SetActive(true);
		}
		if (currentClass == Class.Assassin) {
			health = maxHealth[2];
			defaultHealth = maxHealth[2];
			speed = defaultSpeed[2];
			stamina = maxStamina[2];
			classIndex = 2;

			abilityOne = this.gameObject.transform.FindChild ("Cloak").gameObject;
			abilityTwo = this.gameObject.transform.FindChild ("ChainShot").gameObject;
			boostAbility = this.gameObject.transform.FindChild ("AssassinBoost").gameObject;

			gameObject.transform.FindChild ("Restock").gameObject.SetActive(false);
			gameObject.transform.FindChild ("PulseRadar").gameObject.SetActive(false);
			gameObject.transform.FindChild ("Shield").gameObject.SetActive(false);
			gameObject.transform.FindChild ("Tackle").gameObject.SetActive(false);
			gameObject.transform.FindChild ("TrooperBoost").gameObject.SetActive(false);
			gameObject.transform.FindChild ("EnforcerBoost").gameObject.SetActive(false);
			
			abilityOne.SetActive(true);
			abilityTwo.SetActive(true);
			boostAbility.SetActive(true);
		}
	}

	void Reset()
	{
		health = defaultHealth;
		ammo = maxAmmo;
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

	void OnAssist() //TODO Are assists coded? O_O
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
		spawnManager.SendMessage ("spawnRequest", this.gameObject);
		this.gameObject.SendMessage ("Dead", true);
		this.gameObject.SendMessage ("setLife");

		// Turn the game object off so the player can't see the spawning process and the colliders aren't active
		this.gameObject.SetActive (false);
	}

	void KillYourself()
	{
		Debug.Log (this.gameObject.name + " has died.");
		health = 0;
		networkView.RPC ("OnDeath", RPCMode.All, this.gameObject.networkView.viewID);
	}

	void AddXP(int _xp)
	{
		xp += _xp;

		ArrayList data = new ArrayList ();
		
		data.Add (xp);
		data.Add (level);

		// Send current xp to experience bar (GUI)
		this.gameObject.SendMessage ("getCurrent", xp, SendMessageOptions.DontRequireReceiver);
	}

	void ReceiveLevelUp()
	{
		level += 1;
		skillPoints += 1;

		ArrayList data = new ArrayList();

		data.Add (gameObject.name); // string
		data.Add (level); // int
		data.Add (classIndex); // int
		data.Add (skillPoints); // int

		// Player info and level up screen (GUI)
		this.gameObject.SendMessage ("levelChecking", data, SendMessageOptions.DontRequireReceiver);
	}

	// Golden Functions HERE (Accept level ups from player level up menu)
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

	void AbilityThreeLevelUp()
	{
		gameObject.transform.Find (boostAbility.name).SendMessage ("Changed");
	}

	void IncreaseSprintSpeed()
	{
		sprintSpeed = 12;
	}

	void IncreaseMaxHealth()
	{
		maxHealth[1] = 100;
	}

	void IncreaseTrooperStats()
	{
		this.gameObject.transform.FindChild(abilityOne.name).SendMessage("IncreaseRegenAmount");
		maxStamina[0] = 8;
	}

	void RestartXPCounter(int _leftOverXP)
	{
		// Left over XP is calculated
		xp = 0;
		xp += _leftOverXP;

		ArrayList data = new ArrayList ();

		data.Add (xp);
		data.Add (level);

		// Send current xp to experience bar (GUI)
		this.gameObject.SendMessage ("getCurrent", data, SendMessageOptions.DontRequireReceiver);
	}

	// SUPPORT CLASS SECTION
	void PulseRadar(string _enemy)
	{
		// Send enemy position to GUI
		this.gameObject.SendMessage ("visibleEnemy", _enemy, SendMessageOptions.DontRequireReceiver);
	}
	
	void PulseDamage(float _damage)
	{
		// Damaging the enemy in "pulses". Pulses controlled in ability script.
		health -= _damage;
	}

	// Ammo and health restocking
	void Restock(float _regenIndex)
	{
		if (health >= defaultHealth) {
			health = defaultHealth;
		} else {
			health += _regenIndex * Time.deltaTime;
		}

		if (ammo >= maxAmmo) {
			ammo = maxAmmo;
		} else {
			ammo += _regenIndex * Time.deltaTime;
		}
	}

	// TEST DEFENSE SECTION
	[RPC]
	void Charge(int _duration) 
	{
		RaycastHit hit;
		for (float i = 0.0f; i <= _duration; i += 1.0f * Time.deltaTime) {
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			controller.Move ( forward * speed * 1.5f * Time.deltaTime);

			if(Physics.SphereCast(transform.position, controller.height / 2, transform.forward, out hit, 0.5f)) {
				GameObject enemy = hit.transform.gameObject; 
				if(enemy.transform.tag == "Enemy") {
					networkView.RPC ("KillYourself", enemy.networkView.owner);
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
		this.gameObject.renderer.material.shader = Shader.Find("Diffuse"); //TODO Need an actual shader 
		this.gameObject.renderer.material.SetColor("Color", Color.blue); // For testing
	}
}