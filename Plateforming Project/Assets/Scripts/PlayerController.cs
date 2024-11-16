using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    
    public float speed;
    Vector2 movement;
    //this creates a variable which the enum can set. this sets the default state as .left which 
    FacingDirection direction = FacingDirection.left;


    public enum FacingDirection
    {
        left, right
    }

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

   
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        Vector2 playerInput = new Vector2();
        MovementUpdate(playerInput);
        GetFacingDirection();

        
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        
        //This line adds force to the rigid body, it uses the movement variable for the vector2 then adds speed 
        rb.AddForce(movement * speed);

        //This allows the player to input using the arrow keys
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        print(movement);

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
        boxcheck =  Physics2D.BoxCast(gameObject.transform.position, new Vector2(1, 1), 0, new Vector2(0, -1), 0.25f, LayerMask.GetMask("ground"));

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
