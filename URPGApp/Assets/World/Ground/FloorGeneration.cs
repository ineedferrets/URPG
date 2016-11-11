using UnityEngine;
using System.Collections.Generic;

public class FloorGeneration : MonoBehaviour {

    public Sprite solidSprite;
    public Sprite emptySprite;

    private static List<List<GameObject>> floorTiles;
    private Vector2 tileWorldBounds;
    private Vector2 screenWorldBounds;
    private Vector2 tilesFit;
    private float tileMaxAboveScreen = 10.0f;
    private enum enumTileTypes
    {
        Grass,
        Sand
    }

    private Dictionary<enumTileTypes, Color> tileTypeColors = new Dictionary<enumTileTypes, Color>()
    {
        { enumTileTypes.Grass, new Color(0.396f, 0.471f, 0.376f) }
    };

	// Use this for initialization
	void Start () {
        floorTiles = new List<List<GameObject>>();
        GameObject initTile = new GameObject();
        initTile.AddComponent<SpriteRenderer>();
        initTile.GetComponent<SpriteRenderer>().sprite = solidSprite;
        tileWorldBounds = new Vector2(initTile.GetComponent<SpriteRenderer>().bounds.size.x, initTile.GetComponent<SpriteRenderer>().bounds.size.y);
        Destroy(initTile);
        screenWorldBounds.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        screenWorldBounds.y = tileMaxAboveScreen + Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));
        tilesFit = new Vector2(Mathf.Ceil(screenWorldBounds.x / tileWorldBounds.x), Mathf.Ceil(screenWorldBounds.y / tileWorldBounds.y));

        if (tilesFit.x % 2 == 0)
            tilesFit.x += 1;
        if (tilesFit.y % 2 == 0)
            tilesFit.y += 1;

        Vector3 camWorldBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        float tilePosY = camWorldBottomLeft.y - tileWorldBounds.y/2;
        for (int j = 0; j < tilesFit.y; j++)
        {
            List<GameObject> rowTiles = new List<GameObject>();
            tilePosY += tileWorldBounds.y;
            for (int i = 0; i < tilesFit.x; i++)
            {
                Vector3 tilePos = new Vector3(-((tilesFit.x - 1) / 2) * tileWorldBounds.x + i * tileWorldBounds.x, tilePosY, 10);
                GameObject newTile = new GameObject();
                newTile.AddComponent<SpriteRenderer>().sprite = solidSprite;
                newTile.GetComponent<SpriteRenderer>().color = tileTypeColors[enumTileTypes.Grass] + new Color(Random.value/50, Random.value / 50, Random.value / 50);
                newTile.transform.position = tilePos;
                newTile.transform.parent = GameObject.Find("Landscape").transform;
                newTile.name = "grass1";
                rowTiles.Add(newTile);
            }
            floorTiles.Add(rowTiles);
        }

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 camWorldBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        float tilePosYMin = camWorldBottomLeft.y - tileWorldBounds.y / 2;

        Vector3 camWorldTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        float tilePosYMax = camWorldTopRight.y + tileMaxAboveScreen;


        float maxTileHeight = floorTiles[0][0].transform.position.y;
        for (int k = 0; k < floorTiles.Count; k++)
        {
            maxTileHeight = Mathf.Max(maxTileHeight, floorTiles[k][0].transform.position.y);
            if (floorTiles[k][0].transform.position.y < tilePosYMin)
            {
                List<GameObject> tempRow = floorTiles[k];
                floorTiles.Remove(tempRow);
                for (int r = 0; r < tempRow.Count; r++)
                    Destroy(tempRow[r]);
            }
        }

        if (maxTileHeight <= tilePosYMax)
        {
            float tilePosY = maxTileHeight;
            do
            {
                List<GameObject> rowTiles = new List<GameObject>();
                tilePosY += tileWorldBounds.y;
                for (int i = 0; i < tilesFit.x; i++)
                {
                    Vector3 tilePos = new Vector3(-((tilesFit.x - 1) / 2) * tileWorldBounds.x + i * tileWorldBounds.x, tilePosY, 10);
                    GameObject newTile = new GameObject();
                    newTile.AddComponent<SpriteRenderer>().sprite = solidSprite;
                    newTile.GetComponent<SpriteRenderer>().color = tileTypeColors[enumTileTypes.Grass] + new Color(Random.value / 50f, Random.value / 50f, Random.value / 50f);
                    newTile.transform.position = tilePos;
                    newTile.transform.parent = GameObject.Find("Landscape").transform;
                    newTile.name = "grass1";
                    rowTiles.Add(newTile);
                }
                floorTiles.Add(rowTiles);
            } while (tilePosY <= maxTileHeight);
        }
    }
}
