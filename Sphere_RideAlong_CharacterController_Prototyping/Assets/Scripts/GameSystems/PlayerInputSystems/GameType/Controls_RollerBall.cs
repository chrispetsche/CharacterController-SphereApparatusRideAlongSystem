/// <summary>
/// Controls_RollerBall.cs
/// Version Proto 1.2
/// Author: Chris Petsche
/// Date: 11 Dec 17
/// *** Redo Descriptions after testing ***
/// Initially sets the stage for the control system for influencing the ball to a desired
/// destination. The controls WILL NOT drive the ball. Rather, the ball is moving around
/// the world based on traditional physics. The player is simply within the ball, facing 
/// a desired direction, and applying varying amounts of force to direct the ball as it 
/// rolls about. 
/// 
/// Uses the inherent values of the Custom Controller for needed input variables. Currently
/// tuned only for keyboard, XBox One and PS4 controllers.
/// </summary>

using UnityEngine;
using System.Collections;

public class Controls_RollerBall : CustomController_InputValuesAccess 
{
	[SerializeField]
	CameraPositioning Camera_PositionAndRotation;
	// Give access to the ball the player will be inside of
	[SerializeField]
	Rigidbody _outerShellRigidbody;
	// The Inner Shell is what the player is actually riding along with, and requires a
	// rigidbody to rotate in the direction they wish to start going, as well as to 
	// balance the player along changing different surface angles. 
	Rigidbody _innerShellRigidbody;
	// Will provide access to the balancing system. 
	InnerShell_BalancerSystem_Main InnerShell_BalancingSystem;
	// Used to adjust the amount of force the player can apply to direct the ball. 
	[SerializeField]
	float _forwardSpeed;
	// Used to adjust the amount of force the player can apply to make the Inner Shell
	// rotate toward the direction they wish to view. 
	[SerializeField]
	float _spinSpeed;

	// Initialzes on start up. 
	void Start () 
	{
		// Sets the Inner Shell rigidbody.
		_innerShellRigidbody = GetComponent<Rigidbody> ();
		// Sets the balancing system script. 
		InnerShell_BalancingSystem = GetComponent<InnerShell_BalancerSystem_Main> ();
		// Initializes all the needed start up variables for the balancing system. 
		InnerShell_BalancingSystem.SetAllSensoryStartVariables ();
	}

	// Updates the following each frame for a constant, smooth transition as the game progresses. 
	void Update () 
	{
		// Camera_PositionAndRotation.SetVRCamera_PositionAndRotation ();

		// Powers the balancing systems main function, with some of them requiring the current speed
		// of the ball. 
		// InnerShell_BalancingSystem.DriveAllSensorySystems (_outerShellRigidbody.velocity.magnitude);
		// Powers internal functions below. 
		FollowOuterShell ();
		SpinInnerShell ();
		DriveOuterShell ();
	}

	// As the Inner Shell is not parented to the ball, so that it does not roll freely along with it, 
	// this function takes the position of the balls rigidbody and sets it as its own. 
	void FollowOuterShell ()
	{
		this.transform.position = new Vector3 (_outerShellRigidbody.position.x, 
			_outerShellRigidbody.position.y, _outerShellRigidbody.position.z);
	}

	// Takes the input values from controllers or the keyboard, and applies their values toward the
	// force used to rotate the Inner Shell side to side. 
	void SpinInnerShell ()
	{
		// If the right joystick 'X' value is greater than or equal to 0.01, or less than or equal to -0.01... 
		//if (LeftThumbStick_XValue () >= 0.01f || LeftThumbStick_XValue () <= -0.01f) 
		//{
			// Take the right joystick value of an XBox One or PS4 controller, and apply it as a percentage
			// the spin speed set above, ...
			//float RotSpeed = _spinSpeed * RightThumbStick_XValue ();
			// which is then equal to the force directed onto the Inner Shell. 
			//_innerShellRigidbody.AddTorque (transform.up * RotSpeed);
		//}

		// Uses the traditional Right and Left keyboard inputs to directly apply the spin speed as the force
		// to rotate the Inner Shell.
		// If the 'A' key is pressed...
		if (Input.GetKey (KeyCode.A)) 
		{
			// Apply a negative force (speed) to the rotation of the Inner Shell. 
			_innerShellRigidbody.AddTorque (transform.up * -_spinSpeed);
		}
		// If the 'D' key is pressed...
		else if (Input.GetKey (KeyCode.D)) 
		{
			// Apply a positive force (speed) to the rotation of the Inner Shell. 
			_innerShellRigidbody.AddTorque (transform.up * _spinSpeed);
		}
	}

	// Takes the input values from controllers or the keyboard, and applies their values toward the
	// force used to propel the ball in the direction the Inner Shell is facing. 
	void DriveOuterShell ()
	{
		// If the right joystick 'Y' value is greater than or equal to 0.01, or less than or equal to -0.01...
		//if (LeftThumbStick_YValue () >= 0.01f || LeftThumbStick_YValue () <= -0.01f) 
		//{
			// Take the right joystick value of an XBox One or PS4 controller, and apply it as a percentage
			// the forward speed set above, ...
			//float FwdSpeed = _forwardSpeed * RightThumbStick_YValue ();
			// which is then equal to the force directed onto the ball. 
			//_outerShellRigidbody.AddForce (transform.forward * FwdSpeed);
		//}

		// Uses the traditional Forward and Backward keyboard inputs to directly apply the forward speed as the force
		// to move the ball.
		// If the 'W' key is pressed...
		if (Input.GetKey (KeyCode.W)) 
		{
			// Apply a positive force (speed) to the ball in the direction of the forward axis of the Inner Shell.
			// Making the ball start to travel towards the players forward. 
			_outerShellRigidbody.AddForce (transform.forward * _forwardSpeed);
		}
		// But if the 'S' key is pressed...
		else if (Input.GetKey (KeyCode.S)) 
		{
			// Apply a negative force (speed) to the ball in the direction of the forward axis of the Inner Shell.
			// Making the ball start to travel towards the players rear.
			_outerShellRigidbody.AddForce (transform.forward * -_forwardSpeed);
		}
	}
}
