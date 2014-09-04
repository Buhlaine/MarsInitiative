using UnityEngine;
using System.Collections;

public class dotGrenadeDmgScript : MonoBehaviour //Put this on the actual explosion particle for the dot grenade
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

	private bool isDamaging;
	private float creationTime;
	private float dotDuration = 0.0f;
	private Vector3 targetPosition;  //grabs where the grenade target is in the grenade blast
	private float targetDistance;    //gets the distance of the target from the center of the blast
	private float proximity;
	
	
	
	void Awake () 
	{
		creationTime = Time.time;
		dotDuration = creationTime;
		isDamaging = true;
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

				if (isDot)
				{
					StartCoroutine(dotDmg(dotTic,hit));

				}
			}
		}
		
	}
	
	
	IEnumerator dotDmg(float seconds, Collider hit)
	{
		//for (dotDuration = creationTime; dotDuration <= (dotDmgTime+creationTime); dotDuration += seconds)
		//{ 
			Debug.Log ("coroutine" + seconds);
			Debug.Log(dotDuration);
			if(isDamaging)
			{
				hit.collider.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
				hit.collider.SendMessageUpwards("slowField", speedReduction, SendMessageOptions.DontRequireReceiver);
				isDamaging = false;
				yield return new WaitForSeconds (seconds);
				isDamaging = true;
			}
			
		//}
		
	}
	
}