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
using Valve.VR;

/* VR controller input IS A game object, therefore inherits from it. */
public class SCR_VRControllerInput : MonoBehaviour 
{

	/* Attributes. */
	private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;	/* Used to test if the trigger button has been pressed. */
	private EVRButtonId dPadUp = EVRButtonId.k_EButton_DPad_Up;					/* Used to test if the DPad Up button has been pressed. */
	private EVRButtonId dPadRight = EVRButtonId.k_EButton_DPad_Right;			/* Used to test if the DPad Right button has been pressed. */
	private EVRButtonId dPadLeft = EVRButtonId.k_EButton_DPad_Left;				/* Used to test if the DPad Left button has been pressed. */
	private EVRButtonId dPadDown = EVRButtonId.k_EButton_DPad_Down;				/* Used to test if the DPad Down button has been pressed. */
	private SteamVR_TrackedObject trackedObject = null;							/* Stores the current tracked object. */
	private SteamVR_Controller.Device device = null;							/* Stores the current hand controller. */
	private RaycastHit target;													/* What the controller is aiming at. */

	/* Methods. */
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
		trackedObject = GetComponent<SteamVR_TrackedObject>();

	}

	/*
	*
	*	Overview
	*	--------
	*	This will be called every frame.
	*
	*/
	private void Update()
	{

		/* Track the correct device. */
		device = SteamVR_Controller.Input((int)trackedObject.index);

	}

	/*
	*
	*	Overview
	*	--------
	*	This will be called at a fixed framerate and will be used to
	*	update any physics. 
	*
	*/
	private void FixedUpdate()
	{

		/* Initialising local attributes. */
		Vector3 tempDirection = transform.TransformDirection(Vector3.forward);

		/* Drawing the ray. */
		Debug.DrawRay(transform.position, tempDirection, Color.cyan);

		/* If the ray has collided with something. */
		if(Physics.Raycast(transform.position, tempDirection, out target, 10.0f))
		{

			/* If the target is a base UI element. */
			if(target.transform.GetComponent<SCR_BaseUIElement>() != null)
			{

				/* We have hit something we can interact with. */
				/* Provide some controller feedback to the user. */
				device.TriggerHapticPulse(700);

			}

		}

	}

	/* Getters. */
	/* This will allow us to get the current trigger status of the hand controller. */
	public bool TriggerPressed
	{
		get { return (device.GetPressDown(triggerButton)); }
	}

	/* This will allow us to indicate if up on the hand controller DPad has been pressed. */
	public bool UpPressed
	{
		get { return (device.GetPressDown(dPadUp));}
	}

	/* This will allow us to indicate if right on the hand controller DPad has been pressed. */
	public bool RightPressed
	{
		get { return (device.GetPressDown(dPadRight));}
	}

	/* This will allow us to indicate if left on the hand controller DPad has been pressed. */
	public bool LeftPressed
	{
		get { return (device.GetPressDown(dPadLeft));}
	}

	/* This will allow us to indicate if down on the hand controller DPad has been pressed. */
	public bool DownPressed
	{
		get { return (device.GetPressDown(dPadDown));}
	}

}