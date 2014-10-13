using UnityEngine;
using System.Collections;

public class Tester : MonoBehaviour 
{
	public float health;
	public float ammo;
	public float speed;
	public int xp;
	public int xPPoints;
	public int kills;
	public int deaths;
	public int level;
	public int maxHealth = 100;
	public int maxAmmo = 100;
	public float defaultSpeed = 5.0f;

	public bool isShooting;
	private XPTracker xptracker;

	RaycastHit checkDuckInfo;

	void Start()
	{
		health = 10;
		ammo = 10; 
		xp = 0;
		xPPoints = 0;
		kills = 0;
		deaths = 0;
		level = 1;
		speed = defaultSpeed;
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
		Debug.Log ("Marking!");
	}
	
	void PulseDamage(float _damage)
	{
		health -= _damage;
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
	void Charge(int _duration) 
	{
		Debug.Log ("Charging!");
	}
	
	// TEST OFFENSE SECTION
	void PersonalCamo()
	{
		renderer.material.shader = Shader.Find ("Camo");
	}
	
	void PersonalCamoOff()
	{
		renderer.material.shader = Shader.Find("Diffuse");
		gameObject.renderer.material.SetColor("_Color", Color.blue);
	}
	
	void ChainShot(string _chainShotSender)
	{
		if (Input.GetButtonDown("Fire1")) {
			GameObject.Find (_chainShotSender).SendMessage ("Off");
		}
	}
}