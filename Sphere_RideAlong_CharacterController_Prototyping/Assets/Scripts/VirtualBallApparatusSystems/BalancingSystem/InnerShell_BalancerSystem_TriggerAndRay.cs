/// <summary>
/// InnerShell_BalancerSystem_TriggerAndRay.cs
/// Version Proto 1.2
/// Author: Chris Petsche
/// Date: 11 Dec 17
/// *** Redo Descriptions after testing ***
/// Projects a ray at 1 meter from the ball, and at half its height, downwards. The ray
/// determines the distance to the surface below and makes it ready for reading by the
/// Sensor Unit Collection script that powers the ray. Also, a trigger extending out 
/// toward the point of projection that will indicate if the ball is tilted extremely
/// enough that it is about to roll over. 
/// </summary>

using UnityEngine;
using System.Collections;

public class InnerShell_BalancerSystem_TriggerAndRay : MonoBehaviour 
{
	// Provides a readable value for the distance calculated by the ray
	// for the collection script.
	public float DistanceToHit { get; set; }

	// Fires ray downward from the point this script is attached to, and sets
	// the distance to be read. It is fed a int value that will be the length
	// of the ray. 
	public void ProjectRay (int rayLength)
	{
		RaycastHit _hit;
		// Sets and projects ray according to the description above. 
		Ray SenoryRay = new Ray (transform.position, Vector3.down * rayLength);
		Debug.DrawRay (transform.position, Vector3.down * rayLength);

		// Starts check on ray collision. 
		if (Physics.Raycast (SenoryRay, out _hit, rayLength)) 
		{
			// Checks to make sure the collsion is with a piece of the environment. 
			if (_hit.collider.tag == "Environment") 
			{
				// If it is a piece of the environment, set the distance to the 
				// piece. 
				DistanceToHit = Vector3.Distance (_hit.point, transform.position);
			}

			else if (_hit.collider.tag == "TriggeredAsset") 
			{
				float DistToTriggerAsset = Vector3.Distance (_hit.point, transform.position); 
				DistanceToHit = DistToTriggerAsset += _hit.transform.localScale.y;
			}
		}
	}

	// Sets the scripts trigger variable when system initializes. 
	public void SetSensoryUnitVariables (bool triggerValue) 
	{
		TriggerHit = triggerValue;
	}

	// Provides a readable value for the trigger variable for the
	// collection scripts purposes. 
	public bool TriggerHit { get; set; }

	// Checks what the trigger hits. 
	void OnTriggerEnter (Collider surfaceHitting)
	{
		// If it hits a collider of the environment, and not already
		// triggered, then set as triggered. 
		if (!TriggerHit ) 
		{
			if (surfaceHitting.tag == "Environment") 
			{
				TriggerHit = true;
			}
		}
	}

	// Checks what the trigger leaves. 
	void OnTriggerExit (Collider surfaceleaving)
	{
		// If it leaves a collider of the environment, and is already
		// triggered, then set as not-triggered.
		if (TriggerHit) 
		{
			if (surfaceleaving.tag == "Environment") 
			{
				TriggerHit = false;
			}
		}
	}
}
