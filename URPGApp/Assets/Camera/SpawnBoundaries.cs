using UnityEngine;
using System.Collections;

public class SpawnBoundaries : MonoBehaviour {

    public float zPosition = 1.0f;
    private Vector2 screenSize;
    private Transform topCollider;
    private Transform bottomCollider;
    private Transform leftCollider;
    private Transform rightCollider;
    private Vector3 cameraPos;

	// Use this for initialization
	void Start () {
        topCollider = new GameObject().transform;
        bottomCollider = new GameObject().transform;
        leftCollider = new GameObject().transform;
        rightCollider = new GameObject().transform;

        topCollider.name = "TopCollider";
        bottomCollider.name = "BottomCollider";
        leftCollider.name = "LeftCollider";
        rightCollider.name = "RightCollider";

        topCollider.gameObject.AddComponent<BoxCollider2D>();
        bottomCollider.gameObject.AddComponent<BoxCollider2D>();
        leftCollider.gameObject.AddComponent<BoxCollider2D>();
        rightCollider.gameObject.AddComponent<BoxCollider2D>();

        topCollider.parent = transform;
        bottomCollider.parent = transform;
        leftCollider.parent = transform;
        rightCollider.parent = transform;

        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        topCollider.transform.localScale = new Vector3(screenSize.x * 2.0f, 1, 1);
        topCollider.transform.localPosition = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + 0.5f, zPosition);
        bottomCollider.transform.localScale = new Vector3(screenSize.x*2.0f, 1, 1);
        bottomCollider.transform.localPosition = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - 0.5f, zPosition);
        leftCollider.transform.localScale = new Vector3(screenSize.y*2, 1, 1);
        leftCollider.transform.localRotation = Quaternion.Euler(0, 0, 90);
        leftCollider.localPosition = new Vector3(cameraPos.x - screenSize.x - 0.5f, cameraPos.y, zPosition);
        rightCollider.transform.localScale = new Vector3(screenSize.y*2, 1, 1);
        rightCollider.transform.localRotation = Quaternion.Euler(0, 0, 90);
        rightCollider.localPosition = new Vector3(cameraPos.x + screenSize.x + 0.5f, cameraPos.y, zPosition);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
