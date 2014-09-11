using UnityEngine;
using System.Collections;

public class playerDetect : MonoBehaviour {
	Hashtable playersInZone;
	int spawnWeight = 1000;
	
	void Start () 
	{
		playersInZone = new Hashtable ();
	}

	void Update () 
	{
//Testing---------------------------------------------------------------------------------
//		if(Input.GetKeyDown(KeyCode.P))
//		{
//			foreach(DictionaryEntry entry in playersInZone)
//			{
//				Debug.Log(entry.Key);
//				Debug.Log("--------------------");
//			}
//			if(playersInZone.Count == 0)
//			{
//				Debug.Log("++++++++++++++++++++++++");
//			}
//		}
//		if(Input.GetKeyDown(KeyCode.Y))
//		{
//			this.calcSpawnWeight("Blue");
//			Debug.Log(spawnWeight);
//			this.resetWeight();
//		}
//		if(Input.GetKeyDown(KeyCode.M))
//		{
//			foreach(DictionaryEntry units in playersInZone)
//			{
//				Debug.Log(units.Value.ToString());
//			}
//		}
//--------------------------------------------------------------------------------------------------
	}


	void OnTriggerEnter(Collider playerInRange)
	{
		//If dictionary doesn't already contain the player
		if(!playersInZone.ContainsKey(playerInRange))
		{
			//Add player to dictionary and keep track of it's teamColor
			//Might have to add a check to see if entering object is actually a player
			playersInZone.Add(playerInRange.gameObject, playerInRange.tag);
		}
	}

	void OnTriggerExit(Collider playerOutRange)
	{
		//If dictionary has an entry for this player
		if(playersInZone.ContainsKey(playerOutRange.gameObject))
		{
			//Remove from dictionary so it doesn't affect spawnWeight Calc
			playersInZone.Remove(playerOutRange.gameObject);
		}
	}

	//Calculating spawnWeight
	void calcSpawnWeight(string teamColor)
	{
		//Go through each entry in the dictionary
		foreach(DictionaryEntry units in playersInZone)
		{
			//if the team color doesn't match the team color of the player requesting spawn
			if(units.Value.ToString() != teamColor)
			{
				//subtract 50 from spawnWeight
				spawnWeight -= 50;
			}
			//if team color matches team color of player requesting spawn
			else if(units.Value.ToString() == teamColor)
			{
				//Add 100 to spawnWeight
				spawnWeight += 100;
			}
		}
	}

	//Using in spawnManager script to set spawnWeight in dictionary
	//Might change, check spawnManager script comment
	public int getSpawnWeight()
	{
		return spawnWeight;
	}

	//Reset spawnWeight so next calculation is accurate
	void resetWeight()
	{
		spawnWeight = 1000;
	}
	//Use to remove a player from the hashtable so spawnWeight calc is accurate
	//If not used when player dies, spawnWeigt WILL NOT BE ACCURATE
	public void removeHash(GameObject _requestor)
	{
		if(playersInZone.ContainsKey(_requestor))
		{
			playersInZone.Remove(_requestor);
		}
	}
}
