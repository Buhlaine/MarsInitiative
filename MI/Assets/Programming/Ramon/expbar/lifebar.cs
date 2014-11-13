using UnityEngine;
using System.Collections;

public class lifebar : MonoBehaviour {
	public GUIStyle life;
	public GUIStyle expbar;
	public GUIStyle bomb;
	public int playerL = 9;
	public float currExperience=50;
	private float goalExperience = 100;
	private float expPos;
	public float bombTimer=1;
	private float expSize;
	private float expBegin;


	// Use this for initialization
	void Start () {
	
	}

	void getCurrent( int currexp)
	{
		currExperience = (float)currexp;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerL==1)
		{
			goalExperience=100;
		}
		else if(playerL==2)
		{
			goalExperience=150;
		}
		else if(playerL==3)
		{
			goalExperience=250;
		}
		else if(playerL==4)
		{
			goalExperience=400;
		}
		else if(playerL==5)
		{
			goalExperience=700;
		}
		else if(playerL==6)
		{
			goalExperience=1400;
		}
		else if(playerL==7)
		{
			goalExperience=3000;
		}
		else if(playerL==8)
		{
			goalExperience=5000;
		}
		else if(playerL==9)
		{
			goalExperience=currExperience;
		}

		expSize =Screen.width * 0.5215f;
		expBegin =Screen.width * 0.255f;
		normalizexp (currExperience, goalExperience);
	}

	void normalizexp(float current, float max)
	{
		expPos = (current - 0) / (max - 0);
		//Debug.Log (expPos);
			
		}

	void OnGUI (){

		//LIFE_BAR

		//----------------------------------------------porcentage times the size of the bar makes it move
		GUI.Box (new Rect (expBegin, Screen.height * 0.9365385f, expPos*expSize, Screen.height * 0.025f), "",life);
		//GUI.Box (new Rect (408f, 974f, 834.4f, 26f), "",life);

		//lifebar border
		//GUI.Box (new Rect (260, 934, 1000,106), "", expbar);
		GUI.Box (new Rect (Screen.width * 0.1625f, Screen.height * 0.898077f, Screen.width * 0.625f, Screen.height * 0.1019231f), "", expbar);
		//GUI.Box (new Rect (expBegin, Screen.height * 0.9f, expPos*expSize, Screen.height * 0.05f), "life",life);

//		Debug.LogWarning(((26.0f*100)/Screen.height)/100+"h");
//		Debug.LogWarning(((834.4f*100)/Screen.width)/100+"W");

		}
}
