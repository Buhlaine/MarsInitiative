using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkLobby : MonoBehaviour {

	private HostData[] hostList;
	//public string ip = "127.0.0.1";
	public int port = 23466;
	
	private bool isHost = false;
	
	private string gameName;
	
	//public string MSIP = "smp.awesomecraft.net";
	private string MSIP = "smp.awesomecraft.net";

	void Start(){
		MasterServer.ipAddress = MSIP;
		gameName = "Game "+((int)Random.Range(1000,9999)).ToString();
		RefreshHostList();
		//Network.TestConnection();
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.Log(Network.peerType.ToString());
		}
	}
	
	void RefreshHostList(){
		MasterServer.ClearHostList();
		MasterServer.RequestHostList("Sol");
	}
	
	public IEnumerator waitConnection(){
		while((Network.peerType != NetworkPeerType.Client) && (Network.peerType != NetworkPeerType.Server)){
			yield return null;
		}
		MasterData.MasterDataInstance.SendMessage("sendRegisterRequest");
	}
	
	void OnGUI(){
		
		if(GUI.Button(new Rect(50,Screen.height-100,200,50),"Refresh")){
			RefreshHostList();
		}
		
		if (!isHost){
			gameName = GUI.TextField(new Rect(250,Screen.height-150,200,50),gameName);
			if(GUI.Button(new Rect(250,Screen.height-100,200,50),"Register")){
				//Network.InitializeServer(32, port, false);
				Network.InitializeServer(32, port+(int)Random.Range(1,100), false);
				MasterServer.RegisterHost("Sol",gameName);
				StartCoroutine(waitConnection());
				RefreshHostList();
				isHost = true;
			}
		}
		
		if((MasterServer.PollHostList().Length != 0)){
			for(int i=0;i<MasterServer.PollHostList().Length;i++){
				GUI.Label(new Rect(50,50+(i*50),50,25),i.ToString());
				GUI.Label(new Rect(100,50+(i*50),200,25),MasterServer.PollHostList()[i].gameName);
				if(!isHost){
					if(GUI.Button(new Rect(250,50+(i*50),50,25),"Join")){
						Network.Connect(MasterServer.PollHostList()[i].ip,MasterServer.PollHostList()[i].port);
						StartCoroutine(waitConnection());
					}
				}
			}
		}

		if(GUI.Button(new Rect(Screen.width-250,Screen.height-100,200,50),"DC")){
			Network.Disconnect();
		}

	}
}
