using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterData : MonoBehaviour{
	
	public Dictionary<NetworkPlayer,string> Players = new Dictionary<NetworkPlayer,string>();
	
	public static MasterData MasterDataInstance;
	
	private string team;
	
	void Start(){
		DontDestroyOnLoad(this.gameObject);
		MasterDataInstance = this;
	}
	
	//Client Locals
	public void sendRegisterRequest(string _team){
		team = _team;
		if(Network.peerType == NetworkPeerType.Client){
			Debug.Log("Registering as "+Network.peerType.ToString());
			networkView.RPC("registerPlayer",RPCMode.Server, Network.player, team);
		}
		else{
			Debug.Log("Registering locally as Server");
			registerPlayer(Network.player,team);
		}
	}
	
	
	//Server Locals
	

	//Client RPCs
	[RPC]
	void SendPlayer(NetworkPlayer _player,string _team){
		Debug.Log("received "+_player.ToString());
		bool add = true;
		foreach(KeyValuePair<NetworkPlayer,string> i in Players){
			Debug.Log("Comparing "+_player.ToString()+" to "+i.Key.ToString());
			if(_player.ToString() == i.Key.ToString()){
				add = false;
				Debug.Log("Match, not adding");
			}
		}
		if (add == true){
			Debug.Log(_player.ToString()+" not found, adding");
			Players.Add(_player, _team);
			//networkView.RPC("Wave",
		}
	}

	//Server RPC's
	[RPC]
	void registerPlayer(NetworkPlayer _playerNet, string _team){
		Players.Add(_playerNet, _team);
		Debug.Log("Registered player "+(Players.Count).ToString());
		if(_playerNet.guid != Network.player.guid){
			Debug.Log("Sending current players");
			foreach(KeyValuePair<NetworkPlayer,string> i in Players){
				Debug.Log("Sending "+i.Key.ToString()+" to "+_playerNet.guid.ToString());
				networkView.RPC("SendPlayer",_playerNet,i.Key,_team);
				
			}
		}
	}
	
	void OnGUI(){
		
		GUI.Label(new Rect(Screen.width-300,50,200,30),"Me: "+Network.player.guid.ToString());
		
		int j=0;
		foreach(KeyValuePair<NetworkPlayer,string> i in Players){
			GUI.Label(new Rect(Screen.width-300,100+(50*j),200,30),i.Key.ToString());
			GUI.Label(new Rect(Screen.width-300,120+(50*j),200,30),i.Key.guid.ToString());
			j++;
		}
	}

	void OnApplicationQuit(){
		Network.Disconnect();
	}
	
}