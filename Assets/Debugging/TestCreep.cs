using UnityEngine;
using System.Collections;

public class TestCreep : MonoBehaviour {
    private float speed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float inh = Input.GetAxisRaw("Horizontal");
        float inv = Input.GetAxisRaw("Vertical");
        GetComponent<Rigidbody>().velocity = (new Vector3(inh, inv, 0))*speed;
	}
}
