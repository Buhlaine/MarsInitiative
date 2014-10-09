using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnManagerScript : MonoBehaviour 
{
	Dictionary<GameObject, int> allSpawns = new Dictionary<GameObject, int> ();
	List<GameObject> blueStart = new List<GameObject>();
	List<GameObject> redStart = new List<GameObject>();
	GameObject[] spawnPoints;
	List<GameObject> possibleSpawns = new List<GameObject> ();
	GameObject finalSpawnPos;
	int spawnWeight = 1000;


	//testing----------------------------------------------
	GameObject thisGO;
	//----------------------------------------------------
	public GameObject scout;
	public GameObject heavy;
	public GameObject trooper;
	bool gameBegin = false;

	//Testing stuff
	//int playerNumber = 5;
	
	void Start () 
	{
		GameObject[] allStartSpawn = GameObject.FindGameObjectsWithTag ("startSpawns");

		foreach(GameObject spawnStart in allStartSpawn)
		{
			if(spawnStart.gameObject.name.Contains("Blue") || spawnStart.gameObject.name.Contains("blue"))
			{
				blueStart.Add(spawnStart);
			}
			else if(spawnStart.gameObject.name.Contains("Red") || spawnStart.gameObject.name.Contains("red"))
			{
				redStart.Add(spawnStart);
			}
		}

		blueStart.TrimExcess ();
		redStart.TrimExcess ();

		//Aquiring all spawnPoints in the scene
		spawnPoints = GameObject.FindGameObjectsWithTag ("spawnPoint");

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
			for(int i = 0; i < 8; i++)
			{
				Debug.Log(i);
				checkSpawn(thisGO);
			}
		}
		if(Input.GetKeyDown(KeyCode.P))
		{
			checkSpawn(thisGO);
		}
		if(Input.GetKeyDown(KeyCode.G))
		{
			setGameBegin();
		}
		if(Input.GetKeyDown(KeyCode.H))
		{
			Debug.Log(gameBegin);
		}
	}

	void checkSpawn(GameObject _requestor)
	{
		//Debug.Log (gameBegin);
		if(gameBegin)
		{
			//Debug.Log("--------------ljksdhfjhsdfjkhdflkjslkjf");
			//_requestor.tag = "Red";
			//grabbing playerDetect script to access it's spawnWeight
			//Might change this to recieve an array to avoid having to access script directly
			playerDetect spawnWeight;
			//iterating through all spawns
			foreach(GameObject spawnP in spawnPoints)
			{
				//requesting all spawnPoints to calculate their spawnWeights
				spawnP.SendMessage("calcSpawnWeight", "Red"/*_requestor.tag*/);
				//accessing their script to get spawn weight
				spawnWeight = spawnP.GetComponent<playerDetect>();
				//setting their spawnWeight in the Dictionary
				allSpawns[spawnP] = spawnWeight.getSpawnWeight();
			}
		}
		//running spawn
		spawn (_requestor);
	}

	void spawn(GameObject playerSpawning)
	{
		//For final-------------------------------------------------------------------
		//move playerScript = playerSpawning.GetComponent<move> ();
		//string playerClass = playerScript.playerClass;
		//----------------------------------------------------------------------------

		//for testing---------------------------------------------------------------
		string tag = "Red";
		string playerClass = "scout";
		//-----------------------------------------------------------------------------
		if(!gameBegin)
		{
			//if(playerSpawning.tag == "Blue")
			//testing-------------------------------------------
			if(tag == "Blue")
			{
				if(playerClass == "scout")
				{
					GameObject.Instantiate (scout, blueStart[0].transform.position, Quaternion.identity);
				}
				else if(playerClass == "trooper")
				{
					GameObject.Instantiate (trooper, blueStart[0].transform.position, Quaternion.identity);
				}
				else if(playerClass == "heavy")
				{
					GameObject.Instantiate (heavy, blueStart[0].transform.position, Quaternion.identity);
				}
				blueStart.RemoveAt(0);
			}
			//else if(playerSpawning.tag == "Red")
			//testing------------------------------------
			else if(tag == "Red")
			{
				if(playerClass == "scout")
				{
					GameObject.Instantiate (scout, redStart[0].transform.position, Quaternion.identity);
				}
				else if(playerClass == "trooper")
				{
					GameObject.Instantiate (trooper, redStart[0].transform.position, Quaternion.identity);
				}
				else if(playerClass == "heavy")
				{
					GameObject.Instantiate (heavy, redStart[0].transform.position, Quaternion.identity);
				}

				redStart.RemoveAt(0);
			}
		}
		else if(gameBegin)
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
				if(playerClass == "scout")
					GameObject.Instantiate (scout, possibleSpawns[randNum].transform.position, Quaternion.identity);
				else if(playerClass == "trooper")
					GameObject.Instantiate (trooper, possibleSpawns[randNum].transform.position, Quaternion.identity);
				else if(playerClass == "heavy")
					GameObject.Instantiate (heavy, possibleSpawns[randNum].transform.position, Quaternion.identity);
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
				if(playerClass == "scout")
					GameObject.Instantiate (scout, finalSpawnPos.transform.position, Quaternion.identity);
				else if(playerClass == "trooper")
					GameObject.Instantiate (trooper, finalSpawnPos.transform.position, Quaternion.identity);
				else if(playerClass == "heavy")
					GameObject.Instantiate (heavy, finalSpawnPos.transform.position, Quaternion.identity);

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

	void setGameBegin()
	{
		//Debug.Log ("++++++++++++++++++++++++++ljkhsdfkhsdfojfdjsoidjoifjds");
		gameBegin = !gameBegin;
	}
}
