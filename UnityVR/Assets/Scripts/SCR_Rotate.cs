/*
*
*	Rotate Class
*	============
*
*	Created: 	2016/11/21
*	Class Name: SCR_Rotate
*	Base Class: Monobehaviour
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	The purpose of this class is to allow objects to rotate on their
*				own, which will provide a nice UI and help highlight when a UI
*				element is in focus. This will mainly be used for the primitive
*				spawning buttons.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;

/* Rotate IS A game object, therefore inherits from it. */
public class SCR_Rotate : MonoBehaviour 
{

	/* Attributes. */
	[SerializeField]	private Vector3 rotationSpeed = Vector3.zero;	/* This will store how fast the object will rotate. */

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This will be called every frame.
	*
	*/
	private void Update () 
	{

		/* Rotate the object. */
		transform.Rotate(rotationSpeed);

	}

}