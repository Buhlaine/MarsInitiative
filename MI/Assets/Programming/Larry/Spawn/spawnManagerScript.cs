using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnManagerScript : MonoBehaviour 
{
	Dictionary<GameObject, int> allSpawns = new Dictionary<GameObject, int> ();
	//GameObject[] spawnPoints;
	ArrayList spawnPoints;
	ArrayList possibleSpawns;
	public GameObject playerGO;
	GameObject finalSpawnPos;
	//int spawnWeight = 1000;

	//Testing stuff
	//int playerNumber = 5;
	
	void Start () 
	{
		spawnPoints = new ArrayList ();
		GameObject[] spawnPoints1 = GameObject.FindGameObjectsWithTag ("spawnPoints");

		foreach(GameObject GO in spawnPoints1)
		{
			spawnPoints.Add(GO);
		}
		possibleSpawns = new ArrayList ();
		//Testing start spawn----------------------------------------------------------------
//		GameObject startSpawn = GameObject.FindGameObjectWithTag ("startSpawn");
//		bool spawned = false;
//		Vector2 startSpawns = new Vector2();
//		for(int i = 0; i<playerNumber; i++)
//		{
//			while(!spawned)
//			{
//				startSpawn = Random.insideUnitCircle * 20;
//			}
//		}
		//------------------------------------------------------------------------------------

//		spawnPoints = GameObject.FindGameObjectsWithTag ("spawnPoints");
//		foreach(GameObject GO in spawnPoints)
//		{
//			allSpawns.Add(GO, spawnWeight);
//		}
	}

	void Update () 
	{
		//Testing Purposes
		//Remove input when in game use
		//Player request to spawn
		if(Input.GetKeyDown(KeyCode.Q))
		{
			checkSpawn(playerGO);
		}
		if(Input.GetKeyDown(KeyCode.I))
		{
			Debug.Log(allSpawns.Count);
			spawnPoints.TrimToSize();
			Debug.Log(spawnPoints.Count);
		}
		if(Input.GetKeyDown(KeyCode.H))
		{
			foreach(GameObject GO in spawnPoints)
			{
				Debug.Log(GO.name);
			}
		}
		if(Input.GetKeyDown(KeyCode.C))
		{
			Debug.Log("-------------------------------------------------------------");
		}
	}

	void checkSpawn(GameObject _requestor)
	{
		//_requestor.tag = "Blue";

		//grabbing playerDetect script to access it's spawnWeight
		playerDetect spawnWeight;
		//iterating through all spawns
		foreach(GameObject spawnP in spawnPoints)
		{
			//requesting all spawnPoints to calculate their spawnWeights
			spawnP.SendMessage("calcSpawnWeight", _requestor.tag);
			//accessing their script to get spawn weight
			spawnWeight = spawnP.GetComponent<playerDetect>();
			//setting their spawnWeight in the Dictionary
			allSpawns[spawnP] = spawnWeight.getSpawnWeight();
		}
		//running spawn
		spawn (_requestor);
	}

	void spawn(GameObject playerSpawning)
	{
		bool multiple = false;
		//int posSpawncount = 0;
		spawnPoints.TrimToSize ();
		for(int i = 0; i < spawnPoints.Count; i++)
		{
			if(multiple)
			{
				if(allSpawns[spawnPoints[i] as GameObject] > allSpawns[finalSpawnPos])
				{
					finalSpawnPos = spawnPoints[i] as GameObject;
					multiple = false;
					//posSpawncount = 0;
				}
				else if(allSpawns[spawnPoints[i] as GameObject] == allSpawns[finalSpawnPos])
				{
					possibleSpawns.Add(spawnPoints[i] as GameObject);
					//posSpawncount++;
				}
			}
			else
			{
				if(finalSpawnPos == null)
				{
					finalSpawnPos = spawnPoints[i] as GameObject;
				}
				else
				{
					if(allSpawns[spawnPoints[i] as GameObject] == allSpawns[finalSpawnPos])
					{
						multiple = true;
						//possibleSpawns = new GameObject[8];
						possibleSpawns.Add(finalSpawnPos);
						possibleSpawns.Add(spawnPoints[i] as GameObject);
						//posSpawncount = 2;
					}
					else if(allSpawns[spawnPoints[i] as GameObject] > allSpawns[finalSpawnPos])
					{
						finalSpawnPos = spawnPoints[i] as GameObject;
					}
				}
			}
		}

		if(multiple)
		{
			possibleSpawns.TrimToSize();
			int randNum = Random.Range(0, possibleSpawns.Count - 1);
			GameObject.Instantiate (playerSpawning, (possibleSpawns[randNum] as GameObject).transform.position, Quaternion.identity);
			foreach(GameObject GO in spawnPoints)
			{
				GO.SendMessage("resetWeight");
			}
			possibleSpawns.Clear();
		}
		else
		{
			GameObject.Instantiate (playerSpawning, finalSpawnPos.transform.position, Quaternion.identity);
			foreach(GameObject GO in spawnPoints)
			{
				GO.SendMessage("resetWeight");
			}
		}
	}
}
