using UnityEngine;
using System.Collections.Generic;

public class CaveSpawner : MonoBehaviour {

    public Sprite tempMonstSprite;

    private static List<GameObject> monstList;

    // Use this for initialization
    void Start () {
        monstList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

        float maxHeight;

        Vector3 camWorldBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        float monsterPosYMin = camWorldBottomLeft.y - 4;
        Vector3 camWorldTopLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        float monsterPosYMax = camWorldTopLeft.y + 10.0f;

        Vector2 screenWorldBounds;
        screenWorldBounds.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        screenWorldBounds.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));


        if (monstList.Count != 0)
        {
            maxHeight = monstList[0].transform.position.y;
            for (int i = 0; i < monstList.Count; i++)
            {
                if (monstList[i].transform.position.y < monsterPosYMin)
                {
                    GameObject monsterDelete = monstList[i];
                    monstList.Remove(monsterDelete);
                    Destroy(monsterDelete);
                }
                maxHeight = Mathf.Max(maxHeight, monstList[i].transform.position.y);
            }

            float monstDist = monsterPosYMax - maxHeight;
        }
        else
        {
            GameObject newMonster = new GameObject();
            newMonster.AddComponent<SpriteRenderer>().sprite = tempMonstSprite;
            newMonster.AddComponent<SphereCollider>();
            newMonster.transform.position = new Vector3((camWorldTopLeft.x + newMonster.GetComponent<SpriteRenderer>().bounds.size.x/2.0f)
                + Random.value * (screenWorldBounds.x - newMonster.GetComponent<SpriteRenderer>().bounds.size.x), monsterPosYMax, 5);
            newMonster.transform.parent = GameObject.Find("Landscape").transform;
            newMonster.name = "TempCave";
            monstList.Add(newMonster);
        }
	
	}
}
