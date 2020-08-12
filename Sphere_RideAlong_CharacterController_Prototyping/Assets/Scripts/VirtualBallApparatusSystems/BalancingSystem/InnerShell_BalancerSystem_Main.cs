/// <summary>
/// InnerShell_BalancerSystem_Main.cs
/// Version Proto 1.2
/// Author: Chris Petsche
/// Date: 11 Dec 17
/// *** Redo Descriptions after testing ***
/// Powered by the controls script, this is the main hub for the balancing system for the Inner Shell.
/// It drives the system by sending and reading variable in the collection script to set the adjustment
/// speeds of the x and z axis for the shell, and then puts them into action. 
/// </summary>

using UnityEngine;
using System.Collections;

public class InnerShell_BalancerSystem_Main : MonoBehaviour 
{
	// It also requires access to the collection script to send and read the variables as described above.
	InnerShell_BalancerSystem_SensorUnitCollection BalancingSensoryCollection;

	// Sets each of the system initial variables.
	public void SetAllSensoryStartVariables ()
	{
		// Sets the collection script. 
		BalancingSensoryCollection = GetComponent<InnerShell_BalancerSystem_SensorUnitCollection> ();
		// Tells the collection script to initialize its variables. 
		BalancingSensoryCollection.SetSystemStartVariables ();
	}

	public float BalancerSpeed (int rayLength, float MTR, bool ballGrounded, bool fwdAxis, int deckMag, float CurrentMaxRotationSpeed)
	{
		BalancingSensoryCollection.DriveSensorySystem (rayLength, MTR);

		return (CurrentMaxRotationSpeed * BalancingSensoryCollection.BalancerSpeedMagnifier (ballGrounded, fwdAxis, deckMag)) 
				* BalancingSensoryCollection.BalancerExtremity (fwdAxis);
	}
}
