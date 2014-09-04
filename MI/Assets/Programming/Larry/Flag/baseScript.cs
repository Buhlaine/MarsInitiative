using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class baseScript : MonoBehaviour {

	List<GameObject> teamFlags = new List<GameObject> ();
	GameObject enemyBase;
	string teamTag;
	string enemyTag;
	bool flagHere = true;
	int flagCaps = 5;
	int addAmount = 1;
	GameObject carriedFlag;
	//GameObject carriedFlag;

	// Use this for initialization
	void Start () 
	{
		//gathering flags on map
		GameObject[] flagTest = GameObject.FindGameObjectsWithTag ("Flag");
		//Gathering Bases
		GameObject[] baseTest = GameObject.FindGameObjectsWithTag ("Base");

		//Setting Enemy base
		foreach(GameObject bases in baseTest)
		{
			if(bases.gameObject != this.gameObject)
			{
				enemyBase =  bases;
			}
		}

		//Used for determining team/enemy tag
		Vector3 dist1 = new Vector3 ();
		Vector3 dist2 = new Vector3 ();
		GameObject closestFlag = new GameObject ();

		teamFlags.TrimExcess();
		foreach(GameObject flag in flagTest)
		{
			if(closestFlag == null)
			{
				closestFlag = flag;
			}
			else
			{
				dist1 = flag.transform.position - this.gameObject.transform.position;
				dist2 = closestFlag.transform.position - this.gameObject.transform.position;
				if(dist1.magnitude < dist2.magnitude)
				{
					closestFlag = flag;
				}
			}
		}

		dist2 = closestFlag.transform.position - this.gameObject.transform.position;
		if(closestFlag.name == "blueFlagObj")
		{
			teamTag = "Blue";
			enemyTag = "Red";
		}
		else
		{
			teamTag = "Red";
			enemyTag = "Blue";
		}

		foreach(GameObject flag in flagTest)
		{
			if(flag.gameObject.name == closestFlag.gameObject.name)
			{
				teamFlags.Add(flag);
			}
		}

		foreach(GameObject flag in teamFlags)
		{
			flag.SendMessage("setTeamBase", this.gameObject as GameObject);
		}

		//Testing Purposes
		if(teamTag == "Blue")
		{
			renderer.material.color = Color.blue;
		}
		else
		{
			renderer.material.color = Color.red;
		}

		foreach(GameObject flag in teamFlags)
		{
			flag.SendMessage("setColor", this.renderer.material.color);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//testing flag cap amount
		if(Input.GetKeyDown(KeyCode.Q))
		{
			Debug.Log(enemyTag);
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
				teamFlags[0].SendMessage("followPlayer", colliderInfo.gameObject as GameObject);
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
		//checking to see if flag is returned to keep from adding more than one to flagCaps
		//enemyFlag.SendMessage ("flagReturn", this.gameObject as GameObject);
		carriedFlag.SendMessage ("flagReturn", this.gameObject as GameObject);
		//turning off flag carrying on team to avoid adding more than once to flagCcaps
		_requstor.SendMessage ("notCarrying");

		//this.transform.forward = transform.InverseTransformDirection (this.transform.forward);
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
	
	void flagReturned(GameObject flagObj)
	{
		flagHere = true;
		teamFlags.Remove (flagObj);
		Destroy (flagObj);
	}

	void setCarriedFlag(GameObject flagCarried)
	{
		carriedFlag = flagCarried;
	}
}
