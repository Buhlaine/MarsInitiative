using UnityEngine;
using System.Collections;

public class playerDetect : MonoBehaviour {
	Hashtable playersInZone;
	int spawnWeight = 1000;
	SphereCollider thisCollider;
	
	void Start () 
	{
		playersInZone = new Hashtable ();
		thisCollider = this.gameObject.GetComponent<SphereCollider> ();
	}

	void Update () 
	{
		//Testing
		if(Input.GetKeyDown(KeyCode.P))
		{
			foreach(DictionaryEntry entry in playersInZone)
			{
				Debug.Log(entry.Key);
				Debug.Log("--------------------");
			}
			if(playersInZone.Count == 0)
			{
				Debug.Log("++++++++++++++++++++++++");
			}
		}
		if(Input.GetKeyDown(KeyCode.Y))
		{
			this.calcSpawnWeight("Blue");
			Debug.Log(this.name);
			Debug.Log(spawnWeight);
			this.resetWeight();
		}
		if(Input.GetKeyDown(KeyCode.M))
		{
			foreach(DictionaryEntry units in playersInZone)
			{
				Debug.Log(units.Value.ToString());
			}
		}
	}

	void OnTriggerEnter(Collider playerInRange)
	{
		if(!playersInZone.ContainsKey(playerInRange))
		{
			playersInZone.Add(playerInRange.gameObject, playerInRange.tag);
		}
	}

	void OnTriggerExit(Collider playerOutRange)
	{
		if(playersInZone.ContainsKey(playerOutRange.gameObject))
		{
			playersInZone.Remove(playerOutRange.gameObject);
		}
	}

	void calcSpawnWeight(string teamColor)
	{
		foreach(DictionaryEntry units in playersInZone)
		{
			if(units.Value.ToString() != teamColor)
			{
				Vector3 unitdist = this.transform.position - (units.Key as GameObject).transform.position;
				if(unitdist.magnitude < thisCollider.radius/4)
				{
					spawnWeight -= 100;
				}
				else
				{
					spawnWeight -= 50;
				}
			}
			else if(units.Value.ToString() == teamColor)
			{
				spawnWeight += 100;
			}
		}

		//Debug.Log (this.spawnWeight);
	}

	public int getSpawnWeight()
	{
		return spawnWeight;
	}

	void resetWeight()
	{
		spawnWeight = 1000;
	}

	public void removeHash(GameObject _requestor)
	{
		if(playersInZone.ContainsKey(_requestor))
		{
			playersInZone.Remove(_requestor);
		}
	}
}
