using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovementTank : MonoBehaviour
{


Animator animator;
Rigidbody myRigidBody;
private bool isRunning;

[SerializeField]
private bool isMoving;

[SerializeField]
private bool canMove;

[SerializeField]

private bool isAiming;

[SerializeField]
private float moveSpeed = 5f;

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
        bool forwardKeyPressed = Input.GetKey(KeyCode.W);
        bool backwardKeyPressed = Input.GetKey(KeyCode.S);

        if(forwardKeyPressed && canMove) // move forward and set animation state for walking forward.
        {

            myRigidBody.AddForce(Vector3.forward * moveSpeed);
            animator.SetBool("isWalking", true);
            Debug.Log("You are moving forward!");
            setMoving(true);
        }

        else if(backwardKeyPressed && canMove) // move Backwards
        {
            
            myRigidBody.AddForce(Vector3.back * moveSpeed);
            animator.SetBool("isWalkingBack", true);
            Debug.Log("You are moving backwards!");
            setMoving(true);
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            
            playerAiming();
        }
        else // if nothing is happening, refer to this else statement which serves as the default state.
        {
            isMoving = true;
            isAiming = false;
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingBack", false);
            animator.SetBool("isAiming", false);
            //yes
        }

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
        else if(!Input.GetKey(KeyCode.Space))
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
