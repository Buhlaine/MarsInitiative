
using UnityEngine;
using System.Collections;

public class start_menu : MonoBehaviour {

	public GUIStyle backlogo;
	public GUIStyle frontlogo;
	public GUIStyle[] button;
	public GUIStyle MD;
	public GUIStyle BN;
	string[] description1 = new string[10];
	int selection;
	float[] widthpos=new float[7];
	float[] heightpos=new float[7];
	//float i = 2;
	int type;
	bool recoverOrstrike=false;
	bool createOrplay=false;

	// Use this for initialization
	void Start () {
		selection = 3;
		description1[0] = "";
		description1[1] = "Choose a side and fight for control of Mars.";
		description1[2] = "Rally up and take the fight online.";
		description1[3] = "View the Credits";
		description1[4] = "Sieze the enemy's Tharsium supply.";
		description1[5] = "Eliminate the enemy at all costs.";
		description1[6] = "Start a lobby.";
		description1[7] = "Join an existing lobby.";
		description1[8] = "FOR EARTH!!";
		description1[9] = "FOR MARS!!";
//		description1[4] = "Create: Start a lobby.";
//		description1[5] = "Join: Join an existing lobby.";

		widthpos [0] = 0.09375f;
		heightpos [0] = 0.5288461f;
		
		widthpos [1] = 0.140625f;
		heightpos [1] = 0.6730769f;
		
		widthpos [2] = 0.1875f;
		heightpos [2] = 0.8173077f;

		widthpos [3] = 0.4f;
		heightpos [3] = 0.5288461f;

		widthpos [4] = 0.4f;
		heightpos [4] = 0.7f;

		widthpos [5] = 0.65f;
		heightpos [5] = 0.5288461f;

		widthpos [6] = 0.65f;
		heightpos [6] = 0.7f;
	
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (0.65f*Screen.width);//1040
//		Debug.Log (0.5288461f*Screen.height);//549.9999
		Debug.Log (0.7f*Screen.height);//728
	
	}


	void OnGUI(){
		Vector2 mousePos = new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y);

		GUI.Box (new Rect (0.0f, 0.0f, Screen.width, Screen.height), "", backlogo);

		GUI.Box (new Rect (640.0f, 150.0f, 700f, 350f), "", frontlogo);


		Rect Button1=new Rect (widthpos[0]*Screen.width, heightpos[0]*Screen.height, 350.0f, 150.0f);
		Rect Button2=new Rect (widthpos[1]*Screen.width, heightpos[1]*Screen.height, 350.0f, 150.0f);
		Rect Button3=new Rect (widthpos[2]*Screen.width, heightpos[2]*Screen.height, 350.0f, 150.0f);
		Rect Button4=new Rect (widthpos[3]*Screen.width, heightpos[3]*Screen.height, 350.0f, 150.0f);
		Rect Button5=new Rect (widthpos[4]*Screen.width, heightpos[4]*Screen.height, 350.0f, 150.0f);
		Rect Button6=new Rect (widthpos[5]*Screen.width, heightpos[5]*Screen.height, 350.0f, 150.0f);
		Rect Button6BlueHalf=new Rect (1215f, 550f, 175.0f, 150.0f);
		Rect Button6RedHalf=new Rect (1040f, 550f, 175.0f, 150.0f);
		Rect Button7=new Rect (widthpos[6]*Screen.width, heightpos[6]*Screen.height, 350.0f, 150.0f);
		Rect Button7BlueHalf=new Rect (1215f, 728f, 175.0f, 150.0f);
		Rect Button7RedHalf=new Rect (1040f, 728f, 175.0f, 150.0f);

		if(selection==1)
		{
			//i++;
			recoverOrstrike=true;
			button[0].normal.background=button[0].active.background;
			button[0].hover.background=button[0].active.background;
			widthpos[0]=Mathf.Lerp(widthpos[0], 1.0f, Time.deltaTime);
			widthpos[2]=Mathf.Lerp(widthpos[2], 1.0f, Time.deltaTime);
			Debug.LogWarning(widthpos[0]);

			//Debug.LogWarning(i); 
		}
		else if(selection==2)
		{
			button[0].normal.background=button[0].active.background;
			button[0].hover.background=button[0].active.background;
			widthpos[1]=Mathf.Lerp(widthpos[1], 1.0f, Time.deltaTime);
			widthpos[0]=Mathf.Lerp(widthpos[0], 1.0f, Time.deltaTime);
		}

		//GUI.Box (new Rect (0.0f, 0.0f, Screen.width, Screen.height), "", backlogo);

		/////////BUTTON1//////////////////////BUTTON1//////////////////////BUTTON1//////////////////////BUTTON1//////////////////////BUTTON1/////////////
		//Rect Button1=new Rect (widthpos[0]*Screen.width, heightpos[0]*Screen.height, 350.0f, 150.0f);
		if(GUI.Button (Button1, "Campaign",button[0]))
		{
			//Debug.Log("campaing");
			selection=0;

		}
		GUI.Box (Button1, "Campaign",BN);
		/////////BUTTON1/////////////END/////////BUTTON1/////////////END/////////BUTTON1/////////////END/////////BUTTON1/////////////END


