using UnityEngine;
using System.Collections;

public class heavyAnimCtrlScript : MonoBehaviour {

	//[RequireComponent(typeof(Animator))]

	private Animator anim;
	private AnimatorStateInfo currentBaseState;
	private AnimatorStateInfo primaryWeaponLayer;
	private AnimatorStateInfo secondaryWeaponLayer;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		if(anim.layerCount == 3)
			anim.SetLayerWeight(1,1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void FixedUpdate()
	{
		//setting the vertical and horizontal movement values for the animator
		float VMovement = Input.GetAxis("Vertical");
		float HMovement = Input.GetAxis("Horizontal");
		anim.SetFloat("verticalMovement",VMovement);
		anim.SetFloat("horizontalMovement",HMovement);


		//play the shooting animation until you let go of the key
		if(Input.GetButton("Fire1"))
		{
			anim.SetBool("Firing",true);
		}
		else
		{
			anim.SetBool("Firing",false);
		}

		//activate the aiming bool for animations
		if(Input.GetButton("Fire2"))
		{
			anim.SetBool("IsAiming",true);
		}
		else
		{
			anim.SetBool("IsAiming",false);
		}

		//play the jumping animation
		if(Input.GetButtonDown("Jump"))
		{
			anim.SetBool("IsJumping",true);
		}
		else
		{
			anim.SetBool("IsJumping",false);
		}

		//play the reloading animation
		if(Input.GetKeyDown(KeyCode.R))
		{
			anim.SetBool("IsReloading",true);
		}
		else
		{
			anim.SetBool("IsReloading",false);
		}

		//play the crouching animation & enable crouching anim branch
		if(Input.GetKey(KeyCode.LeftShift))  //change the key to what is on the character controller, ask andrew
		{
			anim.SetBool("IsCrouching",true);
		}
		else
		{
			anim.SetBool("IsCrouching",false);
		}

		//Temporary code for switching to the secondary weapon
		if(Input.GetKeyDown(KeyCode.F))
		{
			if(!anim.GetBool("HasSecondary"))
			{
				anim.SetBool("HasSecondary",true);
			}
			else
			{
				anim.SetBool("HasSecondary",false);
			}
		}

		//Temporary code for melee attack
		if(Input.GetKeyDown(KeyCode.X))
		{
			if(!anim.GetBool("Melee"))
			{
				anim.SetBool("Melee",true);
			}

		}
		else
		{
			anim.SetBool("Melee",false);
		}

		//Temporary play the zipline Animation
		if(Input.GetKey(KeyCode.Q))
		{
			anim.SetBool("IsZipline",true);
		}
		else
		{
			anim.SetBool("IsZipline",false);
		}

	}
}
