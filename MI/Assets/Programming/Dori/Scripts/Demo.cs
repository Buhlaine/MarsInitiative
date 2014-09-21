using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {

	public float ammo;
	public int xp;
	public int xPPoints;
	public int kills;
	public int deaths;
	public int level;
	public int maxHealth = 100;
	public int maxAmmo = 100;
	
	public float health;
	public float speed;
	public float defaultSpeed = 5.0f;
	
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
