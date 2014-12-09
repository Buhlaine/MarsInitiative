using UnityEngine;
using System.Collections;

public class DoorServer : MonoBehaviour 
{
	public GameObject door;

	void Start()
	{
		LaunchServer();
	}

	void LaunchServer() 
	{
		bool useNat = !Network.HavePublicAddress();
		Network.InitializeServer(32, 25000, useNat);

		Network.Instantiate(door, transform.position, transform.rotation, 0);

		if(Network.peerType == NetworkPeerType.Server) {
			Debug.Log ("Destroy the mother door!");
			Destroy(this.gameObject);
		}
	}
}