using UnityEngine;
using System.Collections;

public class StandardSwipe : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 50, 0);
	}
}
