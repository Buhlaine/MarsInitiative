using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour {

	public int hitPoints = 100;
	public float deathDelay = 0.0f;
	public Transform explosion;


	float dotTic = 0.5f;
	float dotDuration = 5.0f;
	int dotDmg = 1;
	private bool isDamaging;

	void Start()
	{
		isDamaging = true;
	}
	

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

	void ApplyDotDamage(float creationTime)
	{
		//for (float i = creationTime; i <= (dotDuration+creationTime); i += Time.deltaTime)
		//{
		StartCoroutine(doDotDmg(creationTime, dotTic));
		//}
	}

	IEnumerator doDotDmg(float creationTime, float seconds)
	{
		Debug.Log ("coroutine" + seconds);
		Debug.Log(dotDuration);
		for (float i = creationTime; i <= (dotDuration+creationTime); i += Time.deltaTime)
		{
			if(isDamaging)
			{
				hitPoints -= dotDmg;
				isDamaging = false;
				yield return new WaitForSeconds (seconds);
				isDamaging = true;
			}
		}

		
	}
}
