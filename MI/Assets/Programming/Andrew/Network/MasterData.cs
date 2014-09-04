using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterData : MonoBehaviour{
	
	public List<Player> Players = new List<Player>();
	
	public static MasterData MasterDataInstance;
	
	public void registerPlayer(int _playerNumber, NetworkViewID _NVID){
		networkView.RPC("RegisterPlayer",RPCMode.Server,_playerNumber,_NVID);
	}
	
	
	[RPC]
	void RegisterPlayer(int _playerNumber, NetworkViewID _NVID){

	}
}

public class Player{
	int playerNumber;
	NetworkViewID NVID;
	
	Player(int _playerNumber, NetworkViewID _NVID){
		playerNumber = _playerNumber;
		NVID = _NVID;
	}
}