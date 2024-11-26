using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float accelerationTime;
    float accelaration;
    float velocity;

    Vector2 movement;

    //this creates a variable which the enum can set. this sets the default state as .left which 
    FacingDirection direction = FacingDirection.left;

    public float apexHeight;
    public float apexTime;
    public float gravity;
    float currentTime;
    Vector2 position;


    //I grabbed this from the codeshare
    public float timeToReachMaxSpeed;
    public float maxSpeed;
    public float timeToDecelerate;
    private float acceleration;
    private float deceleration;
    public float jumpPower;

    public enum FacingDirection
    {
        left, right
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        acceleration = maxSpeed / timeToReachMaxSpeed;
        deceleration = maxSpeed / timeToDecelerate;

    }


    void Update()
    {
        //this is from the codeshare
        Vector2 playerInput = new Vector2();
        MovementUpdate(playerInput);

    }


    private void MovementUpdate(Vector2 playerInput)
    {
        Vector2 currentVelocity = rb.velocity;

        //if move the character to the left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentVelocity += acceleration * Vector2.left * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            currentVelocity -= deceleration * Vector2.left * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentVelocity += acceleration * Vector2.right * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            currentVelocity -= deceleration * Vector2.right * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rb.gravityScale = 0;
            currentVelocity += Vector2.up * jumpPower;
        }

        if (Input.GetKeyUp(KeyCode.Space) && !IsGrounded())
        {
            rb.gravityScale = gravity;
        }

        rb.velocity = currentVelocity;
    }

    public bool IsWalking()
    {
        if (movement.x > 0 || movement.x < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool IsGrounded()
    {
        RaycastHit2D boxcheck;
        boxcheck = Physics2D.BoxCast(gameObject.transform.position, new Vector2(1, 1), 0, new Vector2(0, -1), 0.25f, LayerMask.GetMask("ground"));

        if (boxcheck.collider == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public FacingDirection GetFacingDirection()
    {
        //This grabs momentum and depending on whether its negtive or posative
        //Sit sets the return that it gets as a variable which is then called
        // to save the state in which the character is facing
        if (movement.x > 0)
        {
            direction = FacingDirection.right;
        }
        else if (movement.x < 0)
        {
            direction = FacingDirection.left;
        }

        return direction;

    }
}
