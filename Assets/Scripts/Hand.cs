using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    GameObject bear;
    GameController gc;
    public GameObject bearPrefab;
    public Transform handpoint;
    public float speed = 1;
    Vector3 move = Vector3.down;
    bool handOn, bearTaken;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            handOn = true;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            handOn = bearTaken = false;
            Destroy(bear);
        }
        if (handOn)
            TakeABear(move * Time.deltaTime * speed);
        if (bearTaken)
            TakeABear(move * Time.deltaTime * -speed);
    }

    void TakeABear(Vector3 pos)
    {
        transform.position += pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bearTaken)
        {
            if (collision.tag == "Enemy")
            {
                handOn = false;
                bearTaken = true;
                collision.GetComponent<AIMove>().InHand(handpoint);
                bear = collision.gameObject;
            }
            if (collision.tag == "Floor")
            {
                handOn = false;
                bearTaken = true;
                bear = Instantiate(bearPrefab, handpoint.position, Quaternion.identity, handpoint);
            }
            gc.SetFloorPosition();
            Debug.Log("Floorlevel" + collision.name);
        }
    }
}
