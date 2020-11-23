/// <summary>
/// CustomController_InputValuesAccess.cs
/// Version 3.0
/// Author: Chris Petsche
/// Date: 26 Sept 2020
/// 
/// This is 2 of 2 for the scripts that run the multi input device system. The PlayerInput_Main.cs calls on it to match the 'Action Calls'
/// to specific device inputs that are premapped. Within the script are methods that are linked to all the devices the system is capable
/// of handling. Making it easy to update and expand on both end of the system.
/// </summary>

using UnityEngine;
using System.Collections;

namespace MultiDeviceInputSystem
{
    public class CustomController_InputValuesAccess : MonoBehaviour
    {
        // **************************************************************************************************** //
        // !!!  REMOVE THESE ON CLEAN UP !!! // 

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

        // **************************************************************************************************** //

        // !!!!!!!!!! NEW SETUP BELOW !!!!!!!!!!!!
        // This method handles all inquires about floating point values that apply to keyboard and mouse inputs as set in the input maps
        // designed for this system. It takes a single parameter to check the keyboard button or mouse function and returns a value of 1
        // if the button or mouse action is being sent in.
        public float KeyboardMouse(string input)
        {
            // The switch check takes in the method parameter and matches it to a button or mouse action.
            switch (input)
            {
                case "Button_W":
                    {
                        // If the parameter matched, and if the keyboard / mouse input is being sent in...
                        if (Input.GetKey(KeyCode.W))
                            return 1f; // Return a value of 1.

                        break;
                    }

                case "Button_S":
                    {
                        if (Input.GetKey(KeyCode.S))
                            return 1f;

                        break;
                    }

                case "Button_A":
                    {
                        if (Input.GetKey(KeyCode.A))
                            return 1f;

                        break;
                    }

                case "Button_D":
                    {
                        if (Input.GetKey(KeyCode.D))
                            return 1f;

                        break;
                    }
            }

            // If the parameter is not matched, return zero.
            return 0f;
        }

        // This method is called for any floating point input that may be sent in from an Oculus device. It takes 2 parameters to determine which
        // input is being checked. Some only require a type, like the trigger buttons. Others require the type and the axis, such as the sticks. 
        public float OVRControllerFloatingPt(string inputType, string axis)
        {
            // This is the string that combines the 2 parameters required by this method to check the desired inputs of the Oculus controllers.
            string inputToCheck = inputType + "_" + axis;

            // Variables holding values for the left and right stick via OVR system.
            Vector2 leftstickValues = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick); 
            Vector2 rightstickValues = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

            // In this switch check the above combined input to check is ran.
            switch (inputToCheck)
            {
                // If the check matches the name in the list below...
                case "Left_Stick_x":
                    {
                        // Return the value current being applied to this input.
                        return leftstickValues.x;

                        break;
                    }

                case "Left_Stick_y":
                    {
                        return leftstickValues.y;

                        break;
                    }

                case "Right_Stick_x":
                    {
                        return rightstickValues.x;

                        break;
                    }

                case "Right_Stick_y":
                    {
                        return rightstickValues.y;

                        break;
                    }

                case "Left_Trigger_":
                    {
                        return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);

                        break;
                    }

                case "Right_Trigger_":
                    {
                        return OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

                        break;
                    }

                case "Left_Grip_":
                    {
                        return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

                        break;
                    }

                case "Right_Grip_":
                    {
                        return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);

                        break;
                    }
            }

            // If there is no match in the switch check list, return zero.
            return 0f;
        }

        public bool OVRControllerAnalog(string inputType, string axis)
        {
            // This is the string that combines the 2 parameters required by this method to check the desired inputs of the Oculus controllers.
            string inputToCheck = inputType + "_" + axis;

            // In this switch check the above combined input to check is ran.
            switch (inputToCheck)
            {
                case "Button_A":
                    {
                        return OVRInput.Get(OVRInput.RawButton.A);

                        break;
                    }

                case "ButtonUp_A":
                    {
                        return OVRInput.GetUp(OVRInput.RawButton.A);

                        break;
                    }

                case "ButtonDown_A":
                    {
                        return OVRInput.GetDown(OVRInput.RawButton.A);

                        break;
                    }

                case "Button_B":
                    {
                        return OVRInput.Get(OVRInput.RawButton.B);

                        break;
                    }

                case "ButtonUp_B":
                    {
                        return OVRInput.GetUp(OVRInput.RawButton.B);

                        break;
                    }

                case "ButtonDown_B":
                    {
                        return OVRInput.GetDown(OVRInput.RawButton.B);

                        break;
                    }

                case "Button_X":
                    {
                        return OVRInput.Get(OVRInput.RawButton.X);

                        break;
                    }

                case "ButtonUp_X":
                    {
                        return OVRInput.GetUp(OVRInput.RawButton.X);

                        break;
                    }

                case "ButtonDown_X":
                    {
                        return OVRInput.GetDown(OVRInput.RawButton.X);

                        break;
                    }

                case "Button_Y":
                    {
                        return OVRInput.Get(OVRInput.RawButton.Y);

                        break;
                    }

                case "ButtonUp_Y":
                    {
                        return OVRInput.GetUp(OVRInput.RawButton.Y);

                        break;
                    }

                case "ButtonDown_Y":
                    {
                        return OVRInput.GetDown(OVRInput.RawButton.Y);

                        break;
                    }

                case "Button_Left_Stick":
                    {
                        return OVRInput.Get(OVRInput.Button.PrimaryThumbstick);

                        break;
                    }

                case "ButtonUp_Left_Stick":
                    {
                        return OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick);

                        break;
                    }

                case "ButtonDown_Left_Stick":
                    {
                        return OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick);

                        break;
                    }

                case "Button_Right_Stick":
                    {
                        return OVRInput.Get(OVRInput.Button.SecondaryThumbstick);

                        break;
                    }

                case "ButtonUp_Right_Stick":
                    {
                        return OVRInput.GetUp(OVRInput.Button.SecondaryThumbstick);

                        break;
                    }

                case "ButtonDown_Right_Stick":
                    {
                        return OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick);

                        break;
                    }
            }

            // If there is no match in the switch check list, return false.
            return false;
        }
    }
}
