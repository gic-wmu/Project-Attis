using UnityEngine;
using System.Collections;

public class PeaTower : MonoBehaviour {
    public GameObject bulletObject;
    public float timeDelay = 1;
    private Collider target;
	// Update is called once per frame
	void Update () {
       
    }

    void OnTriggerEnter(Collider co)
    {

        if ((co.tag == "Creep") && !IsInvoking("Shoot"))
        {
            target = co;
            Invoke("Shoot", 0);
        }


    }

    void OnTriggerLeave(Collider co)
    {
        if(co == target)
        {
            StopCoroutine("Shoot");
        }
        
    }

    void OnTriggerStay(Collider co)
    {
        if ((co.tag == "Creep") && !IsInvoking("Shoot"))
        {
            target = co;
            Invoke("Shoot", timeDelay);
            //GameObject g = (GameObject)Instantiate(bulletObject, transform.position, Quaternion.identity);
            //g.GetComponent<Bullet>().target = co.transform;
        }
    }

    void Shoot()
    {
        GameObject g = (GameObject)Instantiate(bulletObject, transform.position, Quaternion.identity);
        g.GetComponent<Bullet>().target = target.transform;
    }
}
