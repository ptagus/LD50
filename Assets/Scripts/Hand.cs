using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    GameObject bear;
    GameController gc;
    public GameObject bearPrefab;
    public GameObject player;
    public Transform handpoint;
    public float speed = 1;
    Vector3 move = Vector3.down;
    Vector3 playerPos;
    bool handOn, bearTaken, end;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.transform.position;
        gc = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            end = true;
        }
        if (handOn)
            transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);
        if (bearTaken)
            TakeABear(move * Time.deltaTime * -speed);
        if (transform.position.y > 30)
        {
            Destroy(bear);
            GetNewBear();
        }
        if(end)
        {
            handOn = false;
            bearTaken = false;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
        }
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
                bear.transform.localScale = new Vector3(0.04f, 0.02f, 1);
            }
            if (collision.tag == "Player")
            {
                Debug.Log("End" + collision.name);
                gc.ShowLoseWindow();
            }
            gc.SetNewScore();
            //gc.SetFloorPosition();
            Debug.Log("Floorlevel" + collision.name);
        }
    }

    void GetNewBear()
    {
        playerPos = player.transform.position - Vector3.up;
        transform.position = new Vector3(Random.Range(-20,20), transform.position.y, 0);
        bearTaken = false;
        handOn = true;
    }
}
