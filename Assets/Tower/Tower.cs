using UnityEngine;
using System.Collections;

public class ScriptTower : MonoBehaviour {
    public GameObject bulletObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider co)
    {
        if(co.tag == "Creep")
        {
            GameObject g = (GameObject)Instantiate(bulletObject, transform.position, Quaternion.identity);
            g.GetComponent<Bullet>().target = co.transform;
        }
        
    }
}
