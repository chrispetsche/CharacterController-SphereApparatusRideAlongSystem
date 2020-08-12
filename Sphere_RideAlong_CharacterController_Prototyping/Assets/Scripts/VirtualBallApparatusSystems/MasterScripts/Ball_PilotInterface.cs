/// <summary>
/// Ball_PilotInterface.cs
/// Version 1.0
/// Author: Chris Petsche
/// Date: 1 Jan 19
/// </summary>
using UnityEngine;
using System.Collections;
/// <summary>
/// Ball pilot interface.
/// </summary>
public class Ball_PilotInterface : MonoBehaviour 
{
	// ************************************************* CONSTANT FUNCTIONS BELOW ****************************************************** //

	/*[SerializeField]
	bool [] ForceAvailableArray;

	public bool IsPilotForceAvailable (int slot)
	{
		bool available = false;

		available = ForceAvailableArray [slot];

		return available;
	}

	public void SetSystemParameters ()
	{
		//DriveSystem.SetSystemAndSubSystemParameters ();
	}

	public void SetAvailableForces (int axis, bool isPos, bool linear, bool forceAvailable)
	{
		switch (axis) 
		{
		case 1: 
			{ 
				if (linear) 
				{ 
					if (isPos) { ForceAvailableArray [1] = forceAvailable; } else { ForceAvailableArray [2] = forceAvailable; }
				} 

				else 
				{
					if (isPos) { ForceAvailableArray [7] = forceAvailable; } else { ForceAvailableArray [8] = forceAvailable; }
				}

				break; 
			}

		case 2: 
			{ 
				if (linear) 
				{ 
					if (isPos) { ForceAvailableArray [3] = forceAvailable; } else { ForceAvailableArray [4] = forceAvailable; }
				} 

				else 
				{
					if (isPos) { ForceAvailableArray [9] = forceAvailable; } else { ForceAvailableArray [10] = forceAvailable; }
				}

				break; 
			}

		case 3: 
			{ 
				if (linear) 
				{ 
					if (isPos) { ForceAvailableArray [5] = forceAvailable; } else { ForceAvailableArray [6] = forceAvailable; }
				} 

				else 
				{
					if (isPos) { ForceAvailableArray [11] = forceAvailable; } else { ForceAvailableArray [12] = forceAvailable; }
				}

				break; 
			}
		}
	}

	[SerializeField]
	bool [] ForceActiveArray;

	public bool IsPilotForceActive (int slot)
	{
		bool active = false;

		active = ForceActiveArray [slot];

		return active;
	}

	public void SetActiveForces (int axis, bool isPos, bool linear, bool forceActive)
	{
		switch (axis) 
		{
			case 1: 
			{ 
				if (linear) { if (isPos) { ForceActiveArray [0] = forceActive; } else { ForceActiveArray [1] = forceActive; }} 
				else { if (isPos) { ForceActiveArray [6] = forceActive; } else { ForceActiveArray [7] = forceActive; }} break; 
			}

			case 2: 
			{ 
				if (linear) { if (isPos) { ForceActiveArray [2] = forceActive; } else { ForceActiveArray [3] = forceActive; }} 
				else { if (isPos) { ForceActiveArray [8] = forceActive; } else { ForceActiveArray [9] = forceActive; }} break; 
			}

			case 3: 
			{ 
				if (linear) { if (isPos) { ForceActiveArray [4] = forceActive; } else { ForceActiveArray [5] = forceActive; }} 
				else { if (isPos) { ForceActiveArray [10] = forceActive; } else { ForceActiveArray [11] = forceActive; }} break; 
			}
		} 
	}

	public float RequestInfluence (bool linear, int axis, float pilotDriveInfluenceRequest)
	{
		return DriveSystem.PilotVelocityRequest (linear, axis, pilotDriveInfluenceRequest);
	}

	// ************************************************* CONSTANT FUNCTIONS ABOVE ****************************************************** //
	// ************************************************** VARYING FUNCTIONS BELOW ****************************************************** //

	[SerializeField]
	GameObject [] InfinityBall_Component_Array;

	InfinityBall_Component_DriveSystem DriveSystem;
	BallPower_SystemRegulator SystemRegulator;
	InfinityBall_Component_CradleAndBallProjection CradleAndProjectionSystem;
	InfinityBall_Component_PrimaryIntake PrimaryIntake;
	InfinityBall_Component_CoolantSystem CoolantSystem;
	InfinityBall_Component_FrictionalIntake FrictionalIntake;
	InfinityBall_Component_ImpactIntake ImpactIntake;
	InfinityBall_Component_PowerCell PowerCell;
	*/

}
