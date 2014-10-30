using UnityEngine;
using System.Collections;

public class DuckScript : MonoBehaviour 
{
	public float health;
	public float speed;
	public string duckKiller;

	void Start()
	{
		this.gameObject.tag = "Duck";
		this.gameObject.name = "Duck";

		health = 100.0f;
		speed = 8.0f;
		duckKiller = null;
	}

	void Update()
	{
		if(health <= 0.0f) {
			Debug.Log (duckKiller + " has killed duck!");
			GameObject dk = GameObject.Find (duckKiller);
			dk.SendMessage ("OnKillDuck"); // should send a message to the duck killer
			Destroy(gameObject);
		}
	}

	void Damage(float damage)
	{
		health -= damage;
	}

	void WhoKillMe(string name)
	{
		duckKiller = GameObject.Find (name).name;
	}
}