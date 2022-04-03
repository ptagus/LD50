using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public GameObject bear;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            bear.GetComponent<AIMove>().Fall();
            Debug.Log("Fall" + name);
        }
    }
}
