using UnityEngine;
using System.Collections;

public class baseScript : MonoBehaviour {

	GameObject teamFlag;
	GameObject enemyFlag;
	GameObject enemyBase;
	string teamTag;
	string enemyTag;
	bool flagHere = true;
	int flagCaps = 5;
	int addAmount = 1;
	//GameObject carriedFlag;

	/*------------------------------------------
	check andrew's tag script
	



	 * ---------------------------------------------*/

	// Use this for initialization
	void Start () 
	{
		//gathering flags on map
		GameObject[] flagTest = GameObject.FindGameObjectsWithTag ("Flag");
		GameObject[] baseTest = GameObject.FindGameObjectsWithTag ("Base");
		Debug.LogWarning ("Flag count: " + flagTest.Length);
		Debug.LogWarning ("Base count: " + baseTest.Length);

		foreach(GameObject bases in baseTest)
		{
			if(bases.gameObject != this.gameObject)
			{
				enemyBase =  bases;
			}
		}

		//finding distance from flag the base is
		Vector3 dist1 = this.transform.position - flagTest [0].transform.position;
		Vector3 dist2 = this.transform.position - flagTest [1].transform.position;

		//Setting team flag and enemy flag by distance from this
		if(dist1.magnitude > dist2.magnitude)
		{
			teamFlag = flagTest[1];
			enemyFlag = flagTest[0];
		}
		else
		{
			teamFlag = flagTest[0];
			enemyFlag = flagTest[1];
		}

		//setting team and enemy color for use later
		if(teamFlag.name.Contains("Blue"))
		{
			//renderer.material.color = Color.blue;
			teamTag = "Blue";
			enemyTag = "Red";
		}
		else if(teamFlag.name.Contains("Red"))
		{
			//renderer.material.color = Color.red;
			teamTag = "Red";
			enemyTag = "Blue";
		}

		teamFlag.SendMessage ("setTeamBase", this.gameObject as GameObject);
		enemyFlag.SendMessage ("setEnemyBase", this.gameObject as GameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//testing flag cap amount
//		if(Input.GetKeyDown(KeyCode.Q))
//		{
//			Debug.Log(flagCaps);
//		}
	}

	void OnTriggerStay(Collider colliderInfo)
	{
		//checking to see if flag is at base
		if(flagHere)
		{
			//checking to see if colliderinfo is enemy
			//if enemy, flag is captured, follow enemy
			if(colliderInfo.tag == "Player")
			{
				Player colPlayer = colliderInfo.gameObject.GetComponent<Player> ();
				if(colPlayer.team == enemyTag)
				{
					teamFlag.SendMessage("followPlayer", colliderInfo.gameObject as GameObject);
					//setting carrying on enemy player
					colliderInfo.SendMessage("obtainedFlag");
					flagHere = false;
					//Debug.Log(colliderInfo.name);
				}
				//check to see if colliderinfo is team
				//if team, check to see if play is carrying
				else if(colliderInfo.tag == teamTag)
				{
					colliderInfo.SendMessage("isCarrying", this.gameObject as GameObject);
				}
			}
		}
	}

	void capFlagRequest(GameObject _requstor)
	{
		//checking to see if flag is returned to keep from adding more than one to flagCaps
		enemyFlag.SendMessage ("flagReturn", this.gameObject as GameObject);
		//turning off flag carrying on team to avoid adding more than once to flagCcaps
		_requstor.SendMessage ("notCarrying");
	}

	void capFlag(bool flagReturned)
	{
		//checking if flag is returned to avoid adding more than once to flagCaps
		if(flagReturned)
		{
			addEnergy(addAmount);
			enemyBase.SendMessage("addEnergy", -addAmount);
		}
	}

	//[RPC]
	void addEnergy(int energyAmount)
	{
		flagCaps += energyAmount;
	}

	//[RPC]
	void flagReturned()
	{
		flagHere = true;
	}

	void OnGUI()
	{
		GUI.Label (new Rect (Camera.main.WorldToScreenPoint (this.transform.position).x, 10, 100, 50), teamTag +  " flag caps: " + flagCaps);
	}

//	void setCarriedFlag(GameObject enemyFlag)
//	{
//		carriedFlag = enemyFlag;
//	}
}
