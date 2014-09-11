using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	// Use this for initialization
	void Start () 
	{
		//gathering flags on map
		GameObject[] flagTest = GameObject.FindGameObjectsWithTag ("Flag");
		GameObject[] baseTest = GameObject.FindGameObjectsWithTag ("Base");

		//Setting Enemy base
		foreach(GameObject bases in baseTest)
		{
			if(bases.gameObject != this.gameObject)
			{
				enemyBase =  bases;
			}
		}
		Vector3 dist1 = flagTest [0].transform.position - this.transform.position;
		Vector3 dist2 = flagTest [1].transform.position - this.transform.position;

		if(dist1.sqrMagnitude < dist2.sqrMagnitude)
		{
			teamFlag = flagTest[0];
			enemyFlag = flagTest[1];
		}
		else
		{
			teamFlag = flagTest[1];
			enemyFlag = flagTest[0];
		}

		if(teamFlag.name == "blueFlagObj")
		{
			this.renderer.material.color = Color.blue;
			teamTag = "Blue";
			enemyTag = "Red";
		}
		else if(teamFlag.name == "redFlagObj")
		{
			this.renderer.material.color = Color.red;
			teamTag = "Red";
			enemyTag = "Blue";
		}

		teamFlag.SendMessage ("setTeamBase", this.gameObject as GameObject);

	}
	
	// Update is called once per frame
	void Update () 
	{
		//testing flag cap amount
		if(Input.GetKeyDown(KeyCode.Q))
		{
			Debug.Log(flagCaps);
		}
	}

	void OnTriggerStay(Collider colliderInfo)
	{
		//checking to see if flag is at base
		if(flagHere)
		{
			//checking to see if colliderinfo is enemy
			//if enemy, flag is captured, follow enemy
			if(colliderInfo.tag == enemyTag)
			{
				teamFlag.SendMessage("followPlayer", colliderInfo.gameObject as GameObject);
				//setting carrying on enemy player
				colliderInfo.SendMessage("obtainedFlag");
				flagHere = false;
				//Debug.Log(colliderInfo.name);
			}
		}
		//check to see if colliderinfo is team
		//if team, check to see if play is carrying
		if(colliderInfo.tag == teamTag)
		{
			colliderInfo.SendMessage("isCarrying", this.gameObject as GameObject);
		}
	}

	void capFlagRequest(GameObject _requstor)
	{
		//FIX THIS SHIT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		//----------------------------------------------------------------------------
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

	void addEnergy(int energyAmount)
	{
		flagCaps += energyAmount;
	}
	
	void flagReturned()
	{
		flagHere = true;
	}
}
