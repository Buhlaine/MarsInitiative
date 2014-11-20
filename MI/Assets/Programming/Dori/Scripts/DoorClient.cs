using UnityEngine;
using System.Collections;

public class DoorClient : MonoBehaviour 
{
	public float doorTimer;
	public bool isOpen;
	public GameObject door;
	private Collider collider;

	void Start()
	{
		doorTimer = 0.0f;
		collider = this.gameObject.GetComponent<Collider> ();
	}

	void Update()
	{
		if(isOpen) {
			doorTimer += 1.0f * Time.deltaTime;
		}

		if(doorTimer >= 10.0f) {
			Reset ();
			doorTimer = 0.0f;
		}
	}

	[RPC]
	void Interact()
	{
		animation.Play ("Open");
		collider.enabled = false;
		// Send RPC to NetworkPeerType
	}

	[RPC]
	void Reset()
	{
		animation.Play ("Close"); 
		collider.enabled = true;
		// Send RPC to NetworkPeerType
	}

	void OnConnectedToServer()
	{
		//TODO Destroy Dummy Door
	}
}