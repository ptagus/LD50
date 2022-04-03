using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public GameObject Enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Enemy.GetComponent<AIMove>().NowMove() && collision.tag == "Floor" && collision.tag != "Player")
            Enemy.GetComponent<AIMove>().Fall();
    }
}
