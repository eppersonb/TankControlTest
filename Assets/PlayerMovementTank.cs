using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovementTank : MonoBehaviour
{


Animator animator;
Rigidbody myRigidBody;

Vector3 movement;

Vector3 currentVelocity;

Quaternion myQuatenion;
private bool isRunning;

[SerializeField]
private bool isMoving;

[SerializeField]
private bool canMove;

private bool isMoveForward;

private bool isMoveBack;

[SerializeField]

private bool isAiming;

[SerializeField]
private float moveSpeed = 5f;

[SerializeField]
private float turnSpeed;

Vector3 turnDirection;


private float stopSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
         //Get Component in current object with script
        
        myRigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // get animator component.
        
    }
    

    // Update is called once per frame
    void Update()
    {
        playerMove(); // this is the playerMove function always updating the players actions
        
    }

    void playerMove()
    {
        
        moveCheck();
        aimCheck();
        float horizontalInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime * 100;
        float verticalInput = Input.GetAxisRaw("Vertical") * Time.deltaTime * 6;
        Vector3 vMoveDir = new Vector3(0, 0, verticalInput).normalized; //this makes sure the input is no greater than 1
        Vector3 hMoveDir = new Vector3(horizontalInput, 0, 0).normalized;



        if(vMoveDir != Vector3.zero && Input.GetKey(KeyCode.W)  && canMove)//This controls forward movement
        {
            Debug.Log("Walking Forward");
            animator.SetBool("isWalking", true);
            transform.Translate(0, 0, verticalInput);
            
        }

        if(vMoveDir != Vector3.zero && Input.GetKey(KeyCode.S) && canMove) // this controls backwards movement
        {
            Debug.Log("is walking backwards");
            animator.SetBool("isWalkingBack", true);
            transform.Translate(0, 0, verticalInput);
        }

        if(hMoveDir != Vector3.zero && canMove) //this controls turns
        {

			transform.Rotate(0, horizontalInput, 0);
        }
        
            

        // if(hMoveDir != Vector3.zero)//This only executes if only you are moving backwards.
        // {
  
        //     Quaternion target = Quaternion.LookRotation(hMoveDir); //this is suppose to handle turning in a direction.
        //     myRigidBody.MoveRotation(Quaternion.RotateTowards(myRigidBody.rotation, target, turnSpeed));
            
        // }



        // if(hMoveDir != Vector3.zero && canMove && myRigidBody.velocity.x <= -10)//This only executes if only you are moving backwards.
        // {

        //     myRigidBody.AddForce(hMoveDir * -moveSpeed);
        // }
        /*
            This code is driving me insane lol
        */

        // if(moveDir == Vector3.zero)
        // {
        //     Quaternion target = Quaternion.LookRotation(moveDir); //this is suppose to handle turning in a direction.
        //     myRigidBody.MoveRotation(Quaternion.RotateTowards(myRigidBody.rotation, target, turnSpeed));
        // }

        // if(moveDir != Vector3.zero)
        // {
        //     Quaternion target = Quaternion.LookRotation(moveDir); //this is suppose to handle turning in a direction.
        //     myRigidBody.MoveRotation(Quaternion.RotateTowards(myRigidBody.rotation, target, turnSpeed));
        // }


        if(vMoveDir == Vector3.zero) // this happens if no inputs are being pressed.
        {
            animator.SetBool("isWalkingBack", false);
            animator.SetBool("isWalking", false);
            myRigidBody.velocity = Vector3.zero;
            // Debug.Log("Stopped");
            
        }




        if(Input.GetKey(KeyCode.Space))
        {
            myRigidBody.velocity = Vector3.zero; // This forces the rigid body to be set to zero.
            playerAiming(); //call player aiming function.
        }

        else // if nothing is happening, refer to this else statement which serves as the default state.
        {
            isMoving = true;
            isAiming = false;
            animator.SetBool("isAiming", false);
            //yes
        }

        // if()

        // bool forwardKeyPressed = Input.GetKey(KeyCode.W);
        // bool backwardKeyPressed = Input.GetKey(KeyCode.S);
        // bool leftKeyPressed = Input.GetKey(KeyCode.A);
        // bool rightKeyPressed = Input.GetKey(KeyCode.D);

        // if(forwardKeyPressed && canMove && myRigidBody.velocity.z <= 10) // move forward and set animation state for walking forward.
        // {

        //     myRigidBody.AddForce(transform.forward * moveSpeed);
        //     animator.SetBool("isWalking", true);
        //     Debug.Log("You are moving forward!");
        //     setMoving(true);
        // }
        
        // if(!forwardKeyPressed)
        // {
        //     animator.SetBool("isWalking", false);
        // }

        // if(leftKeyPressed) // turns
        // {   
        //    transform.Rotate(0, turnSpeed, 0);
        //    Debug.Log("turning");

        // }

        // if(!leftKeyPressed)
        // {
        //     Debug.Log("put something here");
        // }

        // if(rightKeyPressed) // turns right
        // {
        //     transform.Rotate(0, turnSpeed, 0);
        //     setMoving(true);
        // }
        
        // if(!rightKeyPressed)
        // {
        //     Debug.Log("put something here");
        // }


        // if(backwardKeyPressed && canMove && -myRigidBody.velocity.z <= 10) // move Backwards
        // {
            
        //     myRigidBody.AddForce(Vector3.back * moveSpeed);
        //     animator.SetBool("isWalkingBack", true);
        //     Debug.Log("You are moving backwards!");
        //     setMoving(true);
        // }

        // if(!backwardKeyPressed)
        // {
        //     animator.SetBool("isWalkingBack", false);
        // }

        // else // if nothing is happening, refer to this else statement which serves as the default state.
        // {
            
        //     isMoving = true;
        //     isAiming = false;
        //     animator.SetBool("isAiming", false);
        //     //yes
        // }

    }

    void moveCheck() // this function checks if the player is moving.
    {
        if(isMoving)
        {
           canMove = true;
        }
        else if(!isMoving)
        {
            canMove = false;
        }
    }

    void forwardCheck()
    {
        Debug.Log("Do something");
    }

    void backwardsCheck()
    {
        Debug.Log("Do something");
    }

    void aimCheck() // this function checks if the player is aiming.
    {
        if(isAiming)
        {
            myRigidBody.AddForce(Vector3.back * 0);
            Debug.Log("Call aiming script");
            setMoving(false);
        }
        else if(!isAiming)
        {
            setMoving(true);
        }
        
    }
    void playerAiming() // this is the player aiming function.
    {
    
        if(Input.GetKey(KeyCode.Space))
        {
            myRigidBody.AddForce(Vector3.zero);
            setMoving(false);
            setAiming(true);
            animator.SetBool("isAiming", true);
            Debug.Log("You're aiming, you cannot move!");
        }
        if(!Input.GetKey(KeyCode.Space))
        {
            setMoving(true);
            setAiming(false);
        }
    }
    /*
        Getters and setters
    */
    public bool getAiming()
    {
        return isAiming;
    }

    public void setAiming(bool isAiming)
    {
        this.isAiming = isAiming;
    }
    public bool getMoving()
    {
        return isMoving;
    }

        public void setMoving(bool isMoving)
    {
        this.isMoving = isMoving;
    }


}
