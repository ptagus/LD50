using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float ypow, xpow;
    public float yforce, xforce;
    GameObject enemy;
    Vector3 newpos;
    private Vector3 playerVelocity;
    bool groundedPlayer = true;
    public float playerSpeed = 1;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    void Start()
    {
        
    }


    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (move.x !=0)
            Moving(move * Time.deltaTime * playerSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Push();
        }
    }

    void Moving(Vector3 pos)
    {
        //Debug.Log("move");
        Quaternion quaternion = new Quaternion();
        if (pos.x < 0)
        {
            quaternion.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            quaternion.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.rotation = quaternion;
        transform.position += pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor")
            groundedPlayer = true;
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor")
            groundedPlayer = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        enemy = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy = null;
    }

    void Push()
    {
        if (enemy is not null)
        {
            if (transform.rotation.eulerAngles.y == 0)
            {
                xpow = ypow;
            }
            if (transform.rotation.eulerAngles.y == 180)
            {
                xpow = -ypow;
            }
            enemy.GetComponent<AIMove>().Fall();
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(xpow, 0), ForceMode2D.Impulse);
        }
    }
}
