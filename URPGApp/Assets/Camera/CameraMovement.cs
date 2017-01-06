using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private float worldTargetYPosition;
    private float worldMaxYPosition;
    private float screenYPlacement = 0.4f;

    public float camMaxVel;
    public float proxRadius;

	// Use this for initialization
	void Start () {
        var worldTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * screenYPlacement, 0));
        worldMaxYPosition = worldTargetPosition.y;
    }
	
	// Update is called once per frame
	void Update () {
        var worldTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * screenYPlacement, 0));
        worldTargetYPosition = Mathf.Max(worldTargetPosition.y,worldMaxYPosition);

        var playerObj = GameObject.Find("Player");
        var playerYPos = playerObj.transform.position.y;

        var cameraYDirection = Mathf.Clamp(playerYPos - worldTargetYPosition, 0, proxRadius)/proxRadius;

        if (!playerObj.GetComponent<DebugMouseSwipe>().touchedBool && playerObj.GetComponent<Rigidbody2D>().velocity.y >= 0)
            transform.position += new Vector3(0, camMaxVel * cameraYDirection, 0);
	}
}
