/// <summary>
/// InnerShell_BalancerSystem_SensorUnitCollection.cs
/// Version Proto 1.2
/// Author: Chris Petsche
/// Date: 11 Dec 17
/// *** Redo Descriptions after testing ***
///Here, all the distance values and hit variables from each the sensor units are collected
///and processed to be used by the balancing main script. 
/// </summary>

// !!! Clean up for Proto 1.3 !!!
using UnityEngine;
using System.Collections;

public class InnerShell_BalancerSystem_SensorUnitCollection : MonoBehaviour 
{
	// Provides a means to connect the sensor units to this script.
	[SerializeField]
	InnerShell_BalancerSystem_TriggerAndRay [] SensorUnitArray;
	// Each of these are to be one of the units in the array above so that each unit can
	// be interacted with independently. 
	InnerShell_BalancerSystem_TriggerAndRay SensorUnit_F;
	InnerShell_BalancerSystem_TriggerAndRay SensorUnit_R;
	InnerShell_BalancerSystem_TriggerAndRay SensorUnit_B;
	InnerShell_BalancerSystem_TriggerAndRay SensorUnit_L;

	InnerShell_Tilt_BalancerSubMain TiltSubMain;
	[SerializeField]
	InnerShell_TiltConstant TiltConstant;
	[SerializeField]
	Transform UpperTiltPoint;
	Transform TiltConstantPosition;

	// Sets internal as well as calls to set each sensor unit start variable. 
	public void SetSystemStartVariables () 
	{
		// Loops through the sensor unit array.
		for (int i = 0; i < SensorUnitArray.Length; ++i) 
		{
			// Calls each sensor unit to set its start variables, with its triggerHit 
			// being fed in as false. 
			SensorUnitArray [i].SetSensoryUnitVariables (false);
		}

		SetIndividualUnits (); // Calls function below.

		TiltSubMain = gameObject.AddComponent<InnerShell_Tilt_BalancerSubMain> ();
		TiltConstantPosition = TiltConstant.TiltConstantCurrentPosition ();
		TiltSubMain.InitializeTiltSubMain (TiltConstantPosition, UpperTiltPoint);
	}

	// Sets the units so they can be called and read seperately. 
	void SetIndividualUnits ()
	{
		SensorUnit_F = SensorUnitArray [0]; // The 'Front Sensor' equals the first array slot.
		SensorUnit_R = SensorUnitArray [1]; // The 'Right Sensor' equals the second array slot.
		SensorUnit_B = SensorUnitArray [2]; // The 'Back Sensor' equals the third array slot.
		SensorUnit_L = SensorUnitArray [3]; // The 'Left Sensor' equals the last array slot.
	}

	// Powers each of the sensor units.
	public void DriveSensorySystem (int rayLength, float MTR)
	{
		// Loops through the sensor unit array.
		for (int i = 0; i < SensorUnitArray.Length; ++i) 
		{
			// Calls each sensor unit to power their rays and provide them
			// their length. 
			SensorUnitArray [i].ProjectRay (rayLength);
		}

		TiltConstant.SetTiltConstantPosition (MTR);
	}

	public float BalancerExtremity (bool fwd)
	{
		float Extremity = 0f;

		float LgDistPercent = 0f;
		float SmDistPercent = 0f;

		if (fwd) 
		{
			float FAxis_TotalDistance = SensorUnit_F.DistanceToHit + SensorUnit_B.DistanceToHit;

			if (SensorUnit_F.DistanceToHit < SensorUnit_B.DistanceToHit) 
			{
				LgDistPercent = SensorUnit_B.DistanceToHit / FAxis_TotalDistance;
				SmDistPercent = SensorUnit_F.DistanceToHit / FAxis_TotalDistance;

				Extremity = (LgDistPercent - SmDistPercent) * -1;
			} 

			else if (SensorUnit_F.DistanceToHit > SensorUnit_B.DistanceToHit) 
			{
				LgDistPercent = SensorUnit_F.DistanceToHit / FAxis_TotalDistance;
				SmDistPercent = SensorUnit_B.DistanceToHit / FAxis_TotalDistance;

				Extremity = LgDistPercent - SmDistPercent;
			}
		} 

		else 
		{
			float RAxis_TotalDistance = SensorUnit_R.DistanceToHit + SensorUnit_L.DistanceToHit;

			if (SensorUnit_R.DistanceToHit < SensorUnit_L.DistanceToHit) 
			{
				LgDistPercent = SensorUnit_L.DistanceToHit / RAxis_TotalDistance;
				SmDistPercent = SensorUnit_R.DistanceToHit / RAxis_TotalDistance;

				Extremity = LgDistPercent - SmDistPercent;
			} 

			else if (SensorUnit_R.DistanceToHit > SensorUnit_L.DistanceToHit) 
			{
				LgDistPercent = SensorUnit_R.DistanceToHit / RAxis_TotalDistance;
				SmDistPercent = SensorUnit_L.DistanceToHit / RAxis_TotalDistance;

				Extremity = (LgDistPercent - SmDistPercent) * -1;
			}
		}

		return Extremity;
	}

	public float BalancerSpeedMagnifier (bool ballGrounded, bool fAxis, int heightDeckMag)
	{
		float totalMagnifier = 1.0f;

		if (ballGrounded) 
		{
			// If one or the other of the collider triggers are hit...
			if ((fAxis && (SensorUnit_F.TriggerHit && !SensorUnit_B.TriggerHit) || 
				(!SensorUnit_F.TriggerHit && SensorUnit_B.TriggerHit)) || 
				(!fAxis && (SensorUnit_R.TriggerHit && !SensorUnit_L.TriggerHit) || 
					(!SensorUnit_R.TriggerHit && SensorUnit_L.TriggerHit))) 
			{
				totalMagnifier = TiltMagnifier (); 
			}
		} 

		else 
		{
			totalMagnifier = TiltMagnifier () + heightDeckMag; 
		}

		return totalMagnifier;
	}

	public float TiltMagnifier ()
	{
		float mag = 0f;

		switch(TiltSubMain.BallTilt(TiltConstantPosition, UpperTiltPoint))
		{
		case "HomeRange":
			{
				mag = 1.0f;
				break;
			}

		case "Sloping":
			{
				mag = 1.5f;
				break;
			}

		case "HardSloping":
			{
				mag = 2.0f;
				break;
			}

		case "SideWalling":
			{
				mag = 2.5f;
				break;
			}

		case "InvertedSloping":
			{
				mag = 5.0f;
				break;
			}

		case "HardInvertedSloping":
			{
				mag = 7.5f;
				break;
			}

		case "TotallyInverted":
			{
				mag = 10.0f;
				break;
			}
		}

		return mag;
	}
}
