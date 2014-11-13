using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class upgrade_screen : MonoBehaviour {




	public Texture2D dd;
	bool look_ass_abilities;
	//the upgrade window style
	public GUIStyle upgrade_W;
	//player level style
	public GUIStyle player_L;
	//level descripton style
	public GUIStyle PLD;
	//player level
	public float currentLevel;
	//player name
	public string playerName;
	//player class type
	public string className;
	//player team
	public bool team;
	//is upgrade window visble or not
	public bool upgrading;

	//number of points to upgrade
	public int skillpoints;

	//next level description
	string[] next1level=new string[4];
	string[] next2level=new string[4];
	string[] next3level=new string[4];
	//classes upgrade windows
	public Texture2D[] classes_W=new Texture2D[6];
	//ability group 1
	public int abili1Level;
	//ability group 2
	public int abili2Level;
	//ability group 3
	public int abili3Level;

	//ability group 1 style array
	public GUIStyle[] ability_1=new GUIStyle[4];
	//ability group 2 style array
	public GUIStyle[] ability_2=new GUIStyle[4];
	//ability group 3 style array
	public GUIStyle[] ability_3=new GUIStyle[4];

	//Assassin Textures abilities array
	public Texture2D[] assi_abiltex1=new Texture2D[4];
	public Texture2D[] assi_abiltex2=new Texture2D[4];
	public Texture2D[] assi_abiltex3=new Texture2D[4];

	//Enforcer Textures abilities array
	public Texture2D[] enfo_abiltex1=new Texture2D[4];
	public Texture2D[] enfo_abiltex2=new Texture2D[4];
	public Texture2D[] enfo_abiltex3=new Texture2D[4];

	//Trooper Textures abilities array
	public Texture2D[] troo_abiltex1=new Texture2D[4];
	public Texture2D[] troo_abiltex2=new Texture2D[4];
	public Texture2D[] troo_abiltex3=new Texture2D[4];


	public Dictionary<int, Texture2D> player_abil1 = new Dictionary<int, Texture2D> ();
	//public Texture2D[] assi_abiltex1;

	public Dictionary<int, Texture2D> player_abil2 = new Dictionary<int, Texture2D> ();
	//public Texture2D[] assi_abiltex2;

	public Dictionary<int, Texture2D> player_abil3 = new Dictionary<int, Texture2D> ();
	//public Texture2D[] assi_abiltex3;

	public Texture2D[][] player_abiltextures=new Texture2D[4][];
	public Dictionary<int, Texture2D>[] player_abilities = new Dictionary<int, Texture2D>[4];

	public Dictionary<int, Dictionary<int,Texture2D>> playerAbilityMainDictionary = new Dictionary<int, Dictionary<int, Texture2D>> ();


	public Dictionary<int, string> playerclass = new Dictionary<int, string> ();



	// Use this for initialization
	void Start () {

		playerclass.Add (0, "trooper");
		playerclass.Add (1, "enforcer");
		playerclass.Add (2, "assassin");

		upgrading = false;
		//setting the ability dictonaries
		player_abilities [1] = player_abil1;
		player_abilities [2] = player_abil2;
		player_abilities [3] = player_abil3;

		ArrayList h = new ArrayList ();
		h.Add ("sdfsdfsdf");
		h.Add (7);
		h.Add (2);
		h.Add (3);
		levelChecking (h);
		team = true;
		abili1Level = 0;
		abili2Level = 0;
		abili3Level = 0;

	}


	void classSetup()
	{
		if(className == "assassin")
		{
			//setting the texture2D[] to the texture2D[][]
			player_abiltextures [1] = assi_abiltex1;
			player_abiltextures [2] = assi_abiltex2;
			player_abiltextures [3] = assi_abiltex3;
			
			next1level[0]="Chain Shot 1 - Chain Shot causes your next shot to damage enemies near your target";
			next1level[1]="Chain Shot 2 - Chain Shot deals increased damage and has a faster cooldown";
			next1level[2]="Chain Shot 3 - Chain Shot deals even more damage and an even faster cooldown";
			next1level[3]="MAX";
			
			next2level[0]="Conceal 1 - Conceal masks the presence of the Assassin";
			next2level[1]="Conceal 2 - Conceal can be shared with a teammate";
			next2level[2]="Conceal 3 - Conceal lasts longer, and increases your next melee damage";
			next2level[3]="MAX";
			
			next3level[0]="Boost 1 - Assassin gains increased sprint speed";
			next3level[1]="Boost 2 - Assassin can mark enemies by aiming at them";
			next3level[2]="Boost 3 - Assassin deals more damage with his bullets";
			next3level[3]="MAX";
		}
		
		else if(className == "enforcer")
		{
			//setting the texture2D[] to the texture2D[][]
			player_abiltextures [1] = enfo_abiltex1;
			player_abiltextures [2] = enfo_abiltex2;
			player_abiltextures [3] = enfo_abiltex3;
			
			
			next1level[0]="Fortify 1 - Fortify creates a shield that blocks projectiles";
			next1level[1]="Fortify 2 - Fortify covers a large area";
			next1level[2]="Fortify 3 - Fortify lasts longer";
			next1level[3]="MAX";
			
			next2level[0]="Charge 1 - Charge thrusts you forward, killing enemies in your path";
			next2level[1]="Charge 2 - Charge covers an increased distance";
			next2level[2]="Charge 3 - Charge gains a faster cooldown";
			next2level[3]="MAX";
			
			next3level[0]="Boost 1 - Heavy gains an increased clip size on his weapons";
			next3level[1]="Boost 2 - Heavy gains a faster rate of fire on his Chain Gun";
			next3level[2]="Boost 3 - Heavy gains more health";
			next3level[3]="MAX";
		}
		
		else if(className == "trooper")
		{
			//setting the texture2D[] to the texture2D[][]
			player_abiltextures [1] = troo_abiltex1;
			player_abiltextures [2] = troo_abiltex2;
			player_abiltextures [3] = troo_abiltex3;
			
			next1level[0]="Restock 1 - Restock regenerate health and ammo";
			next1level[1]="Restock 2 - Restock gains a radius to provide bonuses to nearby allies";
			next1level[2]="Restock 3 - Restock lasts longer and heals faster";
			next1level[3]="MAX";
			
			next2level[0]="Pulse Radar 1 - Pulse Radar marks enemies while sending out damaging pulses";
			next2level[1]="Pulse Radar 2 - Pulse Radar lasts longer and pulses deals more damage";
			next2level[2]="Pulse Radar 3 - Pulse Radar detects enemies and a greater range, and pulses deal more damage";
			next2level[3]="MAX";
			
			next3level[0]="Boost 1 - Trooper gains increased reload and weapon switch speed";
			next3level[1]="Boost 2 - Trooper gains an additional grenade";
			next3level[2]="Boost 3 - Trooper gains increased health regeneration and infinite stamina";
			next3level[3]="MAX";
		}

		//j=number of abilities//3
		for(int j = 1; j<=3; j++)
		{//i=number of levels//4
			for(int i = 0; i<4; i++)
			{
				if(player_abilities[j].ContainsKey(i))
				{
					//ONLY FOR ERRORDebug.LogWarning("SOMETHING IS WRONG GO FIX IT!");
					//1Debug.Log("j"+j);
					//						Debug.Log(assi_abilities[j][i]);
				}
				else
				{//set the rigt level texture to the right ability dictionary in the right order
					player_abilities [j].Add (i, player_abiltextures[j][i]);
					//1Debug.Log("class"+j+"texture"+i);
				}
				
				//ass.Add (j, assi_abil*+j);
			}
			if(playerAbilityMainDictionary.ContainsKey(j))
			{
				//ONLY FOR ERRORDebug.LogWarning("SOMETHING IS WRONG GO FIX IT!");
				//1Debug.Log(j+"j");
			}
			else//set all the texture from the ability dictionaries to the player dicttonary
				playerAbilityMainDictionary.Add (j, player_abilities [j]);
		}



	}

	[RPC]
	void levelChecking( ArrayList playerInfo)
	{
		playerName = playerInfo[0].ToString();
		currentLevel = (int) playerInfo[1];
		className = playerclass[(int) playerInfo [2]];
		skillpoints = (int) playerInfo [3];
		classSetup ();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Tab))
			upgrading = true;
		else if(Input.GetKeyUp(KeyCode.Tab))
			upgrading=false;

	
	}

	void OnGUI(){
		//setting GUIStyle
		PLD.normal.textColor = new Color32 (255, 255, 255, 255);
		PLD.fontSize = 26;
		PLD.alignment = TextAnchor.MiddleCenter;
		PLD.wordWrap = true;

		player_L.normal.textColor = new Color32(254,245,147,255);
		player_L.fontSize = 85; 

		if(upgrading)
		{
			//THE WHOLE UPGRADE WINDOW
			GUI.Box (new Rect (10.0f, 10.0f, Screen.width - 20.0f, Screen.height - 20.0f), "", upgrade_W);

			//Debug.LogWarning(className);
			if(className == "assassin")
			{
				//Debug.LogWarning("SOMETHING IS WRONG GO FIX IT!");
				if(team==true)
					upgrade_W.normal.background = classes_W[0];
				else
					upgrade_W.normal.background= classes_W[1];
			}
			else if(className == "enforcer")
			{
				//Debug.LogWarning("SOMETHING IS WRONG GO FIX IT!");
				if(team==true)
					upgrade_W.normal.background = classes_W[2];
				else
					upgrade_W.normal.background= classes_W[3];
			}
			else if(className == "trooper")
			{
				//Debug.LogWarning("SOMETHING IS WRONG GO FIX IT!");
				if(team==true)
					upgrade_W.normal.background = classes_W[4];
				else
					upgrade_W.normal.background= classes_W[5];
			}

				//CHECKING LEVEL OF FISRT ABILITY-------------------------------------------------------------------------
				if(playerAbilityMainDictionary.ContainsKey(1) && abili1Level==0)
				{
					if( GUI.Button (new Rect (0.2784063f*Screen.width, 0.1038462f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next1level[abili1Level], PLD) && skillpoints>0)
					{
						skillpoints--;
						abili1Level++;
						gameObject.SendMessage("AbilityOneLevelUp");
						//Debug.Log("skillpoints" + skillpoints);
						//Debug.Log("abili1Level" + abili1Level);
					}
					//Debug.Log("dssfsssdfdsddfs");
					ability_1[1].normal.background = playerAbilityMainDictionary[1][0];
					ability_1[2].normal.background = playerAbilityMainDictionary[1][0];
					ability_1[3].normal.background = playerAbilityMainDictionary[1][0];
				}
				else if(playerAbilityMainDictionary.ContainsKey(1) && abili1Level==1)
				{
					if (GUI.Button (new Rect (0.2784063f*Screen.width, 0.1038462f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next1level[abili1Level], PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili1Level++;
						gameObject.SendMessage("AbilityOneLevelUp");
					}
					//Debug.Log("dssfsssdfdsddfs");
					ability_1[1].normal.background = playerAbilityMainDictionary[1][1];
					ability_1[2].normal.background = playerAbilityMainDictionary[1][0];
					ability_1[3].normal.background = playerAbilityMainDictionary[1][0];
				}
				else if(playerAbilityMainDictionary.ContainsKey(1) && abili1Level==2)
				{
					if( GUI.Button (new Rect (0.2784063f*Screen.width, 0.1038462f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next1level[abili1Level], PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili1Level++;
						gameObject.SendMessage("AbilityOneLevelUp");
					}
					//Debug.Log("dssfsssdfdsddfs");
					ability_1[1].normal.background = playerAbilityMainDictionary[1][1];
					ability_1[2].normal.background = playerAbilityMainDictionary[1][2];
					ability_1[3].normal.background = playerAbilityMainDictionary[1][0];
				}
				else if(playerAbilityMainDictionary.ContainsKey(1) && abili1Level==3)
				{
					if( GUI.Button (new Rect (0.2784063f*Screen.width, 0.1038462f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next1level[abili1Level], PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili1Level++;
						gameObject.SendMessage("AbilityOneLevelUp");
					}
					//Debug.Log("dssfsssdfdsddfs");
					ability_1[1].normal.background = playerAbilityMainDictionary[1][1];
					ability_1[2].normal.background = playerAbilityMainDictionary[1][2];
					ability_1[3].normal.background = playerAbilityMainDictionary[1][3];
				}
				//--------------------------------------------------------------------------------------------------------------------------

				//CHECKING LEVEL OF SECOND ABILITY-------------------------------------------------------------------------
				if(playerAbilityMainDictionary.ContainsKey (2) && abili2Level==0)
				{
					if( GUI.Button (new Rect (0.2784063f*Screen.width, 0.2115385f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next2level[abili2Level],PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili2Level++;
						gameObject.SendMessage("AbilityTwoLevelUp");
					}
					//Debug.Log(playerAbilityMainDictionary[2][1].name);
					ability_2[1].normal.background = playerAbilityMainDictionary[2][0];
					ability_2[2].normal.background = playerAbilityMainDictionary[2][0];
					ability_2[3].normal.background = playerAbilityMainDictionary[2][0];
				}
				else if(playerAbilityMainDictionary.ContainsKey (2) && abili2Level==1)
				{
					if( GUI.Button (new Rect (0.2784063f*Screen.width, 0.2115385f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next2level[abili2Level],PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili2Level++;
						gameObject.SendMessage("AbilityTwoLevelUp");
					}
					//Debug.Log(playerAbilityMainDictionary[2][1].name);
					ability_2[1].normal.background = playerAbilityMainDictionary[2][1];
					ability_2[2].normal.background = playerAbilityMainDictionary[2][0];
					ability_2[3].normal.background = playerAbilityMainDictionary[2][0];
				}
				else if(playerAbilityMainDictionary.ContainsKey (2) && abili2Level==2)
				{
					if(GUI.Button (new Rect (0.2784063f*Screen.width, 0.2115385f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next2level[abili2Level],PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili2Level++;
						gameObject.SendMessage("AbilityTwoLevelUp");
					}
					//Debug.Log(playerAbilityMainDictionary[2][1].name);
					ability_2[1].normal.background = playerAbilityMainDictionary[2][1];
					ability_2[2].normal.background = playerAbilityMainDictionary[2][2];
					ability_2[3].normal.background = playerAbilityMainDictionary[2][0];
				}
				else if(playerAbilityMainDictionary.ContainsKey (2) && abili2Level==3)
				{
					if( GUI.Button (new Rect (0.2784063f*Screen.width, 0.2115385f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next2level[abili2Level],PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili2Level++;
						gameObject.SendMessage("AbilityTwoLevelUp");
					}
					//Debug.Log(ass[2][1].name);
					ability_2[1].normal.background = playerAbilityMainDictionary[2][1];
					ability_2[2].normal.background = playerAbilityMainDictionary[2][2];
					ability_2[3].normal.background = playerAbilityMainDictionary[2][3];
				}
				//-------------------------------------------------------------------------------------------------------------------

				//CHECKING LEVEL OF THIRD ABILITY-------------------------------------------------------------------------
				if(playerAbilityMainDictionary.ContainsKey(3) && abili3Level==0)
				{
					if( GUI.Button (new Rect (0.2784063f*Screen.width, 0.3192308f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next3level[abili3Level],PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili3Level++;
						gameObject.SendMessage("AbilityThreeLevelUp");
					}
					ability_3[1].normal.background = playerAbilityMainDictionary[3][0];
					ability_3[2].normal.background = playerAbilityMainDictionary[3][0];
					ability_3[3].normal.background = playerAbilityMainDictionary[3][0];
				}
				else if(playerAbilityMainDictionary.ContainsKey(3) && abili3Level==1)
				{
					if( GUI.Button (new Rect (0.2784063f*Screen.width, 0.3192308f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next3level[abili3Level],PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili3Level++;
						gameObject.SendMessage("AbilityThreeLevelUp");
					}
					ability_3[1].normal.background = playerAbilityMainDictionary[3][1];
					ability_3[2].normal.background = playerAbilityMainDictionary[3][0];
					ability_3[3].normal.background = playerAbilityMainDictionary[3][0];
				}
				else if(playerAbilityMainDictionary.ContainsKey(3) && abili3Level==2)
				{
					if( GUI.Button (new Rect (0.2784063f*Screen.width, 0.3192308f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next3level[abili3Level],PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili3Level++;
						gameObject.SendMessage("AbilityThreeLevelUp");
					}
					ability_3[1].normal.background = playerAbilityMainDictionary[3][1];
					ability_3[2].normal.background = playerAbilityMainDictionary[3][2];
					ability_3[3].normal.background = playerAbilityMainDictionary[3][0];
				}
				else if(playerAbilityMainDictionary.ContainsKey(3) && abili3Level==3)
				{
					if (GUI.Button (new Rect (0.2784063f*Screen.width, 0.3192308f*Screen.height, 0.28125f*Screen.width, 0.09615385f*Screen.height), next3level[abili3Level],PLD)  && skillpoints>0)
					{
						skillpoints--;
						abili3Level++;
						gameObject.SendMessage("AbilityThreeLevelUp");
					}
					ability_3[1].normal.background = playerAbilityMainDictionary[3][1];
					ability_3[2].normal.background = playerAbilityMainDictionary[3][2];
					ability_3[3].normal.background = playerAbilityMainDictionary[3][3];
				}
				//----------------------------------------------------------------------------------------------------------------




			//player's level
			GUI.Box (new Rect (0.14375f*Screen.width, 0.1442308f*Screen.height, 0.07625f*Screen.width, 0.1201923f*Screen.height), currentLevel.ToString(), player_L);
			
			//PLAYER'S NAME
			Vector2 cobt = player_L.CalcSize (new GUIContent(playerName));
			GUI.Box (new Rect (0.74375f*Screen.width-(cobt.x/2), 0.7980769f*Screen.height, cobt.x, 0.1201923f*Screen.height), playerName, player_L);


//			Debug.LogWarning(((830.0f*100)/Screen.height)/100+"h");
//			Debug.LogWarning(((1190f*100)/Screen.width)/100+"W");
			//Debug.LogWarning(0.1038462f*Screen.height);

			//first ability level one
			GUI.Box (new Rect ((0.6302813f*Screen.width), (0.2423077f*Screen.height), (0.076875f*Screen.width), 0.1201923f*Screen.height), "",ability_1[1]);

			//first ability level two
			GUI.Box (new Rect (0.72375f*Screen.width, 0.1038462f*Screen.height, (0.076875f*Screen.width), 0.1201923f*Screen.height), "",ability_1[2]);

			//first ability level tree

			GUI.Box (new Rect ((0.8177812f*Screen.width), (0.2423077f*Screen.height), (0.076f*Screen.width), 0.1201923f*Screen.height), "",ability_1[3]);


			//second ability level one
			GUI.Box (new Rect (0.2784063f*Screen.width, 0.5115384f*Screen.height, (0.076875f*Screen.width), 0.1201923f*Screen.height), "",ability_2[1]);

			//second ability level two
			GUI.Box (new Rect (0.371875f*Screen.width, 0.6490384f*Screen.height, (0.076875f*Screen.width), 0.1201923f*Screen.height), "",ability_2[2]);

			//second ability level tree
			GUI.Box (new Rect (0.4659063f*Screen.width, 0.5115384f*Screen.height, (0.076875f*Screen.width), 0.1201923f*Screen.height), "",ability_2[3]);




			//third ability level one
			GUI.Box (new Rect ((0.6302813f*Screen.width), 0.5115384f*Screen.height, (0.076875f*Screen.width), 0.1201923f*Screen.height), "",ability_3[1]);

			//third ability level two
			GUI.Box (new Rect (0.72375f*Screen.width, 0.6490384f*Screen.height, (0.076875f*Screen.width), 0.1201923f*Screen.height), "",ability_3[2]);

			//third ability level tree
			GUI.Box (new Rect ((0.8177812f*Screen.width), 0.5115384f*Screen.height, (0.076875f*Screen.width), 0.1201923f*Screen.height), "",ability_3[3]);
		}
	}
}