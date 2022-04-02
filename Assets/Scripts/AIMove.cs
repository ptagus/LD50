using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

enum BearsStates
{
    Moving,
    Panic,
    Fallen,
    onFall
}

public class AIMove : MonoBehaviour
{
    UnityArmatureComponent animator;
    public GameObject head;
    float timer = 3;
    public bool move;
    BearsStates bstate;
    Vector3 newpos;
    private Vector3 playerVelocity;
    bool push;
    public float speed = 0.2f;
    private float jumptimer = 1.0f;
    private float gravityValue = -9.81f;
    Rigidbody2D rb;
    void Start()
    {
        animator = GetComponent<UnityArmatureComponent>();
        jumptimer = Random.Range(3, 10);
        bstate = SetNewState();
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = new Vector2(0, 0.7f);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            bstate = SetNewState();
            Debug.Log(bstate + "_-_" + this.name);
            timer = Random.Range(3, 7);
        }
        jumptimer -= Time.deltaTime;
        if (jumptimer < 0 && move)
        {
            Jump();
            jumptimer = Random.Range(3, 10);
        }
        if (bstate == BearsStates.Moving)
        {
            Vector3 move = new Vector3(speed, 0, 0);
            if (move.x != 0)
                Moving(move * Time.deltaTime * Mathf.Abs(speed));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            bstate = GetNewState(BearsStates.Moving);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bstate = GetNewState(BearsStates.Panic);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RunOut();
            Debug.Log(speed);
        }

        if (push)
        {
            transform.Rotate(new Vector3(0, 0, 1));
            if (Mathf.Abs(transform.rotation.eulerAngles.z) >= 90)
            {
                push = false;
            }
        }
    }

    void Moving(Vector3 pos)
    {
        if (move)
        {
            //Debug.Log("move" + pos);
            Quaternion quaternion = new Quaternion();
            if (pos.x < 0)
                quaternion.eulerAngles = new Vector3(0, 180, 0);
            else
                quaternion.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = quaternion;
            transform.position += pos;
        }
    }

    BearsStates GetNewState(BearsStates newstate)
    {
        return newstate;
    }

    BearsStates SetNewState()
    {
        int i = Random.Range(0, 100);
        if (i < 30)
        {
            return BearsStates.Panic;
        }
        else
        {
            if (i > 70)
            {
                RunOut();
            }
            return BearsStates.Moving;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            RunOut();
            timer = Random.Range(3, 7);
        }
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
    }

    public void Fall()
    {
        animator.animation.Stop();
        push = true;
        move = false;
    }
    void RunOut()
    {
        speed *= -1;
    }

    public void InHand(UnityEngine.Transform parent)
    {
        move = false;
        transform.position = parent.position;
        transform.SetParent(parent);
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
