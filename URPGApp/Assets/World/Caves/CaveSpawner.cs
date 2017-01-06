using UnityEngine;
using System.Collections.Generic;

public class CaveSpawner : MonoBehaviour {

    public Sprite caveSprite;

    private Sprite[] monstSprites;
    private static List<GameObject> caveList;
    private int caveCount = 0;

    // Use this for initialization
    void Start () {
        caveList = new List<GameObject>();
        monstSprites = Resources.LoadAll<Sprite>("Monsters");
	}
	
	// Update is called once per frame
	void Update () {

        float maxHeight;

        Vector3 camWorldBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        float cavePosYMin = camWorldBottomLeft.y - 4;
        Vector3 camWorldTopLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        float cavePosYMax = camWorldTopLeft.y + 10.0f;

        Vector2 screenWorldBounds;
        screenWorldBounds.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        screenWorldBounds.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));


        if (caveList.Count != 0)
        {
            maxHeight = caveList[0].transform.position.y;
            for (int i = 0; i < caveList.Count; i++)
            {
                if (caveList[i].transform.position.y < cavePosYMin)
                {
                    GameObject monsterDelete = caveList[i];
                    caveList.Remove(monsterDelete);
                    Destroy(monsterDelete);
                }
                maxHeight = Mathf.Max(maxHeight, caveList[i].transform.position.y);
            }

            float caveDist = cavePosYMax - maxHeight;
        }
        else
        {
            GameObject newCave = new GameObject();
            newCave.AddComponent<SpriteRenderer>().sprite = caveSprite;
            newCave.AddComponent<SphereCollider>();
            newCave.transform.position = new Vector3((camWorldTopLeft.x + newCave.GetComponent<SpriteRenderer>().bounds.size.x/2.0f)
                + Random.value * (screenWorldBounds.x - newCave.GetComponent<SpriteRenderer>().bounds.size.x), cavePosYMax, 5);
            newCave.transform.parent = GameObject.Find("Landscape").transform;
            newCave.name = "Cave" + caveCount;
            newCave.AddComponent<CaveMonsterInformation>();
            int tmpInt = Random.Range(1, 10);
            newCave.GetComponent<CaveMonsterInformation>().monstCount = tmpInt;
            Sprite[] tmpSprites = new Sprite[tmpInt];
            for (int i = 0; i < tmpInt; i++)
                tmpSprites[i] = monstSprites[Random.Range(0, monstSprites.Length - 1)];
            newCave.GetComponent<CaveMonsterInformation>().monstSprites = tmpSprites;
            caveList.Add(newCave);
            caveCount++;
        }
	}
}
