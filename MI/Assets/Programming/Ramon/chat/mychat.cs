﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class mychat : MonoBehaviour {

	public List<string> log;
	public int maxLogMessages = 200;
	public bool visible = false;
	public string stringToEdit;
	public bool selectTextfield = true;
	
	// Use this for initialization
	void Start () {
		//define list
		log = new List<string> ();
		//makes it possible to use keyboard input; dont know how
		Input.eatKeyPressOnTextFieldFocus = true;
		log.Add("Alpha Build 1.0");
		
	}

	void chat( string text){
		//add the text
		log.Add(text);
		//if bigger than maximum delete first text
		if(log.Count>maxLogMessages)
		{
			log.RemoveAt(0);
		}
	}

	//space
	private Vector2 scrollPos =new Vector2(0, 0);
	private int lastLogLen = 0;
	public GUIStyle printGUIStyle;
	public float maxLogLabelHeight = Screen.height*0.0625f;

	void OnGUI(){
		//visible = false;
		//print (visible);


		if (visible){
			//####set the name of the textfield
			GUI.SetNextControlName ("Chatwindow");
			stringToEdit = GUI.TextField (new Rect (0.0f, Screen.height*0.9519231f, Screen.width*0.125f, Screen.height*0.01923077f), stringToEdit, 100);

			//####the width of the text send into the minichat
			float logBoxWidth = Screen.width*0.10625f;

			//####array containing the height of all the texts together
			ArrayList logBoxHeights = new ArrayList();
			//####the height of all the texts together and the height of the scroollbar
			float totalHeight = 0.0f;
			int i = 0;
			float logBoxHeight=0.0f;


			foreach(string logs in log)
			{
				//####checks the height of each text depending on the width in logBoxWidth
				logBoxHeight = Mathf.Min(maxLogLabelHeight, printGUIStyle.CalcHeight(new GUIContent(logs), logBoxWidth));
				//logBoxHeights[i++]=logBoxHeight;
				logBoxHeights.Insert(i++,logBoxHeight);
				totalHeight += logBoxHeight+10.0f;
				//Debug.Log(logs);
			}

			float innerScrollHeight = totalHeight;
//			//####if there's a new message, automatically scroll to bottom
			if(lastLogLen != log.Count)
			{
				scrollPos = new Vector2(0.0f, innerScrollHeight);
				lastLogLen = log.Count;
			}

			//####defines and give the name to the scrollview
			GUI.SetNextControlName ("Deselecting-scroll");
			scrollPos = GUI.BeginScrollView(new Rect(0.0f, Screen.height*0.8076923f, Screen.width*0.125f, Screen.height*0.1442308f), scrollPos, new Rect(0.0f, 0.0f, Screen.width*0.1125f, innerScrollHeight));

			float currY = 0.0f;
			i=0;
			foreach(string logs in log)
			{
				//####define and make the label with the text send by the player
				logBoxHeight=(float)logBoxHeights[i++];
				GUI.Label(new Rect(10, currY, logBoxWidth, logBoxHeight), logs, printGUIStyle);
				//####add 10 of space between each text
				currY+= logBoxHeight+10.0f;
			}

			GUI.EndScrollView();

			if(selectTextfield)
			{
				//####focus on the textfield
				GUI.FocusControl("Chatwindow");
			}
			else
			{
				//####focus on the scrollview which should unfocus
				GUI.FocusControl("Deselecting-scroll");
				//Debug.LogWarning("not selected");
			}
		}
		//Debug.Log("Current event detected: " + Event.current.keyCode);
		if(Event.current.keyCode==KeyCode.Return)
		{
			//Debug.LogWarning ("it started");
			//####if not empty add the text
			if(stringToEdit != "")
			{
				//Input.eatKeyPressOnTextFieldFocus = false;
				chat(this.name+":"+" "+stringToEdit);
				stringToEdit ="";
				StopCoroutine ("disappearanceTime");
				//Input.eatKeyPressOnTextFieldFocus = true;
			}

		}
	}

	IEnumerator disappearanceTime() {
		//Debug.LogWarning ("it started");
		yield return new WaitForSeconds(10);
		//Debug.LogWarning ("fuck");
		visible=false;
	}

	bool inBetween(float value, float min, float max)
	{
		//bool verdade;
		//return verdade=true;
		if(max>value && min<value)
			return true;
		else
			return false;
	}

	// Update is called once per frame
	void Update () {
//		if (visible && !selectTextfield)
//		{
//			StartCoroutine (disappearanceTime ());
//			visible=false;
//		}

		if(Input.GetMouseButtonDown(0))
	   	{
			if(inBetween(Input.mousePosition.x,0.0f,Screen.width*0.125f) && inBetween(Screen.height-Input.mousePosition.y,Screen.height*0.8076923f,Screen.height*0.9711539f) && visible==true)
			{
				//visible=true;
				selectTextfield = true;
				StopCoroutine ("disappearanceTime");
			}
			else
			{
				//####start 10 seconds before the minichat goes away
				StartCoroutine ("disappearanceTime");
				selectTextfield = false;
			}
		}
		//Debug.Log ("Current input detected: " + Input.inputString);	
		if(Input.GetKeyDown("return"))
		{
			//####makes visible and foucus if not
			if(!visible || !selectTextfield)
			{
				visible=true;
				selectTextfield = true;
				StopCoroutine ("disappearanceTime");
			}

		}
		//#### when "`" is pressed makes chat no visible
		if(Input.GetKeyDown(KeyCode.BackQuote))
		{
			visible=false;
			selectTextfield = false;
			//GUI.FocusControl("Deselecting-scroll");
		}
	}

}


