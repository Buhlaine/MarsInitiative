using UnityEngine;
using System.Collections;

public class lifebar : MonoBehaviour {
	public GUIStyle life;
	public GUIStyle bomb;
	private float currExperience=100;
	private float goalExperience = 789;
	private float expPos;
	private float bombTimer=1;
	public float lifesize;
	public float lifeBegin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifesize =Screen.width * 0.7f;
		lifeBegin =Screen.width * 0.15f;
		normalizexp (currExperience, goalExperience);
	}

	void normalizexp(float current, float max)
	{
		expPos = (current - 0) / (goalExperience - 0);
		//Debug.Log (expPos);
			
		}

	void OnGUI (){

		//LIFE_BAR

		//----------------------------------------------porcentage times the size of the bar makes it move
		GUI.Box (new Rect (lifeBegin, Screen.height * 0.9f, expPos*lifesize, Screen.height * 0.05f), "life",life);
		//lifebar border
		GUI.Box (new Rect (Screen.width * 0.15f, Screen.height * 0.9f, Screen.width * 0.7f, Screen.height * 0.05f), "death");

		//BOMB_BAR

		//top
		//---------------------------------------toppart plus whatever was taken from height so it keeps going down-------------------porcentage times the hieght makes it decrease
		GUI.Box (new Rect (Screen.width * 0.1f, ((1-bombTimer)*(Screen.height * 0.10f))+(Screen.height * 0.55f), Screen.width * 0.05f,bombTimer*(Screen.height * 0.10f)),"bomb",bomb);
		Debug.Log (1 + (bombTimer * bombTimer));

		//bottom
		//----------------------------------------------------------------------------------porcentage times the height makes it decrease; in this case come up
		GUI.Box (new Rect (Screen.width * 0.1f, Screen.height * 0.65f, Screen.width * 0.05f, bombTimer*(Screen.height * 0.10f)),"bomb",bomb);
		GUI.Box (new Rect (Screen.width * 0.1f, Screen.height * 0.55f, Screen.width * 0.05f, Screen.height * 0.20f),"bomb");

		}
}
