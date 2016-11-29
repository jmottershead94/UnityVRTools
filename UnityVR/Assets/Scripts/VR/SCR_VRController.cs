/*
*
*	VR Controller Class
*	===================
*
*	Created: 	2016/11/29
*	Filter:		Scripts/VR
*	Class Name: SCR_VRController
*	Base Class: SCR_VRControllerInput
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	VR Controller will provide a way to implement more specific VR
*				controller functionality depending on the input received. So
*				the user can implement more to each input type if they want 
*				to.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;

/* VR controller IS VR controller input, therefore inherits from it. */
public class SCR_VRController : SCR_VRControllerInput
{

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This method will provide a more specific trigger pressed response.
	*
	*	Params
	*	------
	*	object sender		-	This will contain the object that this event is being sent from.
	*
	*	ClickedEventArgs e	-	This holds the click event being used and can provide access to the
	*							controller index.
	*
	*/
	protected override void TriggerPressed(object sender, ClickedEventArgs e)
	{

		/* This will provide a response for pressing the trigger. */
		Debug.Log("Add more functionality here! Trigger pressed from " + name);

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will provide a more specific trigger released response.
	*
	*	Params
	*	------
	*	object sender		-	This will contain the object that this event is being sent from.
	*
	*	ClickedEventArgs e	-	This holds the click event being used and can provide access to the
	*							controller index.
	*
	*/
	protected override void TriggerReleased(object sender, ClickedEventArgs e)
	{

		/* This will provide a response for releasing the trigger. */
		Debug.Log("Add more functionality here! Trigger released from " + name);

	}

}