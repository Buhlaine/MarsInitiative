using UnityEngine;
using System.Collections;

public class grenadeExplosionScript : MonoBehaviour 
{
	
	public Transform grenadeExplosion;
	public float explosionDelay = 3.0f;
	public float explosionRadius = 5.0f;
	public float force = 10.0f;
	public float explosiveLift = 1.0f;
	public float damage = 100.0f;
	public float speedReduction;  //the amount a player is slowed in the dot field
	public bool isDot = false;
	public bool isConcussion = false;
	public float dotDmgTime = 0.0f;
	public float dotTic = 0.1f; //the amount of time in seconds for every damage tic
	
	private float creationTime;
	private float dotDuration = 0.0f;
	private Vector3 targetPosition;  //grabs where the grenade target is in the grenade blast
	private float targetDistance;    //gets the distance of the target from the center of the blast
	private float proximity;



	void Awake () 
	{
		creationTime = Time.time;
		dotDuration = creationTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time > (creationTime + explosionDelay))
		{
			doDmg();
		}

	}
	


	void doDmg()
	{
		Vector3 grenadePosition = transform.position;
		Collider[] colliders = Physics.OverlapSphere(grenadePosition,explosionRadius);
		
		
		foreach(Collider hit in colliders)
		{
			if (hit.rigidbody)
			{
				//calculating the damage on a hit
				targetPosition = hit.rigidbody.position;
				targetDistance = (grenadePosition - targetPosition).magnitude;
				proximity = 1 - (targetDistance/explosionRadius);
				if (isConcussion)
				{
					damage *= proximity;
				}
				
				
				hit.rigidbody.AddExplosionForce(force, grenadePosition, explosionRadius, explosiveLift);
				hit.collider.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
				if (isDot)
				{
					//for(dotDuration = creationTime;dotDuration <= (dotDmgTime+creationTime);dotDuration += Time.deltaTime) //while (dotDuration <= (dotDmgTime+creationTime))
					//{
						//method 1
						StartCoroutine(dotDmg(dotTic,hit));
						//hit.collider.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
						//hit.collider.SendMessageUpwards("slowField", speedReduction, SendMessageOptions.DontRequireReceiver);


						//method 2
						//if(dotTic > 0)
						//{
						//	dotTic -= Time.deltaTime;
						//}
						//else if(dotTic <= 0)
						//{
						//	hit.collider.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
						//	hit.collider.SendMessageUpwards("slowField", speedReduction, SendMessageOptions.DontRequireReceiver);
						//	dotTic = dotTicSet;
						//	//StartCoroutine(dotDmg(dotTicSet));
						//}
						//dotDuration += Time.deltaTime;
					//}
					
				}
			}
		}
		//put these in another script to delete the grenade and instatiate the explosion seems to be messing with the coroutine
		Instantiate(grenadeExplosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
		
	}


	IEnumerator dotDmg(float seconds, Collider hit)
	{
		for (dotDuration = creationTime; dotDuration <= (dotDmgTime+creationTime); dotDuration += seconds)
		{ 
			Debug.Log ("coroutine" + seconds);
			Debug.Log(dotDuration);
			hit.collider.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
			hit.collider.SendMessageUpwards("slowField", speedReduction, SendMessageOptions.DontRequireReceiver);
			yield return new WaitForSeconds (seconds);

		}

	}

}


