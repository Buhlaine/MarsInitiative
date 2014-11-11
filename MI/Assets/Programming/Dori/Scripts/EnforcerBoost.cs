using UnityEngine;
using System.Collections;

public class EnforcerBoost : MonoBehaviour 
{
	public int currentAbilityLevel;

	void Start()
	{
		currentAbilityLevel = 0;
	}

	void Update()
	{
		if(currentAbilityLevel == 1) {
			// Increased Clip Size
		}
		
		if(currentAbilityLevel == 2) {
			// Faster rate of fire for Chain Gun
		}
		
		if(currentAbilityLevel == 3) {
			// Gains more health
			this.gameObject.transform.parent.SendMessage("IncreaseMaxHealth");
		}
	}

	void Changed()
	{
		currentAbilityLevel += 1;
	}
}