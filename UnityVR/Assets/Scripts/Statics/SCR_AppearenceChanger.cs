using UnityEngine;
using System.Collections;

public class SCR_AppearenceChanger
{

	/* Methods. */
	public static void ChangeMaterial(GameObject gameObject, Material materialToChangeTo)
	{

		MeshRenderer tempMeshRenderer = gameObject.GetComponent<MeshRenderer>();

		if(tempMeshRenderer.material != materialToChangeTo)
		{

			tempMeshRenderer.material = materialToChangeTo;

		}

	}

}