using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class OverlappingBehaviour : MonoBehaviour {

    public Text collisionText;

	// Use this for initialization
	void Start () {
        collisionText.text = "No Monster\n";
	}
	
	// Update is called once per frame
	void Update () {
	    RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit);

        if (hit.transform.name.Contains("Monster") && transform.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            collisionText.text = "Monster!";
            SceneManager.LoadScene("Combat");
        }
        else
        {
            collisionText.text = "No Monster\n";
        }
	}
}
