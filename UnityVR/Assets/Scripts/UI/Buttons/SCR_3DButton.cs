/*
*
*	3D Button Class
*	===============
*
*	Created: 	2016/11/21
*	Filter:		Scripts/UI/Buttons
*	Class Name: SCR_3DButton
*	Base Class: SCR_BaseUIElement
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	This class will provide the base foundations for any of the 
*				3D buttons used in the application - providing standard 
*				responses for 3D button interaction.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;

/* 3D button IS A UI element, therefore inherits from it. */
public class SCR_3DButton : SCR_BaseUIElement 
{

	/* Attributes. */
	protected SCR_SceneEditor sceneEditor = null;					/* Used to access the scene editor to provide scene editing functionality through 3D buttons. */

	[Header ("3D Button Transition Properties")]
	[SerializeField]	private float scaleUpFactor = 1.05f;		/* The speed used to scale the 3D buttons up when being highlighted. */
	[SerializeField]	private float speed = 1.0f;					/* The speed used to move the 3D buttons up when being highlighted. */

	private Vector3 originalPosition = Vector3.zero; 				/* The original position of the 3D button, used to lerp back to when out of focus. */
	private Vector3 destination = Vector3.zero;						/* The destination of the 3D button, used to lerp to when in focus. */
	private Vector3 originalScale = Vector3.zero;					/* The original scale of the 3D button, used to lerp back to when out of focus. */
	private Vector3 destinationScale = Vector3.zero;				/* The destination scale of the 3D button, used to lerp to when in focus. */
	private Vector3 originalDistanceDifference = Vector3.zero;		/* Used to update the 3D buttons with the camera. */
	private bool isInteractable = true;								/* Used to indicate if this button is interactable or not. */
	protected SCR_3DMenu menu = null;
	protected SCR_Panel parentPanel = null;							/* Accessing the panel that this button belongs to. */

	/* Methods. */
	/* Virtual. */
	/* Each inheriting button class can implement a specific button press response. */
	public virtual void ButtonPressResponse(){}

	/* Each inheriting button class can implement a specific button release response. */
	public virtual void ButtonReleasedResponse(){}

	/*
	*
	*	Overview
	*	--------
	*	This will be called before initialisation.
	*
	*/
	protected void Awake()
	{

		/* Initialising our attributes. */
		menu = GameObject.Find ("PRE_3DMenu").transform.FindChild("Menu").GetComponent<SCR_3DMenu>();
		sceneEditor = GameObject.Find("Scene Editor").GetComponent<SCR_SceneEditor>();
		originalPosition = transform.position;
		originalScale = transform.localScale;
		destination = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z - 0.2f);
		destinationScale = new Vector3(originalScale.x * scaleUpFactor, originalScale.y * scaleUpFactor, originalScale.z * scaleUpFactor);
		parentPanel = transform.parent.GetComponent<SCR_Panel>();

		if (GameObject.Find ("Controller (left)") != null) 
		{

			leftController = GameObject.Find ("Controller (left)").GetComponent<SCR_VRControllerInput>();

		}

		if (GameObject.Find ("Controller (right)") != null) 
		{

			rightController = GameObject.Find ("Controller (right)").GetComponent<SCR_VRControllerInput>();

		}

		if (leftController != null) 
		{

			originalDistanceDifference = leftController.transform.position - transform.position;

		}
		else
		{

			originalDistanceDifference = Camera.main.transform.position - transform.position;

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will provide a standard response to the button being in focus.
	*
	*/
	private void ButtonInFocusStandardResponse()
	{

		if(!isInFocus)
		{

			isInFocus = true;

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will provide a standard response to the button being out of focus.
	*
	*/
	private void ButtonOutOfFocusStandardResponse()
	{

		if(isInFocus)
		{

			isInFocus = false;

		}

	}

	/* This function will need a VR equivalent. */
	/* VR Equivalent: If the user is aiming at this game object with the right hand controller and they have pressed the right hand controller trigger. */
	/*
	*
	*	Overview
	*	--------
	*	This method checks if there has been a left mouse click on this 
	*	game object.
	*
	*/
	private void OnMouseDown()
	{

		/* Perform a specific button response. */
		ButtonPressResponse();

	}

	/* This function will need a VR equivalent. */
	/* VR Equivalent: If the user is aiming at this game object with the right hand controller. */
	/*
	*
	*	Overview
	*	--------
	*	This method checks if there has been the mouse cursor is hovering over this 
	*	game object.
	*
	*/
	private void OnMouseOver()
	{

		/* Perform a standard in focus button response. */
		ButtonInFocusStandardResponse();

	}

	/* This function will need a VR equivalent. */
	/* VR Equivalent: If the user is not aiming at this game object with the right hand controller. */
	/*
	*
	*	Overview
	*	--------
	*	This method checks to see if the mouse cursor has left this game object.
	*
	*/
	private void OnMouseExit()
	{

		/* Perform a standard out of focus button response. */
		ButtonOutOfFocusStandardResponse();

	}

	/*
	*
	*	Overview
	*	--------
	*	This will check and provide the appropriate method calls depending
	*	on the current focus of the object.
	*
	*/
	override protected void CheckFocus()
	{

		/* If this object is currently in focus. */
		if(isInFocus)
		{

			/* Move and scale to the destination values, this button is being highlighted. */
			transform.position = Vector3.Lerp(transform.position, destination, speed);
			transform.localScale = Vector3.Lerp(transform.localScale, destinationScale, speed);

		}
		/* Otherwise, this object is not in focus. */
		else
		{

			Debug.Log ("Left controller position = " + leftController.transform.position);
			Debug.Log ("Original Position = " + originalPosition);

			/* Move and scale to the original values, this button is not being highlighted. */
			transform.position = Vector3.Lerp(transform.position, originalPosition, speed);
			transform.localScale = Vector3.Lerp(transform.localScale, originalScale, speed);

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will make sure that the buttons will appear in the correct position
	*	based on where the camera is currently located.
	*
	*/
	private void UpdateUIPositions()
	{

		if (leftController != null) {
			
			/* Update the original position based on where the camera is located. */
			originalPosition = menu.transform.parent.transform.position + (leftController.transform.position - originalDistanceDifference);

		} else {
			
			/* Update the original position based on where the camera is located. */
			originalPosition = Camera.main.transform.position - originalDistanceDifference;

		}

		/* Update the destination based on the new original position. */
		destination = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z - 0.2f);

	}

	/*
	*
	*	Overview
	*	--------
	*	This will be called every frame.
	*
	*/
	protected void Update()
	{

		/* Comment this out for VR. */
		/* Update the button with the camera location. */
		//UpdateUIPositions();

		/* If this button is interactable. */
		if(isInteractable)
		{

			/* Handles VR control logic. */
			VRControls(ButtonPressResponse);

			/* Comment this out for VR. */
			/* Keep checking the focus of this object. */
			//CheckFocus();

		}

	}

	/* Getters/Setters. */
	/* This will allow us to get/set the current interactive state of this button. */
	public bool IsInteractable
	{
		get { return isInteractable; }
		set { isInteractable = value; }
	}

}