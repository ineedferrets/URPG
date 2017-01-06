using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class OverlappingBehaviour : MonoBehaviour {

    public int maxMonstInt;
    public Vector3 cameraOriginalPosition;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.forward, out hit);

            if (hit.transform.name.Contains("Cave") && transform.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                hit.transform.name = "Conquered";
                hit.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
                GameObject combatCave = GameObject.Find("CaveEnvironment");
                combatCave.GetComponent<MonsterSpawner>().enabled = true;
                combatCave.transform.position = new Vector3(40, 0, 1);
                Camera.main.gameObject.GetComponent<CameraMovement>().enabled = false;
                cameraOriginalPosition = Camera.main.transform.position;
                Camera.main.transform.position = new Vector3(40, 0, -1);
                combatCave.GetComponent<MonsterSpawner>().Setup(hit.transform.GetComponent<CaveMonsterInformation>().monstCount, hit.transform.GetComponent<CaveMonsterInformation>().monstSprites);
                transform.GetComponent<DebugMouseSwipe>().enabled = false;
                transform.GetComponent<OverlappingBehaviour>().enabled = false;
            }
        }
	}
}
