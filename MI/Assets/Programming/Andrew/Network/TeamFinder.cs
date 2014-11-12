using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamFinder : MonoBehaviour {
	
	void Start () {
		Dictionary<NetworkPlayer,string> Players = GameObject.Find("NetworkManager").GetComponent<MasterData>().Players;
		
		foreach(KeyValuePair<NetworkPlayer,string> i in Players){
			if(networkView.owner.ToString() == i.Key.ToString() && !networkView.isMine){
				this.gameObject.tag = i.Value;
			}
		}
		
		this.gameObject.GetComponent<Player>().team = MasterData.team;
		
	}
	
}
