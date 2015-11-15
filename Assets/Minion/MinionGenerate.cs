//Apolinar Ortega
//GenerateMinion handles generating minions for the level
//HoardStat conatains data for each hoard of minions
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MinionGenerate : MonoBehaviour 
{ 
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
		private void GenerateMinion()
		{
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
		public bool IsDone() //Is done once all minions are destroyed
		{
			return MinionCount <= 0;
		}
	}

	[Serializable]
	public class Hoard
	{
		public HoardStat[] Hoards;
		public Hoard(HoardStat[] hoardStat)
		{
			Hoards = hoardStat;
		}
		public void Update(int time)
		{
			for (int i = 0; i < Hoards.Length; i++)
				Hoards [i].Update (time);
		}
		public bool IsDone()
		{
			for (int i = 0; i < Hoards.Length; i++)
				if (!Hoards[i].IsDone ()) //If an item in the HoardStat is not done, then Hoard is not done
					return false;
			return true;
		}
	}

	public int TimeFromStart { get; private set;}//Time from the start of the wave
	public List<GameObject> Minions; //Holds a reference to all the minions generated
	public GameObject MinionTemplate; 
	public GameObject startTile;
	public GameObject endTile;

	//For Waves
	public Hoard[] Waves;
	public int WaitTime = 100;
	private int index = 0;
	private int TimeWaiting = 0;

	void Start () 
	{
		TimeFromStart = 0;
		Minions = new List<GameObject> ();
	}
	
	void Update () 
	{
		if (ReadyForNextWave ()) //If ready for the next wave, then move to the next wave
			ToNextWave ();

		if(TimeWaiting == WaitTime)//If the wait time is over, then reset the timer since the next wave is starting
			TimeFromStart = 0;

		if (CanStartWave ())//If the wave can start, then update it
			Waves [index].Update (TimeFromStart);

		TimeWaiting++;
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


	//These methods relate to Waves

	//Wave can start wave once wait time is over
	private bool CanStartWave() 
	{
		return (TimeWaiting >= WaitTime) && index < Waves.Length;
	}

	// Resets wait timer to 0 and moves to the next index
	private void ToNextWave()
	{
		index++;
		TimeWaiting = 0;
	}

	//Is true if all enemies are dead, there's a next wave, and if wave is done
	public bool ReadyForNextWave()
	{
		return Minions.Count == 0 && index < Waves.Length && Waves [index].IsDone ();
	}
	
	//Skips wait for the wave to start
	public void SkipWait() 
	{
		TimeWaiting = WaitTime;
	}
}