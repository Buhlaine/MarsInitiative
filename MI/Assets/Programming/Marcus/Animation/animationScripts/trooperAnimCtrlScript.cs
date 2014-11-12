using UnityEngine;
using System.Collections;

public class trooperAnimCtrlScript : MonoBehaviour 
{
	
	//[RequireComponent(typeof(Animator))]
	
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
	
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		if(anim.layerCount == 3)
			anim.SetLayerWeight(1,1);
		HasSecondary = false;
		Melee = false;
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
			
			//Temporary code for switching to the secondary weapon
			if(Input.GetKeyDown(KeyCode.F))
			{
				if(!anim.GetBool("HasSecondary"))
				{
					HasSecondary = true;
					anim.SetBool("HasSecondary",HasSecondary);
				}
				else
				{
					HasSecondary = false;
					anim.SetBool("HasSecondary",HasSecondary);
				}
			}
			
			//Temporary code for melee attack
			if(Input.GetButtonDown("Fire2"))
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
			if(Input.GetKey(KeyCode.Q))
			{
				IsZipline = true;
				anim.SetBool("IsZipline",IsZipline);
			}
			else
			{
				IsZipline = false;
				anim.SetBool("IsZipline",IsZipline);
			}
			
		}
	}
	
	
	
	// Synchronizing variables across the network
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		float syncVMovement = 0;
		float syncHMovement= 0;
		bool syncFiring = false;
		bool syncIsAiming = false;
		bool syncIsJumping = false;
		bool syncIsReloading = false;
		bool syncIsCrouching = false;
		bool syncHasSecondary = false;
		bool syncMelee = false;
		bool syncIsZipline = false;
		
		if(stream.isWriting)
		{
			syncVMovement = VMovement;
			syncHMovement = HMovement;
			syncFiring = Firing;
			syncIsAiming = IsAiming;
			syncIsJumping = IsJumping;
			syncIsReloading = IsReloading;
			syncIsCrouching = IsCrouching;
			syncHasSecondary = HasSecondary;
			syncMelee = Melee;
			syncIsZipline = IsZipline;
			
			stream.Serialize(ref syncVMovement);
			stream.Serialize(ref syncHMovement);
			stream.Serialize(ref syncFiring);
			stream.Serialize(ref syncIsAiming);
			stream.Serialize(ref syncIsJumping);
			stream.Serialize(ref syncIsReloading);
			stream.Serialize(ref syncIsCrouching);
			stream.Serialize(ref syncHasSecondary);
			stream.Serialize(ref syncMelee);
			stream.Serialize(ref syncIsZipline);
		}
		else
		{
			stream.Serialize(ref syncVMovement);
			stream.Serialize(ref syncHMovement);
			stream.Serialize(ref syncFiring);
			stream.Serialize(ref syncIsAiming);
			stream.Serialize(ref syncIsJumping);
			stream.Serialize(ref syncIsReloading);
			stream.Serialize(ref syncIsCrouching);
			stream.Serialize(ref syncHasSecondary);
			stream.Serialize(ref syncMelee);
			stream.Serialize(ref syncIsZipline);
			
			VMovement = syncVMovement;
			HMovement = syncHMovement;
			Firing = syncFiring;
			IsAiming = syncIsAiming;
			IsJumping = syncIsJumping;
			IsReloading = syncIsReloading;
			IsCrouching = syncIsCrouching;
			HasSecondary = syncHasSecondary;
			Melee = syncMelee;
			IsZipline = syncIsZipline;
		}
	}
}
