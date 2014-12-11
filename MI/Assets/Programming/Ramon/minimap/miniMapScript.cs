using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class miniMapScript : MonoBehaviour {

	public GUIStyle MiniMap;//for the map texture
	
	//public Transform this.transform;//player location on world
	//public Transform enemy;//enemy location on world
	
	public GUIStyle playerIcon;//player location on map
	public GUIStyle enemyIcon;//enemy location on map
	public GUIStyle allyIcon;//ally location on map
	public GUIStyle mapstyle;
	
	public float mapMiddleX;//middle of the minimap
	public float mapMiddleY;
	
	public float mapWidth = 200.0f;//size of the minimap
	public float mapHeight = 200.0f;
	
	public float sceneWidth = 500.0f;//resolution of the terrain
	public float sceneHeight = 500.0f;
	
	public float playerIconWidth;
	public float playerIconHeight;
	
	public float iconSize = 10;
	private float iconHalfSize;
	public float angle = 0;//amount of rotation to the minimap
	Rect mapPos;// map rect
	Rect iconPos;//player's icon rect
	Vector2 pivot;//pivot for the rotation of the minimap
	public Texture2D map = null;
	public Texture2D mapBorder = null;
	
	
	//finding enemies stuff
	//public ArrayList enemies;
	GameObject[] enemy;

	//finding allies stuff
	//public ArrayList allies;
	GameObject[] ally;
	
	string visibleEnemyOnMap;
	
	// Use this for initialization
	void Start () {
		iconHalfSize = iconSize / 2;
		//findEnemies();
		Debug.LogWarning("starttttttttttttttt");
		
	}
	
	void findEnemies()
	{
		if(this.GetComponent<Player>().team=="Red")
		{
			Debug.LogWarning("REEDDDDDDDDDDDDD");
			enemy = GameObject.FindGameObjectsWithTag ("Blue");

			ally = GameObject.FindGameObjectsWithTag ("Red");
		}
		else if(this.GetComponent<Player>().team=="Blue")
		{
			Debug.LogWarning("BLUEEEEEEEEEEEEE");
			enemy = GameObject.FindGameObjectsWithTag ("Red");
			
			ally = GameObject.FindGameObjectsWithTag ("Blue");
		}

		Debug.LogWarning("NOTHINNGGGGGGGGGGGGGGG");
		//for(int i=0; i<enemy.Length; i++)
		//Debug.Log (enemy[i].transform.name);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("spwnManager").GetComponent<spawnManagerScript>().gameBegin)
		{
			findEnemies();
		}
		Debug.LogWarning("starttttttttttttttt");
		playerIconWidth = (0.9f * Screen.width);
		playerIconHeight = (0.1f * Screen.height);
		mapMiddleY = playerIconHeight + iconHalfSize;
		mapMiddleX = playerIconWidth + iconHalfSize;
		
	}
	
	float GetMapPos( float pos, float MapSize, float sceneSize)// it gets the scale between the terrain and the map then finds player position
	{
		return pos * (MapSize / sceneSize);
	}
	
	void OnGUI()
	{
		Rect mapParameter = new Rect (((mapMiddleX) - (mapWidth / 4)), ((mapMiddleY) - (mapHeight / 4)), mapWidth / 2, mapHeight / 2);
		GUI.BeginGroup (new Rect (((mapMiddleX)-(mapWidth/4)), ((mapMiddleY)-(mapHeight/4)) , mapWidth/2, mapHeight/2), mapstyle);
		//----------------middle - halfwidth = right border-|||--middle - halfwidth = top border
		//GUI.Box (new Rect (1, 1 , iconSize, iconSize), "", enemyIcon);
		iconPos = new Rect ((mapMiddleX)- (mapMiddleX - (mapWidth / 4))-iconHalfSize,(mapMiddleY)-(mapMiddleY - (mapHeight / 4))-iconHalfSize, iconSize, iconSize);//defining
		//------------------distance between middle and border-------|||--- distance between middle and top
		//Debug.Log ("sdfssfdfdsfdsd" + ((mapMiddleHeight+iconHalfSize)-((mapMiddleHeight+iconHalfSize) - (mapHeight / 4))));
		
		pivot = new Vector2(iconPos.xMin + iconPos.width * 0.5f, iconPos.yMin + iconPos.height * 0.5f);
		//defining pivot as the middle of the icon
		angle = -this.transform.transform.rotation.eulerAngles.y;//defining angle as the same roatation as the player
		
		//player's icon position
		float px = GetMapPos (this.transform.position.x, mapWidth, sceneWidth);
		float pz = GetMapPos (this.transform.position.z, mapHeight, sceneHeight);
		float playerMapx = px ;//
		float playerMapz = ((pz * -1.0f)) + mapHeight;
		
		
		mapPos = new Rect (((mapMiddleX)-((mapMiddleX) - (mapWidth / 4)))-playerMapx,((mapMiddleY)-((mapMiddleY) - (mapHeight / 4))-playerMapz), mapWidth, mapHeight);
		//(middle - halfwidth = right border)-player's translationX= map moving on x axis--|||||--(middle - halfwidth = top border)-player's translationZ wolrd=map moving on y axis screen
		//mapPos = new Rect ((mapMiddleWidth), (mapMiddleHeight), mapWidth, mapHeight);
		
		
		//makes rotate. how?
		Matrix4x4 matrixBackup = GUI.matrix;
		GUIUtility.RotateAroundPivot(angle, pivot);
		GUI.DrawTexture (mapPos, map);//makes map

		if(GameObject.FindGameObjectWithTag("spwnManager").GetComponent<spawnManagerScript>().gameBegin)
		{
			//enemy
			if(enemy.Length>0)
			{
				for(int i=0; i<enemy.Length; i++)
				{
					if(enemy[i].name==visibleEnemyOnMap)
					{
						//enemy's icon position
						float ex = GetMapPos (enemy[i].transform.position.x, mapWidth, sceneWidth);
						float ez = GetMapPos (enemy[i].transform.position.z, mapHeight, sceneHeight);
						float enemyMapx = ((ex - iconHalfSize)+((mapMiddleX)-((mapMiddleX) - (mapWidth / 4)))-playerMapx);
						// playericonwidth= so it starts at the same place|||playermapx=so it move with the map
						float enemyMapz = ((((ez * -1.0f) - iconHalfSize) + mapHeight)+((mapMiddleY)-((mapMiddleY) - (mapHeight / 4))-playerMapz));

						GUI.Box (new Rect (enemyMapx, enemyMapz , iconSize, iconSize), "", enemyIcon);

					}//enemy icon
				}
			}

			//ally
			if(ally.Length>0)
			{
				for(int i=0; i<ally.Length; i++)
				{
						//ally's icon position
						float ex = GetMapPos (ally[i].transform.position.x, mapWidth, sceneWidth);
						float ez = GetMapPos (ally[i].transform.position.z, mapHeight, sceneHeight);
						float enemyMapx = ((ex - iconHalfSize)+((mapMiddleX)-((mapMiddleX) - (mapWidth / 4)))-playerMapx);
						// playericonwidth= so it starts at the same place|||playermapx=so it move with the map
						float enemyMapz = ((((ez * -1.0f) - iconHalfSize) + mapHeight)+((mapMiddleY)-((mapMiddleY) - (mapHeight / 4))-playerMapz));
						
						GUI.Box (new Rect (enemyMapx, enemyMapz , iconSize, iconSize), "", allyIcon);
						
				}
			}
		}
		GUI.matrix = matrixBackup;
		//GUI.DrawTexture (new Rect (0, 0, mapWidth, mapHeight), texture2);
		
		
		GUI.Box (iconPos, "", playerIcon);//player icon
		
		
		GUI.EndGroup ();
		//takes the diagonal of the map square
		float mapDiagonal=Mathf.Sqrt(Mathf.Pow(mapParameter.width,2)+Mathf.Pow(mapParameter.height,2));

		//using the diagonal of the map define the size of the circle border so it doesnt so the corners
		GUI.DrawTexture (new Rect (mapParameter.xMin-((mapDiagonal-mapParameter.width)), mapParameter.yMin-((mapDiagonal-mapParameter.height)), mapParameter.width+(mapDiagonal-mapParameter.width)*2f, mapParameter.height+(mapDiagonal-mapParameter.height)*2f), mapBorder);
		
	}
	
	void visibleEnemy( string enename)
	{
		visibleEnemyOnMap = enename;
	}
	
}
