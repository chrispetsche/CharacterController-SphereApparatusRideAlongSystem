/// <summary>
/// PlayerInput_Main.cs
/// Version 3.0
/// Author: Chris Petsche
/// Date: 26 Sept 2020
/// 
/// The purpose of this script is to play the main interface between an external system with the potential need to access to multiple input
/// devices for a single experience and a secondary script that will read all the inputs coming in from those devices. The requirement for 
/// the system to work per its design is the external system calling in needs to pass in an 'Action Call' that has been mapped to the inputs
/// on each device. The developer using the system also needs to set a priority level for each device the experience will be using. In the 
/// inspector the level must be greater than zero for the system to recognize its input, but should not be greater than the number of devices
/// the system is capable of checking. And if the experience includes a VR version, the VR rig needs to set up per the instructions found below
/// for the primary and secondary controllers.
/// </summary>
using UnityEngine;
using System.Collections;
using System;

namespace MultiDeviceInputSystem
{
    public class PlayerInput_Main : MonoBehaviour
    {
        // This array is public so that the priorities of each device can be set within the inspector or from an external call. The purpose
        // of the array is to let the system know whether or not the device is active, if the priority is greater than zero, and if there
        // are multiple device the experience will be capable of using, which to use over the others if there is input read from one or more.
        public int[] devicePriorityArray = new int[4];
        // If there is an input from a device that is a floating point value, it will be logged here for the call that is currenly cycling in
        // the slot that is equivalent to the slot the device is connected to in the priority array. 
        float[] deviceFloatingInputValue;
        // The analog, or true/false inputs will be logged here.
        bool[] deviceAnalogInputValue;

        // The second part of this system with be the script that reads the input of each device to match it with the designed 'Action Calls'
        // the system is based on the make it all work. Below are 2 slots that can be filled in the inspector by the developer linking it up
        // for use. If the experience is not using a VR controller, more specifically an Oculus device, then the slots need no attention as
        // the system will auto-fill the first or primary controller and set the secondary as null when it initializes. But if there is a VR
        // setup that will be employed, the primary controller will be the anchor for the left hand controller in the VR Rig, and the right
        // anchor for the secondary.
        [SerializeField]
        CustomController_InputValuesAccess primaryController;
        [SerializeField]
        CustomController_InputValuesAccess secondaryController;

        // ************************************************************************ //
        // !!!  REMOVE THESE ON CLEAN UP !!! //
        [SerializeField]
        CustomController_InputValuesAccess leftController;
        [SerializeField]
        CustomController_InputValuesAccess rightController;
        // ************************************************************************ //

        void Start()
        {
            // Here, if the Oculus Quest device is less than one, it is considered inactive...
            if (devicePriorityArray[3] < 1)
            {
                // The system adds and sets the primary controller script automatically.
                primaryController = gameObject.AddComponent<CustomController_InputValuesAccess>();
                // And sets the seconday to null.
                secondaryController = null;
            }
            // Automatically set the input value arrays to the same length as the priority array length.
            deviceFloatingInputValue = new float[devicePriorityArray.Length];
            deviceAnalogInputValue = new bool[devicePriorityArray.Length];
        }

        // **************************************************************************************************** //
        // !!!  REMOVE THESE ON CLEAN UP !!! // 

        public float Ball_OuterShellForwardAxisInfluenceValue()
        {
            float driveValue = 0f;

            if (Input.GetKey("KeyCode.W"))
            {
                driveValue = 1.0f;
            }

            if (Input.GetKey(KeyCode.S))
            {
                driveValue = -1.0f;
            }

            if (leftController.OVR_LeftStickValues("y") >= 0.001f || leftController.OVR_LeftStickValues("y") <= -0.001f)
            {
                driveValue = leftController.OVR_LeftStickValues("y");
            }

            return driveValue;
        }

        public float Ball_OuterShellRightAxisInfluenceValue()
        {
            float driveValue = 0f;

            if (Input.GetKey(KeyCode.D))
            {
                driveValue = 1.0f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                driveValue = -1.0f;
            }

            if (leftController.OVR_LeftStickValues("x") >= 0.01f || leftController.OVR_LeftStickValues("x") <= -0.01f)
            {
                driveValue = leftController.OVR_LeftStickValues("x");
            }
            return driveValue;
        }

        public float Ball_InnerShellPivotInfluenceValue()
        {
            float driveValue = 0f;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                driveValue = 1.0f;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                driveValue = -1.0f;
            }

            if (rightController.OVR_RightStickValues("x") >= 0.01f || rightController.OVR_RightStickValues("x") <= -0.01f)
            {
                driveValue = rightController.OVR_RightStickValues("x");
            }

            return driveValue;
        }

        // **************************************************************************************************** //

