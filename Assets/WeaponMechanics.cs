/*
 * Script that allows the player to use their weapon
 * This includes aiming, shooting, and (probably) reloading
 *
 * Right Mouse Button should be aiming
 * Left Mouse Button should be shooting (not implemented)
 *
 * Want to make it so that you can't move or turn while aiming (already done, see line 30 in TankControls.cs), line number may change but it's written as WeaponMechanics.isAiming
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMechanics : MonoBehaviour {
	/*********************************************************************************************/
	/* This is the aiming stuff */
	
	public static bool isAiming = false; // "static" allows the initialized 'isAiming' to be used in TankControls.cs
	public GameObject thePlayer;
	public float horizontalAim = 2.0F;
	public float verticalAim = 2.0F;
	
	public static double aimDownLimit = 45.3; // value needs to be slightly higher than the intended target limit
	public double aimDownReset = 45; // aimingDownLimit cannot be == to when it resets or else it'll get locked because it resets always resets to a value very slightly higher than 45
	public static double aimUpLimit = 315; // these two doubles are made static so the overlaps below can use them in their calculations
	
	public double aimDownRangeOverlap = (aimUpLimit - 5.0); // value to overlap the aimUpLimit should never be a value above it 
	public double aimUpRangeOverlap = (aimDownLimit + 5.0); // value to overlap the aimDownLimit should never be a value below it 
	
	public float aimY;
	public float aimX;
	
	public Vector3 objectRotation; //initialize vector to reset rotation to be upright
	
	/*********************************************************************************************/
	/* This is the shooting stuff */
	
	// make the reload like re5 and allow inventory reload, but that would be in the inventory section rather here
	public bool isGunEmpty = false;
	public int ammoSpare = 30; // !!!placeholder value of 30!!!
	public int ammoMagazineSize = 10;
	public int ammoCount; // !!!placeholder value of 10!!!
	
	public float fireRate = 0.75F; //weapon firerate
	public float reloadWait = 5.0F; //weapon reload time length
	private float nextFire = 0.0F;
	
	
	
	// Start is called before the first frame update
	void Start() {
		ammoCount = ammoMagazineSize; // start frame with ammocount full
	}


    // Update is called once per frame
    void Update() {
		objectRotation = transform.localEulerAngles; // making the vector3 object rotation copy values of xyz rotations at all times
		objectRotation.z = 0;						 // setting z-axis to zero because there is no reason for it to be modified unless want leaning
		transform.localEulerAngles = objectRotation; // applying new angle of z-axis
		
        if (Input.GetButton("Fire2")) { // when inputkey to fire2 (default is right mouse button), player will aim their weapon
			isAiming = true;
			thePlayer.GetComponent<Animator>().Play("aimWeapon"); //animation for aiming weapon                      !!!MISSING ANIMATION!!!
			
			//print("I am aiming!"); // This is the debug message for the console as placeholder for animations!!!!!
			
			
			/**************************************************************************************************************************************************************************************************************/
			// while aiming (default right mouse button), you can move your mouse to rotate                      !!!currently rotates the entire object, later want to just rotate only above the hip/upper body half!!!  maybe use inverse kinematics?
			aimX = Input.GetAxis("Mouse X") * horizontalAim; // this is the character rotating horizontally while aiming
			aimY = Input.GetAxis("Mouse Y") * verticalAim;   // this is the character rotating vertically while aiming
			transform.Rotate(-aimY,aimX,0);
			
			// set limit of how far aiming can rotate along the vertical axis creating a range that cannot be between 45 degrees to 315 degrees
			if (objectRotation.x > aimDownLimit && objectRotation.x < aimDownRangeOverlap) { //player cannot look down beyond the aimDownlimit, the aimDownRangeOverlap overlap the looking up restriction, should not be a value above aimUpLimit
				objectRotation.x = (float)aimDownReset; // cast to be float
				transform.localEulerAngles = objectRotation;
				aimY = 0f;
			}
			if (objectRotation.x < aimUpLimit && objectRotation.x > aimDownRangeOverlap) { // player cannot look up beyond a 45 degree angle, shown as 315 from 360-45. the 50 degree angle is to overlap the looking down description, should not be under 45.3
				objectRotation.x = (float)aimUpLimit; // cast to be float
				transform.localEulerAngles = objectRotation;
				aimY = 0f;
			}
			/**************************************************************************************************************************************************************************************************************/
			
			/* the code here is for firing the weapon */
			
			if (ammoCount < 1) {
				isGunEmpty = true; // gun is empty
			} else {
				isGunEmpty = false; // gun is not empty therefore can shoot
				if(Input.GetButton("Fire1") && (Time.time > nextFire)) { // when inputkey to fire1 (default is left mouse button), while fire2 is being inputted, player will fire weapon  !!!Currently only removes ammocount, one click unloads all 10 ammocount!!!
																		// (Time.time > nextFire) is the delay of when can be fired again. Time.time is the allotted time instance is running, delay works as long as time is greater than nextFire, but every input of
																		//       shooting results in nextFire being "fireRate" higher than time alloted.
					ammoCount--; // removes an ammo
					print("schoot gun"); //this is debug message for shoot gun
					nextFire = Time.time + fireRate; // delay before can fire again
					
					// animation should just be coded to make recoil, just moving arm back (animation code goes here)
				}
			}
			
		}else { // the player is no longer inputting the key to aim (default is right mouse button), this else assumes that the player is not aiming
			isAiming = false;
			
			
			/**************************************************************************************************************************************************************************************************************/
			transform.Rotate(0,0,0); // this stops a bug where if you aim and unaim while moving the mouse, the object will keep spinning in a direction like a globe
			objectRotation.x = 0;   // setting a vertical axis of rotation to zero
			transform.localEulerAngles = objectRotation; // applying value so that the horizontal axis of the aiming changes but not the vertical axis when not aiming anymore
			/**************************************************************************************************************************************************************************************************************/
			
		}
		
		if(Input.GetButton("Reload") && ammoSpare > 0 && ammoCount != (ammoMagazineSize + 1) && (Time.time > nextFire)) { // when input to reload (default is r) is inputted when 
																														  // gun is empty AND player has spare ammo AND if the ammocount is not equal to the magazinesize (the +1 is the chamber), player reloads
																														  // for (Time.time > nextFire), see above from the code about shooting !!!Note should probably put this in the reloading calculation
		
												//play animation here, wait length of animation then reload ammo                     !!!CODING NOTES, STILL UNFINISHED!!!
												
			thePlayer.GetComponent<Animator>().Play("reloadWeapon"); //animation for reloading weapon
			print("reloading weapon"); // debug message
			
			
			/* Reloading Calculation */
			if (ammoCount < ammoMagazineSize) { // calculation to add ammo to gun and remove from spare if ammoCount is less than ammoMagazineSize
				nextFire = Time.time + reloadWait; // reloading delay before adding the ammo back into gun       !!!NOT WORKING PROPERLY, PROBABLY CONFLICTING WITH THE FIRING DELAY!!! probably put this above before this if and add (Time.time > nextFire) to this if
				ammoSpare = ammoSpare - (ammoMagazineSize - ammoCount);
				ammoCount = ammoCount + (ammoMagazineSize - ammoCount);
				print("weapon reloaded");
			}
			
		}else if (Input.GetButton("Reload") && ammoSpare < 1 || ammoCount == (ammoMagazineSize + 1)) { //player cannot reload, out of spare ammo or magazine is full
			print("cannot reload!"); //debug message
		}
					
		
		
		/*   !!! THIS SECTION IS ADDED INTO THE if (Input.GetButton("Fire2")) AND else ABOVE, SEE BOXED CODE !!!
		
		if (isAiming == true) { // while aiming (default right mouse button), you can move your mouse to rotate                      !!!currently rotates the entire object, later want to just rotate only above the hip/upper body half!!!
			aimX = Input.GetAxis("Mouse X") * horizontalAim; // this is the character rotating horizontally while aiming
			aimY = Input.GetAxis("Mouse Y") * verticalAim;   // this is the character rotating vertically while aiming
			transform.Rotate(-aimY,aimX,0);
			
			// set limit of how far aiming can rotate along the vertical axis creating a range that cannot be between 45 degrees to 315 degrees
			if (objectRotation.x > aimDownLimit && objectRotation.x < aimDownRangeOverlap) { //player cannot look down beyond the aimDownlimit, the aimDownRangeOverlap overlap the looking up restriction, should not be a value above aimUpLimit
				objectRotation.x = (float)aimDownReset; // cast to be float
				transform.localEulerAngles = objectRotation;
				aimY = 0f;
			}
			if (objectRotation.x < aimUpLimit && objectRotation.x > aimDownRangeOverlap) { // player cannot look up beyond a 45 degree angle, shown as 315 from 360-45. the 50 degree angle is to overlap the looking down description, should not be under 45.3
				objectRotation.x = (float)aimUpLimit; // cast to be float
				transform.localEulerAngles = objectRotation;
				aimY = 0f;
			}
		} else { // this else assumes that the player is not aiming
			transform.Rotate(0,0,0); // this stops a bug where if you aim and unaim while moving the mouse, the object will keep spinning in a direction like a globe
			objectRotation.x = 0;   // setting a vertical axis of rotation to zero
			transform.localEulerAngles = objectRotation; // applying value so that the horizontal axis of the aiming changes but not the vertical axis when not aiming anymore
		}
			
		    !!! THIS SECTION IS ADDED INTO THE if (Input.GetButton("Fire2")) AND else ABOVE, SEE BOXED CODE !!!
			
		 */  
			
			
    }
	
}
