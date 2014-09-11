using UnityEngine;
using System.Collections;

public class map : MonoBehaviour {

	public GUIStyle MiniMap;//for the map texture

	public Transform player;//player location on world
	public Transform enemy;//enemy location on world

	public GUIStyle playerIcon;//player location on map
	public GUIStyle enemyIcon;//enemy location on map

	//public float mapOffSetX = 862.0f;//where the minimap begins
	//public float mapOffSetY = 610.0f;

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
	public Texture2D texture = null;

	// Use this for initialization
	void Start () {
		iconHalfSize = iconSize / 2;
	
	}
	
	// Update is called once per frame
	void Update () {
		playerIconWidth = (0.9f * Screen.width);
		playerIconHeight = (0.1f * Screen.height);
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
		GUI.BeginGroup (new Rect (0, 0, Screen.width, Screen.height));
		GUI.Box (new Rect (Screen.width*0.8f, Screen.height*0.8f , iconSize, iconSize), "", enemyIcon);
		iconPos = new Rect (playerIconWidth, playerIconHeight, iconSize, iconSize);//defining


		pivot = new Vector2(iconPos.xMin + iconPos.width * 0.5f, iconPos.yMin + iconPos.height * 0.5f);//defining
		angle = -player.transform.rotation.eulerAngles.y;//defining as the same roatation as the player

		//player's icon position
		float px = GetMapPos (player.position.x, mapWidth, sceneWidth);
		float pz = GetMapPos (player.position.z, mapHeight, sceneHeight);
		float playerMapx = px - iconHalfSize;//
		float playerMapz = ((pz * -1.0f) - iconHalfSize) + mapHeight;


		mapPos = new Rect ((playerIconWidth-playerMapx), (playerIconHeight-playerMapz), mapWidth, mapHeight);

		//enemy's icon position
		float ex = GetMapPos (enemy.position.x, mapWidth, sceneWidth);
		float ez = GetMapPos (enemy.position.z, mapHeight, sceneHeight);
		float enemyMapx = ((ex - iconHalfSize)+(playerIconWidth-playerMapx));
		float enemyMapz = ((((ez * -1.0f) - iconHalfSize) + mapHeight)+(playerIconHeight-playerMapz));

		//makes rotate. how?
		Matrix4x4 matrixBackup = GUI.matrix;
		GUIUtility.RotateAroundPivot(angle, pivot);
		GUI.DrawTexture(mapPos, texture);//makes map
		//print (enemyMapx + "sdsdsdf" + enemyMapz + "sdfsdfdsf" + iconSize + "sdfsdf" + iconSize);
		GUI.Box (new Rect (enemyMapx, enemyMapz , iconSize, iconSize), "", enemyIcon);//enemy icon
		GUI.matrix = matrixBackup;


		GUI.Box (iconPos, "", playerIcon);//player icon

		GUI.Box (new Rect (((playerIconWidth+5)-(mapWidth/4)), ((playerIconHeight+5)-(mapHeight/4)) , mapWidth/2, mapHeight/2), "");

		//Debug.Log ((((playerIconWidth+5)-(playerIconWidth+5) - (mapWidth / 4))) +"="+ (((playerIconWidth + 5) + (mapWidth / 4))-(playerIconWidth+5)));
		//print (((playerIconHeight+5)-((playerIconHeight+5) - (mapHeight / 4)))+ "="+ (((playerIconHeight + 5) - (mapHeight / 4))-(playerIconHeight+5)));

//		GUI.BeginGroup (new Rect (mapOffSetX, mapOffSetY, mapWidth, mapHeight));
//		float px = GetMapPos (player.position.x, mapWidth, sceneWidth);
//		float pz = GetMapPos (player.position.z, mapHeight, sceneHeight);
//		float playerMapx = px - iconHalfSize;
//		float playerMapz = ((pz * -1.0f) - iconHalfSize) + mapHeight;


		GUI.EndGroup ();


	}
}
