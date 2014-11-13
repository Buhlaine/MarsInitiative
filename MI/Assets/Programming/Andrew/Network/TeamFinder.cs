using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamFinder : MonoBehaviour {
	
	public GameObject[] enables;
	public MonoBehaviour[] enables2;
		
	void Start () {
		Dictionary<NetworkPlayer,string> Players = GameObject.Find("NetworkManager").GetComponent<MasterData>().Players;
		
		foreach(KeyValuePair<NetworkPlayer,string> i in Players){
			if(networkView.owner.ToString() == i.Key.ToString()){
				if(networkView.isMine){
					foreach(GameObject q in enables){
						q.SetActive(true);
					}
					foreach(MonoBehaviour q in enables2){
						q.enabled = true;
					}
				}
				else{
					this.gameObject.tag = i.Value;
				}
			}
		}
		this.gameObject.GetComponent<Player>().team = MasterData.team;
		
	}
	
}
