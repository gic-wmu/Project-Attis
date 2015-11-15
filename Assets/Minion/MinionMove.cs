// Apolinar Ortega
// Used to move the enemy to the destination
using UnityEngine;
using System.Collections;

//ALLOWED_END_RANGE to be calculated based on speed?

public class MinionMove : MonoBehaviour 
{
	private const float ALLOWED_END_RANGE = 0.1f;// If within this distance, then minion is done moving
	void Start () {}
	
	void Update () {}

	// Is done moving if minion is within range
	public bool IsDoneMoving(Vector3 destination)
	{
		return (destination - transform.position).magnitude <= ALLOWED_END_RANGE; 
	}

	//Moves towards the destination
	public void Move(Vector3 destination, float  moveSpeed)
	{
		if (!IsDoneMoving (destination)) 
		{
			//SetToLookForward (destination);
			Vector3 displacement = destination - gameObject.transform.position;
			Vector3 velocity = GetUnitVector (displacement) * moveSpeed; //Speed in the given direction 
			transform.Translate (velocity);
		}
	}

	private Vector3 GetUnitVector(Vector3 vector)
	{
		return (1 / vector.magnitude) * vector;
	}


	//Rotates object until it looks at the same position velocity is heading
	private void SetToLookForward(Vector3 destination)//has issues
	{
		gameObject.transform.rotation = Quaternion.FromToRotation (gameObject.transform.position, destination);
	}
}