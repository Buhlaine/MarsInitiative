using UnityEngine;
using System.Collections;

public class TrooperBoost : MonoBehaviour 
{
	public int currentAbilityLevel;
	
	void Start()
	{
		currentAbilityLevel = 0;
	}

	void Update()
	{
		if(currentAbilityLevel == 1) {
			// Increased reload and weapon switch speed
		}
		
		if(currentAbilityLevel == 2) {
			// Additional grenade
		}
		
		if(currentAbilityLevel == 3) {
			// Increased health regeneration and infinite stamina
			this.gameObject.transform.parent.SendMessage("IncreaseTrooperStats");
		}
	}
	
	void Changed()
	{
		currentAbilityLevel += 1;

	}
}