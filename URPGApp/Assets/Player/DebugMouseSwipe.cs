using UnityEngine;
using System.Collections;

public class DebugMouseSwipe : MonoBehaviour
{
    // Stores pos of where touched and where object is at start of touch and where object wants to be
    private Vector3 touchBeginPos;
    private Vector3 objBeginPos;
    private Vector3 objDesiredPos;

    // Bool for peing pulled and variables for movement
    public Sprite heroSprite;
    public float maxFreeVel = 50.0f;
    public float slingRadius = 2.0f;
    public float maxRubberVel = 2.0f;
    public bool movingBool;
    public bool touchedBool = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Where the user touches and the pulling vector from the user
        Vector3 touchWorldPos;
        Vector3 pullVec;

        // Limits how minimum speed
        if (transform.GetComponent<Rigidbody2D>().velocity.magnitude < 0.2f)
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (Mathf.Abs(transform.GetComponent<Rigidbody2D>().angularVelocity) < 10f)
            transform.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        if (transform.GetComponent<Rigidbody2D>().velocity.magnitude == 0f && Mathf.Abs(transform.GetComponent<Rigidbody2D>().angularVelocity) < 2f)
            movingBool = false;
        else if (!touchedBool)
            movingBool = true;

        if (movingBool)
            return;
        else if (Input.GetMouseButtonDown(0))
        {
            touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            touchWorldPos.z = 0;
            touchBeginPos = touchWorldPos;
            objBeginPos = transform.position;
            objDesiredPos = objBeginPos;

            Transform heroGhost = new GameObject().transform;
            heroGhost.name = "heroGhost";
            heroGhost.gameObject.AddComponent<SpriteRenderer>().sprite = heroSprite;
            heroGhost.transform.position = transform.position + new Vector3(0f, 0f, 1f);
            heroGhost.transform.rotation = transform.rotation;
            heroGhost.transform.localScale = transform.localScale;
            Color tmp = heroGhost.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.5f;
            heroGhost.gameObject.GetComponent<SpriteRenderer>().color = tmp;

            touchedBool = true;
            return;
        }
        else if (Input.GetMouseButton(0) && touchedBool)
        {
            touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            touchWorldPos.z = 0;
            pullVec = touchWorldPos - touchBeginPos;
            objDesiredPos = objBeginPos + Vector3.ClampMagnitude(pullVec, slingRadius);
            pullVec = objDesiredPos - transform.position;
            pullVec.Normalize();
            transform.GetComponent<Rigidbody2D>().velocity = maxRubberVel * Vector3.Distance(transform.position, objDesiredPos) * pullVec;
        }

        else if (Input.GetMouseButtonUp(0) && touchedBool)
        {
            GameObject heroGhost = GameObject.Find("heroGhost");
            Destroy(heroGhost);
            touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            touchWorldPos.z = 0;
            if (Vector3.Distance(touchWorldPos, touchBeginPos) < 0.2) { transform.position = objBeginPos; transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero; return; }
            pullVec = transform.position - objBeginPos;
            transform.GetComponent<Rigidbody2D>().velocity = maxFreeVel * (-pullVec / slingRadius);
            touchedBool = false;
        }

    }
}