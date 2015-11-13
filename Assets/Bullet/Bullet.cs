using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public int damage = 10;
	public float speed = 30;
	public Transform target;
	// Update for physics items
	
	public virtual void Start(){}
	public virtual void Update(){}
	
	public virtual void FixedUpdate () 
	{
		if (target) //if target exists
		{
			//fly towards the target
			Vector3 dir = target.position - transform.position;
			GetComponent<Rigidbody>().velocity = dir.normalized * speed;
		}
		else
			Destroy(gameObject);
	}
	
	public virtual void OnTriggerEnter(Collider co)
	{
		if(co.tag == "Creep")
		{
			if(co.transform.gameObject.GetComponent<Minion>() != null)
				co.transform.gameObject.GetComponent<Minion>().Damage(damage);
			Destroy(gameObject);
		}   
	}
}