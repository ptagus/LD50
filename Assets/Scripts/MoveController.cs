using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class MoveController : MonoBehaviour
{
    GameController gc;
    UnityArmatureComponent animator;

    GameObject enemy;

    float pushpower, currentPushPower;
    float playerSpeed;

    bool groundedPlayer, move, jumping, ready;
    void Start()
    {
        animator = GetComponent<UnityArmatureComponent>();
        gc = GameController.Instance;
        playerSpeed = gc.playerSpeed;
        pushpower = gc.PlayerPushPower;
        currentPushPower = pushpower;
        animator.animation.timeScale = gc.enemyIdleSpeed;
        animator.animation.Play("Stand", 1);
    }


    void Update()
    {
        if (ready)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            if (move.x != 0)
                Moving(move * Time.deltaTime * playerSpeed);
            if (move.x == 0)
                Stay();

            if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer && !jumping)
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Push();
            }
        }
        else
        {
            ready = gc.ready;
        }
    }

    void Moving(Vector3 pos)
    {
        if (!move && !jumping)
        {
            move = true;
            animator.animation.timeScale = gc.enemyRunSpeed;
            animator.animation.Play("Run");
        }
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

    void Jump()
    {
        Debug.Log("startJump");
        jumping = true;
        animator.animation.timeScale = gc.enemyJumpSpeed;
        animator.animation.Play("Jump");
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
    }

    void Stay()
    {
        if (move && !jumping)
        {
            move = false;
            animator.animation.timeScale = gc.enemyIdleSpeed;
            animator.animation.Play("Idle");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor")
            StopJump();
    }

    public void StopJump()
    {
        Debug.Log("StopJump");
        jumping = false;
        if (!move)
        {
            move = true;
        }
        else
        {
            move = false;
        }
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
            GetComponent<AudioSource>().Play();
            if (transform.rotation.eulerAngles.y == 0)
            {
                currentPushPower = pushpower;
            }
            if (transform.rotation.eulerAngles.y == 180)
            {
                currentPushPower = -pushpower;
            }
            enemy.GetComponent<AIMove>().Fall();
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(currentPushPower, 0), ForceMode2D.Impulse);
        }
    }
}
