using UnityEngine;
using System.Collections;

public class weaponScript : MonoBehaviour {

	public float range = 10000.0f;         //how far the gun can shoot
	public float fireRate = 1.0f;        //how fast the gun shoots
	public float force = 10.0f;          //how much force the gun gives/how much it will push something back
	public int damage = 10;              //damage of the weapon
	public int bulletsPerClip = 15;      //how many bullets are in the clip
	public int clips = 10;               //how many clips the player has
	public float reloadSpeed = 0.5f;      //how long it takes to reload the gun
	private ParticleEmitter hitParticles;//visual feedback of the gun shooting
	public Renderer muzzleFlash;         //used to see the gun firing the bullet
	public float spread = 0.0f;          //used for the base spread of the bullets
	public float maxSpread = 4.0f;       //tell where the max spread will stop
	public bool fullAuto = true;         //switch to turn on and off hold down to fire

	//public AnimationCurve spreadCurve;  //use this to implament curve based accuracy instead of linear

	//variables for upgrades
	public enum statGroups{damage, speed, clipSize, accuracy};
	public int damageUpgrade = 1;
	public float fireRateUpgrade = 1.0f;
	public int bullPerClipUpgrade = 1;
	public float maxSpreadUpgrade = 0.1f;

	private int bulletsLeft = 0;         //store how many bullets left in a clip
	private int looseBullets = 0;        //stores the amount of bullets left in a clip after a manual reload
	private float nextFireTime = 0.0f;   //helps to regulate the rate of fire based on time instead of computer speed
	private int m_LastFrameShot = -1;    //also regulates the fire rate

	// Use this for initialization
	void Start () {
		//Grabs the particles attached to the weapon gameobject
		hitParticles = GetComponentInChildren<ParticleEmitter>();

		//only allows particles to emit if hitting something
		if(hitParticles)
		{
			hitParticles.emit = false;
		}
		bulletsLeft = bulletsPerClip;


	}

	void Update() 
	{
		//fire weapon
		if (fullAuto)
		{
			if (Input.GetButton("Fire1"))
			{
				Fire ();
			}
		}
		else if (!fullAuto)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				Fire ();
			}
		}

		//zoom in
		if (Input.GetButtonDown("Fire2"))
		{
			//send a message to andrews character controller camera script
			gameObject.SendMessageUpwards("zoomIn",SendMessageOptions.DontRequireReceiver);
		}

		//manual reload
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			Reload();
		}

		if (Input.GetKeyDown (KeyCode.E)) 
		{
			Debug.Log(looseBullets);
		}

		if (looseBullets >= bulletsPerClip) 
		{
			looseBullets -= bulletsPerClip;
			clips++;
		}
	}


	void LateUpdate () 
	{
		
		if (muzzleFlash)
		{
			//checks if we shot this frame to produce the muzzle flash
			if (m_LastFrameShot == Time.frameCount)
			{
				//enable the muzzle flash and have the animation align with the rotation
				muzzleFlash.transform.localRotation = Quaternion.AngleAxis(Random.Range(0,359),Vector3.forward);
				muzzleFlash.enabled = true;
				
			

				//checks if there is audio and plays it if there is
				if (audio)
				{
					if (!audio.isPlaying)
					{
						audio.Play();
						audio.loop = true;
					}
				}
			}
			//disables muzzle flash and audio if it's playing
			else 
			{
				muzzleFlash.enabled = false;
				enabled = false;
			
				//stop the audio
				if (audio)
				{
					audio.loop =false;
				}
			}

		}
	}

	//this functions checks if we can fire then does so
	void Fire()
	{
		if (bulletsLeft == 0)
		{
			return;
		}

		//if there is more than one bullet between the last frame and current frame, reset the fire time
		//this is to regulate the fore rate to be based off of time
		if (Time.time - fireRate > nextFireTime)
		{
			nextFireTime = Time.time - Time.deltaTime;
		}

		//keeps the gun firing until the fire time is used up
		while (nextFireTime < Time.time && bulletsLeft != 0)
		{
			FireOneShot();
			nextFireTime += fireRate;
		}
	}

	//called for every bullet shot
	void FireOneShot()
	{
		//simple shot that hits the same point every time
		//Vector3 fireDirection = transform.TransformDirection(Vector3.forward);

		//shoots a forward vector with a randomized spread
		Vector3 fireDirection = transform.TransformDirection(Random.Range(-maxSpread, maxSpread) * spread, Random.Range(-maxSpread, maxSpread) * spread, 1);
		RaycastHit hit;

		//check if something was hit
		if (Physics.Raycast(transform.position, fireDirection,out hit, range))
		{
			//if there is a rigid body apply force to it
			if(hit.rigidbody)
			{
				hit.rigidbody.AddForceAtPosition(force * fireDirection, hit.point);
			}

			//spawn particles at the point the ray hit
			if (hitParticles)
			{
				ParticleEmitter newParticles = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as ParticleEmitter;
				hitParticles.transform.position = hit.point;
				hitParticles.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
				hitParticles.Emit();
				newParticles.GetComponent<ParticleAnimator>().autodestruct = true;
				Destroy(newParticles.gameObject,0.5f);
			}

			//send damage message to the hit object
			hit.collider.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		}

		bulletsLeft--;

		//this tells the LateUpdate function that we shot and to enable the audio and muzzle flash
		m_LastFrameShot = Time.frameCount;
		enabled = true;

		//this allows the gun to reload automatically if the clip is empty
		if (bulletsLeft == 0)
		{
			Reload();
		}


	}


	void Reload()
	{
		//we want the actual reloading of the bullets to wait for the reloadSpeed
		StartCoroutine(Wait(reloadSpeed));

	}

	//gets how many bullets are left in the clip
	public int GetBulletsLeft()
	{
		return bulletsLeft;
	}

	//Coroutine code to wait the reloadSpeed to actually reload
	IEnumerator Wait(float seconds)	
	{
		yield return new WaitForSeconds(seconds);
		if (bulletsLeft > 0) 
		{
			looseBullets += bulletsLeft;
		}
		//check if we have clips to reload
		if(clips > 0)
		{
			clips--;
			bulletsLeft = bulletsPerClip;
		}
		else if (clips == 0 && looseBullets > 0)
		{
			bulletsLeft = looseBullets;
			looseBullets = 0;
		}
	}


	void editStats(int updateStat)
	{

		switch (updateStat)
		{
		case 0:
			damage += damageUpgrade;
			break;
		case 1:
			fireRate += fireRateUpgrade;
			break;
		case 2:
			bulletsPerClip += bullPerClipUpgrade;
			break;
		case 3:
			maxSpread -= maxSpreadUpgrade;
			break;
		case 4:
			break;
		case 5:
			break;
		default:
			break;
		}

		/*if (updateStat == 0)
		{
			damage += damageUpgrade;
		}
		else if (updateStat == 1)
		{
			fireRate += fireRateUpgrade;
		}
		else if (updateStat == 2)
		{
			bulletsPerClip += bullPerClipUpgrade;
		}
		else if (updateStat == 3)
		{
			maxSpread -= maxSpreadUpgrade;
		}*/

	}


} 
