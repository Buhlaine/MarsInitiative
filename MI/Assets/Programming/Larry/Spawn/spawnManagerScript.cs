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
	public float spawnWaitTime = 5.0f;
	bool check = false;

	/*----------------------------------------------------

	make sure spawn happens once. spawning twice

	----------------------------------------------------*/
	//testing----------------------------------------------
	GameObject thisGO;
	//----------------------------------------------------
	public GameObject assassin;
	public GameObject enforcer;
	public GameObject trooper;
	bool gameBegin = true;
	GameObject testObj;

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
//		if(Input.GetKeyDown(KeyCode.Y))
//		{
//			for(int i = 0; i < 8; i++)
//			{
//				Debug.Log(i);
//				//checkSpawn(thisGO);
//			}
//		}
//		if(Input.GetKeyDown(KeyCode.H))
//		{
//			spawnRequest(thisGO);
//		}
//		if(Input.GetKeyDown(KeyCode.J))
//		{
//			networkView.RPC("setGameBegin", RPCMode.All);
//			if(networkView.isMine)
//				check = true;
//		}
//		if(Input.GetKeyDown(KeyCode.K))
//		{
//			Debug.Log(gameBegin);
//		}
	}
	
	IEnumerator spawnDelay(float delayTime, GameObject thisGO)
	{
		Debug.Log ("Spawn Start time: " + Time.time);
		yield return new WaitForSeconds(delayTime);
		checkSpawn(thisGO);
	}


	void spawnRequest (GameObject thisGO)
	{
		Debug.Log ("This Worked!!!---+++");
		StartCoroutine (spawnDelay(spawnWaitTime, thisGO));
	}


	void checkSpawn(GameObject thisGO)
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
		spawn (thisGO);
	}
	
	void spawn(GameObject thisGO)
	{
		//For final-------------------------------------------------------------------
		//move playerScript = playerSpawning.GetComponent<move> ();
		//string playerClass = playerScript.playerClass;
		//----------------------------------------------------------------------------
		testObj = thisGO;
		//for testing---------------------------------------------------------------
		string tag = "Red";
		string playerClass = "assassin";
		//-----------------------------------------------------------------------------
		if(!gameBegin)
		{
			//if(playerSpawning.tag == "Blue")
			//testing-------------------------------------------
			if(tag == "Blue")
			{
				if(playerClass == "assassin")
				{
					GameObject.Instantiate (assassin, blueStart[0].transform.position, Quaternion.identity);
				}
				else if(playerClass == "trooper")
				{
					GameObject.Instantiate (trooper, blueStart[0].transform.position, Quaternion.identity);
				}
				else if(playerClass == "enforcer")
				{
					GameObject.Instantiate (enforcer, blueStart[0].transform.position, Quaternion.identity);
				}
				blueStart.RemoveAt(0);
			}
			//else if(playerSpawning.tag == "Red")
			//testing------------------------------------
			else if(tag == "Red")
			{
				if(playerClass == "assassin")
				{
					GameObject.Instantiate (assassin, redStart[0].transform.position, Quaternion.identity);
				}
				else if(playerClass == "trooper")
				{
					GameObject.Instantiate (trooper, redStart[0].transform.position, Quaternion.identity);
				}
				else if(playerClass == "enforcer")
				{
					GameObject.Instantiate (enforcer, redStart[0].transform.position, Quaternion.identity);
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

			//if multiple is true
			if(multiple)
			{
				//Make sure possibleSpawns count is accurate
				possibleSpawns.TrimExcess();
				//Get a random number to choose spawnPoint randomly
				int randNum = Random.Range(0, possibleSpawns.Count - 1);

				//Stand-in for actual spawning. Need to add check for different classes
				//networkView.RPC("spawnPlayer", RPCMode.All, possibleSpawns[randNum].transform.position);
				spawnPlayer(thisGO, possibleSpawns[randNum].transform.position);

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
				Debug.Log(thisGO);
				//networkView.RPC("spawnPlayer", RPCMode.All, finalSpawnPos.transform.position);
				spawnPlayer(thisGO, finalSpawnPos.transform.position);
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
			Debug.Log("Character Spawned: " + Time.time);
		}
	}

	[RPC]
	void setGameBegin()
	{
		//Debug.Log ("++++++++++++++++++++++++++ljkhsdfkhsdfojfdjsoidjoifjds");
		gameBegin = true;
	}


	void spawnPlayer(GameObject playerRequest, Vector3 instPosition)
	{
		playerRequest.SendMessage ("setPosition", instPosition);
        playerRequest.SendMessage("setLifeOff");
	}
}
