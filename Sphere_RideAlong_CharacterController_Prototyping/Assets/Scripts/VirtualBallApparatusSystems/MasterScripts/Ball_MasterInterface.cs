/// <summary>
/// VRController_SphereApparatus_StraddlingSystem_MasterGovernor.cs << VRController_SphereApparatus_StraddlingSystem_DeveloperInterface.cs
/// Version 3.0
/// Author: Chris Petsche
/// Date: 2 May 2020
/// 
/// This script was created, originally as the 'Ball_MasterInterface', to give access to all the primary varialbles that make the apparatus
/// controller behave as it does for developers wanting to use the tool. Keeping the source code secure, and leaving the already constructed 
/// system to run as precise as it was designed to be.
/// 
/// It's first purpose is to check:
/// If there is to be any player/viewer input -or- if the force applied to drive the sphere and the player/viewers perspective within it will
/// be coming through an external system and directly to the state of motion on the qualifiying axis.
/// 
/// And then set the value of force/influence to be used as it's passed to the 'VRController_SphereApparatus_StraddlingSystem_MasterGovernor.cs'
/// script that governor all the systems that make up the apparatus controller.
/// 
/// The reason for this check is that the system knows whether to “listen” for player/viewer input, or just sit back and allow the developers
/// external system drive the movement directly, maybe with an AI system for example.
/// </summary>
using UnityEngine;

public class Ball_MasterInterface : MonoBehaviour
{
    // This bool is public so that it can be set in the inspeector or through an external system tapping into this interface. It simply lets
    // the master governor for the apparatus know that it's either listening for player/ viewer inputs or just allowing the external system to
    // apply an influential force directly to the qualifying axis of the sphere or player/ viewer within it.
    public bool isPlayerDriven;
    // The two vector3s below are public so that external systems can access player/viewer inputs, but sets the value given privately vie the
    // 'SetPlayerInputPercentageValuesForExternalUse_And_RetrieveInfluenceValueFromExternal' below them. That way they their values can't be
    // accidentally altered by the external system. External systems can use these percentage values that range from 1 to -1 to determine how
    // much force/ influence to pass back to the script for the 'VRController_SphereApparatus_StraddlingSystem_MasterGovernor.cs' to collect
    // and use.
    public static Vector3 DirectionalInfluenceRequestPercentage { get { return directionalInfluenceRequestPercentage; } }
    public static Vector3 RotationalInfluenceRequestPercentage { get { return rotationalInfluenceRequestPercentage; } }

    public void SetInfluenceValuesFromExternalSystem(bool linear, float externalInfluenceValueX, float externalInfluenceValueY, float externalInfluenceValueZ)
    {
        if(linear)
        {
            DirectionalInfluenceValues = new Vector3(externalInfluenceValueX, externalInfluenceValueY, externalInfluenceValueZ);
        }

        else
        {
            RotationalInfluenceValues = new Vector3(externalInfluenceValueX, externalInfluenceValueY, externalInfluenceValueZ);
        }

    }

    // The next two vector3s are also public for a couple reasons. First, to allow external systems a means of passing a value back in to influece 
    // the sphere to move or the player/viewer to rotate within it. Second, so the master governor can grab it directly and put it to use. Any 
    // manipulation of these values are to be done in an external system, in combination with the player/viwer input percentages available abover 
    // or without them. 
    public Vector3 DirectionalInfluenceValues { get; set; }
    public Vector3 RotationalInfluenceValues { get; set; }

    private float AdjustPolarityOfInfluenceValuesSetByExternalSystems(bool linear, string axis)
    {
        float adjustedInfluenceValue = 0f;

        // Here the type of motion is checked.
        if (linear)
        {
            // Then what axis to apply the results to. 
            switch (axis)
            {
                case "z":
                    {
                        adjustedInfluenceValue = DirectionalInfluenceValues.z;

                        break;
                    }

                case "x":
                    {
                        adjustedInfluenceValue = DirectionalInfluenceValues.x;

                        break;
                    }

                case "y":
                    {
                        adjustedInfluenceValue = DirectionalInfluenceValues.y;

                        break;
                    }
            }
        }

        else
        {
            switch (axis)
            {
                case "x":
                    {
                        adjustedInfluenceValue = RotationalInfluenceValues.x;

                        break;
                    }

                case "y":
                    {
                        adjustedInfluenceValue = RotationalInfluenceValues.y;

                        break;
                    }

                case "z":
                    {
                        adjustedInfluenceValue = RotationalInfluenceValues.z;

                        break;
                    }
            }
        }

        return adjustedInfluenceValue;
    }

