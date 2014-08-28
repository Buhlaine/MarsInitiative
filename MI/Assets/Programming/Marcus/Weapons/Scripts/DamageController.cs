using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour {

	public int hitPoints = 100;
	public float deathDelay = 0.0f;
	public Transform explosion;

	int ApplyDamage(int damage)
	{
		//check if object is already dead
		if (hitPoints <= 0)
		{
			return 0;
		}

		hitPoints -= damage;

		if (hitPoints <= 0)
		{
			Destroy(this.gameObject, deathDelay);
		}
		return hitPoints;
	}
}
