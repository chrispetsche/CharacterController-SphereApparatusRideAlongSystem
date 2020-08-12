using UnityEngine;
using System.Collections;

public class CameraPositioning : MonoBehaviour 
{
	Transform PointToFollow { get; set; }

	public void SetVRCamera_PositionAndRotation (Transform ptToFollow) 
	{
		PointToFollow = ptToFollow;

		transform.position = new Vector3 (PointToFollow.position.x, PointToFollow.position.y, PointToFollow.position.z);
		transform.eulerAngles = new Vector3 (PointToFollow.eulerAngles.x, PointToFollow.eulerAngles.y, PointToFollow.eulerAngles.z);
	}

	public void RunVRCamera_PositionAndRotation () 
	{
		transform.position = new Vector3 (PointToFollow.position.x, PointToFollow.position.y, PointToFollow.position.z);
		transform.eulerAngles = new Vector3 (PointToFollow.eulerAngles.x, PointToFollow.eulerAngles.y, PointToFollow.eulerAngles.z);
	}
}
