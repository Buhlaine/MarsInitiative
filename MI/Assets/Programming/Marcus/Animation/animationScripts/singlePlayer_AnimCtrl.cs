using UnityEngine;
using System.Collections;

public class singlePlayer_AnimCtrl : MonoBehaviour 
{
	private Animator anim;
	private AnimatorStateInfo currentBaseState;
	private AnimatorStateInfo primaryWeaponLayer;
	private AnimatorStateInfo secondaryWeaponLayer;
	
	private float VMovement;
	private float HMovement;
	private bool Firing;
	private bool IsAiming;
	private bool IsJumping;
	private bool IsReloading;
	private bool IsCrouching;
	private bool HasSecondary;
	private bool Melee;
	private bool IsZipline;
	private bool IsDead;
	private bool HasCapsule;
	private bool BroFist;
	
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		if(anim.layerCount == 4)
			anim.SetLayerWeight(1,1);
		HasSecondary = false;
		Melee = false;
		HasCapsule = false;
		BroFist = false;
		IsZipline = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void FixedUpdate()
	{
		if(networkView.isMine)
		{
			//setting the vertical and horizontal movement values for the animator
			VMovement = Input.GetAxis("Vertical");
			HMovement = Input.GetAxis("Horizontal");
			anim.SetFloat("verticalMovement",VMovement);
			anim.SetFloat("horizontalMovement",HMovement);
			
			
			//play the shooting animation until you let go of the key
			if(Input.GetButton("Fire1"))
			{
				Firing = true;
				anim.SetBool("Firing",Firing);
			}
			else
			{
				Firing = false;
				anim.SetBool("Firing",Firing);
			}
			
			//activate the aiming bool for animations
			if(Input.GetButton("Fire2"))
			{
				IsAiming = true;
				anim.SetBool("IsAiming",IsAiming);
			}
			else
			{
				IsAiming = false;
				anim.SetBool("IsAiming",IsAiming);
			}
			
			//play the jumping animation
			if(Input.GetButtonDown("Jump"))
			{
				IsJumping = true;
				anim.SetBool("IsJumping",IsJumping);
			}
			else
			{
				IsJumping = false;
				anim.SetBool("IsJumping",IsJumping);
			}
			
			//play the reloading animation
			if(Input.GetKeyDown(KeyCode.R))
			{
				IsReloading = true;
				anim.SetBool("IsReloading",IsReloading);
			}
			else
			{
				IsReloading = false;
				anim.SetBool("IsReloading",IsReloading);
			}
			
			//Bro Fist!
			if(Input.GetKeyDown(KeyCode.F))
			{
				BroFist = true;
				anim.SetBool("BroFist",BroFist);
			}
			else
			{
				BroFist = false;
				anim.SetBool("BroFist",BroFist);
			}
			
			//play the crouching animation & enable crouching anim branch
			if(Input.GetKey(KeyCode.LeftShift))  //change the key to what is on the character controller, ask andrew
			{
				IsCrouching = true;
				anim.SetBool("IsCrouching",IsCrouching);
			}
			else
			{
				IsCrouching = false;
				anim.SetBool("IsCrouching",IsCrouching);
			}
			
			
			anim.SetBool("HasSecondary",HasSecondary);
			
			
			
			//Temporary code for melee attack
			if(Input.GetButtonDown("Fire3"))
			{
				if(!anim.GetBool("Melee"))
				{
					Melee = true;
					anim.SetBool("Melee",Melee);
				}
				
			}
			else
			{
				Melee = false;
				anim.SetBool("Melee",Melee);
			}
			
			//Temporary play the zipline Animation
			if(IsZipline == true)
			{
				anim.SetBool("IsZipline",IsZipline);
			}
			else
			{
				anim.SetBool("IsZipline",IsZipline);
			}
			
			//if the player dies play annimation
			if(IsDead == true)
			{
				anim.SetBool("IsDead",IsDead);
				anim.SetBool("IsDead",false);
			}
			
			if(HasCapsule == true)
			{
				anim.SetBool("HasCapsule",HasCapsule);
			}
			else
			{
				anim.SetBool("HasCapsule",HasCapsule);
			}
			
		}
	}
	
	public void Zipline(bool activate)
	{
		IsZipline = activate;
	}
	
	public void Dead(bool activate)
	{
		IsDead = activate;
	}
	
	public void Capsule(bool activate)
	{
		HasCapsule = activate;
	}
	
	public void ZiplineStart()
	{
		IsZipline = true;
	}
	
	public void ZiplineStop()
	{
		IsZipline = false;
	}
	
	public void SwitchToSecondary()
	{
		HasSecondary = true;
	}
	
	public void SwitchFromSecondary()
	{
		HasSecondary = false;
	}

}
