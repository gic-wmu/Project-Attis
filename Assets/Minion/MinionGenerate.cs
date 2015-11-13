//Apolinar Ortega
//GenerateMinion handles generating minions for the level
//HoardStat conatains data for each hoard of minions
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MinionGenerate : MonoBehaviour 
{
	#region Done
	[Serializable]
	public class HoardStat
	{
		public int DelayBetweenMinions, StartTime, MinionHealth, MinionWorth, MinionCount;
		private int TimeActive = 0;
		public float MinionSpeed;
		public GameObject MinionTemplate;

		public HoardStat(int startTime, int delayBetweenMinions, int minionCount, int minionHealth, 
		                 int minionWorth, float minionSpeed, GameObject minionTemplate)
		{
			DelayBetweenMinions = delayBetweenMinions;
			MinionCount = minionCount;
			StartTime = startTime;
			MinionHealth = minionHealth;
			MinionWorth = minionWorth;
			MinionSpeed = minionSpeed;
			MinionTemplate = minionTemplate;
		}
		public void Update(int time)
		{
			if (CanDoSomething(time) && TimeActive % DelayBetweenMinions == 0)
				GenerateMinion ();
			if(CanStart(time))
				TimeActive++;
		}
		private void Log(int time)
		{
			if(CanStart (time))
			{
				Debug.Log (time + ": " + StartTime + ": " + TimeActive);
				Debug.Log (MinionCount);
			}
		}
		private void GenerateMinion()
		{
			Debug.Log ("Generating minion!");
			GameObject.Find ("Minions").GetComponent<MinionGenerate> ().SpawnEnemy (MinionHealth, MinionWorth, MinionSpeed); 
			MinionCount--;
		}

		private bool CanStart(int time)
		{
			return (time >= StartTime);
		}
		// Can do something if there are minions left to generate and is after startime
		private bool CanDoSomething(int time) 
		{
			return CanStart (time) && !IsDone ();
		}
		private bool IsDone() //Is done once all minions are destroyed
		{
			return MinionCount <= 0;
		}
	}

	public int TimeFromStart { get; private set;}
	public HoardStat[] Hoards;
	public List<GameObject> Minions; //Holds a reference to all the minions generated
	public GameObject MinionTemplate; 
	public GameObject startTile;
	public GameObject endTile;

	void Start () 
	{
		TimeFromStart = 0;
		Minions = new List<GameObject> ();
		//startTest ();
	}
	
	void Update () 
	{
		for (int i = 0; i < Hoards.Length; i++)
			Hoards [i].Update (TimeFromStart);

		//updateTest ();
		TimeFromStart++;
	}
	//Spawns an enemy and adds it to the list of minionsv
	public void SpawnEnemy(int healths, int bounties, float moveSpeeds)
	{
		GameObject minioni = (GameObject) GameObject.Instantiate(MinionTemplate, 
			startTile.transform.position, Quaternion.identity);
			
		minioni.GetComponent<Minion>().SetMinion(healths, bounties, moveSpeeds, startTile, endTile);
			
		//Adds minioni to list and sets as a child of this gameObject
		Minions.Add (minioni);
		minioni.transform.SetParent (gameObject.transform);
			
		//Sets minion's start and end
		minioni.GetComponent<Minion>().startTile = startTile;
		minioni.GetComponent<Minion>().endTile = endTile;
	}
	//Removes the minion from the list
	public void RemoveMinion(GameObject minion)
	{
		Minions.Remove (minion);
	}
	#endregion
	//startTest and updateTest are not active
	/*#region CurrentWork
	public List<HoardStat[]> Waves;
	public int WaitTime = 100;
	public int WaitTimeLeft;

	private void startTest() //for testing
	{
		Waves = new List<HoardStat>();
	}
	private void updateTest()//for testing
	{
	}
	public void SkipWait() //Skips wait by setting waitTimeLeft to 0
	{
		WaitTimeLeft = 0;
	}
	#Endregion*/
}