using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BearsStates
{
    Moving,
    Panic,
    Fallen,
    onFall
}

public class AIMove : MonoBehaviour
{
    public bool move;
    BearsStates bstate;
    Vector3 newpos;
    private Vector3 playerVelocity;
    bool groundedPlayer = true;
    public float speed = 0.2f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    Rigidbody2D rb;
    void Start()
    {
        move = true;
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = new Vector2(0, 0.7f);
    }

    void Update()
    {
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

    public void Fall()
    {
        move = false;
        //rb.centerOfMass = new Vector2(0, 0);
    }
    void RunOut()
    {
        speed *= -1;
    }

    public void InHand(Transform parent)
    {
        move = false;
        transform.SetParent(parent);
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
