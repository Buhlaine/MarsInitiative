using UnityEngine;
using System.Collections;

public class start_menu : MonoBehaviour {

	public GUIStyle backlogo;
	public GUIStyle[] button;
	public GUIStyle MD;
	string[] description = new string[6];
	int selection;
	float[] widthpos=new float[3];
	float[] heightpos=new float[3];
	float i = 2;

	// Use this for initialization
	void Start () {
		selection = 3;
		description[0] = "Choose a side and fight for control of Mars.";
		description[1] = "";
		description[2] = "Recover: Sieze the enemy's Tharsium supply.";
		description[3] = "Strike: Eliminate the enemy at all costs.";
		description[4] = "Create: Start a lobby.";
		description[5] = "Join: Join an existing lobby.";

		widthpos [0] = 0.09375f;
		heightpos [0] = 0.5288461f;
		
		widthpos [1] = 0.140625f;
		heightpos [1] = 0.6730769f;
		
		widthpos [2] = 0.1875f;
		heightpos [2] = 0.8173077f;
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}


	void OnGUI(){
		if(selection==1)
		{
			i++;
			button[1].normal.background=button[1].active.background;
			widthpos[0]=Mathf.Lerp(widthpos[0], 1.0f, Time.deltaTime);
			widthpos[2]=Mathf.Lerp(widthpos[2], 1.0f, Time.deltaTime);
			Debug.LogWarning(widthpos[0]);
			//Debug.LogWarning(i); 
		}
		else if(selection==2)
		{
			button[2].normal.background=button[2].active.background;
			widthpos[1]=Mathf.Lerp(widthpos[1], 1.0f, Time.deltaTime);
			widthpos[0]=Mathf.Lerp(widthpos[0], 1.0f, Time.deltaTime);
		}

		GUI.Box (new Rect (0.0f, 0.0f, Screen.width, Screen.height), "", backlogo);

		Rect Button1=new Rect (widthpos[0]*Screen.width, heightpos[0]*Screen.height, 350.0f, 150.0f);
		if(GUI.Button (Button1, "Campaign",button[0]))
		{
			//Debug.Log("campaing");
			selection=0;

		}

		Rect Button2=new Rect (widthpos[1]*Screen.width, heightpos[1]*Screen.height, 350.0f, 150.0f);
		if(GUI.Button (Button2, "Mutilplayer",button[1]))
		{
			//Debug.Log("multiplayer");
			selection=1;

		}

		Rect Button3=new Rect (widthpos[2]*Screen.width, heightpos[2]*Screen.height, 350.0f, 150.0f);
		if(GUI.Button (Button3, "Recover/Strike",button[2]))
		{
			//Debug.Log("options");
			selection=2;

		}

		Debug.Log(Button1.Contains(Input.mousePosition));
		Vector2 mousePos = new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y);

		if(Button1.Contains(mousePos))
		{
			GUI.Box (new Rect (960f, 927f, 650f, 31f), description[0], MD);
			GUI.Box (new Rect (940f, 968f, 650f, 31f), description[1], MD);
		}
		if(Button2.Contains(mousePos))
		{
			GUI.Box (new Rect (960f, 927f, 650f, 31f), description[2], MD);
			GUI.Box (new Rect (940f, 968f, 650f, 31f), description[3], MD);
		}
		if(Button3.Contains(mousePos))
		{
			GUI.Box (new Rect (960f, 927f, 650f, 31f), description[4], MD);
			GUI.Box (new Rect (940f, 968f, 650f, 31f), description[5], MD);
		}

		}
}
