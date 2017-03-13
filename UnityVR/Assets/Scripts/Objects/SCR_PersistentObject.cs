/*
*
*	Persistent Object Class
*	=======================
*
*	Created: 	2016/11/21
*	Filter:		Scripts/Objects
*	Class Name: SCR_PersistentObject
*	Base Class: SCR_BaseUIElement
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	The purpose of this class is to provide a base for all of the objects
*				that can be saved using this application, and also provide access to
*				the primitive type of the game object.
*
*/

/* Unity includes here. */
using UnityEngine;
using System;
using System.Collections;

/* Persistent object IS A UI element, therefore inherits from it. */
public class SCR_PersistentObject : SCR_BaseUIElement
{

	/* Attributes. */
	[SerializeField]	private bool execute = false;
	private PrimitiveType primitiveType;					/* Accessing what primitive type we are using. */
	private int id;											/* Accessing the ID number of the object. */
	private Material currentMaterial = null;				/* The current material attached to the game object, will be used to update the current material of the mesh renderer. */
	private Material glowingMaterial = null;				/* Stores the material used to indicate that the game object has been highlighted. */
	private Material defaultMaterial = null;				/* Stores the default material used originally for the game object. */
	private Material changingMaterial = null;
	private bool isMouseOver = false;
	private bool holding = false;
	private SCR_GrabButton grabButton = null;
	private Vector3 screenPoint;
	private Vector3 offset;

	/* Methods. */
	/* Virtual. */
	/* All persistent objects must implement their own specific on focus event. */
	protected virtual void OnFocus(){}

	/* All persistent objects must implement their own specific out of focus event. */
	protected virtual void OutOfFocus(){}

	/*
	*
	*	Overview
	*	--------
	*	This will be called before initialisation.
	*
	*/
	new protected void Awake()
	{
		base.Awake();

		/* Initialising our attributes. */
		currentMaterial = GetComponent<MeshRenderer>().materials[0];
		defaultMaterial = currentMaterial;
		glowingMaterial = Resources.Load("Materials/MAT_ObjectSelected") as Material;
		changingMaterial = Resources.Load("Materials/MAT_ChangingColour") as Material;

		if(!execute)
			return;

		GameObject grabCheck = GameObject.Find ("GrabModeButton");
		if(grabCheck != null)
			grabButton = GameObject.Find ("GrabModeButton").GetComponent<SCR_GrabButton> ();
	}

	/*
	*
	*	Overview
	*	--------
	*	This method will provide a response for indicating that this game object
	*	is currently in focus.
	*
	*/
	private void ObjectInFocusStandardResponse()
	{
		if(!execute)
			return;

		/* If the current material has not been set to the glowing material. */
		if(currentMaterial != glowingMaterial)
		{

			/* Set the current material so that it is now glowing. */
			currentMaterial = glowingMaterial;

			/* Set the current material being used by this game object to the new current material. */
			GetComponent<MeshRenderer>().material = currentMaterial;

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will provide a response for indicating that this game object
	*	is out of focus.
	*
	*/
	private void ObjectOutOfFocusStandardResponse()
	{

		if(!execute)
			return;

		/* If the current material has not been set to the default material. */
		if(currentMaterial != defaultMaterial)
		{

			/* Set the current material so that it is now default. */
			currentMaterial = defaultMaterial;

			/* Set the current material being used by this game object to the new current material. */
			GetComponent<MeshRenderer>().material = currentMaterial;

		}

	}

	private void FocusSwitch()
	{
		if(!execute)
			return;

		/* If this object is currently in focus. */
		if(isInFocus)
		{

			/* Change this object so it is out of focus. */
			isInFocus = false;

		}
		/* Otherwise, this object is not in focus. */
		else
		{

			/* Change this object so it is in focus. */
			isInFocus = true;

		}
	}

	private void HoldingObject()
	{
		if(!execute)
			return;

		InFocus = true;
		holding = true;
		SCR_VRUtilities.Holding = true;
	}

	private void DropObject()
	{
		if(!execute)
			return;

		Transform previousTransform = GameObject.Find("Scene Editor").transform;

		if(InFocus)
		{
			InFocus = false;
			holding = false;

			if(SCR_VRUtilities.Holding)
				SCR_VRUtilities.Holding = false;

			if(transform.parent != previousTransform)
				transform.SetParent(previousTransform);
		}
	}

	private void PCHolding()
	{
		if(!execute)
			return;
		HoldingObject();
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	private void VRHolding()
	{
		if(!execute)
			return;

		if (!SCR_VRUtilities.Holding) 
		{
			HoldingObject ();
			transform.SetParent (rightController.transform);
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
		if(!execute)
			return;

		FocusSwitch();
	}

	/* VR Equivalent: Right controller aiming at this and the trigger is held. */
	private void OnMouseOver()
	{
		if(!execute)
			return;

		isMouseOver = true;
	}

	private void OnMouseDrag()
	{
		if(!execute)
			return;

		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = cursorPosition;
	}

	private void OnMouseExit()
	{
		if(!execute)
			return;

		isMouseOver = false;

		if(InFocus && holding)
			DropObject();
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

		if(!execute)
			return;

		/* If this object is currently in focus. */
		if(isInFocus)
		{

			/* Handles standard object focus response. */
			ObjectInFocusStandardResponse();

			/* Handles any specific focus responses. */
			OnFocus();

		}
		/* Otherwise, this object is not in focus. */
		else
		{

			/* Handles standard object focus response. */
			ObjectOutOfFocusStandardResponse();

			/* Handles any specific focus responses. */
			OutOfFocus();

		}
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
		if(grabButton == null || !execute)
			return;

		if(grabButton.TransformType == SCR_GrabButton.VRTransformType.freeForm)
			VRTriggerResponse(FocusSwitch);
		else
			VRTriggerHeldResponse(VRHolding, DropObject, holding);

//		if(isMouseOver && grabButton.TransformType == SCR_GrabButton.VRTransformType.grab)
//		{
//			if(Input.GetMouseButton(0))
//				PCHolding();
//			else if(Input.GetMouseButtonUp(0) && holding)
//				DropObject();
//		}

		/* Handles any game object focus updates. */
		CheckFocus();
	}

	/* Getters/Setters. */
	/* This will allow us to get/set the primitive type of the game object. */
	public PrimitiveType ObjectType
	{
		get { return primitiveType; }
		set { primitiveType = value; }
	}

	/* This will allow us to get/set the current ID number of this game object. */
	public int ID
	{
		get { return id; }
		set { id = value; }
	}

	public Material DefaultMaterial
	{
		get { return defaultMaterial; }
		set { defaultMaterial = value; }
	}

	public Material CurrentMaterial
	{
		get { return currentMaterial; }
		set { currentMaterial = value; }
	}

	public Material HighLightedMaterial
	{
		get { return glowingMaterial; }
		set { glowingMaterial = value; }
	}


	public Material ChangingMaterial
	{
		get { return changingMaterial; }
		set { changingMaterial = value; }
	}

}