using UnityEngine;
using System.Collections;

public class ClassSelect : MonoBehaviour {
	
	private string[] names = {"Assassin", "Enforcer", "Trooper"}; 
	public GameObject[] Prefabs_rb_A_E_T;
	
	private int spawns = 0;
	
	void OnGUI(){
		for (int i=0; i<3; i++) {
			if(GUI.Button(new Rect(50,50+(i*50),200,40),names[i])){
				int butts = 0;
				if(MasterData.team == "Blue"){
					butts = 3;
				}
				Network.Instantiate(Prefabs_rb_A_E_T[i+butts],Vector3.zero,Quaternion.identity,i+butts);
				//GameObject.FindGameObjectWithTag("spwnManager").SendMessage("spawnRequest",GameObject.FindGameObjectWithTag("Player"));
				
				if(Network.peerType == NetworkPeerType.Client){
					Debug.Log("ST "+Network.peerType.ToString());
					Destroy(this.gameObject);
					networkView.RPC("newSpawn",RPCMode.Server);
				}
				else{
					newSpawn();
				}
			}
		}
		
		if(Network.peerType == NetworkPeerType.Server){
			if (GUI.Button(new Rect(Screen.width-250,50,200,40),"Start with "+spawns.ToString()+" of "+Network.connections.Length.ToString())){
				//GameObject.FindGameObjectWithTag("spwnManager").SendMessage("setgameBegin");
				Destroy(this.gameObject);
			}
		}
		
	}
	
	[RPC]
	void newSpawn(){
		spawns++;
	}
	
}