		/////////BUTTON2//////////////////////BUTTON2//////////////////////BUTTON2//////////////////////BUTTON2//////////////////////BUTTON2/////////////
		//Rect Button2=new Rect (widthpos[1]*Screen.width, heightpos[1]*Screen.height, 350.0f, 150.0f);
		if(GUI.Button (Button2, "Mutilplayer",button[0]))
		{
			//Debug.Log("multiplayer");
			selection=1;
//			if(GUI.Button (new Rect (0.09375f*Screen.width, 0.5288461f*Screen.height, 350.0f, 150.0f), "Campaign"))
//			{
//				//Debug.Log("campaing");
//				selection=0;
//			}

		}
		GUI.Box (Button2, "Mutilplayer",BN);
		/////////BUTTON2/////////////END/////////BUTTON2/////////////END/////////BUTTON2/////////////END/////////BUTTON2/////////////END

		/////////BUTTON3//////////////////////BUTTON3//////////////////////BUTTON3//////////////////////BUTTON3//////////////////////BUTTON3/////////////
		//Rect Button3=new Rect (widthpos[2]*Screen.width, heightpos[2]*Screen.height, 350.0f, 150.0f);
		if(GUI.Button (Button3, "Credits",button[0]))
		{
			//Debug.Log("options");
			selection=2;

		}
		GUI.Box (Button3, "Credits",BN);
		/////////BUTTON3/////////////END/////////BUTTON3/////////////END/////////BUTTON3/////////////END/////////BUTTON3/////////////END

		if(recoverOrstrike)
		{
			if(GUI.Button (Button4, "Recover", button[1]))
			{
				//Debug.Log("campaing");
				type=4;
				createOrplay=true;
			}
			GUI.Box (Button4, "Recover",BN);

			if(GUI.Button (Button5, "Strike", button[1]))
			{
				//Debug.Log("campaing");
				type=3;
				createOrplay=true;
			}
			GUI.Box (Button5, "Strike",BN);

			if(createOrplay)
			{
				button[1].normal.background=button[1].active.background;
				button[1].hover.background=button[1].active.background;
				widthpos[type]=Mathf.Lerp(widthpos[type], 1.0f, Time.deltaTime);
				widthpos[type]=Mathf.Lerp(widthpos[type], 1.0f, Time.deltaTime);
				GUI.Box (Button6, "Register", button[2]);
//				if(GUI.Button (Button6, "Register"))
//				{
//					//Debug.Log("campaing");
//					//selection=3;
//					//createOrplay=true;
//				}
				if(GUI.Button (Button6RedHalf, "Register",button[4]))
				{
					button[2].normal.background=button[4].active.background;
					//Debug.Log("campaing");
					//selection=3;
					//createOrplay=true;
				}
				GUI.Box (Button6RedHalf, "Register",BN);

				if(GUI.Button (Button6BlueHalf, "Register",button[5]))
				{
					button[2].normal.background=button[5].active.background;
					//Debug.Log("campaing");
					//selection=3;
					//createOrplay=true;
				}
				GUI.Box (Button6BlueHalf, "Register",BN);

				GUI.Box (Button7, "Join", button[3]);
//				if(GUI.Button (Button7, "Join", button[3]))
//				{
//					//Debug.Log("campaing");
//					//selection=4;
//					//createOrplay=true;
//				}

				if(GUI.Button (Button7RedHalf, "Join",button[4]))
				{
					button[3].normal.background=button[4].active.background;
					//Debug.Log("campaing");
					//selection=3;
					//createOrplay=true;
				}
				GUI.Box (Button7RedHalf, "Join",BN);

				if(GUI.Button (Button7BlueHalf, "Join",button[5]))
				{
					button[3].normal.background=button[5].active.background;
					//Debug.Log("campaing");
					//selection=3;
					//createOrplay=true;
				}
				GUI.Box (Button7BlueHalf, "Join",BN);


			}
		}
		//Debug.Log(Button1.Contains(Input.mousePosition));
		//Vector2 mousePos = new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y);

		if(Button1.Contains(mousePos))
		{
			GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[1], MD);
			GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[0], MD);
		}
		if(Button2.Contains(mousePos))
		{
			GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[2], MD);
			GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[0], MD);
		}
		if(Button3.Contains(mousePos))
		{
			GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[3], MD);
			GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[0], MD);
		}
		if(Button4.Contains(mousePos))
		{
			GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[4], MD);
			GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[0], MD);
		}
		if(Button5.Contains(mousePos))
		{
			GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[5], MD);
			GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[0], MD);
		}
		if(Button6.Contains(mousePos))
		{
			GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[6], MD);
			GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[0], MD);
			if(Button6BlueHalf.Contains(mousePos))
			{
				GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[0], MD);
				GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[8], MD);
			}
			if(Button6RedHalf.Contains(mousePos))
			{
				GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[0], MD);
				GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[9], MD);
			}
		}
		if(Button7.Contains(mousePos))
		{
			GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[7], MD);
			GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[0], MD);
			if(Button7BlueHalf.Contains(mousePos))
			{
				GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[0], MD);
				GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[8], MD);
			}
			if(Button7RedHalf.Contains(mousePos))
			{
				GUI.Box (new Rect (960f, 927f, 650f, 31f), description1[0], MD);
				GUI.Box (new Rect (940f, 968f, 650f, 31f), description1[9], MD);
			}
		}

		}
}
