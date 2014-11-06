using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class misc_text : MonoBehaviour {

	public string stringToEdit;
	public GameObject one;
	public GameObject two;
	public int maxEventLog=3;
	public int events=5;
	public List<string> event_log;




	// Use this for initialization
	void Start () {
		event_log = new List<string> ();
	
	}

	void add_event( string text){
		//add the text
		event_log.Add(text);
		//if bigger than maximum delete first text
		Debug.Log (event_log.Count);
		if(event_log.Count>maxEventLog)
		{
			event_log.RemoveAt(0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("return"))
		{
			UpperCornerEvent (one, 1, two);
			UpperCornerEvent (one, 4);
		}
		//UpperCornerEvent (one, "killed", two);
		//eventTypes.KILL;
		//Debug.Log (eventTypes);
	
	}

	void UpperCornerEvent(GameObject doer, int _event, GameObject doee)

	{
			switch (_event) 
			{
			case 1:
				stringToEdit = ">> " + doer.name + " KILLED " + doee;
				break;
			case 2:
				stringToEdit = ">> " + doer.name + " EXPLODED " + doee;
				break;
			case 3:
				stringToEdit = ">> " + doer.name + " BROFISTED " + doee;
				break;
			case 4:
				stringToEdit = ">> " + doer.name + _event + doee;
				break;
			case 5:
				stringToEdit = ">> " + doer.name + _event + doee;
				break;
			}
		add_event (stringToEdit);
		StartCoroutine ("disappearanceTime");
		//GUI.Label (new Rect (0.0f, Screen.height * 0.9519231f, Screen.width * 0.125f, Screen.height * 0.01923077f), stringToEdit);
	}
		void UpperCornerEvent(GameObject doer, int _event)
		{
				switch (_event) 
				{
				case 4:
					stringToEdit = ">> " + doer.name + " LEVEL UP!!!!";
					break;
				case 5:
					stringToEdit = ">> " + doer.name + " COMMITTED SUICIDE.";
					break;
				case 6:
					stringToEdit = ">> " + doer.name + " KILLED THE DUCK!";
					break;
				case 7:
					stringToEdit = ">> " + doer.name + " HAS THE FLAG";
					break;
				}
		add_event (stringToEdit);
		StartCoroutine ("disappearanceTime");
		//GUI.Label (new Rect (0.0f, Screen.height * 0.9519231f, Screen.width * 0.125f, Screen.height * 0.01923077f), stringToEdit);

		}

	IEnumerator disappearanceTime() {
		//Debug.LogWarning ("it started");
		yield return new WaitForSeconds(20);
		//Debug.LogWarning ("fuck");
		if(event_log.Count>0)
		{
			Debug.Log (event_log.Count);
			event_log.RemoveAt(0);
		}

	}

	public GUIStyle eventStyle;
	public GUIStyle behindStyle;
	public float maxLogLabelHeight = Screen.height*0.0625f;

	void OnGUI ()
	{
		if(event_log.Count>0)
		{

			float logBoxWidth = Screen.width*0.10625f;

			ArrayList logBoxHeights = new ArrayList();

			float totalHeight = 0.0f;
			int i = 0;
			float logBoxHeight=0.0f;

			foreach(string logs in event_log)
			{
				//####checks the height of each text depending on the width in logBoxWidth
				logBoxHeight = Mathf.Min(maxLogLabelHeight, eventStyle.CalcHeight(new GUIContent(logs), logBoxWidth));
				//logBoxHeights[i++]=logBoxHeight;
				logBoxHeights.Insert(i++,logBoxHeight);
				totalHeight += logBoxHeight+20.0f;
				//Debug.Log(logs);
			}

			GUI.BeginGroup (new Rect (Screen.width*0.01f, Screen.height*0.01f, Screen.width * 0.1125f, totalHeight), behindStyle);

			//float currY = 15.0f;
			float currY = Screen.height*0.01442308f;
			//Debug.Log((15.0f/Screen.height)*100);
			//Debug.Log(Screen.height*0.01442308f);
			i=0;
			foreach(string logs in event_log)
			{
				//####define and make the label with the text send by the player
				logBoxHeight=(float)logBoxHeights[i++];
				GUI.Label(new Rect(4, currY, logBoxWidth, logBoxHeight), logs, eventStyle);
				//####add 10 of space between each text
				currY+= logBoxHeight+(Screen.height*0.01442308f);
			}

			GUI.EndGroup ();
		}

	}
}
