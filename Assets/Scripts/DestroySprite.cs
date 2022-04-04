using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySprite : MonoBehaviour
{
    Color c;
    SpriteRenderer sr;
    public float delta = 1;
    GameController gc;
    void Start()
    {
        gc = GameController.Instance;
        sr = GetComponent<SpriteRenderer>();
        c = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.ready)
        {
            if (c.a > 0)
            {
                c.a -= Time.deltaTime / delta;
                sr.color = c;
            }
            else
            {
                Destroyit();
            }
        }
    }

    void Destroyit()
    {
        Destroy(this.gameObject);
    }
}
