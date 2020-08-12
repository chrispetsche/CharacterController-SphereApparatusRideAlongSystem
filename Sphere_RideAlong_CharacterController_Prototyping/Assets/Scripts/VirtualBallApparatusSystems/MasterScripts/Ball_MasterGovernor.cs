/// <summary>
/// Ball master governor sets and runs all system mains on the ball.
/// </summary>

using UnityEngine;

public class Ball_MasterGovernor : MonoBehaviour 
{
	[SerializeField]
	GameObject OuterShell;

	[SerializeField]
	Transform CurrentVRCamera;
	[SerializeField]
	Transform VRCameraRidePt;

	[SerializeField]
	BallGrounded_Main BallGroundedSystem;
    Ball_MasterInterface DeveloperInterface;
    PlayerInput_Main PlayerInputSystem;

	InnerShell_BalancerSystem_Main BalancerSystem;
	DriveSystem_Main DriveSystem;

	//Camera PlayerCam; // maybe in scene governor
	CameraPositioning CameraPos;

	Rigidbody OuterShell_Body;
	Rigidbody InnerShell_Body;

	CapsuleCollider InnerShell_MainTriggerZone { get; set; }
	float MTR { get; set; } // Main Trigger Radius

	// !!!! **** To be determined through power system **** !!!! //
	// ** SEE NOTES **
	bool CanRotateInnerShell;
	bool CanInfluenceOuterShell_Forward;
	bool CanInfluenceOuterShell_ForwardAndReverse;

	bool CanInfluenceOuterShell_Sway;
	//bool CanInfluenceOuterShell_Hop;
	//bool CanInfluenceOuterShell_Fly;
	// ******************************************************* //


	// Replaced by the Scene Governor
	void Start ()
	{
		InitializeBallSystems ();
	}

	public void InitializeBallSystems ()
	{
        //PlayerCam = GetComponent<Camera> ();

        DeveloperInterface = GetComponent<Ball_MasterInterface>();
        PlayerInputSystem = GetComponent<PlayerInput_Main> ();
		CameraPos = GetComponent<CameraPositioning> ();
		DriveSystem = GetComponent<DriveSystem_Main> ();
		BalancerSystem = GetComponent<InnerShell_BalancerSystem_Main> ();

		OuterShell_Body = OuterShell.GetComponent<Rigidbody> ();
		InnerShell_Body = GetComponent<Rigidbody> ();

		CameraPos.SetVRCamera_PositionAndRotation (VRCameraRidePt);
		DriveSystem.InitializeDriveSystem (OuterShell, OuterShell_Body, this.gameObject, InnerShell_Body);

		InnerShell_MainTriggerZone = GetComponent<CapsuleCollider> ();
		MTR = InnerShell_MainTriggerZone.radius / 100;
		BallGroundedSystem.InitializeSystem (MTR, 0.015f); // !!!2nd paramater value to be set in inherited class!!!
		BalancerSystem.SetAllSensoryStartVariables ();

		// ********** !!!!Below will be removed by the end of this scripts clean up!!!! ******************* //
		CanInfluenceOuterShell_Forward = false;
		CanInfluenceOuterShell_ForwardAndReverse = true;
		CanRotateInnerShell = true;
		CanInfluenceOuterShell_Sway = false;

        // ********** !!!!These and the methods below are to be refined for the balancing system!!!! ******************* //
        MaxPlayer_ForwardInfluence = 7.5f;
		MaxPlayer_RotationInfluence = 15.0f;
		MaxBallSpeed = 100.0f; // Temp to be set in master class
		BallBalancerStallSpeed = MaxBallSpeed / 2;
	}

    // ********************************************************************************************************** //
    // ********************************************************************************************************** //
    // ********** !!!!These and the variable above in the Initialization function are to be refined for the balancing system!!!! ******************* //
    // !!!! **** To be determined through power system **** !!!! //
    // ** SEE NOTES **
    float MaxPlayer_ForwardInfluence { get; set; }
	float MaxPlayer_RotationInfluence { get; set; }
	float MaxBallSpeed { get; set; }
	// ******************************************************* //
	float BallBalancerStallSpeed { get; set; }
	// Temp
	//[SerializeField]
	//float ballSpeed;
	public float CurrentBallSpeed ()
	{
		return OuterShell_Body.velocity.magnitude;
	}

	// Provides access to the current adjusted ball speed to regulate how fast the ball can go, and is going.
	// ** ADD  ball speed regulator!! **
	float AdjustedBallSpeed ()
	{ 
		float speed = 0f;

		if(CurrentBallSpeed () >= MaxBallSpeed)
		{
			speed = MaxBallSpeed; //
			//OuterShell_Body.velocity. = MaxBallSpeed;
		}
		//
		else if(CurrentBallSpeed () < MaxBallSpeed && CurrentBallSpeed () >= BallBalancerStallSpeed)
		{
			speed = CurrentBallSpeed (); //
		}

		else if(CurrentBallSpeed () <= BallBalancerStallSpeed)
		{
			speed = BallBalancerStallSpeed; //
		}

		return speed;
	}

