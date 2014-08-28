using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnManagerScript : MonoBehaviour 
{
	Dictionary<GameObject, int> allSpawns = new Dictionary<GameObject, int> ();
	GameObject[] spawnPoints;
	List<GameObject> possibleSpawns = new List<GameObject> ();
	public GameObject playerGO;
	GameObject finalSpawnPos;
	int spawnWeight = 1000;

	//Testing stuff
	//int playerNumber = 5;
	
	void Start () 
	{
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

		//Aquiring all spawnPoints in the scene
		spawnPoints = GameObject.FindGameObjectsWithTag ("spawnPoints");

		//Putting spawns in a dictionary to associate spawn with its weight for choosing spawnPoint when spawn is requested
		foreach(GameObject GO in spawnPoints)
		{
			allSpawns.Add(GO, spawnWeight);
		}
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
	}

	void checkSpawn(GameObject _requestor)
	{
		//_requestor.tag = "Blue";

		//grabbing playerDetect script to access it's spawnWeight
		//Might change this to recieve an array to avoid having to access script directly
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
		//used if multiple spawn points have the same weight
		bool multiple = false;
		//int posSpawncount = 0;

		//going through all spawnPoints in array spawnPoints
		foreach(GameObject GO in spawnPoints)
		{
			//if multiple spawnPoints have the same weight
			if(multiple)
			{
				//if current spawnPoint, GO, has a higher spawnWeight
				if(allSpawns[GO] > allSpawns[finalSpawnPos])
				{
					//the finalSpawn is changed to GO
					finalSpawnPos = GO;
					//multiple is set to false
					multiple = false;
					//posSpawnPoints is cleared
					possibleSpawns.Clear();
				}
				//if current spawn has same weight
				else if(allSpawns[GO] == allSpawns[finalSpawnPos])
				{
					//add to list possibleSpawns
					possibleSpawns.Add(GO);
					//posSpawncount++;
				}
			}
			//if multiple is false
			else
			{
				//if finalSpawn isn't set
				if(finalSpawnPos == null)
				{
					//finalSpawn is set to current spawn
					finalSpawnPos = GO;
				}
				//if finalSpawn is set
				else
				{
					//if currentSpawn has same weight as finalSpawn and finalSpawn isn't the same object as currentSpawn
					if(allSpawns[GO] == allSpawns[finalSpawnPos] && finalSpawnPos != GO)
					{
						//set multiple to true
						multiple = true;
						//Adds finalSpawn to possibleSpawns
						possibleSpawns.Add(finalSpawnPos);
						//Adds currentSpawn to possibleSpawns
						possibleSpawns.Add(GO);
					}
					//if currentSpawns spawnWeight is greater
					else if(allSpawns[GO] > allSpawns[finalSpawnPos])
					{
						//finalSpawn is set to currentSpawn
						finalSpawnPos = GO;
					}
				}
			}
		}
//-----------------------------------------------------------------------------------------------------------------------
		//For some reason I didnt think to use foreach
		//Dumb me, fixed it up top. Leaving this part in for now just in case top doesn't work right
//		for(int i = 0; i < spawnPoints.Length; i++)
//		{
//			if(multiple)
//			{
//				if(allSpawns[spawnPoints[i]] > allSpawns[finalSpawnPos])
//				{
//					finalSpawnPos = spawnPoints[i];
//					multiple = false;
//					//posSpawncount = 0;
//				}
//				else if(allSpawns[spawnPoints[i]] == allSpawns[finalSpawnPos])
//				{
//					possibleSpawns.Add(spawnPoints[i]);
//					//posSpawncount++;
//				}
//			}
//			else
//			{
//				if(finalSpawnPos == null)
//				{
//					finalSpawnPos = spawnPoints[i];
//				}
//				else
//				{
//					if(allSpawns[spawnPoints[i]] == allSpawns[finalSpawnPos])
//					{
//						multiple = true;
//						//possibleSpawns = new GameObject[8];
//						possibleSpawns.Add(finalSpawnPos);
//						possibleSpawns.Add(spawnPoints[i]);
//						//posSpawncount = 2;
//					}
//					else if(allSpawns[spawnPoints[i]] > allSpawns[finalSpawnPos])
//					{
//						finalSpawnPos = spawnPoints[i];
//					}
//				}
//			}
//		}
//---------------------------------------------------------------------------------------------------------------------------

		//if multiple is true
		if(multiple)
		{
			//Make sure possibleSpawns count is accurate
			possibleSpawns.TrimExcess();
			//Get a random number to choose spawnPoint randomly
			int randNum = Random.Range(0, possibleSpawns.Count - 1);
			//Stand-in for actual spawning. Need to add check for different classes
			GameObject.Instantiate (playerSpawning, possibleSpawns[randNum].transform.position, Quaternion.identity);
			//Goes through each spawnPoint and request them to resetspawn so weight is accurate next time spawn is requested
			foreach(GameObject GO in spawnPoints)
			{
				GO.SendMessage("resetWeight");
			}
			//Clear possibleSpawns so old spawns aren't included in new calculation
			possibleSpawns.Clear();
			//Make sure multiple is set to false
			multiple = false;
		}
		else
		{
			//Stand-in for actual spawning. Need to add check for different classes
			GameObject.Instantiate (playerSpawning, finalSpawnPos.transform.position, Quaternion.identity);
			//Goes through each spawnPoint and request them to resetspawn so weight is accurate next time spawn is requested
			foreach(GameObject GO in spawnPoints)
			{
				GO.SendMessage("resetWeight");
			}
			//Clear possibleSpawns so old spawns aren't included in new calculation
			possibleSpawns.Clear();
			//Make sure multiple is set to false
			multiple = false;
		}
	}
}
