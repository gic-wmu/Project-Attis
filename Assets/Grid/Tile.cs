using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    public GameObject Tower;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void PlaceTower()
    {
        GameObject t = (GameObject)Instantiate(Tower);
        t.transform.position = transform.position + Vector3.up; //place new tower at tile location plus one unit up
    }
}
