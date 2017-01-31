using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour {

    private List<GameObject> monstArr = new List<GameObject>();

	// Use this for initialization
	void Start () {

	}

    public void Setup(int monstCount, Sprite[] monstSprites)
    {
        Vector2 screenWorldOrigin;
        Vector2 screenWorldBounds;

        screenWorldOrigin = new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y);
        screenWorldBounds.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        screenWorldBounds.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));

        for (int i = 0; i < monstCount; i++)
        {
            GameObject tmp = new GameObject();
            tmp.name = "monster";
            tmp.AddComponent<SpriteRenderer>().sprite = monstSprites[i];
            tmp.AddComponent<BoxCollider>();
            tmp.AddComponent<MonsterBehaviour>();
            tmp.transform.parent = transform;
            tmp.transform.position = new Vector3(
                screenWorldOrigin.x + tmp.GetComponent<SpriteRenderer>().bounds.size.x / 2 + (screenWorldBounds.x - tmp.GetComponent<SpriteRenderer>().bounds.size.x) * Random.Range(0.0f, 1.0f),
                screenWorldOrigin.y + tmp.GetComponent<SpriteRenderer>().bounds.size.y / 2 + (screenWorldBounds.y - tmp.GetComponent<SpriteRenderer>().bounds.size.y) * Random.Range(0.0f, 1.0f),
                4 + Random.Range(-1.0f,1.0f));
            monstArr.Add(tmp);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.name.Contains("monster"))
            {
                hit.transform.GetComponent<MonsterBehaviour>().Death();
                monstArr.Remove(hit.transform.gameObject);
                Destroy(hit.transform.gameObject);
                if (monstArr.Count == 0)
                {
                    GameObject player = GameObject.Find("Player");
                    Camera.main.transform.position = player.transform.GetComponent<OverlappingBehaviour>().cameraOriginalPosition;
                    player.transform.GetComponent<OverlappingBehaviour>().enabled = true;
                    player.transform.GetComponent<DebugMouseSwipe>().enabled = true;
                    Camera.main.transform.GetComponent<CameraMovement>().enabled = true;
                }
            }
        }
    }
}
