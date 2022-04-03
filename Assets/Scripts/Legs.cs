using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    public GameObject Enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Enemy.GetComponent<AIMove>().NowMove())
            Enemy.GetComponent<AIMove>().StopJump();
    }
}