        // In this method an external system looking for an input with a floating point value as a return can pass in an 'Action Call'
        // mapped to a list of inputs from multiple devices. This system will ask if there is any input being sent in and then check
        // its priority to decide which devices input is retuned to the external system.
        public float FloatingPointInputValue(string actionCall)
        {
            switch (actionCall)
            {
                // If the 'Action Call' is to move along the forward or z axis in either the forward or backward direction...
                case "MOVE_FWD1":
                    {
                        // Ask the keyboard if there is a value being sent in on the W button.
                        deviceFloatingInputValue[0] = primaryController.KeyboardMouse("Button_W");
                        // If the keyboard value is still zero...
                        if (deviceFloatingInputValue[0] == 0f)
                            deviceFloatingInputValue[0] = -1 * primaryController.KeyboardMouse("Button_S"); // Set its value to the value
                            // sent in on the S button and multiply it by -1 since the S button is mapped to going in reverse.

                        // Check the XBox left stick y value and set the second device input array slot to the percentage returned.
                        deviceFloatingInputValue[1] = 0f;
                        // Check the Playstation left stick y value and set the third device input array slot to the percentage returned.
                        deviceFloatingInputValue[2] = 0f;
                        // Check the Oculus Quest left stick y value and set the forth device input array slot to the percentage returned.
                        deviceFloatingInputValue[3] = primaryController.OVRControllerFloatingPt("Left_Stick", "y");

                        // Return the value of the device that passes the priority check.
                        return DevicePriorityCheck_FloatingPointValue();

                        break;
                    }

                case "MOVE_1":
                    {
                        if (deviceFloatingInputValue[0] > 0)
                        {
                            deviceFloatingInputValue[0] = primaryController.KeyboardMouse("Button_W");

                            if (deviceFloatingInputValue[0] == 0f)
                                deviceFloatingInputValue[0] = -1 * primaryController.KeyboardMouse("Button_S");
                        }

                        deviceFloatingInputValue[2] = 0f;
                        deviceFloatingInputValue[3] = 0f;
                        deviceFloatingInputValue[4] = primaryController.OVRControllerFloatingPt("Left_Stick", "y");

                        break;
                    }

                case "FWD1":
                    {

                        break;
                    }
            }

            // If no 'Action Call' found return zero.
            return 0f;
        }

        // This is the method that handles checking the priorty level of each device and which input value to
        // send out to the external system requesting the value.
        float DevicePriorityCheck_FloatingPointValue()
        {
            // A loop is run for both the device priority and device input arrays.
            for (int i = 0; i < devicePriorityArray.Length; ++i)
            {
                for (int j = 0; j < deviceFloatingInputValue.Length; ++j)
                {
                    // In cycling through the loop, if the value of j is equal to i, add 1 to j.
                    // This is to prevent the device from checking itself against itself.
                    if (j == i)
                        ++j;
                    // If the device currently being checked has a priority level greater than zero, the device is active.
                    // And if the device is active and its level is less than all the other devices or, its level is greater than
                    // the rest of the devices but all their input values are equal to zero or not in use...
                    if (devicePriorityArray[i] > 0 && (devicePriorityArray[i] < devicePriorityArray[j] ||
                        (devicePriorityArray[i] > devicePriorityArray[j] && deviceFloatingInputValue[j] == 0f)))
                    {
                        // Then return its input value.
                        return deviceFloatingInputValue[i];
                    }
                }
            }

            // If the loop return nothing the check returns a zero value.
            return 0f;
        }

        // Similar to the floating point method above, this returns a boolean value for analog inputs. It works in the
        // same way. Only returns the different value type.
        public bool AnalogInputValue(string actionCall)
        {
            switch (actionCall)
            {
                case "ACTION1":
                    {
                        
                        return true;

                        break;
                    }
            }

            // If no 'Action Call' matches, retun false.
            return false;
        }

        // Like the floating point check...
        bool DevicePriorityCheck_AnalogValue()
        {
            // Again, a loop is run for both the device priority and device input arrays.
            for (int i = 0; i < devicePriorityArray.Length; ++i)
            {
                for (int j = 0; j < deviceAnalogInputValue.Length; ++j)
                {
                    // In cycling through the loop, if the value of j is equal to i, add 1 to j.
                    // This is to prevent the device from checking itself against itself.
                    if (j == i)
                        ++j;
                    // If the device currently being checked has a priority level greater than zero, the device is active.
                    // And if the device is active and its level is less than all the other devices or, its level is greater than
                    // the rest of the devices but all their input values are equal to false or not in use...
                    if (devicePriorityArray[i] > 0 && (devicePriorityArray[i] < devicePriorityArray[j] ||
                        (devicePriorityArray[i] > devicePriorityArray[j] && deviceAnalogInputValue[j] == false)))
                    {
                        // Then return its input value.
                        return deviceAnalogInputValue[i];
                    }
                }
            }

            // If the loop return nothing the check returns a false value.
            return false;
        }
    }
}
