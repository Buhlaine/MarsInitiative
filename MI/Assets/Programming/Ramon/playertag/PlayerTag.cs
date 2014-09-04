using UnityEngine;
using System.Collections;

public class PlayerTag : MonoBehaviour {

	public bool side;

	public float level = 0;

	public float overhead;

	public float speed;

	//Reference to the global XP manager
	private GameObject XPManager;
	
	void Start () {

		//Find the manager by tag
		//XPManager = GameObject.FindGameObjectWithTag("Manager");
	}

	void Update () {
		//Try to level up
		if(Input.GetKeyDown(KeyCode.Space)){
			//send a request for xp to the global xp manager with a reference back to self
			XPManager.SendMessage("requestXP",this.gameObject);
		}
		//testing
		if(Input.GetKeyDown(KeyCode.R))
		{

		}
		
	}

	void OnGUI()
	{
		//Debug.Log ("1this.renderer.bounds.size.y" + this.renderer.bounds.size.y);
		//Debug.Log ("1this.transform.position" +this.transform.position);
		//Vector3 screenloc = Camera.main.WorldToScreenPoint ((this.transform.position));
		overhead = (this.renderer.bounds.size.y)*0.75f;
		Vector3 headpos = (new Vector3 (0, overhead, 0)) + (this.transform.position);


		//Debug.Log ("this.renderer.bounds.size.y" + this.renderer.bounds.size.y);
		//Debug.Log ("2this.transform.position" +headpos);
		if (this.renderer.isVisible) {
				Vector3 screenloc = Camera.main.WorldToScreenPoint (headpos);
				GUI.Box (new Rect (Mathf.Clamp (screenloc.x, 0, (Screen.width - 100)), Mathf.Clamp ((Screen.height - screenloc.y), 0, Screen.height - 20), 100, 20), "LVL: " + level.ToString ());
			                  Debug.Log (Mathf.Clamp (screenloc.x, 0, Screen.width));
			//Debug.Log ("Y" + this.renderer.isVisible);
			float rightangle = Vector3.Angle(new Vector3(screenloc.x,0.0f,screenloc.z), new Vector3(Camera.main.transform.forward.x,0.0f,Camera.main.transform.forward.z));
			//Debug.Log ("Angle" + rightangle);
			//Debug.DrawLine(headpos,Camera.main.transform.position, Color.red);
			//Gizmos.DrawLine(headpos,Camera.main.WorldToScreenPoint (headpos));
		}
		//else {

			//Vector3 shit = Vector3.RotateTowards (headpos, Camera.main.transform.position, step, 0.0f);
			//Vector3 screenloc = Camera.main.WorldToScreenPoint (headpos);
//			if(screenloc.x>screenloc.x-(Screen.width - 100))
//			{
//				print("fsdfdfdfdfdf"+Mathf.Max(1, 2));
//				screenloc.x +=100;
//				//GUI.Box (new Rect ((Screen.width - 100), Mathf.Clamp (Screen.height - screenloc.y, 0, Screen.height - 20), 100, 20), "LVL: " + level.ToString ());
//				//Debug.Log ("dfdafasdsdasSD");
//			}
//			else
//			{
//				screenloc.x -=100;
//				//GUI.Box (new Rect (0, Mathf.Clamp (Screen.height - screenloc.y, 0, Screen.height - 20), 100, 20), "LVL: " + level.ToString ());
//				//Debug.Log ("dfdafasdsdasSD");
//			}
//			if(screenloc.y>screenloc.y-(Screen.height - 20))
//			{
//				screenloc.y+=100;
//				//GUI.Box (new Rect (xpos, ypos, 100, 20), "LVL: " + level.ToString ());
//				//Debug.Log ("dfdafasdsdasSD");
//			}
//			else
//			{
//				screenloc.y-=100;
//				//GUI.Box (new Rect (Mathf.Clamp (screenloc.x, 0, (Screen.width - 100)), (Screen.height - 20), 100, 20), "LVL: " + level.ToString ());
//				//Debug.Log ("dfdafasdsdasSD");
//			}
//			
//			GUI.Box (new Rect (Mathf.Clamp (screenloc.x, 0, (Screen.width - 100)), Mathf.Clamp (Screen.height - screenloc.y, 0, Screen.height - 20), 100, 20), "LVL: " + level.ToString ());
//			
//		}
		//else {
//			Debug.Log ("screenloc.x"+screenloc.x);
//			Debug.Log ("whichSide" + whichSide(screenloc.x));
//			if(side && screenloc.x<=Screen.width)
//			{
//				Mathf.Clamp(screenloc.x,0,Screen.width);
//				screenloc.x-=Screen.width;
//				Debug.Log ("if1screenloc.x"+screenloc.x);
//				whichSide(screenloc.x);
//				Debug.Log ("if1whichSide" + whichSide(screenloc.x));
//			}
//			else if(!side && screenloc.x<=0)
//			{
//				screenloc.x-=Screen.width;
//				Debug.Log ("if2screenloc.x"+screenloc.x);
//				whichSide(screenloc.x);
//				Debug.Log ("if2whichSide" + whichSide(screenloc.x));
//			}
//			Debug.Log ("screenloc.x"+screenloc.x);
//			Debug.Log ("screenloc.y"+screenloc.y);
//				if(screenloc.y>20 || screenloc.y<(Screen.height-20))
//				{
//					if(screenloc.x<Screen.width/2)
//					{
//						xpos=0;
//
//						ypos=Mathf.Clamp (Screen.height - screenloc.y, 0, Screen.height - 20);
//					}
//					else
//					{
//						xpos=(Screen.width-100);
//						
//						ypos=Mathf.Clamp (Screen.height - screenloc.y, 0, Screen.height - 20);
		//				if(screenloc.y==0)
		//				{
		//					ypos=0;
		//					xpos=Mathf.Clamp (screenloc.x, 0, (Screen.width - 100));
		//				}
		//				else if(screenloc.y==(Screen.height - 20))
		//				{
		//					ypos=(Screen.height - 20);
		//					xpos=Mathf.Clamp (screenloc.x, 0, (Screen.width - 100));
		//				}
						//GUI.Box (new Rect ((Screen.width - 100), Mathf.Clamp (Screen.height - screenloc.y, 0, Screen.height - 20), 100, 20), "LVL: " + level.ToString ());
						//Debug.Log ("dfdafasdsdasSD");
//					}
//				}
//				else
//				{
//					if(screenloc.y<Screen.height/2)
//					{
//						xpos=Mathf.Clamp (screenloc.x, 0, (Screen.width - 100));;
//						
//						ypos=0;
//					}
//					else
//					{
//						xpos=Mathf.Clamp (screenloc.x, 0, (Screen.width - 100));
//						
//						ypos=(Screen.height - 20);
//					}
	//				if(screenloc.y==0)
	//				{
	//					ypos=0;
	//					xpos=Mathf.Clamp (screenloc.x, 0, (Screen.width - 100));
	//				}
	//				else if(screenloc.y==(Screen.height - 20))
	//				{
	//					ypos=(Screen.height - 20);
	//					xpos=Mathf.Clamp (screenloc.x, 0, (Screen.width - 100));
	//				}
					//GUI.Box (new Rect (0, Mathf.Clamp (Screen.height - screenloc.y, 0, Screen.height - 20), 100, 20), "LVL: " + level.ToString ());
					//Debug.Log ("dfdafasdsdasSD");
//				}
	//			if(screenloc.y>Screen.height/2)
	//			{
	//				ypos=(Screen.height - 20);
	//				xpos=Mathf.Clamp (screenloc.x, 0, (Screen.width - 100));
	//				//GUI.Box (new Rect (xpos, ypos, 100, 20), "LVL: " + level.ToString ());
	//				//Debug.Log ("dfdafasdsdasSD");
	//			}
	//			else
	//			{
	//				ypos=0;
	//				xpos=Mathf.Clamp (screenloc.x, 0, (Screen.width - 100));
	//				//GUI.Box (new Rect (Mathf.Clamp (screenloc.x, 0, (Screen.width - 100)), (Screen.height - 20), 100, 20), "LVL: " + level.ToString ());
	//				//Debug.Log ("dfdafasdsdasSD");
	//			}

			//GUI.Box (new Rect (Mathf.Clamp (screenloc.x, 0, (Screen.width - 100)), Mathf.Clamp ((Screen.height - screenloc.y), 0, Screen.height - 20), 100, 20), "LVL: " + level.ToString ());
			//Debug.Log ("X" + this.renderer.isVisible);
				//Debug.Log ("dfdafasdsdasDD"+screenloc.x);
				//Debug.Log (whichSide(screenloc.x));
		
			//}
	}
	
	//receiver for the reply from the global manager;
	void replyXP(float _xp){
		//Make sure the manager replied with enough exp
		if(_xp >= 75f){
			//If there is enough XP, level up
			level += 1;
			//tell the manager you've used some XP
			XPManager.SendMessage("addXP",-75f);
		}
	}
	
	
}
