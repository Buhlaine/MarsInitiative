
using UnityEngine;
using System.Collections;

public class start_menu : MonoBehaviour {

	public float fontpercent;
	public GUIStyle backlogo;
	public GUIStyle frontlogo;
	public GUIStyle[] button;
	public GUIStyle MD;
	public GUIStyle BN;
	string[] description1 = new string[10];
	int selection;
	float[] widthpos=new float[11];
	float[] heightpos=new float[11];
	float[] widthdim=new float[5];
	float[] heightdim=new float[5];
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





		//////////////////BUTTONS POSITION//////////////////////////////////////////////
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

		widthpos [5] = 0.65f;//1040
		heightpos [5] = 0.5288461f;

		widthpos [6] = 0.759375f;//1215
		heightpos [6] = 0.5288461f;//549.9999

		widthpos [7] = 0.65f;
		heightpos [7] = 0.5288461f;

		widthpos [8] = 0.65f;
		heightpos [8] = 0.7f;//728

		widthpos [9] = 0.759375f;
		heightpos [9] = 0.7f;//728

		widthpos [10] = 0.65f;
		heightpos [10] = 0.7f;//728
		//////////////////BUTTONS POSITION//////////////////////////////////////////////
		/// 
		/// 
		//////////////////BUTTONS DIMENSIONS//////////////////////////////////////////////
		widthdim [0] = 0.21875f;//350
		heightdim [0] = 0.1442308f;//150

		widthdim [1] = 0.109375f;//175
		heightdim [1] = 0.1442308f;//150
		//////////////////BUTTONS DIMENSIONS//////////////////////////////////////////////
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (0.09375f*Screen.width);
//		Debug.Log (0.5288461f*Screen.height);//549.9999
//		Debug.Log (0.7f*Screen.height);//728
		//960f, 927f, 650f, 31f
		//0.6f, 0.8913462f, 0.8913462f, 0.02980769f
		//Debug.Log (((940f / Screen.width) * 100) / 100);//0.759375
		//Debug.Log (((968f / Screen.height) * 100) / 100);//0.759375
		fontpercent=((widthpos[0]*Screen.width) / 150);
	
	}


	void OnGUI(){
		Vector2 mousePos = new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y);

		GUI.Box (new Rect (0.0f, 0.0f, Screen.width, Screen.height), "", backlogo);

		//GUI.Box (new Rect (640.0f, 150.0f, 700f, 350f), "", frontlogo);
		GUI.Box (new Rect (0.4f*Screen.width, 0.1442308f*Screen.height, 0.4375f*Screen.width, 0.3365385f*Screen.height), "", frontlogo);
		//comment1=960f, 927f, 650f, 31f
		Rect comment1 = new Rect (0.6f * Screen.width, 0.8913462f * Screen.height, 0.8913462f * Screen.width, 0.02980769f * Screen.height);
		//comment2=940f, 968f, 650f, 31f
		Rect comment2 = new Rect (0.5875f * Screen.width, 0.9307692f * Screen.height, 0.8913462f * Screen.width, 0.02980769f * Screen.height);
		Rect Button1=new Rect (widthpos[0]*Screen.width, heightpos[0]*Screen.height, widthdim[0]*Screen.width, heightdim[0]*Screen.height);
		Rect Button2=new Rect (widthpos[1]*Screen.width, heightpos[1]*Screen.height, widthdim[0]*Screen.width, heightdim[0]*Screen.height);
		Rect Button3=new Rect (widthpos[2]*Screen.width, heightpos[2]*Screen.height, widthdim[0]*Screen.width, heightdim[0]*Screen.height);
		Rect Button4=new Rect (widthpos[3]*Screen.width, heightpos[3]*Screen.height, widthdim[0]*Screen.width, heightdim[0]*Screen.height);
		Rect Button5=new Rect (widthpos[4]*Screen.width, heightpos[4]*Screen.height, widthdim[0]*Screen.width, heightdim[0]*Screen.height);
		Rect Button6=new Rect (widthpos[5]*Screen.width, heightpos[5]*Screen.height, widthdim[0]*Screen.width, heightdim[0]*Screen.height);
		Rect Button6BlueHalf=new Rect (widthpos[6]*Screen.width, heightpos[6]*Screen.height, widthdim[1]*Screen.width, heightdim[1]*Screen.height);
		Rect Button6RedHalf=new Rect (widthpos[7]*Screen.width, heightpos[7]*Screen.height, widthdim[1]*Screen.width, heightdim[1]*Screen.height);
		Rect Button7=new Rect (widthpos[8]*Screen.width, heightpos[8]*Screen.height, widthdim[0]*Screen.width, heightdim[0]*Screen.height);
		Rect Button7BlueHalf=new Rect (widthpos[9]*Screen.width, heightpos[9]*Screen.height, widthdim[1]*Screen.width, heightdim[1]*Screen.height);
		Rect Button7RedHalf=new Rect (widthpos[10]*Screen.width, heightpos[10]*Screen.height, widthdim[1]*Screen.width, heightdim[1]*Screen.height);

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
			GUI.Box (comment1, description1[1], MD);
			GUI.Box (comment2, description1[0], MD);
		}
		if(Button2.Contains(mousePos))
		{
			GUI.Box (comment1, description1[2], MD);
			GUI.Box (comment2, description1[0], MD);
		}
		if(Button3.Contains(mousePos))
		{
			GUI.Box (comment1, description1[3], MD);
			GUI.Box (comment2, description1[0], MD);
		}
		if(Button4.Contains(mousePos))
		{
			GUI.Box (comment1, description1[4], MD);
			GUI.Box (comment2, description1[0], MD);
		}
		if(Button5.Contains(mousePos))
		{
			GUI.Box (comment1, description1[5], MD);
			GUI.Box (comment2, description1[0], MD);
		}
		if(Button6.Contains(mousePos))
		{
			GUI.Box (comment1, description1[6], MD);
			GUI.Box (comment2, description1[0], MD);
			if(Button6BlueHalf.Contains(mousePos))
			{
				GUI.Box (comment1, description1[0], MD);
				GUI.Box (comment2, description1[8], MD);
			}
			if(Button6RedHalf.Contains(mousePos))
			{
				GUI.Box (comment1, description1[0], MD);
				GUI.Box (comment2, description1[9], MD);
			}
		}
		if(Button7.Contains(mousePos))
		{
			GUI.Box (comment1, description1[7], MD);
			GUI.Box (comment2, description1[0], MD);
			if(Button7BlueHalf.Contains(mousePos))
			{
				GUI.Box (comment1, description1[0], MD);
				GUI.Box (comment2, description1[8], MD);
			}
			if(Button7RedHalf.Contains(mousePos))
			{
				GUI.Box (comment1, description1[0], MD);
				GUI.Box (comment2, description1[9], MD);
			}
		}

		}
}
