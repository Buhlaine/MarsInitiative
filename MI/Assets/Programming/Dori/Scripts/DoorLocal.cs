using UnityEngine;
using System.Collections;

public class DoorLocal : MonoBehaviour 
{
	private BoxCollider boxCol;
	private SphereCollider sphereCol;

	void Start() 
	{
		boxCol = this.gameObject.GetComponent<BoxCollider> ();
		sphereCol = this.gameObject.GetComponent<SphereCollider> ();
	}

	void OpenDoor(bool _open)
	{
		if(_open) {
			animation["Take 001"].speed = 1.0f;
			animation.Play("Take 001");
		}

		if (!_open) {
			animation["Take 001"].speed = -1.0f;
			animation["Take 001"].time = animation["Take 001"].length;
			animation.Play ("Take 001");
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "Player") {
			OpenDoor(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player") {
			OpenDoor(false);
		}
	}
}