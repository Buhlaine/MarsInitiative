using UnityEngine;
using System.Collections;

public class DuckScript : MonoBehaviour 
{
	public float health = 100.0f;
	public float speed = 7.5f;

	private Player player;

	void Start()
	{

	}

	void Update()
	{
		if(health <= 0.0f) {
			Destroy(gameObject);
		}
	}


}