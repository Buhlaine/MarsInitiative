using UnityEngine;
using System.Collections;

public class guns_display : MonoBehaviour {
	public Texture2D grenade_on;
	public Texture2D grenade_off;
	public GUIStyle display_all;
	public GUIStyle display_one;
	public GUIStyle display_second;
	public GUIStyle[] display_bomb= new GUIStyle[4];
	//public GUIStyle display_bomb_off;
	public int bomb_numb;
	public const int bomb_total=4;



	// Use this for initialization
	void Start () {
		bomb_numb = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if (bomb_numb > 4)
						bomb_numb = 4;

		for(int i=0; i<bomb_numb; i++)
		{
			display_bomb[i].normal.background=grenade_on;
		}
		for(int i=bomb_total-(bomb_total-bomb_numb); i<bomb_total; i++)
		{
			display_bomb[i].normal.background=grenade_off;
		}
	
	}

	void OnGUI() {

		//gun background diplay
		GUI.Box (new Rect (Screen.width-275f, Screen.height-325f, 275f, 340f),"",display_all);
		//GUI.Box (new Rect (0f, 0f, 50f, 50f),"",display_bomb);

		GUI.Box (new Rect (Screen.width-250f, Screen.height-290f, 50f, 50f),"",display_bomb[0]);
		GUI.Box (new Rect (Screen.width-190f, Screen.height-290f, 50f, 50f),"",display_bomb[1]);
		GUI.Box (new Rect (Screen.width-130f, Screen.height-290f, 50f, 50f),"",display_bomb[2]);
		GUI.Box (new Rect (Screen.width-70f, Screen.height-290f, 50f, 50f),"",display_bomb[3]);
		//first gun picture 
		GUI.Box (new Rect (Screen.width-250f, Screen.height-230f, 230f, 100f),"330");
		//second gun picture
		GUI.Box (new Rect (Screen.width-250f, Screen.height-120f, 220f, 100f),"210");
		}
}
