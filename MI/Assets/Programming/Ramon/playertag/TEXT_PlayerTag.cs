using UnityEngine;
using System.Collections;

public class TEXT_PlayerTag : MonoBehaviour {

	public GameObject TagToBe;
	public GameObject TagObj;
	public TextMesh Tag;
	public float overhead;
	Vector3 headpos;

	public float level=7;
	public string playerName="LEROY_JENKINS";

	// Use this for initialization
	void Start () {
		checkHeadpos ();
		TagObj = Instantiate (TagObj, headpos, Quaternion.identity) as GameObject;
		Tag=TagObj.GetComponent<TextMesh> ();

	
	}
	void checkHeadpos(){
		overhead = (this.renderer.bounds.size.y)*0.75f;
		headpos = (new Vector3 (0, overhead, 0)) + (this.transform.position);

		}
	
	// Update is called once per frame
	void Update () {
		checkHeadpos ();
		TagObj.transform.position = headpos;
		//TagObj.transform.LookAt (Camera.main.transform.position, Vector3.up);
		TagObj.transform.rotation= Quaternion.LookRotation (TagObj.transform.position - Camera.main.transform.position);
		Tag.text = playerName + " \\ " + level;
	
	}
}
