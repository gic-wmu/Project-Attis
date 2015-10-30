// Apolinar Ortega
// Used to move the enemy to the destination
using UnityEngine;
using System.Collections;

public class MinionMove : MonoBehaviour 
{
	void Start () {}
	
	void Update () {}

	//Error
	public bool IsDoneMoving(Vector3 destination) //may need to change if is within range
	{
		return gameObject.transform.position == destination;
	}

	//Create method getUnitVectorOf(int magnitude) that returns a vector of the magnitude and direction
	public void Move(Vector3 destination, float  moveSpeed)
	{
		//velocity is the unit vector of displacement * the moveSpeed
		//SetToLookForward (destination);
		Vector3 displacement = destination - gameObject.transform.position;
		Vector3 velocity = ScaleVector(GetUnitVector (displacement), moveSpeed); 
		transform.Translate (velocity);
	}

	private Vector3 GetUnitVector(Vector3 vector) //Gets the unit vector
	{
		//Returns unit Vector u of Vector v; u = v / |v|
		return ScaleVector (vector, 1 / GetMagnitude(vector));
	}

	private float GetMagnitude(Vector3 vector) //Gets the magnitude of the vector
	{
		float dot = Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2);
		return Mathf.Pow(dot, 0.5f);
	}

	private Vector3 ScaleVector(Vector3 vector, float scale) //Scales vector by the scale given
	{
		return new Vector3(vector.x * scale, vector.y * scale, vector.z * scale);
	}

	//Rotates object until it looks at the same position velocity is heading
	private void SetToLookForward(Vector3 destination)
	{
		gameObject.transform.rotation = Quaternion.FromToRotation (gameObject.transform.position, destination);
	}
}