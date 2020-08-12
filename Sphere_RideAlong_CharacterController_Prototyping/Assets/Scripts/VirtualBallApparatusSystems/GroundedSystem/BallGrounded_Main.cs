using UnityEngine;
using System.Collections;

public class BallGrounded_Main : MonoBehaviour 
{
	[SerializeField]
	BallGrounded_Trigger [] BallGrounded_TriggerArray;

	BallGrounded_Trigger _ballGrounded_Trigger_A;
	BallGrounded_Trigger _ballGrounded_Trigger_B;
	BallGrounded_Trigger _ballGrounded_Trigger_C;
	BallGrounded_Trigger _ballGrounded_Trigger_D;
	BallGrounded_Trigger _ballGrounded_Trigger_E;
	BallGrounded_Trigger _ballGrounded_Trigger_F;
	BallGrounded_Trigger _ballGrounded_Trigger_G;
	BallGrounded_Trigger _ballGrounded_Trigger_H;
	BallGrounded_Trigger _ballGrounded_Trigger_I;
	BallGrounded_Trigger _ballGrounded_Trigger_J;
	BallGrounded_Trigger _ballGrounded_Trigger_K;
	BallGrounded_Trigger _ballGrounded_Trigger_L;

	public float GroundedDistance { get; set; }

	public void InitializeSystem (float MTR, float groundedDistBuffer)
	{
		for (int i = 0; i < BallGrounded_TriggerArray.Length; ++i) 
		{
			BallGrounded_TriggerArray [i].TriggerValue = 0;
		}

		_ballGrounded_Trigger_A = BallGrounded_TriggerArray [0];
		_ballGrounded_Trigger_B = BallGrounded_TriggerArray [1];
		_ballGrounded_Trigger_C = BallGrounded_TriggerArray [2];
		_ballGrounded_Trigger_D = BallGrounded_TriggerArray [3];
		_ballGrounded_Trigger_E = BallGrounded_TriggerArray [4];
		_ballGrounded_Trigger_F = BallGrounded_TriggerArray [5];
		_ballGrounded_Trigger_G = BallGrounded_TriggerArray [6];
		_ballGrounded_Trigger_H = BallGrounded_TriggerArray [7];
		_ballGrounded_Trigger_I = BallGrounded_TriggerArray [8];
		_ballGrounded_Trigger_J = BallGrounded_TriggerArray [9];
		_ballGrounded_Trigger_K = BallGrounded_TriggerArray [10];
		_ballGrounded_Trigger_L = BallGrounded_TriggerArray [11];

		GroundedDistance = MTR + groundedDistBuffer;
	}

	public void DriveGroundedSystem (int rayLength)
	{
		OrbitGroundTriggers ();
		ProjectRays (rayLength); // 25 std

		CheckGrounding ();
	}

	public bool BallGrounded { get; set; }

	void CheckGrounding ()
	{
		if (UpRay_HitDistance > GroundedDistance && DownRay_HitDistance > GroundedDistance && TotalGroundTriggerValue () <= 0) 
		{
			BallGrounded = false;
		}

		if(UpRay_HitDistance <= GroundedDistance || DownRay_HitDistance <= GroundedDistance || TotalGroundTriggerValue () > 0)
		{
			BallGrounded = true;
		}
	}

	int TotalGroundTriggerValue ()
	{ 
		int value = 0;

		value = _ballGrounded_Trigger_A.TriggerValue + _ballGrounded_Trigger_B.TriggerValue + _ballGrounded_Trigger_C.TriggerValue + _ballGrounded_Trigger_D.TriggerValue 
			+ _ballGrounded_Trigger_E.TriggerValue + _ballGrounded_Trigger_F.TriggerValue + _ballGrounded_Trigger_G.TriggerValue + _ballGrounded_Trigger_H.TriggerValue 
			+ _ballGrounded_Trigger_I.TriggerValue + _ballGrounded_Trigger_J.TriggerValue + _ballGrounded_Trigger_K.TriggerValue + _ballGrounded_Trigger_L.TriggerValue;

		return value;
	}

	void OrbitGroundTriggers ()
	{
		int orbitSpeed = 250;
		transform.Rotate (0, 0, orbitSpeed * Time.deltaTime);
	}

	public float UpRay_HitDistance { get; set; }
	public float DownRay_HitDistance { get; set; }

	void ProjectRays (int rayLength)
	{
		RaycastHit _hit;

		Ray UpRay = new Ray (transform.position, Vector3.up * rayLength);
		Debug.DrawRay (transform.position, Vector3.up * rayLength);

		Ray DownRay = new Ray (transform.position, Vector3.down * rayLength);
		Debug.DrawRay (transform.position, Vector3.down * rayLength);

		if (Physics.Raycast (UpRay, out _hit, rayLength)) 
		{
			if (_hit.collider.tag == "Environment") 
			{
				// If it is a piece of the environment, set the distance to the 
				// piece. 
				UpRay_HitDistance = Vector3.Distance (_hit.point, transform.position);
			} 

			else if (_hit.collider.tag == "TriggeredAsset") 
			{
				float DistToTriggerAsset = Vector3.Distance (_hit.point, transform.position); 
				UpRay_HitDistance = DistToTriggerAsset += _hit.transform.localScale.y;
			} 

			else 
			{
				UpRay_HitDistance =  0f;
			}
		}

		if (Physics.Raycast (DownRay, out _hit, rayLength)) 
		{
			if (_hit.collider.tag == "Environment") 
			{
				// If it is a piece of the environment, set the distance to the 
				// piece. 
				DownRay_HitDistance = Vector3.Distance (_hit.point, transform.position);
			}

			else if (_hit.collider.tag == "TriggeredAsset") 
			{
				float DistToTriggerAsset = Vector3.Distance (_hit.point, transform.position); 
				DownRay_HitDistance = DistToTriggerAsset += _hit.transform.localScale.y;
			}

			else 
			{
				DownRay_HitDistance =  0f;
			}
		}
	}
}
