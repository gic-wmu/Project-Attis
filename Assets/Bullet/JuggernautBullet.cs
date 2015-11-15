//Apolinar Ortega
//untested
//This is a bullet that has a set path and whatever it hits, it kills without destroying itself
using UnityEngine;
using System.Collections;
//Error: may go on forever; may die too soon if target dies
//OntriggerStay to hit the enemy constantly

public class JuggernautBullet : Bullet 
{
	public float MaxDistance = 20; // The maximum distance it can move
	private Vector3 start; //Its starting point

	public override void Start () 
	{
		start = transform.position;
		if (target) //Velocity = speed in the direction facing the target
			GetComponent<Rigidbody> ().velocity = GetUnitVector(target.position - transform.position) * speed;
		else
			Destroy (gameObject);
	}

	public override void Update () 
	{
		if(IsOutOfRange()) // If out of range, thenn it destroys itself
		{
			Debug.Log ("I am too far from home!");
			Destroy(gameObject);
		}
	}
	
	private Vector3 GetUnitVector(Vector3 vector)
	{
		return (1 / vector.magnitude) * vector;
	}
	
	private bool IsOutOfRange() 
	{
		return (transform.position - start).magnitude > MaxDistance;
	}

	public override void FixedUpdate(){}

	public override void OnTriggerEnter(Collider co)
	{
		if(co.tag == "Creep")
		{
			if(co.transform.gameObject.GetComponent<Minion>() != null)
				co.transform.gameObject.GetComponent<Minion>().Damage(damage);
		}   
	}
}