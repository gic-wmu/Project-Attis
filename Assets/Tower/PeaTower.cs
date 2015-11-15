using UnityEngine;
using System.Collections;
//Error when enemy has been slain
//Need a more elgant way to do TargetExists method
//Stop coroutine shoot before it's updated if target does not exist

public class PeaTower : MonoBehaviour 
{
	public GameObject bulletObject;
	public float timeDelay = 1;
	private Collider target = null;

	private bool targetLast = false;

	void Update () {}
	
	void OnTriggerEnter(Collider co) //When the target gets in range, start shooting at it
	{
		if ((co.tag == "Creep") && !IsInvoking("Shoot"))
		{
			target = co;
			Invoke("Shoot", 0);
		}
	}
	
	void OnTriggerLeave(Collider co) //When target leaves, stop shooting at it
	{
		if(co == target)
			StopCoroutine("Shoot");
	}
	
	void OnTriggerStay(Collider co) //As the target is in range keep shooting at it
	{
		if ((co.tag == "Creep") && !IsInvoking("Shoot"))
		{
			target = co;
			Invoke("Shoot", timeDelay);
		}
	}

	private bool TargetExists(Collider co) //Detects if collider does not exist
	{
		try
		{
			return co.attachedRigidbody != null;
		}
		catch(MissingReferenceException)
		{
			return false;
		}
	}
	
	void Shoot() // Creates a bullet, sets it a child of this tower, and sets it's target
	{
		if(TargetExists(target)) //Better to not have this within the Shoot method
		{
			GameObject g = (GameObject)Instantiate(bulletObject, transform.position, Quaternion.identity);
			g.transform.SetParent (gameObject.transform); //Sets bullet as a child of this
			g.GetComponent<Bullet>().target = target.transform; //Sets the target
		}
	}
}