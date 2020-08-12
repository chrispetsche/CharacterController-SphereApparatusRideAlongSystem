using UnityEngine;
using System.Collections;

public class PlayerInput_Main : MonoBehaviour
{
    [SerializeField]
    CustomController_InputValuesAccess leftController;
    [SerializeField]
    CustomController_InputValuesAccess rightController;

    // **************** GAME TYPE INPUTS **************** //

    // Ball Control Influence Values
    public float Ball_OuterShellForwardAxisInfluenceValue ()
	{
		float driveValue = 0f;

		if (Input.GetKey (KeyCode.W)) 
		{
            //Debug.Log("W Key pressed");
            driveValue = 1.0f;
		}

		if (Input.GetKey (KeyCode.S))
		{
            //Debug.Log("S Key pressed");
            driveValue = -1.0f;
		}

		if (leftController.OVR_LeftStickValues("y") >= 0.001f || leftController.OVR_LeftStickValues("y") <= -0.001f) 
		{
			driveValue = leftController.OVR_LeftStickValues("y");
		}

        //if (Vive_LeftController_TrackPad_YAxis () >= 0.01f || Vive_LeftController_TrackPad_YAxis () <= -0.01f) 
        //{
        //driveValue = Vive_LeftController_TrackPad_YAxis ();
        //}

        //Debug.Log("Direct Forward Force = " + driveValue);

        return driveValue;
	}

	public float Ball_OuterShellRightAxisInfluenceValue ()
	{
		float driveValue = 0f;

		if (Input.GetKey (KeyCode.D)) 
		{
            //Debug.Log("D Key pressed");
			driveValue = 1.0f;
		}

		if (Input.GetKey (KeyCode.A))
        {
            //Debug.Log("A Key pressed");
            driveValue = -1.0f;
		}

		if (leftController.OVR_LeftStickValues("x") >= 0.01f || leftController.OVR_LeftStickValues("x") <= -0.01f) 
		{
			driveValue = leftController.OVR_LeftStickValues("x");
		}

        //if (Vive_LeftController_TrackPad_XAxis () >= 0.01f || Vive_LeftController_TrackPad_XAxis () <= -0.01f) 
        //{
        //driveValue = Vive_LeftController_TrackPad_XAxis ();
        //}

        //Debug.Log(leftController.testMethod());
        return driveValue;
	}

	public float Ball_InnerShellPivotInfluenceValue ()
	{
		float driveValue = 0f;

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
            //Debug.Log("Right Arrow Key pressed");
            driveValue = 1.0f;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
            //Debug.Log("Left Arrow Key pressed");
            driveValue = -1.0f;
		}

		if (rightController.OVR_RightStickValues("x") >= 0.01f || rightController.OVR_RightStickValues("x") <= -0.01f) 
		{
			driveValue = rightController.OVR_RightStickValues("x");
		}

		//if (Vive_LeftController_TrackPad_XAxis () >= 0.01f || Vive_LeftController_TrackPad_XAxis () <= -0.01f) 
		//{
			//driveValue = Vive_LeftController_TrackPad_XAxis ();
		//}

		return driveValue;
	}

	// **************** UI TYPE INPUTS **************** //

	// Standard Ball UI Control Values
}
