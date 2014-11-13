using UnityEngine;
using System.Collections;

public class playerFlagMngr : MonoBehaviour
{

	public float speed = 10.0f;
	private bool carryFlag = false;
	private int exp = 0;
	//string teamColor;
	bool isAlive = true;
	GameObject carriedFlag;
	bool displayThis = false;


	// Use this for initialization
	void Start () 
	{
//		if(this.tag == "Red")
//		{
//			teamColor = "Red";
//			//enemyColor = "Blue";
//		}
//		else if(this.tag == "Blue")
//		{
//			teamColor = "Blue";
//			//enemyColor = "Red";
//		}

		//GameObject[] flagObjs = GameObject.FindGameObjectsWithTag ("Flag");
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if(networkView.isMine)
//		{
			if(this.tag == "Red")
			{
				this.transform.Translate (Vector3.right * speed * Input.GetAxis ("Horizontal") * Time.deltaTime);
				this.transform.Translate (Vector3.forward* speed * Input.GetAxis ("Vertical") * Time.deltaTime);
			}
			else if(this.tag == "Blue")
			{
				if(Input.GetKey(KeyCode.I))
				{
					this.transform.Translate (Vector3.forward * speed * Time.deltaTime);
				}
				if(Input.GetKey(KeyCode.J))
				{
					this.transform.Translate (Vector3.left* speed * Time.deltaTime);
				}
				if(Input.GetKey(KeyCode.K))
				{
					this.transform.Translate (Vector3.back * speed * Time.deltaTime);
				}
				if(Input.GetKey(KeyCode.L))
				{
					this.transform.Translate (Vector3.right* speed * Time.deltaTime);
				}
			}
			//checking if player is carrying flag
			if(Input.GetMouseButtonDown(1))
			{
				Debug.Log(carryFlag);
			}

			//testing for dropped flag
//			if(Input.GetKeyDown(KeyCode.X))
//			{
//				isAlive = false;
//			}
//			if(Input.GetKeyDown(KeyCode.Z))
//			{
//				isAlive = true;
//			}
		//}
		//checking if dead
		if(Input.GetKeyDown(KeyCode.X))
		{
			//checking if carrying
			if(carryFlag)
			{
				RaycastHit hit;
				Physics.Raycast(this.transform.position, Vector3.down, out hit, 10.0f);
				networkView.RPC("flagDropped",RPCMode.All, hit.point);
				networkView.RPC("notCarrying", RPCMode.All);
				//networkView.RPC("displayChange", RPCMode.All);
			}
		}
	}

	void addExp(int expAmount)
	{
		exp += expAmount;
	}

	void obtainedFlag()
	{
		carryFlag = true;
	}

	[RPC]
	void notCarrying()
	{
		carryFlag = false;
	}

	void isCarrying(GameObject senderObj)
	{
		if (carryFlag == true)
		{
			senderObj.SendMessage("capFlagRequest", this.gameObject as GameObject);
		}
	}

	void setCarriedFlag(GameObject enemyFlag)
	{
		this.carriedFlag = enemyFlag;
	}

    void setLife()
    {
        isAlive = true;
    }

    void setLifeOff()
    {
        isAlive = false;
    }

    void areYouAlive(GameObject _requestor)
    {
        if (isAlive)
        {
            _requestor.SendMessage("followPlayer", this.gameObject as GameObject);
            _requestor.SendMessage("flagPickedUp");
            carryFlag = true;
        }
    }

}
