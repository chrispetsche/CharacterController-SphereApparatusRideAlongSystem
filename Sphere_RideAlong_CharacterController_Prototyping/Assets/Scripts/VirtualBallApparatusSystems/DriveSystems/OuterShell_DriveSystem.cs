using UnityEngine;
using System.Collections;

public class OuterShell_DriveSystem : MonoBehaviour 
{
	public Rigidbody BodyToEffect { get; set; }
	public Transform InfluencingPoint { get; set; }

	public void DriveOuterShellDirection (int axis, float Force) 
	{
		switch (axis)
		{
		case 1:
			{
				BodyToEffect.AddForce (InfluencingPoint.right * Force);

				break;
			}

		case 2:
			{
				BodyToEffect.AddForce (InfluencingPoint.up * Force);

				break;
			}

		case 3:
			{
				BodyToEffect.AddForce (InfluencingPoint.forward * Force);

				break;
			}
		}
	}
}
