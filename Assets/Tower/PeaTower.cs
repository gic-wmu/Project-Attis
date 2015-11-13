using UnityEngine;
using System.Collections;
//Error when enemy has been slain

public class PeaTower : MonoBehaviour 
{
	public GameObject bulletObject;
	public float timeDelay = 1;
	private Collider target = null;

	private bool targetLast = false;

	void Update () 
	{
		//if(target != null)
		//	DetectingTarget ();
	}
	
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

	private void DetectingTarget()//Detects if target has been lost
	{
		if (targetLast && TargetExists (target)) //If last updat target existed but this not, then
			Debug.Log ("Target has been destroyed!");

		targetLast = TargetExists (target);
	}

	private bool TargetExists(Collider co) //Detects if collider does not exist
	{
		return co.gameObject.GetComponent<SphereCollider>() != null;
	}
	
	void Shoot() // Creates a bullet, sets it a child of this tower, and sets it's target
	{
		GameObject g = (GameObject)Instantiate(bulletObject, transform.position, Quaternion.identity);
		g.transform.SetParent (gameObject.transform); //Sets bullet as a child of this
		g.GetComponent<Bullet>().target = target.transform; //Sets the target
	}
}