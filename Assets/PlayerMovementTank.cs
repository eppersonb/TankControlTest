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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        

    }

    void playerMove()
    {
        moveCheck();
        aimCheck();

        if(Input.GetKey(KeyCode.W) && canMove) // move forward
        {

            myRigidBody.AddForce(Vector3.forward * moveSpeed);
            Debug.Log("You are moving forward!");
            setMoving(true);
        }
        else if(Input.GetKey(KeyCode.S) && canMove) // move Backwards
        {
            
            myRigidBody.AddForce(Vector3.back * moveSpeed);
            setMoving(true);
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            
            playerAiming();
        }
        else
        {
            isMoving = true;
            isAiming = false;
            //yes
        }

    }

    void moveCheck()
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

    void aimCheck()
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
    void playerAiming()
    {
    
        if(Input.GetKey(KeyCode.Space))
        {
            myRigidBody.AddForce(Vector3.zero);
            setMoving(false);
            setAiming(true);
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
