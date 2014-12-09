using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerDetect : MonoBehaviour {

	Hashtable playersInZone;
	int spawnWeight = 1000;
	public float distanceAmount = 20.0f;
	int weightOffset = 20;
	int numOffset = 0;

	//Close spawnPoints
	List<GameObject> closeSpawns = new List<GameObject>();
	
	void Start () 
	{
		playersInZone = new Hashtable ();

		//Finding other spawnPoints within a certain radius
		GameObject[] allSpawns = GameObject.FindGameObjectsWithTag ("spawnPoint");
		
		foreach(GameObject spawn in allSpawns)
		{
			if(spawn != this.gameObject)
			{
				if((spawn.transform.position - this.transform.position).magnitude < distanceAmount)
				{
					closeSpawns.Add(spawn);
				}
			}
		}

		//closeSpawnPrintTest ();
	}
	//Testing------------------------------------------------------
	void spawnWeightPrint()
	{
		Debug.Log (this.name + ": " + spawnWeight);
	}

	void closeSpawnPrintTest()
	{
		Debug.Log ("++++++++++++++++++++++++++++++++++++");
		Debug.Log (this.name + ": ");
		foreach(GameObject spawn in closeSpawns)
		{
			Debug.Log(spawn.name);
		}
		Debug.Log ("------------------------------------");
	}
	//-------------------------------------------------------------

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
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    this.calcSpawnWeight("Red");
        //    spawnWeightPrint();
        //    this.resetWeight();
        //}
//		if(Input.GetKeyDown(KeyCode.M))
//		{
//			foreach(DictionaryEntry units in playersInZone)
//			{
//				Debug.Log(units.Value.ToString());
//			}
//		}
//--------------------------------------------------------------------------------------------------
	}

	void setNumOffset(int offset)
	{
		numOffset += offset;
	}

	void resetOffset()
	{
		numOffset = 0;
	}

	void OnTriggerEnter(Collider playerInRange)
	{
		//If dictionary doesn't already contain the player
		if(!playersInZone.ContainsKey(playerInRange))
		{
			//Add player to dictionary and keep track of it's teamColor
			//Might have to add a check to see if entering object is actually a player
			playersInZone.Add(playerInRange.gameObject, playerInRange.GetComponent<Player>().team);
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
		int offsetNum = 0;
		//Go through each entry in the dictionary
		foreach(DictionaryEntry units in playersInZone)
		{
			//if the team color doesn't match the team color of the player requesting spawn
			if(units.Value.ToString() != teamColor)
			{
				//subtract 50 from spawnWeight
				spawnWeight -= 50;
				offsetNum++;
			}
			//if team color matches team color of player requesting spawn
			else if(units.Value.ToString() == teamColor)
			{
				//Add 100 to spawnWeight
				spawnWeight += 100;
			}
		}

		if(offsetNum > 0)
		{
			foreach(GameObject spawn in closeSpawns)
			{
				spawn.SendMessage("setNumOffset", offsetNum);
			}
		}

		Debug.Log (this.name + ": " + offsetNum);

		spawnWeight -= (numOffset * weightOffset);

		Debug.LogWarning (this.name + " final offset amount: " + (numOffset * weightOffset));

		resetOffset ();
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
