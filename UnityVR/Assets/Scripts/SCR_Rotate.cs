using UnityEngine;
using System.Collections;

public class SCR_Rotate : MonoBehaviour 
{

	/* Attributes. */
	[SerializeField]	private Vector3 rotationSpeed = Vector3.zero;

	/* Methods. */	
	void Update () 
	{

		transform.Rotate(rotationSpeed);

	}
}
