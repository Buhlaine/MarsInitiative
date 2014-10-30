using UnityEngine;
using System.Collections;

/* Script that keeps track of how many points each team has from
 * flag captures. */

public class PointTracker : MonoBehaviour 
{
	public int redTeamPoints;
	public int blueTeamPoints;
	public int capturePoints;

	public bool addingPoints;

	void Start()
	{
		redTeamPoints = 0;
		blueTeamPoints = 0;

		capturePoints = 1;
	}

	public void AddPoints(string _team)
	{
		// Will this keep it from happening more than once?
		if(_team == "Blue" && !addingPoints) {
			addingPoints = true;
			blueTeamPoints += capturePoints;
			addingPoints = false;
			// TODO Send message to GUI to display points
		}
		if(_team == "Red" && !addingPoints) {
			addingPoints = true;
			redTeamPoints += capturePoints;
			addingPoints = false;
			// TODO Send message to GUI to display points
		}
	}
}