/*
*
*	Base UI Element Class
*	=====================
*
*	Created: 	2016/11/21
*	Filter:		Scripts/UI
*	Class Name: SCR_BaseUIElement
*	Base Class: Monobehaviour
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	This class will provide the foundations for any UI element in this application,
*				and also provide access to the current focus status of the element.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;

/* Base UI element IS A game object, therefore inherits from it. */
public class SCR_BaseUIElement : MonoBehaviour 
{

	/* Attributes. */
	protected SCR_VRControllerInput rightController = null;
	protected SCR_VRControllerInput leftController = null;
	protected bool isInFocus = false;							/* Used to indicate if this game object is in focus or not. */


	/* Methods. */
	/* Delegates. */
	public delegate void UIResponseDelegate();

	/* Virtual. */
	/* All UI elements must check focus. */
	protected virtual void CheckFocus(){}

	protected void Awake()
	{

		if(GameObject.Find("Controller (left)") != null)
		{
			leftController = GameObject.Find("Controller (left)").GetComponent<SCR_VRControllerInput>();
		}

		if(GameObject.Find("Controller (right)") != null)
		{
			rightController = GameObject.Find("Controller (right)").GetComponent<SCR_VRControllerInput>();
		}

	}

	protected bool ControllerAimingAtSomething(SCR_VRControllerInput controller)
	{
		if(controller.IsAimingAtSomething)
		{
			if (controller.Target.transform.gameObject != null) 
			{
				if (controller.Target.transform.gameObject == gameObject) 
				{
					return true;
				}
			}
		}

		return false;
	}

	/*
	*
	*	Overview
	*	--------
	*	This method will provide checks for user input through VR.
	*
	*/
	protected void VRTriggerResponse(UIResponseDelegate pressMethod)
	{
		if(GameObject.Find ("Controller (right)") == null)
			return;

		rightController = GameObject.Find ("Controller (right)").GetComponent<SCR_VRControllerInput>();

		if(ControllerAimingAtSomething(rightController))
		{
			if (rightController.TriggerPressed ()) 
				pressMethod();
		}
	}

	// You may need to pass in a bool for the holding parameter.
	protected void VRTriggerHeldResponse(UIResponseDelegate holdMethod, UIResponseDelegate releaseMethod, bool holding)
	{
		if(GameObject.Find ("Controller (right)") == null)
			return;

		rightController = GameObject.Find ("Controller (right)").GetComponent<SCR_VRControllerInput>();

		if(ControllerAimingAtSomething(rightController))
		{
			if (rightController.TriggerHeld ())
				holdMethod ();
			else if (!rightController.TriggerPressed () && holding) 
			{
				Debug.Log ("Should release...");
				releaseMethod ();
			}
		}
	}

	/* Getters/Setters. */
	/* This will allow us to get/set the current focus status of this game object. */
	public bool InFocus
	{
		get { return isInFocus; }
		set { isInFocus = value; }
	}

}