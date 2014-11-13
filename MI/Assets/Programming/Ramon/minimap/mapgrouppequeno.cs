using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mapgrouppequeno : MonoBehaviour {

	public GUIStyle MiniMap;//for the map texture

	//public Transform this.transform;//player location on world
	//public Transform enemy;//enemy location on world

	public GUIStyle playerIcon;//player location on map
	public GUIStyle enemyIcon;//enemy location on map
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
	public GameObject[] enemy;

	// Use this for initialization
	void Start () {
		iconHalfSize = iconSize / 2;
		findEnemies();
	
	}

	void findEnemies()
	{
		enemy = GameObject.FindGameObjectsWithTag ("Enemy");
		//for(int i=0; i<enemy.Length; i++)
			//Debug.Log (enemy[i].transform.name);

		}
	
	// Update is called once per frame
	void Update () {
		playerIconWidth = (0.9f * Screen.width);
		playerIconHeight = (0.1f * Screen.height);
		mapMiddleY = playerIconHeight + iconHalfSize;
		mapMiddleX = playerIconWidth + iconHalfSize;
		//playerIconWidth = (0.9f * Screen.width);
		//playerIconHeight = (0.1f * Screen.height);
//		float px = GetMapPos (player.position.x, mapWidth, sceneWidth);
//		float pz = GetMapPos (player.position.z, mapHeight, sceneHeight);
//		float playerMapx = px - iconHalfSize;
//		float playerMapz = ((pz * -1.0f) - iconHalfSize) + mapHeight;
//		mapPos = new Rect ((mapOffSetX-playerMapx), (mapOffSetY-playerMapz), mapWidth, mapHeight);
//		iconPos = new Rect (862.0f, 610.0f, iconSize, iconSize);
//		iconHalfSize = iconSize / 2;
//		pivot = new Vector2(iconPos.xMin + iconPos.width * 0.5f, iconPos.yMin + iconPos.height * 0.5f);
//		angle = -player.transform.rotation.eulerAngles.y;
//		print (player.transform.rotation.eulerAngles.y + "fffddf");
	
	}

	float GetMapPos( float pos, float MapSize, float sceneSize)// it gets the scale between the terrain and the map then finds player position
	{
		return pos * (MapSize / sceneSize);
	}

	void OnGUI()
	{
		Rect mapParameter = new Rect (((mapMiddleX) - (mapWidth / 4)), ((mapMiddleY) - (mapHeight / 4)), mapWidth / 2, mapHeight / 2);
		GUI.BeginGroup (mapParameter, mapstyle);
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

		for(int i=0; i<enemy.Length; i++)
		{
			//enemy's icon position
			float ex = GetMapPos (enemy[i].transform.position.x, mapWidth, sceneWidth);
			float ez = GetMapPos (enemy[i].transform.position.z, mapHeight, sceneHeight);
			float enemyMapx = ((ex - iconHalfSize)+((mapMiddleX)-((mapMiddleX) - (mapWidth / 4)))-playerMapx);
			// playericonwidth= so it starts at the same place|||playermapx=so it move with the map
			float enemyMapz = ((((ez * -1.0f) - iconHalfSize) + mapHeight)+((mapMiddleY)-((mapMiddleY) - (mapHeight / 4))-playerMapz));

//		//makes rotate. how?
//		Matrix4x4 matrixBackup = GUI.matrix;
//		GUIUtility.RotateAroundPivot(angle, pivot);
//		GUI.DrawTexture (mapPos, texture);//makes map
		//GUI.DrawTexture (new Rect (0, 0, mapWidth, mapHeight), texture2);
		//GUI.DrawTexture(new Rect(((mapMiddleX)-((mapMiddleX) - (mapWidth / 4)))-playerMapx,((mapMiddleY)-((mapMiddleY) - (mapHeight / 4))-playerMapz),200,200), texture);
		//print (enemyMapx + "sdsdsdf" + enemyMapz + "sdfsdfdsf" + iconSize + "sdfsdf" + iconSize);
			GUI.Box (new Rect (enemyMapx, enemyMapz , iconSize, iconSize), "", enemyIcon);//enemy icon
		}
		GUI.matrix = matrixBackup;
		//GUI.DrawTexture (new Rect (0, 0, mapWidth, mapHeight), texture2);


		GUI.Box (iconPos, "", playerIcon);//player icon
		//GUI.DrawTexture (new Rect (((mapMiddleX) - (mapWidth / 4)), ((mapMiddleY) - (mapHeight / 4)), mapWidth / 2, mapHeight / 2), mapBorder);

		//Debug.Log (((playerIconWidth+5)-((playerIconWidth+5) - (mapWidth / 4))) +"="+ (((playerIconWidth + 5) + (mapWidth / 4))-(playerIconWidth+5)));
		//print (((playerIconHeight+5)-((playerIconHeight+5) - (mapHeight / 4)))+ "="+ (((playerIconHeight + 5) - (mapHeight / 4))-(playerIconHeight+5)));

//		GUI.BeginGroup (new Rect (mapOffSetX, mapOffSetY, mapWidth, mapHeight));
//		float px = GetMapPos (player.position.x, mapWidth, sceneWidth);
//		float pz = GetMapPos (player.position.z, mapHeight, sceneHeight);
//		float playerMapx = px - iconHalfSize;
//		float playerMapz = ((pz * -1.0f) - iconHalfSize) + mapHeight;


		GUI.EndGroup ();
		float mapDiagonal=Mathf.Sqrt(Mathf.Pow(mapParameter.width,2)+Mathf.Pow(mapParameter.height,2));
		//GUI.DrawTexture (new Rect (((mapMiddleX) - (mapWidth / 4))-20.71067811865475f, ((mapMiddleY) - (mapHeight / 4))-20.71067811865475f, (mapWidth / 2)+41.4213562373095f, (mapHeight / 2)+41.4213562373095f), mapBorder);
		GUI.DrawTexture (new Rect (mapParameter.xMin-((mapDiagonal-mapParameter.width)/2), mapParameter.yMin-((mapDiagonal-mapParameter.height)/2), mapParameter.width+(mapDiagonal-mapParameter.width), mapParameter.height+(mapDiagonal-mapParameter.height)), mapBorder);
		Debug.Log (mapDiagonal+"jkl");
		Debug.Log (mapParameter.xMax);
		//GUI.DrawTexture (new Rect (((mapMiddleX)-(mapWidth/4))-(mapWidth/4), ((mapMiddleY)-(mapHeight/4)-(mapHeight/4)) , mapWidth, mapHeight), texture2);
		//GUI.DrawTexture (new Rect (((mapMiddleX)-(mapWidth/4))-(mapWidth/4), 0 , mapWidth, mapHeight), texture2);


	}
}
