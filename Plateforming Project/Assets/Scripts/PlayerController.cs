using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float acceleration;
    Vector2 movement;

    //this creates a variable which the enum can set. this sets the default state as .left which 
    FacingDirection direction = FacingDirection.left;

    public float apexHeight;
    public float apexTime;
    public float initialMult;
    public float gravityMult;

    float initialJumpVel;
    float gravity;

    bool inAir;
    public float terminalSpeed;




    float currentTime;
    public float coyoteTime;

    //I grabbed this from the codeshare
    /*    public float timeToReachMaxSpeed;
        public float maxSpeed;
        public float timeToDecelerate;
        public float acceleration;
        private float deceleration;
    */
    public enum FacingDirection
    {
        left, right
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

  /*      acceleration = maxSpeed / timeToReachMaxSpeed;
        deceleration = maxSpeed / timeToDecelerate;
*/


        gravity = gravityMult * apexHeight / Mathf.Pow(apexTime, 2);
        initialJumpVel = initialMult * apexHeight / apexTime;
    }


    void Update()
    {
        //this is from the codeshare
        Vector2 playerInput = new Vector2();
        MovementUpdate(playerInput);




        //Debug.Log(IsGrounded());


    }
    private void FixedUpdate()
    {
        if (inAir)
        {
            rb.AddForce(new Vector2(0, gravity));
        }
    }


    private void MovementUpdate(Vector2 playerInput)
    {
        /* Vector2 currentVelocity = rb.velocity;

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
         }*/





        float inputX = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(inputX * acceleration, 0));




        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            //rb.gravityScale = 0;
            //currentVelocity += Vector2.up * jumpPower;

            rb.AddForce(new Vector2(0, initialJumpVel), ForceMode2D.Impulse);

        }


      /*  if (IsGrounded())
        {
            inAir = false;
        }
        else if (!IsGrounded())
        {
            inAir = true;
        }*/



        if (rb.velocity.y <= terminalSpeed)
        {
            //Debug.Log(rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x, terminalSpeed);
        }




        if (IsGrounded())
        {
            inAir = false;

        }
        else if (!IsGrounded())

        {
            currentTime += Time.deltaTime;
            if (currentTime >= coyoteTime)
            {
                inAir = true;
                currentTime = 0;
            }


        }












       //Debug.Log(rb.velocity.y);




        //This was another way to have the player jump. This moves the transform of the player by making it a new vector2
        //this vector 2 does nothing to the x but changes the y based on the current velocity of the chcracter moving, time.deltatime a set multiplier for gravity
        // then using gravity and isgroundedbool it goes across the curve that we set using jump height and time, then mutliply it by time.deltatime for the final of the
        //equation to round it back to a more tangable number and 
        /*else if (!IsGrounded()) 
        {
            isGroundedBool = 1;
        }
        else if (IsGrounded()) 
        {
            isGroundedBool = 0;
            currentVel = 0;
        }

        currentVel = currentVel + gravity * isGroundedBool * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, transform.position.y + currentVel * Time.deltaTime + 0.5f * gravity * isGroundedBool * Time.deltaTime * Time.deltaTime);

*/
        // rb.velocity = currentVelocity;
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

        //Debug.Log(boxcheck);

        if (boxcheck.collider == true)
        {
            return true;

        }
        else
        {
            return false;
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
