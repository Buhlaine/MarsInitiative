using UnityEngine;
using System.Collections;

public class DemoTeammate : MonoBehaviour {
	
	public int ammo;
	public int xp;
	public int kills;
	public int deaths;
	public int level;
	public int maxHealth = 100;

	public float health;
	public float speed;
	public float defaultSpeed = 5.0f;

	void Start()
	{
		health = 25;
		ammo = 0;
		xp = 0;
		kills = 0;
		deaths = 0;
		level = 1;
		speed = defaultSpeed;
	}

	// TEST SUPPORT SECTION
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
	void AmmoRegen(int _amount)
	{
		Debug.Log ("Ammo Amount: " + _amount);
		ammo += _amount;
	}
	
	void ActivateShield()
	{
		gameObject.layer = LayerMask.NameToLayer ("Immune");
	}

	void DeactivateShield()
	{
		gameObject.layer = LayerMask.NameToLayer ("Default");
	}

	// TEST OFFENSE SECTION
	void PersonalCamo(float _speed)
	{
		// Check with Marcus about this
		gameObject.renderer.material.shader = Shader.Find ("Camo");
		speed = _speed;
		Debug.Log ("Speed decreased by 6%: " + speed);
	}
	
	void PersonalCamoOff()
	{
		// Check with Marcus about this
		renderer.material.shader = Shader.Find("Diffuse");
		gameObject.renderer.material.SetColor("_Color", Color.blue);
		speed = defaultSpeed;
	}

	void MarkEnemy()
	{
		// Send enemy coordinates to GUI mini map of teammates 
		// Check with Ramone
	}
}
