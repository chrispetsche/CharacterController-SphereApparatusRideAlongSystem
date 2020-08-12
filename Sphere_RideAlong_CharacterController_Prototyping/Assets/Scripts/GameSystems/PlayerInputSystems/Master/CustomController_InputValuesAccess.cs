/// <summary>
/// CustomController_InputValuesAccess.cs
/// Version 2.0
/// Author: Chris Petsche
/// Date: 9 Jan 18
/// 
/// ** Edit description **
/// This script will provide access, by reference ONLY, to all the different input values that one might use in a
/// game/experience. Initially XBox and Playstation controllers with PC adaptability, and then 
/// process through the various VR inputs to come that follows the basic scheme of the Unity
/// Input Manager. 
/// ** Edit description **
/// </summary>

using UnityEngine;
using System.Collections;

public class CustomController_InputValuesAccess : MonoBehaviour 
{
    
    public float testMethod()
    {
        return 1f;
    }

    // ******************************* !!!! OVR INPUTS !!!! ******************************************** //

    public float OVR_LeftStickValues(string axis)
    {
        float axisValue = 0f;
        Vector2 leftstickValues = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        if (axis == "x")
        {
            axisValue = leftstickValues.x;
        }

        if (axis == "y")
        {
            axisValue = leftstickValues.y;
        }

        return axisValue;
    }

    public float OVR_RightStickValues(string axis)
    {
        float axisValue = 0f;
        Vector2 rightstickValues = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        if (axis == "x")
        {
            axisValue = rightstickValues.x;
        }

        if (axis == "y")
        {
            axisValue = rightstickValues.y;
        }

        return axisValue;
    }


    // ******************************* !!!! XBOX INPUTS !!!! ******************************************** //

    /*public float XBox_LeftThumbStick_XValue ()
	{
		return UnityEngine.Input.GetAxis ("XBox_LeftStickX");
	}

	public float XBox_LeftThumbStick_YValue ()
	{
		return UnityEngine.Input.GetAxis ("XBox_LeftStickY");
	}

	public float XBox_RightThumbStick_XValue ()
	{
		return UnityEngine.Input.GetAxis ("XBox_RightStickX");
	}

	public float XBox_RightThumbStick_YValue ()
	{
		return UnityEngine.Input.GetAxis ("XBox_RightStickY");
	}

	// ****************************************************** //

	public float XBox_DPad_XValue ()
	{
		return UnityEngine.Input.GetAxis ("XBox_DPadX");
	}

	public float XBox_DPad_YValue ()
	{
		return UnityEngine.Input.GetAxis ("XBox_DPadY");
	}

	// ****************************************************** //

	public float XBox_LeftTrigger ()
	{
		return UnityEngine.Input.GetAxis ("XBox_LeftTrigger");
	}

	public float XBox_RightTrigger ()
	{
		return UnityEngine.Input.GetAxis ("XBox_RightTrigger");
	}

	public float XBox_EitherTrigger ()
	{
		return UnityEngine.Input.GetAxis ("XBox_LOrRTrigger");
	}

	// ****************************************************** //

	public bool XBox_StartButtonPressed ()
	{
		return UnityEngine.Input.GetButton ("XBox_StartButton");
	}

	public bool XBox_BackButtonPressed ()
	{
		return UnityEngine.Input.GetButton ("XBox_BackButton");
	}

	// ****************************************************** //

	public bool XBox_FunctionButton1Pressed ()
	{
		return UnityEngine.Input.GetButton ("XBox_FunctionButton1");
	}

	public bool XBox_FunctionButton2Pressed ()
	{
		return UnityEngine.Input.GetButton ("XBox_FunctionButton2");
	}

	public bool XBox_FunctionButton3Pressed ()
	{
		return UnityEngine.Input.GetButton ("XBox_FunctionButton3");
	}

	public bool XBox_FunctionButton4Pressed ()
	{
		return UnityEngine.Input.GetButton ("XBox_FunctionButton4");
	}

	// ****************************************************** //

	public bool XBox_LeftBumperPressed ()
	{
		return UnityEngine.Input.GetButton ("XBox_LeftBumper");
	}

	public bool XBox_RightBumperPressed ()
	{
		return UnityEngine.Input.GetButton ("XBox_RightBumper");
	}

	// ****************************************************** //

	public bool XBox_LeftJoyStickButtonPressed ()
	{
		return UnityEngine.Input.GetButton ("XBox_LeftStickButton");
	}

	public bool XBox_RightJoyStickButtonPressed ()
	{
		return UnityEngine.Input.GetButton ("XBox_RightStickButton");
	}*/
}
