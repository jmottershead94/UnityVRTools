/*
*
*	VR Controller Input Class
*	=========================
*
*	Created: 	2016/11/29
*	Filter:		Scripts/VR
*	Class Name: SCR_VRControllerInput
*	Base Class: Monobehaviour
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	This class will provide a way to easily access VR controller input
*				with trigger and button presses. This will provide a base for any
*				VR controller input, and will allow for the user to expand upon the 
*				functionality by overriding the virtual methods for each input type.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;

/* VR controller input IS A game object, therefore inherits from it. */
public class SCR_VRControllerInput : MonoBehaviour 
{

	/* Attributes. */
	private SteamVR_TrackedController handController = null;	/* Provides access to the VR hand controller. */
	protected bool triggerPressed = false;						/* Used to indicate if the trigger for this hand controller has been pressed. */

	/* Methods. */
	/* Virtual. */
	/* Each inheriting VR controller class can implement a specific trigger press response. */
	protected virtual void TriggerPressed(object sender, ClickedEventArgs e){}

	/* Each inheriting VR controller class can implement a specific trigger release response. */
	protected virtual void TriggerReleased(object sender, ClickedEventArgs e){}

	/*
	*
	*	Overview
	*	--------
	*	This will be called before initialisation.
	*
	*/
	private void Awake()
	{

		/* Initialising our attributes. */
		handController = GetComponent<SteamVR_TrackedController>();

		/* Adding on trigger events for the hand controller. */
		/* Adding standard and specific trigger pressed responses. */
		handController.TriggerClicked += TriggerPressedStandardResponse;
		handController.TriggerClicked += TriggerPressed;

		/* Adding standard and specific trigger released responses. */
		handController.TriggerUnclicked += TriggerReleasedStandardResponse;
		handController.TriggerUnclicked += TriggerReleased;

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will provide a standard trigger pressed response.
	*
	*	Params
	*	------
	*	object sender		-	This will contain the object that this event is being sent from.
	*
	*	ClickedEventArgs e	-	This holds the click event being used and can provide access to the
	*							controller index.
	*
	*/
	private void TriggerPressedStandardResponse(object sender, ClickedEventArgs e)
	{

		/* This will provide a response for pressing the trigger. */
		Debug.Log("Trigger pressed from " + name);
		triggerPressed = true;

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will provide a standard trigger released response.
	*
	*	Params
	*	------
	*	object sender		-	This will contain the object that this event is being sent from.
	*
	*	ClickedEventArgs e	-	This holds the click event being used and can provide access to the
	*							controller index.
	*
	*/
	private void TriggerReleasedStandardResponse(object sender, ClickedEventArgs e)
	{

		/* This will provide a response for pressing the trigger. */
		Debug.Log("Trigger released from " + name);
		triggerPressed = false;

	}

	/* Getters. */
	/* This will allow us to get the current trigger status of the hand controller. */
	public bool HasTriggerBeenPressed
	{
		get { return triggerPressed; }
	}

}