using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterData : MonoBehaviour{
	
	public List<NetworkPlayer> Players = new List<NetworkPlayer>();
	
	public static MasterData MasterDataInstance;
	
	public static NetworkPlayer me;
	
	private int responses;
	
	void Start(){
		DontDestroyOnLoad(this.gameObject);
		MasterDataInstance = this;
	}
	
	//Client Locals
	public void sendRegisterRequest(){
		if(Network.peerType == NetworkPeerType.Client){
			Debug.Log("Registering as "+Network.peerType.ToString());
			networkView.RPC("registerPlayer",RPCMode.Server, Network.player);
		}
		else{
			Debug.Log("Registering locally as Server");
			registerPlayer(Network.player);
		}
	}
	
	
	//Server Locals
	

	//Client RPCs
	[RPC]
	void SendPlayer(NetworkPlayer _player){
		Debug.Log("received "+_player.ToString());
		bool add = true;
		foreach(NetworkPlayer i in Players){
			Debug.Log("Comparing "+_player.ToString()+" to "+i.ToString());
			if(_player.ToString() == i.ToString()){
				add = false;
				Debug.Log("Match, not adding");
			}
		}
		if (add == true){
			Debug.Log(_player.ToString()+" not found, adding");
			Players.Add(_player);
			//networkView.RPC("Wave",
		}
	}

	//Server RPC's
	[RPC]
	void registerPlayer(NetworkPlayer _playerNet){
		Players.Add(_playerNet);
		Debug.Log("Registered player "+(Players.Count).ToString());
		if(_playerNet.guid != Network.player.guid){
			Debug.Log("Sending current players");
			foreach(NetworkPlayer i in Players){
				Debug.Log("Sending "+i.guid.ToString()+" to "+_playerNet.guid.ToString());
				networkView.RPC("SendPlayer",_playerNet,i);
				
			}
		}
	}

	void OnGUI(){

		GUI.Label(new Rect(Screen.width-300,50,200,30),"Me: "+Network.player.guid.ToString());

		for(int i = 0; i<Players.Count; i++){
			GUI.Label(new Rect(Screen.width-300,100+(50*i),200,30),Players[i].ToString());
			GUI.Label(new Rect(Screen.width-300,120+(50*i),200,30),Players[i].guid.ToString());
		}
	}

	void OnApplicationQuit(){
		Network.Disconnect();
	}
	
}