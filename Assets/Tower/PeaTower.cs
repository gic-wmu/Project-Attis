using UnityEngine;
using System.Collections;

public class PeaTower : MonoBehaviour {
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
            GameObject g = (GameObject)Instantiate(bulletObject, transform.position, transform.rotation);
            g.GetComponent<Bullet>().target = co.transform;
        }
        
    }
}
