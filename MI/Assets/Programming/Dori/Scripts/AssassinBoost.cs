using UnityEngine;
using System.Collections;

public class AssassinBoost : MonoBehaviour 
{
	public int currentAbilityLevel;
	public bool canMark;
	
	void Start()
	{
		currentAbilityLevel = 0;
		canMark = false;
	}

	void Update()
	{
		// TODO Ask design team if Z is okay to use for marking enemies / ability three activiation
		RaycastHit hit;

		if (Input.GetKeyDown (KeyCode.Z) && canMark) {
			if(Physics.Raycast(transform.position, Vector3.forward, out hit, 10.0f)) {
				if(hit.transform.tag == "Enemy") {
					this.gameObject.SendMessage("visibleEnemy", hit.transform.name);
				}
			}
		}

		if(currentAbilityLevel == 1) {
			this.gameObject.transform.parent.SendMessage ("IncreaseSprintSpeed");
		}
		
		if(currentAbilityLevel == 2) {
			canMark = true;
		}
		
		if(currentAbilityLevel == 3) {
			// TODO Deals more damage with bullets
		}
	}
	
	void Changed()
	{
		currentAbilityLevel += 1;
	}
}
