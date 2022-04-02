using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform handpoint;
    public float speed = 1;
    Vector3 move = Vector3.down;
    bool handOn, bearTaken;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            handOn = true;
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
        if (collision.tag == "Enemy")
        {
            handOn = false;
            bearTaken = true;
            collision.GetComponent<AIMove>().InHand(handpoint);
        }
        Debug.Log(collision.name);
    }
}
