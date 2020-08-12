using UnityEngine;
using System.Collections;

public class Ball_ClassReference : MonoBehaviour //UniverseElementMasterReference 
{
	public float OuterShellVariables (string shellBrand, string shellMakeAndModel, string shellVariable)
	{
		float shellValue = 0f;

		switch (shellBrand)
		{
		case "BrandA":
			{
				switch (shellMakeAndModel)
				{
				case "MakeA_ModelA":
					{
						switch (shellVariable)
						{
						case "Mass":
							{
								shellValue = 250f;

								break;
							}
						}

						break;
					}
				}

				break;
			}
		}

		return shellValue;
	}

	public float InnerShellVariables (string shellBrand, string shellMakeAndModel, string shellVariable)
	{
		float shellValue = 0f;

		switch (shellBrand)
		{
		case "BrandA":
			{
				switch (shellMakeAndModel)
				{
				case "MakeA_ModelA":
					{
						switch (shellVariable)
						{
						case "Mass":
							{
								shellValue = 250f;

								break;
							}
						}

						break;
					}
				}

				break;
			}
		}

		return shellValue;
	}
}
