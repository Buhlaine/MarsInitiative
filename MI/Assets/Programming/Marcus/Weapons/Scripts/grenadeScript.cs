using UnityEngine;
using System.Collections;

public class grenadeScript : MonoBehaviour {

	public int[] amountOfGrenades = {0};
	public GameObject grenadeObject;
	public float throwForce = 1000.0f;       //amount of force the player thorws the grenade

	private int statsLevel = 0;              //regulates variables based on stat level of character

	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire3"))
		{
			if ( amountOfGrenades[statsLevel] > 0)
			{
				GameObject grenade = Instantiate(grenadeObject, transform.position, Quaternion.identity) as GameObject;

				grenade.rigidbody.AddForce(transform.forward * throwForce);

				amountOfGrenades[statsLevel]--;
			}

		}


	}

	void StatsLevelup()
	{
		statsLevel++;
	}

}
