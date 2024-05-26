using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;
   // private SpriteRenderer sprite; //Cách 2 để quay đầu
    private float traiPhai;
    public float speed = 5.0f;
    public float jump = 8.0f;
    private Boolean quay = true;

    private enum MovementState { idle , run , jump , falling }

    private BoxCollider2D coll;
    public LayerMask jumpableGround;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        //sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        traiPhai = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(traiPhai * speed, rb.velocity.y);
        if (quay == true  && traiPhai == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            quay = false;
        }
        if (quay == false &&  traiPhai == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            quay = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
        
        UpdateAnimation();

    }
    void UpdateAnimation()
    {
        MovementState state;
        if (traiPhai > 0f)
        {
            state = MovementState.run;
            //sprite.flipX = false; 
        }
        else if (traiPhai < 0f)
        {
            state = MovementState.run;
           // sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        if(rb.velocity.y > .1f)
        {
            state = MovementState.jump;
        }else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state",(int)state);
    }
    private bool IsGrounded()
    {
      return  Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
