using UnityEngine;
using System.Collections;

public class grenadeScript : MonoBehaviour {

	public int amountOfGrenades = 0;
	public GameObject grenadeObject;
	public float throwForce = 1000.0f;


	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire1"))
		{
			if ( amountOfGrenades > 0)
			{
				GameObject grenade = Instantiate(grenadeObject, transform.position, Quaternion.identity) as GameObject;

				grenade.rigidbody.AddForce(transform.forward * throwForce);

				amountOfGrenades--;
			}

		}


	}
}
