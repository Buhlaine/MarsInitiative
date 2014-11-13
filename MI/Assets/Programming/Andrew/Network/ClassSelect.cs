using UnityEngine;
using System.Collections;

public class ClassSelect : MonoBehaviour {
	
	private string[] names = {"Assassin", "Enforcer", "Trooper"}; 
	public GameObject[] Prefabs_rb_A_E_T;
	
	void OnGUI(){
		for (int i=0; i<3; i++) {
			if(GUI.Button(new Rect(50,50+(i*50),200,40),names[i])){
				int butts = 0;
				if(MasterData.team == "Blue"){
					butts = 3;
				}
				Network.Instantiate(Prefabs_rb_A_E_T[i+butts],Vector3.zero,Quaternion.identity,i+butts);
				Destroy(this);
			}
		}
	}
}
