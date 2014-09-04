using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XPTracker : MonoBehaviour 
{
	public int maxXP = 100;
	public int killXP = 32;
	public int leftOverXP;
	public int currentXP;
	public int DuckXP;
	
	private Dictionary<string, string> playerList = new Dictionary<string, string>();
	
	void StorePlayer(ArrayList _playerInfo)
	{
		playerList.Add (_playerInfo[0].ToString (), _playerInfo[1].ToString ());
	}

	void UpdateXP(ArrayList _data)
	{
		// unpack array into a string "player", and an int "xp" to be used for checking xp and sending player level update messages / leftover xp
		string player = _data[0].ToString();
		int xp = (int) _data[1];

		currentXP = xp;

		if (xp < maxXP) {
			GameObject.Find(player).SendMessage("AddXP", killXP);
		}
		if (xp >= maxXP)
		{
			leftOverXP = currentXP - maxXP;

			GameObject.Find(player).SendMessage ("RestartXPCounter", leftOverXP);
			// Changed calls CheckStats and is based on currentAbilityLevel in the affected gameObjects. Should happen after the player has chosen
			// an ability to upgrade
			GameObject.Find (player).transform.GetChild(0).SendMessage ("Changed");
			GameObject.Find (player).transform.GetChild(1).SendMessage ("Changed");

			GameObject.Find(player).SendMessage ("ReceiveLevelUp"); // General level ... NOT ability level
		}

		// reset
		player = null;
	}

	void CompleteLevel(ArrayList _data)
	{
		// For killing the duck
		string player = _data [0].ToString ();
		int xp = (int) _data [1];

		currentXP = xp;
		DuckXP = maxXP - currentXP;
		// Changed calls CheckStats and is based on currentAbilityLevel in the affected gameObjects. Should happen after the player has chosen
		// an ability to upgrade
		GameObject.Find (player).transform.GetChild(0).SendMessage ("Changed");
		GameObject.Find (player).transform.GetChild(1).SendMessage ("Changed");
		GameObject.Find(player).SendMessage ("ReceiveLevelUp");

		// reset
		player = null;
	}
}