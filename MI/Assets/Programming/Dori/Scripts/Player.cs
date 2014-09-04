using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float ammo;
	public int xp;
	public int kills;
	public int deaths;
	public int level;
	public int maxHealth = 100;
	public int maxAmmo = 100;

	public float health;
	public float speed;
	public float defaultSpeed = 5.0f;

	public Rigidbody bullet;
	public Transform bulletSpawn;
	private XPTracker xptracker;
	private DuckScript duck;
	
	void Start()
	{
		health = 100;
		ammo = 10; 
		xp = 0;
		kills = 0;
		deaths = 0;
		level = 1;
		speed = defaultSpeed;

		xptracker = GameObject.FindGameObjectWithTag ("XPTracker").GetComponent<XPTracker> ();
		duck = GameObject.FindGameObjectWithTag ("Duck").GetComponent<DuckScript> ();

		// When player logs in, send a message to XPTracker with this players name to be stored into a team list
		ArrayList playerInfo = new ArrayList();
		playerInfo.Add (this.gameObject.name);
		playerInfo.Add (this.gameObject.tag);

		xptracker.SendMessage ("StorePlayer", playerInfo);
	}
	
	void Update()
	{
		// Simulating when player scores a kill
		if (Input.GetKeyDown (KeyCode.Space)) {
			OnKill ();
		}

		// Test Character Controls (DELETE LATER)
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (-Vector3.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (-Vector3.right * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
		if (Input.GetButtonDown ("Fire1")) {
			Rigidbody clone;
			clone = Instantiate (bullet, bulletSpawn.transform.position, transform.rotation) as Rigidbody;
			clone.velocity = transform.TransformDirection(Vector3.forward * 50);
		}
	}

	// XP SECTION
	void OnKill()
	{
		string player = this.gameObject.name;
		ArrayList data = new ArrayList();

		// store data into an array to be sent to the XPTracker
		data.Add(player);
		data.Add(xp);

		xptracker.SendMessage ("UpdateXP", data);
		kills += 1;
	}

	void OnKillDuck()
	{
		// If this player killed the duck, send info of this player that killed the duck
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

	void AddXP(int _xp)
	{
		xp += _xp;
	}

	void ReceiveLevelUp()
	{
		level += 1;
		// Open up menu to allow the player to select the ability to level up? 
		// GUI.SendMessage ("AbilityLevelUp");
	}
	
	void RestartXPCounter(int _leftOverXP)
	{
		// Left over XP is calculated
		xp = 0;
		xp += _leftOverXP;
	}

	// SUPPORT CLASS SECTION
	void ApplySpeedBoost(int _abilityLevel)
	{
		float speedBoostAmount = defaultSpeed * 0.15f;
		speed = defaultSpeed + speedBoostAmount;
	}

	void DefaultSpeed()
	{
		speed = defaultSpeed;
	}

	void HealthRegen(int _abilityLevel)
	{
		float regenAmount = 0.0f;

		if (_abilityLevel == 1) {
			regenAmount = maxHealth * 0.06f;
		}
		if (_abilityLevel == 2) {
			regenAmount = maxHealth * 0.12f;
		}
		if (_abilityLevel == 3) {
			regenAmount = maxHealth * 0.24f;
		}

		// Is this correct?
		health += regenAmount * Time.deltaTime;

		if (health >= maxHealth) {
			health = maxHealth;
		}
	}

	// TEST DEFENSE SECTION
	void AmmoRegen(float _abilityLevel)
	{
		float regenAmount = 0.0f;
		
		// Check regenAmount based on player ability level
		if (_abilityLevel == 1) {
			regenAmount = maxAmmo * 0.06f;
		}
		if (_abilityLevel == 2) {
			regenAmount = maxAmmo * 0.12f;
		}
		if (_abilityLevel == 3) {
			regenAmount = maxAmmo * 0.24f;
		}
		
		// Is this correct? Ammo reaches insane levels.
		ammo += regenAmount;
		
		if(ammo >= maxAmmo) {
			ammo = maxAmmo;
		}
	}
	
	// TEST OFFENSE SECTION
	void PersonalCamo(float _speed)
	{
		// Turn on "invisible" shader and decrease player speed
		gameObject.renderer.material.shader = Shader.Find ("Camo");
		speed = _speed;
		Debug.Log ("Speed decreased by 6%: " + speed);
	}
	
	void PersonalCamoOff()
	{
		//Turn off "invisible" shader and reset speed to default
		renderer.material.shader = Shader.Find("Diffuse");
		gameObject.renderer.material.SetColor("_Color", Color.blue);
		speed = defaultSpeed;
	}
}