	float PlayerInputForceToBeApplied (string forceToBeSet)
	{
		float force = 0f;

		switch (forceToBeSet)
		{
		case "ForwardAxis":
			{
				force = MaxPlayer_ForwardInfluence * PlayerInputSystem.Ball_OuterShellForwardAxisInfluenceValue ();

				break;
			}

		case "HorizontalAxis":
			{
				//force = ;

				break;
			}

		case "VerticalAxis":
			{
				//force = ;

				break;
			}

		case "PivotAxis":
			{
				force = MaxPlayer_RotationInfluence * PlayerInputSystem.Ball_InnerShellPivotInfluenceValue ();

				break;
			}
		}

		return force;
	}

	// Provides access to the maximum possible rotation speed for balancing.
	float MaxBalancerSpeed ()
	{
		// ** Edit with ball speed regulator **
		float speed = 0f;
		float maxSpeed = 50f;
		float stallSpeed = 1f;

		if(CurrentBallSpeed () >= maxSpeed)
		{
			speed = maxSpeed; //
		}

		else if(CurrentBallSpeed () < maxSpeed && CurrentBallSpeed () > stallSpeed)
		{
			speed = CurrentBallSpeed (); //
		}

		else if(CurrentBallSpeed () <= stallSpeed)
		{
			speed = stallSpeed; //
		}

		return speed;
	}
    // ********** !!!!These methods above are to be refined for the balancing system!!!! ******************* //
    // ********************************************************************************************************** //
    // ********************************************************************************************************** //


    // Replaced by the Scene Governor
    void Update () 
	{	
		RunBallGovernor ();
	}
		
	public void RunBallGovernor ()
	{
		CameraPos.RunVRCamera_PositionAndRotation ();

		PowerGroundingSystem ();
		PowerInnerShellBalancing ();
		PowerDriveSystem ();
	} 
		
	void PowerGroundingSystem ()
	{
		// Temp - to be set as a default value if developers using
        // the apparatus want to adjust it for their external systems
        // running along side these.
		int groundingRayLength = 25;

		BallGroundedSystem.DriveGroundedSystem (groundingRayLength);
	}

	void PowerInnerShellBalancing ()
	{
        // Temp - to be set as a default value if developers using
        // the apparatus want to adjust it for their external systems
        // running along side these.
        int balancerRayLength = 55;

		int DeckHeightMagnifier = 0;

		if (BallGroundedSystem.DownRay_HitDistance >= DeckHeight ("UpperDeck")) 
		{
			DeckHeightMagnifier = 1;
		}

		else if (BallGroundedSystem.DownRay_HitDistance < DeckHeight ("UpperDeck") && BallGroundedSystem.DownRay_HitDistance > DeckHeight ("MidDeck")) 
		{
			DeckHeightMagnifier = 2;
		}

		else if (BallGroundedSystem.DownRay_HitDistance < DeckHeight ("MidDeck") && BallGroundedSystem.DownRay_HitDistance > DeckHeight ("LowerDeck")) 
		{
			DeckHeightMagnifier = 3;
		}

		else if (BallGroundedSystem.DownRay_HitDistance < DeckHeight ("LowerDeck") && BallGroundedSystem.DownRay_HitDistance > DeckHeight ("BottomDeck")) 
		{
			DeckHeightMagnifier = 4;
		}

		else if (BallGroundedSystem.DownRay_HitDistance <= DeckHeight ("BottomDeck")) 
		{
			DeckHeightMagnifier = 5;
		}

        // ********************************************************************************************************** //
        // ********************************************************************************************************** //
        // !!! These calls will be refined with the methods above that set them !!! //
        DriveSystem.DriveDirectionAndRotation ("Balancer", "Right", BalancerSystem.BalancerSpeed (balancerRayLength, MTR,  
			BallGroundedSystem.BallGrounded, true, DeckHeightMagnifier, MaxBalancerSpeed ()));
		DriveSystem.DriveDirectionAndRotation ("Balancer", "Forward", BalancerSystem.BalancerSpeed (balancerRayLength, MTR, 
			BallGroundedSystem.BallGrounded, false, DeckHeightMagnifier, MaxBalancerSpeed ()));
        // ********************************************************************************************************** //
        // ********************************************************************************************************** //
    }


    // !!! This could be refined and opened up to be tuned by a developer using the apparatus and connecting an
    // external system to tune the apparatus function to their desires !!! //
    int DeckHeight (string deck)
	{
		int height = 0;

		switch(deck)
		{
		case "UpperDeck":
			{
				height = 20;

				break;
			}

		case "MidDeck":
			{
				height = 15;

				break;
			}

		case "LowerDeck":
			{
				height = 11;

				break;
			}

		case "BottomDeck":
			{
				height = 8;

				break;
			}
		}

		return height;
	}

