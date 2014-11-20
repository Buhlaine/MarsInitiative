using UnityEngine;
using System.Collections;

public class DuckScript : MonoBehaviour 
{
	public bool isAlive;

	void Start()
	{
		this.gameObject.tag = "Duck";
		this.gameObject.name = "Duck";

		isAlive = true;
	}

	void KillDuck(string name)
	{
		Debug.Log (name + " has killed duck!");

		isAlive = false;

		GameObject.Find (name).SendMessage ("OnKillDuck");
		Destroy(this.gameObject);
	}
}