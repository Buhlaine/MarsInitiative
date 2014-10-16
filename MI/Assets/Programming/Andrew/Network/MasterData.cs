﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterData : MonoBehaviour{
	
	public List<NetworkPlayerModule> Players = new List<NetworkPlayerModule>();
	
	public static MasterData MasterDataInstance;
	
	public static Player me;
	
	private int responses;
	
	void Start(){
		MasterDataInstance = this;
	}
	
	//Client Locals
	public void sendRegisterRequest(NetworkViewID _NVID){
		networkView.RPC("registerPlayer",RPCMode.Server, this.networkView.viewID);
	}
	
	
	//Server Locals
	public IEnumerator Waitresponses(){
		responses = 0;
		networkView.RPC("sendPlayerCount",RPCMode.All,Players.Count);
		while(responses < Players.Count){
			yield return null;
		}
		foreach(NetworkPlayerModule i in Players){
			networkView.RPC("sendPlayerInfo",RPCMode.All,i.playerNumber,i.NVID);
		}
	}
	
	//Client RPCs
	[RPC]
	void sendPlayerCount(int _number){
		networkView.RPC("incrementResponse",RPCMode.Server);
	}
	
	
	//Server RPC's
	[RPC]
	void registerPlayer(NetworkViewID _NVID){
		Players.Add(new NetworkPlayerModule(Players.Count+1,_NVID));
	}
	
	[RPC]
	void incrementResponse(){
		responses++;
	}
	
	[RPC]
	void requestPlayerList(){
		
	}
	
}

public class NetworkPlayerModule{
	public int playerNumber;
	public NetworkViewID NVID;
	
	public NetworkPlayerModule(int _playerNumber, NetworkViewID _NVID){
		playerNumber = _playerNumber;
		NVID = _NVID;
	}
}