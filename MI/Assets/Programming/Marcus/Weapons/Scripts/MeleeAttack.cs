using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour 
{
	public float[] meleeDamage;
	public float punchForce;
	private int statsLevel = 0;              //regulates variables based on stat level of character



	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(2))
		{
			attack();
		}
	}

	void attack()
	{
		//anim controller to send make the player swing or fist bump

		//if audio is attached
		if (audio)
		{
			if (!audio.isPlaying)
			{
				audio.Play();
				audio.loop = true;
			}
			else
			{
				audio.loop =false;
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		//see of the collider hits something and apply damage
		other.collider.SendMessageUpwards("ApplyDamage", meleeDamage[statsLevel], SendMessageOptions.DontRequireReceiver);

		if(other.rigidbody)
		{
			other.rigidbody.AddForce(Vector3.forward*punchForce);
		}
	}

	void StatsLevelup()
	{
		statsLevel++;
	}



}
