using UnityEngine;
using System.Collections;

public class BalancerSystemCenter_DriveSystem : MonoBehaviour 
{
	public Rigidbody BodyToEffect { get; set; }

	public void DriveBalancerRotation (int axis, float Force) 
	{
		switch (axis)
		{
		case 1:
			{
				BodyToEffect.AddTorque (transform.right * Force);

				break;
			}

		case 2:
			{
				BodyToEffect.AddTorque (transform.up * Force);

				break;
			}

		case 3:
			{
				BodyToEffect.AddTorque (transform.forward * Force);

				break;
			}
		}
	}
}
