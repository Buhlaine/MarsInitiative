using UnityEngine;
using System.Collections;

public class flagScript : MonoBehaviour {

	bool follow = false;
	bool returned = true;
	bool dropped = false;
	GameObject enemyPlayer;
	GameObject teambase;
	GameObject enemybase;
	Vector3 homePos;
	string enemyTeam;
	string homeTeam;
	public Vector3 fudgeFactor = new Vector3 (0.0f, 0.5f, 0.0f);

	// Use this for initialization
	void Start () 
	{
		//setting enemy team
		//used in collision for flag drop
		if(this.name == "blueFlagObj")
		{
			enemyTeam = "Red";
			homeTeam = "Blue";
		}
		else if(this.name == "redFlagObj")
		{
			enemyTeam = "Blue";
			homeTeam = "Red";
		}
		homePos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if flag taken, follow enemy
		if(follow)
		{
			this.transform.position = enemyPlayer.transform.position + fudgeFactor;
			enemyPlayer.SendMessage("setCarriedFlag", this.gameObject as GameObject);
		}

	}

	void OnTriggerStay(Collider colliderInfo)
	{
		if(dropped)
		{
			if(colliderInfo.gameObject.tag == enemyTeam)
			{
				//May be removed.
				//had issues during testing where dead player picked flag up
				colliderInfo.gameObject.SendMessage("areYouAlive", this.gameObject as GameObject);
			}
			else if(colliderInfo.gameObject.tag == homeTeam)
			{
				follow = false;
				this.transform.position = homePos;
				returned = true;
				dropped = false;
				teambase.SendMessage("flagReturned");
			}
		}
	}

	//setting player to follow from flagManager
	void followPlayer(GameObject player)
	{
		enemyPlayer = player;
		follow = true;
		returned = false;
		dropped = false;
	}
	
	void flagReturn(GameObject _requestor)
	{
		//return flag to base
		//this.transform.position = homePos;
		//set follow to false so it stays at base
		//follow = false;
		//set returned to true to avoid adding more than once to enemy flagCaps
		returned = true;
		//telling enemyBase to add to flagCaps
		teambase.SendMessage ("flagReturned", this.gameObject as GameObject);
		_requestor.SendMessage ("capFlag", returned);
		Destroy (this.gameObject);
	}

	//setting teamBase so it can return
	void setTeamBase(GameObject tB)
	{
		this.teambase = tB;
	}

	void setEnemyBase(GameObject eB)
	{
		this.enemybase = eB;
	}

	void flagPickedUp()
	{
		dropped = false;
	}
	

	//set position when carrying player dies
	void flagDropped(Vector3 dropPosition)
	{
		this.transform.position = dropPosition + fudgeFactor;
		follow = false;
		dropped = true;
	}


	//Testing Purposes---------------------------------------------------------------------------
	void setColor(Color changeColor)
	{
		this.renderer.material.color = changeColor;
	}
}
