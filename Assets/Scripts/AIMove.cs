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
    GameController gc;
    UnityArmatureComponent animator;
    float stateTimer, jumptimer;
    float jumpforce;
    BearsStates bstate;
    bool push, jumping, move, fall;
    float speed = 1;
    Rigidbody2D rb;
    Quaternion quaternion = new Quaternion();

    public GameObject head, legs;
    void Start()
    {
        move = true;
        gc = GameController.Instance;
        animator = GetComponent<UnityArmatureComponent>();
        speed = gc.enemySpeed;
        stateTimer = gc.SetStateTimer();
        jumptimer = gc.SetJumpTimer();
        jumpforce = gc.enemyJumpPower;
        bstate = SetNewState();
        rb = GetComponent<Rigidbody2D>();
        //rb.centerOfMass = new Vector2(0, 0.7f);
        StartCoroutine("Standing");
    }



    void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0 && !jumping)
        {
            bstate = SetNewState();
            stateTimer = gc.SetStateTimer();
        }

        jumptimer -= Time.deltaTime;
        if (jumptimer < 0 && move)
        {
            Jump();
            jumptimer = gc.SetJumpTimer();
        }

        if (bstate == BearsStates.Moving)
        {
            Vector3 move = new Vector3(speed, 0, 0);
            if (move.x != 0)
                Moving(move * Time.deltaTime * Mathf.Abs(speed));
        }

        if (push)
        {
            transform.Rotate(new Vector3(0, 0, 1));
            if (Mathf.Abs(transform.rotation.eulerAngles.z) >= 90)
            {
                push = false;
            }
        }

        if (fall)
        {
            StandUpStart();
            fall = false;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Fall();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.animation.Stop();
            move = false;
            push = true;
        }
    }

    void Moving(Vector3 pos)
    {
        if (move)
        {
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
            animator.animation.Play("Idle");
            return BearsStates.Panic;
        }
        else
        {
            if (i > 70)
            {
                RunOut();
            }
            animator.animation.Play("Run");
            return BearsStates.Moving;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            RunOut();
            stateTimer = gc.SetStateTimer();
        }
    } 

    public void StopJump()
    {
        jumping = false;
        if (bstate == BearsStates.Moving)
        {
            animator.animation.timeScale = gc.enemyRunSpeed;
            animator.animation.Play("Run");
        }
        if (bstate == BearsStates.Panic)
        {
            animator.animation.timeScale = gc.enemyIdleSpeed;
            animator.animation.Play("Idle");
        }
    }

    public void Jump()
    {
        jumping = true;
        animator.animation.timeScale = gc.enemyJumpSpeed;
        animator.animation.Play("Jump");
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
    }

    public void Fall()
    {
        Debug.Log("fall");
        animator.animation.Stop();
        push = true;
        move = false;
        fall = true;
    }

    public void StandUpStart()
    {        
        StartCoroutine("Standing");
    }

    IEnumerator Standing()
    {
        quaternion.eulerAngles = new Vector3(0, 0, 0);
        this.transform.rotation = quaternion;
        animator.animation.Play("Stand");
        yield return new WaitForSeconds(gc.enemyStandSpeed);
        StandUp();
    }

    public void StandUp()
    {
        push = false;
        move = true;
        SetNewState();
    }
    void RunOut()
    {
        speed *= -1;
    }

    public void InHand(UnityEngine.Transform parent)
    {
        Destroy(rb);
        animator.animation.Stop();
        move = false;
        transform.position = parent.position;
        transform.SetParent(parent);
    }

    public bool NowMove()
    {
        return move;
    }
}
