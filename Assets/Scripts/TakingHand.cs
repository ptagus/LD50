using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingHand : MonoBehaviour
{
    public GameObject hand;
    bool take;
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
        if (!take)
        {
            if (collision.tag == "Enemy" || collision.tag == "Floor")
            {
                take = true;
                hand.GetComponent<Hand>().TakeBear(collision.tag, collision.gameObject);
            }
        }
    }

    public void setTake()
    {
        take = false;
    }
}