    // The last set of vector3s are the values passed into the first set, but as the private values set in the 
    // 'SetPlayerInputPercentageValuesForExternalUse_And_RetrieveInfluenceValueFromExternal' function below if and when the master governor calls
    // on it for the force/ influence values to be used. 
    private static Vector3 directionalInfluenceRequestPercentage;
    private static Vector3 rotationalInfluenceRequestPercentage;

    // In this function, if the apparatus is player driven, the master governor call on it to apply the appropriate force the an axis the 
    // player/ viewer is requesting to move. It does this by passing in a the information for the function to determine is the force and applicable
    // axis are to move linear like or rotational, and then the percentage of force/ influence being requested. 

    // It's also setting the percentage value for an external system to grab, and then uses the value it passes back to return to the governor for
    // its needs in driving the apparatus.
    public float SetPlayerInputPercentageValuesForExternalUse_And_RetrieveInfluenceValueFromExternal(bool linear, string axis, float inputPercentage)
    {
        Debug.Log(linear + ", " + axis + ", " + inputPercentage);
        // Like a magnet, the numberic system used to apply a force/ influence along or around
        // an axis have positive and negative values to determine direction in multiple dimensions.
        // This variable help ensure that values are sent back to the master governor correctly.
        int polarityValue = 1;
        // If the input being sent in by the player/ viewer is less than zero,
        // set the polarity value to -1. If the input is greater than zero,
        // the polarity is preset to a positive 1.
        //
        // The value given to the external system is designed to always be positive. So, the inputPercentage
        // is also multiplied by -1 if it is less than zero.
        if (inputPercentage < 0)
        {
            polarityValue *= -1;
            inputPercentage *= -1;
        }

        // The end force/ influence is automatically set to zero so that if there is no value change between
        // it and the return, the value sent out remains zero.
        float influenceValueFromExternal = 0f;

        // Here the type of motion is checked.
        if (linear)
        {
            // Then what axis to apply the results to. 
            switch (axis)
            {
                case "z":
                    {
                        // This sets the percentage for the external system to use
                        // in a private, secure fashion.
                        directionalInfluenceRequestPercentage.z = inputPercentage;
                        // As the external system will have set the vector3 above
                        // for the force/ influence to pass back to the governor,
                        // it is the value needed for the return value.
                        influenceValueFromExternal = AdjustPolarityOfInfluenceValuesSetByExternalSystems(linear, axis); // DirectionalInfluenceValues.z;

                        break;
                    }

                case "x":
                    {
                        directionalInfluenceRequestPercentage.x = inputPercentage;
                        influenceValueFromExternal = AdjustPolarityOfInfluenceValuesSetByExternalSystems(linear, axis); // DirectionalInfluenceValues.x;

                        break;
                    }

                case "y":
                    {
                        directionalInfluenceRequestPercentage.y = inputPercentage;
                        influenceValueFromExternal = AdjustPolarityOfInfluenceValuesSetByExternalSystems(linear, axis); // DirectionalInfluenceValues.y;

                        break;
                    }
            }
        }

        else
        {
            switch (axis)
            {
                case "x":
                    {
                        rotationalInfluenceRequestPercentage.x = inputPercentage;
                        influenceValueFromExternal = AdjustPolarityOfInfluenceValuesSetByExternalSystems(linear, axis); // RotationalInfluenceValues.x;

                        break;
                    }

                case "y":
                    {
                        rotationalInfluenceRequestPercentage.y = inputPercentage;
                        influenceValueFromExternal = AdjustPolarityOfInfluenceValuesSetByExternalSystems(linear, axis); // RotationalInfluenceValues.y;

                        break;
                    }

                case "z":
                    {
                        rotationalInfluenceRequestPercentage.z = inputPercentage;
                        influenceValueFromExternal = AdjustPolarityOfInfluenceValuesSetByExternalSystems(linear, axis); // RotationalInfluenceValues.z;

                        break;
                    }
            }
        }

        Debug.Log(influenceValueFromExternal *= polarityValue);
        
        // When giving the final value of the force/ influence to be used, the influence determined
        // by an external system should be a positive number. If the original input of the player/ viewer
        // was negative it would need to be given back to the master governor as a negative. By multiplying
        // the external influence by the polarityValue set at the beginning of the function, the governor
        // will have its correct influence to use, per the player/ viewers request.
        return influenceValueFromExternal *= polarityValue;
    }
}
