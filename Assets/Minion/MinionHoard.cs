//Apolinar Ortega
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//This class is to hold a reference to all the minions and spawns them
public class MinionHoard : MonoBehaviour 
{
	public List<GameObject> Minions; //Holds a reference to all the minions generated
	public GameObject MinionTemplate; 
	public GameObject startTile;
	public GameObject endTile;

	void Start () 
	{
		Minions = new List<GameObject> ();
		SpawnEnemies (1, 25, 3, 0.02f);
		SpawnEnemies (1, 100, 3, 0.01f);
	}
	
	void Update () {}

	// Generates minions
	//Extra Parameter for minion type?
	public void SpawnEnemies(int minionCount, int healths, int bounties, float moveSpeeds )
	{
		GameObject minioni;
		for (int i = 0; i < minionCount; i++) 
		{
			minioni = (GameObject) GameObject.Instantiate(
				MinionTemplate, startTile.transform.position, Quaternion.identity);

			minioni.GetComponent<Minion>().SetMinion(healths, bounties, moveSpeeds, startTile, endTile);

			//Adds minioni to list and sets as a child of this gameObject
			Minions.Add (minioni);
			minioni.transform.SetParent (gameObject.transform);

			//Sets minio;n's start and end
			minioni.GetComponent<Minion>().startTile = startTile;
			minioni.GetComponent<Minion>().endTile = endTile;
		}
	}

	//Removes the minion from the list
	public void RemoveMinion(GameObject minion)
	{
		Minions.Remove (minion);
	}
}