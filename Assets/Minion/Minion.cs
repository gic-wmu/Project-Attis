//Apolinar Ortega
//This handle the minion's data
using UnityEngine;
using System.Collections;
//fix Violence

public class Minion : MonoBehaviour 
{
	#region ValuesAndDefaults
	public int Health { get; private set;} // Minion HP
	public int Bounty { get; private set;} // How much money/points minion drops
	public float MoveSpeed { get; private set;} // How fast  the minion moves
	public GameObject startTile, endTile;
	
	public void Start()
	{
		SetMinionColor (new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f)));
	}
	public void Update()
	{
		gameObject.GetComponent<MinionMove> ().Move ( endTile.transform.position, MoveSpeed);
		DisplayHealth ();
		UpdateHealth ();
	}
	#endregion

	#region Violence
	private void UpdateHealth()
	{
		if (gameObject.GetComponent<MinionMove> ().IsDoneMoving (endTile.transform.position))
			DieWinning ();
	}

	// Called to damage the enemy
	public void Damage(int Damage)
	{
		Health -= Damage;
		if (Health <= 0)
			DieLosing ();
	}
	private void DieLosing()
	{
		Debug.Log ("I have been slain!");
		//Player.Gold += bounty
		Die ();
	}

	private void DieWinning()
	{
		Debug.Log("I will slay you!");
		//Player.health --
		Die ();
	}

	private void Die()
	{
		gameObject.transform.parent.GetComponent<MinionGenerate> ().RemoveMinion (gameObject); //Removes from list
		GameObject.Destroy (gameObject);
	}

	#endregion
	
	#region Utility
	public void SetMinion(int health, int bounty, float movespeed, GameObject start, GameObject end)
	{
		this.Health = health;
		this.Bounty = bounty;
		this.MoveSpeed = movespeed;
		startTile = start;
		endTile = end;
	}
	private void SetMinionColor(Color color)
	{
		gameObject.GetComponent<Renderer> ().material.color = color;
	}
	
	private void DisplayHealth()
	{
		gameObject.transform.FindChild ("Minion Text").GetComponent<TextMesh> ().text = Health + "";
	}
	#endregion
}