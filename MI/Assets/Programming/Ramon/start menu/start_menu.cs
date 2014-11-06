using UnityEngine;
using System.Collections;

public class start_menu : MonoBehaviour {

	public GUIStyle backlogo;
	public GUIStyle[] button;
	int selection;
	float[] widthpos=new float[3];
	float[] heightpos=new float[3];

	// Use this for initialization
	void Start () {
		selection = 3;

		widthpos [0] = 0.09375f;
		heightpos [0] = 0.5288461f;
		
		widthpos [1] = 0.125f;
		heightpos [1] = 0.625f;
		
		widthpos [2] = 0.15625f;
		heightpos [2] = 0.7211539f;
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.LogWarning(selection);

		widthpos [0] = 0.09375f;
		heightpos [0] = 0.5288461f;

		widthpos [1] = 0.140625f;
		heightpos [1] = 0.6730769f;

		widthpos [2] = 0.1875f;
		heightpos [2] = 0.8173077f;


		Debug.Log (Time.time);
		//GUI.Box (new Rect (0.0f, 0.0f, Screen.width, Screen.height), "ssds");
	
	}

	void OnGUI(){
		Debug.LogWarning(((850.0f*100)/Screen.height)/100+"h");
		Debug.LogWarning(((300f*100)/Screen.width)/100+"W");
		Debug.LogWarning(0.1038462f*Screen.height);
		if(selection==1)
		{
			button[1].normal.background=button[1].active.background;
			widthpos[0]=Mathf.MoveTowards(widthpos[0], 1.0f, 0.1f);
			widthpos[2]=Mathf.Lerp(widthpos[2], 1.0f, 5f*Time.deltaTime);
			Debug.LogWarning(widthpos[0]);                                                                        
		}
		else if(selection==2)
		{
			button[2].normal.background=button[2].active.background;
			widthpos[1]=Mathf.Lerp(widthpos[1], 1.0f, Time.deltaTime);
			widthpos[0]=Mathf.Lerp(widthpos[0], 1.0f, Time.deltaTime);
		}
		else
		{
		}

		GUI.Box (new Rect (0.0f, 0.0f, Screen.width, Screen.height), "", backlogo);

		if(GUI.Button (new Rect (widthpos[0]*Screen.width, heightpos[0]*Screen.height, 350.0f, 150.0f), "",button[0]))
		{
			//Debug.Log("campaing");
			selection=0;
			Debug.Log("MEEEHHHHH");
//			Mathf.Lerp(widthpos[1], 1.0f, Time.time);
//			Mathf.Lerp(widthpos[2], 1.0f, Time.time);
		}
		if(GUI.Button (new Rect (widthpos[1]*Screen.width, heightpos[1]*Screen.height, 350.0f, 150.0f), "",button[1]))
		{
			Debug.Log("multiplayer");
			selection=1;
//			widthpos[0]=Mathf.Lerp(widthpos[0], 1.0f, Time.deltaTime);
//			widthpos[2]=Mathf.Lerp(widthpos[2], 1.0f, Time.deltaTime);

		}
		if(GUI.Button (new Rect (widthpos[2]*Screen.width, heightpos[2]*Screen.height, 350.0f, 150.0f), "",button[2]))
		{
			Debug.Log("options");
			selection=2;
//			Mathf.Lerp(widthpos[0], 1.0f, Time.time);
//			Mathf.Lerp(widthpos[1], 1.0f, Time.time);
		}


//		if(GUI.Button (new Rect (150f, 550f, 350.0f, 150.0f), "",button[0]))
//		{
//			//Debug.Log("campaing");
//			selection=0;
//			Debug.Log("MEEEHHHHH");
//			//			Mathf.Lerp(widthpos[1], 1.0f, Time.time);
//			//			Mathf.Lerp(widthpos[2], 1.0f, Time.time);
//		}
//		if(GUI.Button (new Rect (225f, 700f, 350.0f, 150.0f), "",button[1]))
//		{
//			Debug.Log("multiplayer");
//			selection=1;
//			//			widthpos[0]=Mathf.Lerp(widthpos[0], 1.0f, Time.deltaTime);
//			//			widthpos[2]=Mathf.Lerp(widthpos[2], 1.0f, Time.deltaTime);
//			
//		}
//		if(GUI.Button (new Rect (300f, 850f, 350.0f, 150.0f), "",button[2]))
//		{
//			Debug.Log("options");
//			selection=2;
//			//			Mathf.Lerp(widthpos[0], 1.0f, Time.time);
//			//			Mathf.Lerp(widthpos[1], 1.0f, Time.time);
//		}

		}
}
