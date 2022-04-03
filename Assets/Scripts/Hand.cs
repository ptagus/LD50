using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Hand : MonoBehaviour
{
    GameController gc;
    UnityArmatureComponent animator;

    public GameObject bearPrefab;
    public GameObject player;
    public UnityEngine.Transform handpoint;

    GameObject bear;
    float yborder;
    float speed = 1;
    float deltaSpeed;
    Vector3 move = Vector3.down;
    Vector3 playerPos;
    bool handOn, bearTaken, end, animstart, needNew, ready;
    int takePlayerCount;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<UnityArmatureComponent>();
        playerPos = player.transform.position;
        gc = GameController.Instance;
        speed = gc.handSpeed;
        yborder = gc.yBorder;
        deltaSpeed = gc.handDeltaSpeed;
        handOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            if (handOn)
                transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);
            if (bearTaken)
                TakeABear(move * Time.deltaTime * -speed);
            if (transform.position.y > yborder && bearTaken)
            {
                Destroy(bear);
                GetNewBear();
            }
            if (end && !bearTaken)
            {
                handOn = false;
                bearTaken = false;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
            }
        }
        else
        {
            ready = gc.ready;
        }
    }

    void TakeABear(Vector3 pos)
    {
        transform.position += pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Handing!" + collision.name);
        if (!animstart && (collision.tag == "Player" || collision.tag == "Enemy" || collision.tag == "Floor"))
        {
            Debug.Log("Great Handing!" + collision.name);
            Taken();
            animstart = true;
            if (collision.tag == "Player")
            {
                gc.ShowLoseWindow();
            }
        }
    }

    public void TakeBear(string tag, GameObject go)
    {
        if (tag == "Floor")
        {
            handOn = false;
            bearTaken = true;
            bear = Instantiate(bearPrefab, handpoint.position, Quaternion.identity, handpoint);
            bear.GetComponent<AIMove>().enabled = false;
            bear.GetComponent<Rigidbody2D>().isKinematic = true;
            bear.transform.localScale = new Vector3(0.25f, 0.25f, 1);
            gc.SetFloorDown();
        }
        if (tag == "Enemy")
        {
            handOn = false;
            bearTaken = true;
            go.GetComponent<AIMove>().InHand(handpoint);
            bear = go;
            needNew = true;
            gc.SetFloorDown();
        }
    }

    void Taken()
    {
        animator.animation.Play("animtion0", 1);
    }

    void GetNewBear()
    {
        gc.SetNewScore();
        takePlayerCount++;
        if (needNew)
        {
            if (!end)
            {
                gc.CreateNewBoy();
            }
            needNew = false;
        }
        animstart = false;
        handpoint.GetComponent<TakingHand>().setTake();
        animator.animation.Stop();
        if (takePlayerCount > 1)
        {
            playerPos = player.transform.position - Vector3.up;
            takePlayerCount = 0;
        }
        else
        {
            playerPos = new Vector2(Random.Range(-30, 20), player.transform.position.y - 1);
        }
        transform.position = new Vector3(Random.Range(-20,20), transform.position.y, 0);
        bearTaken = false;
        speed += deltaSpeed;
        if (gc.end)
        {
            end = gc.end;
            handOn = false;
        }
        else
        {
            handOn = true;
        }
    }
}
