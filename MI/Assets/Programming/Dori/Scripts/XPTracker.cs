using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XPTracker : MonoBehaviour 
{
	public int maxXP = 100;
	public int leftOverXP;
	public int currentXP;
	public int DuckXP;
	public int[] toLevel;
	public int[] xpAmounts;
	
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
		int index = (int) _data [2];
//		string abilityOne = _data[2].ToString();
//		string abilityTwo = _data[3].ToString();

		currentXP = xp;

		if (xp < toLevel[GameObject.Find (player).GetComponent<Player>().level]) {
			GameObject.Find(player).SendMessage("AddXP", xpAmounts[index]);
		}
		if (xp >= toLevel[GameObject.Find (player).GetComponent<Player>().level])
		{
			leftOverXP = currentXP - toLevel[GameObject.Find (player).GetComponent<Player>().level];

			GameObject.Find(player).SendMessage ("RestartXPCounter", leftOverXP);
			// Changed calls CheckStats and is based on currentAbilityLevel in the affected gameObjects. Should happen after the player has chosen
			// an ability to upgrade
//			GameObject.Find (player).transform.Find(abilityOne).SendMessage ("Changed");
//			GameObject.Find (player).transform.Find(abilityTwo).SendMessage ("Changed");

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
//		string abilityOne = _data[2].ToString();
//		string abilityTwo = _data[3].ToString();

		currentXP = xp;
		DuckXP = toLevel[GameObject.Find (player).GetComponent<Player>().level] - currentXP;
		// Changed calls CheckStats and is based on currentAbilityLevel in the affected gameObjects. Should happen after the player has chosen
		// an ability to upgrade
//		GameObject.Find (player).transform.Find(abilityOne).SendMessage ("Changed");
//		GameObject.Find (player).transform.Find(abilityTwo).SendMessage ("Changed");
		GameObject.Find(player).SendMessage ("ReceiveLevelUp");

		// reset
		player = null;
	}
}