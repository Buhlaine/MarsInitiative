using UnityEngine;
using System.Collections;

public class DoorClient : MonoBehaviour 
{
	private BoxCollider collider;

	void Start()
	{
		collider = this.gameObject.GetComponent<BoxCollider>();

		ConnectToServer();
	}

	void ConnectToServer() {
		Network.Connect("127.0.0.1", 25000);
	}

	[RPC]
	void OpenDoor(bool colliderOff)
	{
		animation.Play ();
		collider.enabled = colliderOff;
		networkView.RPC("OpenDoor", RPCMode.All, colliderOff);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player") {
			OpenDoor(false);
		}
	}
}