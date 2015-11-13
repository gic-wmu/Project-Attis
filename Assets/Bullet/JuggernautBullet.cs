//Apolinar Ortega
//untested
//This is a bullet that has a set path and whatever it hits, it kills without destroying itself
using UnityEngine;
using System.Collections;
//Error: may go on forever; may die too soon if target dies
//OntriggerStay to hit the enemy constantly

public class JuggernautBullet : Bullet 
{
	/*
	override public int damage = 10; 
	override public float speed = 1f;
	override public Transform target;*/

	public float MaxDistance = 20; // The maximum distance it can move
	private Vector3 start;

	public override void Start () 
	{
		start = transform.position;
		if (target) //If it has a target, then move
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
	
	private Vector3 GetUnitVector(Vector3 vector) //Gets the unit vector
	{
		//Returns unit Vector u of Vector v; u = v / |v|
		return ScaleVector (vector, 1 / GetMagnitude(vector));
	}
	private Vector3 ScaleVector(Vector3 vector, float scale) //Scales vector by the scale given
	{
		return new Vector3(vector.x * scale, vector.y * scale, vector.z * scale);
	}

	private bool IsOutOfRange() // Is true if the magnitude of displacement is more than MaxDistance
	{
		return GetMagnitude (transform.position - start) >  MaxDistance; 
	}

	private float GetMagnitude(Vector3 v)
	{
		return Mathf.Sqrt(Mathf.Pow((v.x),2) + Mathf.Pow((v.y),2) + Mathf.Pow((v.z),2));
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