using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Enemy.GetComponent<AIMove>().NowMove() && collision.tag == "Floor" && collision.tag != "Player")
            Enemy.GetComponent<AIMove>().Fall();
    }
}
