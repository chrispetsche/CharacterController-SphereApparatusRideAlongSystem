using UnityEngine;

public class DriveSystem_Main : MonoBehaviour 
{
	OuterShell_DriveSystem OuterShellSystem;
	InnerShell_DriveSystem InnerShellSystem;

	public void InitializeDriveSystem (GameObject outerShell, Rigidbody outerBody, GameObject innerShell, Rigidbody innerBody) 
	{
		OuterShellSystem = outerShell.AddComponent<OuterShell_DriveSystem> ();
		InnerShellSystem = innerShell.AddComponent<InnerShell_DriveSystem> ();

		OuterShellSystem.BodyToEffect = outerBody;
		InnerShellSystem.BodyToEffect = innerBody;

		OuterShellSystem.InfluencingPoint = innerShell.transform;
	}

	public void DriveConstantSystem (Transform outerShell) 
	{
		InnerShellSystem.DriveConstantPosition (outerShell);
	}

	public void DriveDirectionAndRotation (string systemToDrive, string axisToDrive, float forceToApply)
	{
		switch (systemToDrive)
		{
		case "OuterShell":
			{
				switch (axisToDrive) 
				{
				case "Right":
					{
						OuterShellSystem.DriveOuterShellDirection (1, forceToApply);

						break;
					}

				case "Up":
					{
						OuterShellSystem.DriveOuterShellDirection (2, forceToApply);

						break;
					}

				case "Forward":
					{
						OuterShellSystem.DriveOuterShellDirection (3, forceToApply);

						break;
					}
				}

				break;
			}

		case "InnerShell":
			{
				switch (axisToDrive) 
				{
				case "Right":
					{
						InnerShellSystem.DriveInnerShellRotation (1, forceToApply);

						break;
					}

				case "Up":
					{
						InnerShellSystem.DriveInnerShellRotation (2, forceToApply);

						break;
					}

				case "Forward":
					{
						InnerShellSystem.DriveInnerShellRotation (3, forceToApply);

						break;
					}
				}

				break;
			}

		case "Balancer":
			{
				switch (axisToDrive) 
				{
				case "Right":
					{
						InnerShellSystem.DriveInnerShellRotation (1, forceToApply);

						break;
					}

				case "Up":
					{
						InnerShellSystem.DriveInnerShellRotation (2, forceToApply);

						break;
					}

				case "Forward":
					{
						InnerShellSystem.DriveInnerShellRotation (3, forceToApply);

						break;
					}
				}

				break;
			}
		}
	}
}
