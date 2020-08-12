using UnityEngine;
using System.Collections;

public class InnerShell_Tilt_BalancerSubMain : MonoBehaviour 
{
	float MaxUpperToConstDistance { get; set; }

	public void InitializeTiltSubMain (Transform TiltConst, Transform UpperPt) 
	{
		MaxUpperToConstDistance = UpperToConstant_CurrentDistance (TiltConst, UpperPt);
	}

	public string BallTilt (Transform TConst, Transform UPt)
	{
		string tilt = "";

		if(UpperToConstant_CurrentDistance (TConst, UPt) < MaxUpperToConstDistance && 
			UpperToConstant_CurrentDistance (TConst, UPt) > MaxUpperToConstDistance * 0.9474f)
		{
			tilt = "HomeRange";
		}

		else if(UpperToConstant_CurrentDistance (TConst, UPt) < MaxUpperToConstDistance * 0.9474f && 
			UpperToConstant_CurrentDistance (TConst, UPt) > MaxUpperToConstDistance * 0.8158f)
		{
			tilt = "Sloping";
		}

		else if(UpperToConstant_CurrentDistance (TConst, UPt) < MaxUpperToConstDistance * 0.8158f && 
			UpperToConstant_CurrentDistance (TConst, UPt) > MaxUpperToConstDistance * 0.7105f)
		{
			tilt = "HardSloping";
		}

		else if(UpperToConstant_CurrentDistance (TConst, UPt) < MaxUpperToConstDistance * 0.7105f && 
			UpperToConstant_CurrentDistance (TConst, UPt) > MaxUpperToConstDistance * 0.3684f)
		{
			tilt = "SideWalling";
		}

		else if(UpperToConstant_CurrentDistance (TConst, UPt) < MaxUpperToConstDistance * 0.3684f && 
			UpperToConstant_CurrentDistance (TConst, UPt) > MaxUpperToConstDistance * 0.2642f)
		{
			tilt = "InvertedSloping";
		}

		else if(UpperToConstant_CurrentDistance (TConst, UPt) < MaxUpperToConstDistance * 0.2642f && 
			UpperToConstant_CurrentDistance (TConst, UPt) > MaxUpperToConstDistance * 0.1842f)
		{
			tilt = "ExtremeInvertedSloping";
		}

		else if(UpperToConstant_CurrentDistance (TConst, UPt) < MaxUpperToConstDistance * 0.1842f)
		{
			tilt = "TotallyInverted";
		}

		return tilt;
	}

	float UpperToConstant_CurrentDistance (Transform TCon, Transform UppPt)
	{
		return Vector3.Distance (TCon.position, UppPt.position);
	}

	Vector3 TiltPtCurrentPosition (Transform Point)
	{ 
		Vector3 currPos = Point.position;

		return currPos;
	}
}
