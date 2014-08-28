using UnityEngine;
using System.Collections;

public class WeaponsManager : MonoBehaviour {
	private int weaponChoice;

	void Awake () 
	{
		weaponChoice = 0;
		//selects the first weapon
		SelectWeapon(weaponChoice);
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKeyDown("1"))
		{
			weaponChoice = 0;
			SelectWeapon(weaponChoice);
		}
		else if(Input.GetKeyDown("2"))
		{
			weaponChoice = 1;
			SelectWeapon(weaponChoice);
		}
		else if(Input.GetKeyDown("3"))
		{
			weaponChoice = 2;
			SelectWeapon(weaponChoice);
		}
		else if(Input.GetKeyDown("4"))
		{
			weaponChoice = 3;
			SelectWeapon(weaponChoice);
		}
		else if(Input.GetKeyDown("5"))
		{
			weaponChoice = 4;
			SelectWeapon(weaponChoice);
		}
		else if(Input.GetKeyDown("6"))
		{
			weaponChoice = 5;
			SelectWeapon(weaponChoice);
		}


		if (Input.GetAxisRaw ("Mouse ScrollWheel") < 0) //scroll back
		{
			weaponChoice += 1;
			if (weaponChoice > 5)
			{
				weaponChoice = 0;
			}
			SelectWeapon (weaponChoice);
		}
		else if(Input.GetAxisRaw ("Mouse ScrollWheel") > 0) //scroll forward
		{
			weaponChoice -= 1;
			if (weaponChoice < 0)
			{
				weaponChoice = 5;
			}
			SelectWeapon (weaponChoice);
		}
		


	}

	int SelectWeapon(int weaponSelected)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			//activate the selected weapon
			if ( i == weaponSelected)
			{
				transform.GetChild(i).gameObject.SetActive(true);
			}
			//deactivate the other weapons
			else
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		return weaponSelected;
	}
}