    // The purpose of this function is to obviously to communicate with and run the drive system of the apparatus, but also the developer interface system 
    // to check if it's to listen for player/ viewer input to pass a movement request percentage or just pull in a direct force/ influencial value sent in 
    // via an external system. 
	void PowerDriveSystem ()
	{
        // As part of the drive system functionality, the Straddling Center Point of the apparatus needs a
        // position to 'straddle'. This call to the drive system feeds in the position of the sphere that
        // it's meant perform it's duties with. 
        DriveSystem.DriveConstantSystem (OuterShell.transform);

        // !!!! **** Add in remaining forces for both linear and torque, and a check to see if the ball needs to be grounded to apply forces. **** !!!!
        // Here the developer interface is checked to see if the developer has selected to have the player/ viewer be the one to drive the apparatus via
        // the input system built into it. 
        if (DeveloperInterface.isPlayerDriven)
        {
            // ******************** Linear Drives *********************** //
            
            // For the linear drive force/ influences, the sphere is required to be on a surface or very close to, specifically if the player/ viewer
            // is doing the controlling.
            if (BallGroundedSystem.BallGrounded || BallGroundedSystem.DownRay_HitDistance <= MTR + 0.5f)
            {
                // Then the different inputs are checked to see if the player/ viewer has requested some movement.

                // ******Axis Z - or - 1 ****** //
                // If the a request is made for the forward axis... (The forward axis being the primary axis for the design of the system, and so gets the
                // numberic value of '1' to be passed into the developer interface)
                if (PlayerInputSystem.Ball_OuterShellForwardAxisInfluenceValue() >= 0.01f || PlayerInputSystem.Ball_OuterShellForwardAxisInfluenceValue() <= -0.01f)
                {
                    //Debug.Log(PlayerInputSystem.Ball_OuterShellForwardAxisInfluenceValue());
                    // Tell the apparatus' built in drive system that the sphere is to move in the direction of the player/ viewers forward axis
                    // at the force/ influence value sent back from the developer interface after passing it the movement percentage value being
                    // input by the player/ viewer.
                    DriveSystem.DriveDirectionAndRotation("OuterShell", "Forward",
                        PlayerInputSystem.Ball_OuterShellForwardAxisInfluenceValue());
                }


                // ****** Axis X -or- 2 ****** //
                // If the a request is made for the axis responsible for side to side motion... (The side to side axis being the secondary to the 
                // forward axis for the design of the system, and so gets the numberic value of '2' to be passed into the developer interface)
                if (PlayerInputSystem.Ball_OuterShellRightAxisInfluenceValue() >= 0.01f || PlayerInputSystem.Ball_OuterShellRightAxisInfluenceValue() <= -0.01f)
                {
                    DriveSystem.DriveDirectionAndRotation("OuterShell", "Right",
                        PlayerInputSystem.Ball_OuterShellRightAxisInfluenceValue());

                }
            }

            // ******************** Torque Drives *********************** //
            // ****** Axis Z -or- 3 ****** //
            // If the a request is made for the axis responsible for pivoting or a yaw like motion... (The torque axis for the system are slightly
            // different than the linear. For the yaw movements, the numberic value passed to the interface is set to '3' for the design of the system)
            if (PlayerInputSystem.Ball_InnerShellPivotInfluenceValue() >= 0.01f || PlayerInputSystem.Ball_InnerShellPivotInfluenceValue() <= -0.01f)
            {
                // The other piece that is different than the linear motion is that the torque tells the Straddling Center Point to rotate 
                // around its vertical axis. 
                DriveSystem.DriveDirectionAndRotation("InnerShell", "Up",
                    PlayerInputSystem.Ball_InnerShellPivotInfluenceValue());
            }
        }

        // If the apparatus is not to be player driven...
        else if (!DeveloperInterface.isPlayerDriven)
        {
            // The governor just pulls directly on the value set in the developer interface that's set by the developers external system,
            // then passes it in the same way it would if the player/ viewer were to be able to make a request of their own. 
            DriveSystem.DriveDirectionAndRotation("OuterShell", "Forward", DeveloperInterface.DirectionalInfluenceValues.z);
            DriveSystem.DriveDirectionAndRotation("OuterShell", "Right", DeveloperInterface.DirectionalInfluenceValues.x);

            DriveSystem.DriveDirectionAndRotation("InnerShell", "Up", DeveloperInterface.RotationalInfluenceValues.z);
        }
    }

	// ***************************************************** PUBLIC ACCESS ***************************************************************** //
    // !!! Make these accessable from the 'Developer Interface' !!! //
	public GameObject Outer_Shell ()
	{
		return OuterShell;
	}

	public Vector3 LocalTrans ()
	{
		return transform.position;
	}
}

