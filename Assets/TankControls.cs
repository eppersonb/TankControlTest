/*
 * Script that gives tank controls to player
 * may need animator to contain idle and walking animation
 * --in order for camera to not be swaying with the animation, remove Q and T root in the animation
 * 
 * moving is wasd
 * 
 * for controls, check Edit > Project Settings > Inputs
 *
 * drag and drop gameobject (player avatar with animation controller) into 'thePlayer'
 *
 * NOTE: if elses could be more optimized if they were combined together more rather than adding more if elses
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControls : MonoBehaviour {

    public GameObject thePlayer;
    public bool isMoving;
    public bool isRunning;

	public bool isAiming;
	public bool backwardsCheck = false;
    public float horizontalMove;
    public float verticalMove;

    // Update is called once per frame
    void Update()
    {
		if (isAiming == false) { // isAiming is taken from WeaponMechanics.cs where is initialized as a 'static' bool which allows it to be here, this if makes sure you can't move if you're aiming
			if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0) { // if inputkey is going backwards, turn on backwards check to disable sprinting
				backwardsCheck = true;
			}else
			{
				backwardsCheck = false;
			}
				
			if (Input.GetButton("Vertical")) { // Moving foward and backwards
				if  (Input.GetKey(KeyCode.LeftShift) && (backwardsCheck == false)) { // while moving, if leftshift is pressed, run  !!!(MAY NEED TO CHANGE 'GetKey' TO ACTUAL INPUT/GetButton)!!!
					isRunning = true;
				}else {
					isRunning = false;
				}

				if (isRunning == true) { // If running is true, increase forward moving speed
					thePlayer.GetComponent<Animator>().Play("Run");//                                                                     !!!CURRENTLY NO RUNNING ANIMATION!!!
					
					print("I am running!"); // This is the debug message for the console as placeholder for animations!!!!!
					
					verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 6; // move forward with increased movement speed
					thePlayer.transform.Translate(0, 0, verticalMove);
				}else {
					if (Input.GetAxisRaw("Vertical") > 0){ // if character is pressing button to MOVE FORWARD
					thePlayer.GetComponent<Animator>().Play("WalkForward"); // plays walk animation if button pressed to move is pressed     !!!MISSING ANIMATION!!!
					
					print("I am moving forward!"); // This is the debug message for the console as placeholder for animations!!!!!
					
					verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 3;
					thePlayer.transform.Translate(0, 0, verticalMove);
					
					} else { //this code assumes that the player is WALKING BACKWARDS, potential bugs regarding walking backwards may occur here
						thePlayer.GetComponent<Animator>().Play("WalkBackwards"); //plays walk animation if button pressed to move is pressed    !!!CURRENTLY NO WALKING BACKWARDS ANIMATION!!!
						
						print("I am moving backwards!"); // This is the debug message for the console as placeholder for animations!!!!!
						
						verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 1; // walking is slower backwards
						thePlayer.transform.Translate(0, 0, verticalMove);
					}	
				}

				isMoving = true;
				horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 50; // if player is moving, turn speed is slower
				thePlayer.transform.Rotate(0, horizontalMove, 0);
			}else {
				if (Input.GetButton("Horizontal"))  // Turning only which is A and D
			{
					isMoving = true;
					if (Input.GetAxisRaw("Horizontal") > 0) { // checks if button to TURN RIGHT is pressed 
					thePlayer.GetComponent<Animator>().Play("TurnRight"); // character plays animation to turn right                     !!!MISSING ANIMATION TO TURN RIGHT!!!
					
					print("I am turning right!"); // This is the debug message for the console as placeholder for animations!!!!!
					
					} else { // assumes player is pressing the button to TURN LEFT, potential bugs regarding turning left may occur here
					thePlayer.GetComponent<Animator>().Play("TurnLeft"); // character plays animation to turn left                       !!!MISSING ANIMATION TO TURN LEFT!!!
					
					print("I am turning left!"); // This is the debug message for the console as placeholder for animations!!!!!
					
					}
					horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
					thePlayer.transform.Rotate(0, horizontalMove, 0);
				}
				else  // Not moving or turning
				{
					isMoving = false;
					thePlayer.GetComponent<Animator>().Play("Idle"); // play idle animation if character is not moving                   !!!MISSING ANIMATION TO IDLE!!!
				}
			}
		}
	}
}

