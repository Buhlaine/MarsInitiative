using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XPTracker : MonoBehaviour 
{
	public int maxXP = 100;
	public int killXP = 32;
	public int leftOverXP;
	public int currentXP;
	
	private Dictionary<string, string> playerList = new Dictionary<string, string>();
	
	void StorePlayer(ArrayList _playerInfo)
	{
		playerList.Add (_playerInfo[0].ToString (), _playerInfo[1].ToString ());
	}

	void UpdateXP(ArrayList _data)
	{
		foreach (var str in playerList) {
			Debug.Log (str);
		}

		// unpack array into a string "player", and an int "xp" to be used for checking xp and sending player level update messages / leftover xp
		string player = _data[0].ToString();
		int xp = (int) _data[1];

		currentXP = xp;

		Debug.Log (player.ToString () + " . " + xp);

		if (xp < maxXP) {
			GameObject.Find(player).SendMessage("AddXP", killXP);
		}
		if (xp >= maxXP)
		{
			leftOverXP = currentXP - maxXP;

			Debug.Log(player);
			GameObject.Find(player).SendMessage ("RestartXPCounter", leftOverXP);
			Debug.Log ("Leftover XP heading back to player: " + leftOverXP);
			// This should be sent when the player chooses the ability to level up. Maybe GUI should send this to player?
			GameObject.Find (player).transform.GetChild(0).SendMessage ("Changed");
			GameObject.Find (player).transform.GetChild(1).SendMessage ("Changed");
			GameObject.Find(player).SendMessage ("ReceiveLevelUp");
		}

		// reset
		player = null;
	}
}