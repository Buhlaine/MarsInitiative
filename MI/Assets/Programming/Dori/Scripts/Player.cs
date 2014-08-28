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

	private XPTracker xptracker;
	private GameObject ammoObject;
	
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

		// When player logs in, send a message to XPTracker with this players name to be stored into a team list
		ArrayList playerInfo = new ArrayList();
		playerInfo.Add (this.gameObject.name);
		playerInfo.Add (this.gameObject.tag);

		ammoObject = this.gameObject.transform.FindChild ("AmmoRegen").gameObject;

		xptracker.SendMessage ("StorePlayer", playerInfo);
	}
	
	void Update()
	{
		// Simulating when player scores a kill
		if (Input.GetKeyDown (KeyCode.Space)) {
			OnKill ();
		}

		// Test Character Controls
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
	}
	
	void RestartXPCounter(int _leftOverXP)
	{
		xp = 0;
		xp += _leftOverXP;
	}

	// SUPPORT CLASS SECTION
	void ApplySpeedBoost(float _speedBoostAmount)
	{
		speed = _speedBoostAmount + defaultSpeed;
	}

	void DefaultSpeed()
	{
		speed = defaultSpeed;
	}

	void HealthRegen(float _regenAmount)
	{
		health += _regenAmount * Time.deltaTime;

		if (health >= maxHealth) 
		{
			health = maxHealth;
		}
	}

	// TEST DEFENSE SECTION
	void AmmoRegen(float _amount)
	{
		ammo += _amount;
	}

	void CheckRegenAmount(int _abilityLevel)
	{
		float regenAmount = 0.0f;

		if (_abilityLevel == 1) {
			regenAmount = maxAmmo * 0.06f;
		}
		if (_abilityLevel == 2) {
			regenAmount = maxAmmo * 0.12f;
		}
		if (_abilityLevel == 3) {
			regenAmount = maxAmmo * 0.24f;
		}

		ammoObject.SendMessage ("RecieveRegenAmount", regenAmount);
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