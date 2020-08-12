using UnityEngine;
using System.Collections;

public class BallGrounded_Trigger : MonoBehaviour 
{
	public int TriggerValue { get; set; }

	void OnTriggerEnter (Collider entering) 
	{
		if (entering.tag == "Environment") 
		{
			TriggerValue = 1;
		}
	}

	void OnTriggerExit (Collider exiting) 
	{
		if (exiting.tag == "Environment") 
		{
			TriggerValue = 0;
		}
	}
}
