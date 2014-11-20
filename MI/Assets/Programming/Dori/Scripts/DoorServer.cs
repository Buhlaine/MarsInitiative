using UnityEngine;
using System.Collections;

public class DoorServer : MonoBehaviour 
{
	public GameObject door;

	void Update()
	{
		if(Network.peerType == NetworkPeerType.Server) {
			Network.Instantiate (door, transform.position, transform.rotation, 0);
		}
	}
}