using UnityEngine;
using System.Collections;

public class MonsterBehaviour : MonoBehaviour {

    public int maxMonstInt;
    public Sprite monstSprite;

    private int monsterInt;
    private Vector2 screenWorldOrigin;
    private Vector2 screenWorldBounds;

	// Use this for initialization
	void Start () {
        screenWorldOrigin = new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(0,0)).x, Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y);
        screenWorldBounds.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        screenWorldBounds.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));

        monsterInt = Random.Range(1, maxMonstInt+1);

        for (int i = 0; i < monsterInt; i++)
        {
            GameObject tmp = new GameObject();
            tmp.name = "monster";
            tmp.AddComponent<SpriteRenderer>().sprite = monstSprite;
            tmp.AddComponent<BoxCollider2D>();
            tmp.transform.parent = transform;
            tmp.transform.position = new Vector3(
                screenWorldOrigin.x + tmp.GetComponent<SpriteRenderer>().bounds.size.x/2 + (screenWorldBounds.x - tmp.GetComponent<SpriteRenderer>().bounds.size.x) * Random.Range(0.0f, 1.0f),
                screenWorldOrigin.y + tmp.GetComponent<SpriteRenderer>().bounds.size.y/2 + (screenWorldBounds.y - tmp.GetComponent<SpriteRenderer>().bounds.size.y) * Random.Range(0.0f, 1.0f),
                4);
        }
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            Physics.Raycast(ray, out hit);

            Debug.Log(hit.transform);

            if (hit.transform.name.Contains("monster"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
	}
}
