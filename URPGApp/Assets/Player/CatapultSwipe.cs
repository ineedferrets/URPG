using UnityEngine;
using System.Collections;

public class CatapultSwipe : MonoBehaviour {

    private Vector3 touchBeginPos;
    private Vector3 objBeginPos;
    private Vector3 objDesiredPos;

    public bool pulled = false;
    public float maxFreeVel = 50.0f;
    public float slingRadius = 2.0f;
    public float maxRubberVel = 2.0f;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update() {

        Vector3 touchWorldPos;
        Vector3 pullVec;

        if (transform.GetComponent<Rigidbody2D>().velocity.magnitude < 0.1 && transform.GetComponent<Rigidbody2D>().inertia < 0.1)
        { transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero; transform.GetComponent<Rigidbody2D>().inertia = 0f; }

        if (Input.touchCount == 1)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0));
                    touchWorldPos.z = 0;
                    touchBeginPos = touchWorldPos;
                    objBeginPos = transform.position;
                    objDesiredPos = objBeginPos;
                    pulled = true;
                    return;

                case TouchPhase.Moved:
                    touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0));
                    touchWorldPos.z = 0;
                    pullVec = touchWorldPos - touchBeginPos;
                    objDesiredPos = objBeginPos + Vector3.ClampMagnitude(pullVec, slingRadius);
                    pullVec = objDesiredPos - transform.position;
                    pullVec.Normalize();
                    transform.GetComponent<Rigidbody2D>().velocity = maxRubberVel * Vector3.Distance(transform.position, objDesiredPos) * pullVec;
                    break;

                case TouchPhase.Stationary:
                    pullVec = objDesiredPos - transform.position;
                    pullVec.Normalize();
                    transform.GetComponent<Rigidbody2D>().velocity = maxRubberVel * Vector3.Distance(transform.position, objDesiredPos) * pullVec;
                    break;

                case TouchPhase.Ended:
                    touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0));
                    touchWorldPos.z = 0;
                    pulled = false;
                    if (Vector3.Distance(touchWorldPos,touchBeginPos) < 0.2) { transform.position = objBeginPos; transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero; return; }
                    pullVec = transform.position - objBeginPos;
                    transform.GetComponent<Rigidbody2D>().velocity = maxFreeVel * (-pullVec/slingRadius);
                    return;
            }
        }
	}
}
