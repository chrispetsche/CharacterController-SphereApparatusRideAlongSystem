using UnityEngine;
using System.Collections;

public class InnerShell_TiltConstant : MonoBehaviour 
{
	public void SetTiltConstantPosition (float MTR)
	{
		float xPos = transform.parent.localPosition.x;
		float yPos = transform.parent.localPosition.y;
		float zPos = transform.parent.localPosition.z;

		float newYPos = yPos -= MTR;

		transform.position = new Vector3 (xPos, newYPos, zPos);
	}

	public Transform TiltConstantCurrentPosition ()
	{
		return transform;
	}
}
