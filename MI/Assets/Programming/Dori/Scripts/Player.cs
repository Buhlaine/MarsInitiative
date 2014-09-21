using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float ammo;
	public int xp;
	public int xPPoints;
	public int kills;
	public int deaths;
	public int level;
	public int maxHealth = 100;
	public int maxAmmo = 100;

	public string abilityOne;
	public string abilityTwo;

	public float health;
	public float speed;
	public float defaultSpeed = 5.0f;

	public bool isShooting;
	private XPTracker xptracker;

	RaycastHit checkDuckInfo;

	void Awake()
	{
		this.gameObject.tag = "Player";
	}

	void Start()
	{
		health = 100;
		ammo = 10; 
		xp = 0;
		xPPoints = 0;
		kills = 0;
		deaths = 0;
		level = 1;
		speed = defaultSpeed;

		xptracker = GameObject.FindGameObjectWithTag ("XPTracker").GetComponent<XPTracker> ();

		// When player logs in, send a message to XPTracker with this players name to be stored into a team list
		ArrayList playerInfo = new ArrayList();
		playerInfo.Add (this.gameObject.name);
		playerInfo.Add (this.gameObject.tag);

		xptracker.SendMessage ("StorePlayer", playerInfo);
	}
	
	void Update()
	{
		// Simulating when player scores a kill
		if (Input.GetKeyDown (KeyCode.J)) {
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

		// Shoot a raycast at area in front of player. If object == duck than check if health == 0. If duck health == 0, then this player has 
		// killed the duck and deserves that sweet XP.
		Vector3 forwardRay = transform.TransformDirection (Vector3.forward);
		if (Physics.Raycast(transform.position, forwardRay, out checkDuckInfo, 10.0f)) { // last parameter should be actual weapon's range. 10 for testing.
			if(checkDuckInfo.transform.name == "Duck" && isShooting) {
				checkDuckInfo.transform.SendMessage("WhoKillMe", this.gameObject.name); // Apply damage to the duck, as well as the name of this player. 
				checkDuckInfo.transform.gameObject.SendMessage("Damage", 10.0f); // Should be actual weapon's damage. 10 for testing.

				// the name of the player will be saved by the duck as the current playe doing damage to the duck. If the ducks health reaches 0 while a name is saved, then
				// that player will be rewarded with sweet sweet points.
			}
		}
	}

	void AbilityOne(string _nameOfActiveAbility)
	{
		abilityOne = _nameOfActiveAbility;
	}

	void AbilityTwo(string _nameOfActiveAbility)
	{
		abilityTwo = _nameOfActiveAbility;
	}

	// XP SECTION
	void OnKill()
	{
		Debug.Log ("OnKill");
		string player = this.gameObject.name;
		ArrayList data = new ArrayList();

		// store data into an array to be sent to the XPTracker
		data.Add(player);
		data.Add(xp);
		data.Add (abilityOne);
		data.Add (abilityTwo);

		xptracker.SendMessage ("UpdateXP", data);
		kills += 1;
	}

	void OnKillDuck()
	{
		// If this player killed the duck - send info of this player that killed the duck
		string player = this.gameObject.name;
		ArrayList data = new ArrayList ();

		data.Add (player);
		data.Add (xp);
		data.Add (abilityOne);
		data.Add (abilityTwo);

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
		xPPoints += 1;

		int levelUpEvent = 1; // might change depending...

		ArrayList data = new ArrayList();

		data.Add (gameObject.name);
		data.Add (levelUpEvent);

		gameObject.SendMessage ("UpperCornerEvent", data);
	}

	// Golden Functions HERE
	void AbilityOneLevelUp()
	{
		// Sending a level up to ability one
		gameObject.transform.Find (abilityOne).SendMessage ("Changed");
	}

	void AbilityTwoLevelUp()
	{
		// Sending a level up to ability two
		gameObject.transform.Find (abilityTwo).SendMessage ("Changed");
	}

	void StatsLevelUp()
	{
		// Sending a level up for stats 
		gameObject.SendMessage ("Changed");
	}

	void Changed()
	{
		// stat level ups
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
		if (_onoroff == true) {

		}

		if (_onoroff == false) {

		}
	}
	
	void PulseDamage(float _damage)
	{
		health -= _damage; // TODO NOT WORKING (PULSE DAMAGE)
	}

	void Restock(int _regenIndex)
	{
		if (health >= maxHealth) {
			health = maxHealth;
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
	void Charge(bool _onoroff) 
	{
		if (_onoroff == true) {

		}

		if (_onoroff == false) {

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

	void ChainShot(string _chainShotSender)
	{
		if (Input.GetButtonDown("Fire1")) {
			GameObject.Find (_chainShotSender).SendMessage ("Off");
		}
	}
}