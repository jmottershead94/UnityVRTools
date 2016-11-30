using UnityEngine;
using System.Collections;

public class SCR_VRUtilities : MonoBehaviour 
{

	/* Methods. */
	public static void AssignControllers(SCR_VRControllerInput vrController, int index)
	{

		if (index == 0) 
		{

			if (GameObject.Find ("Controller (left)") != null) 
			{

				vrController = GameObject.Find ("Controller (left)").GetComponent<SCR_VRControllerInput> ();

			}

		}
		else if(index == 1) 
		{

			if (GameObject.Find ("Controller (right)") != null) 
			{

				vrController = GameObject.Find ("Controller (right)").GetComponent<SCR_VRControllerInput> ();

			}

		}

	}

}
