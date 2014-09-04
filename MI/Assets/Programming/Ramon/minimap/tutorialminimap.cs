using UnityEngine;
using System.Collections;

public class tutorialminimap : MonoBehaviour {

	public GUIStyle MiniMap;//for the map texture
	
	public Transform player;//player location on world
	public Transform enemy;//enemy location on world
	
	public GUIStyle playerIcon;//player location on map
	public GUIStyle enemyIcon;//enemy location on map
	
	public float mapOffSetX = 762.0f;
	public float mapOffSetY = 510.0f;
	
	public float mapWidth = 200.0f;
	public float mapHeight = 200.0f;
	
	public float sceneWidth = 500.0f;//resolution of the terrain
	public float sceneHeight = 500.0f;
	
	public int iconSize = 10;
	private int iconHalfSize;
	public float angle = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		iconHalfSize = iconSize / 2;
	
	}

	float GetMapPos( float pos, float MapSize, float sceneSize)
	{
		return pos * (MapSize / sceneSize);
	}

	void OnGUI()
	{
		GUI.BeginGroup (new Rect (mapOffSetX, mapOffSetY, mapWidth, mapHeight), MiniMap);
		float px = GetMapPos (player.position.x, mapWidth, sceneWidth);
		float pz = GetMapPos (player.position.z, mapHeight, sceneHeight);
		float playerMapx = px - iconHalfSize;
		float playerMapz = ((pz * -1.0f) - iconHalfSize) + mapHeight;
		GUI.Box (new Rect (playerMapx, playerMapz, iconSize, iconSize), "", playerIcon);

		float ex = GetMapPos (enemy.position.x, mapWidth, sceneWidth);
		float ez = GetMapPos (enemy.position.z, mapHeight, sceneHeight);
		float enemyMapx = ex - iconHalfSize;
		float enemyMapz = ((ez * -1.0f) - iconHalfSize) + mapHeight;
		GUI.Box (new Rect (enemyMapx, enemyMapz, iconSize, iconSize), "", enemyIcon);
		
				GUI.EndGroup ();

		}
}
