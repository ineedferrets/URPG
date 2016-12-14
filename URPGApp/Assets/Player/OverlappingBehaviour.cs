using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class OverlappingBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit);

        if (hit.transform.name.Contains("Cave") && transform.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            SceneManager.LoadScene("Combat");
        }
	}
}
