//Apolinar Ortega
using UnityEngine;
using System.Collections;

//Serializable?
public class Minion : MonoBehaviour 
{
	public int Health { get; private set;} // Minion HP
	public int Bounty { get; private set;} // How much money/points minion drops
	public float MoveSpeed { get; private set;} // How fast  the minion moves
	private GameObject startTile, endTile;

	public void Start()
	{
		startTile = GameObject.Find("tileEnter");
		endTile = GameObject.Find ("tileExit");
		SetMinionColor (new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f)));
	}
	public void Update()
	{
		//Error
		//if(gameObject.GetComponent<MinionMove>().IsDoneMoving(endTile.transform.position))
		//	DieWinning ();
		//To be changed to th next position
		gameObject.GetComponent<MinionMove> ().Move ( endTile.transform.position, MoveSpeed);
		DisplayHealth ();
	}

	// Before the bullet destroys itself, it hits the enemy (Add code to bullet)
	// Called to cause damage to minion's health
	public void Damage(int Damage)
	{
		Health -= Damage;
		if (Health <= 0)
			Die ();
	}

	//Called when the minion is killed in combat
	private void Die()
	{
		Debug.Log ("I have been slain!");
		gameObject.transform.parent.GetComponent<MinionHoard> ().RemoveMinion (gameObject); //Removes from list
		GameObject.Destroy (gameObject);
	}

	//Error
	private void DieWinning() //Called when they die winning
	{
		//Takes life from player
		gameObject.transform.parent.GetComponent<MinionHoard> ().RemoveMinion (gameObject); //Rmoves from list
		GameObject.Destroy (gameObject);
	}

	public void SetMinion(int health, int bounty, float movespeed, GameObject start, GameObject end)
	{
		this.Health = health;
		this.Bounty = bounty;
		this.MoveSpeed = movespeed;
		startTile = start;
		endTile = end;
	}
	public void SetMinionColor(Color color)
	{
		gameObject.GetComponent<Renderer> ().material.color = color;
	}

	public void DisplayHealth()
	{
		gameObject.transform.FindChild ("Minion Text").GetComponent<TextMesh> ().text = Health + "";
	}

	//public abstract void Die ();
	//when the minion dies, it gives the player gold, removes itself from the list, and destroys itself
	//To have parameter specifying who or what killed it?
	//Condition as to why it dies? Or new method?
}