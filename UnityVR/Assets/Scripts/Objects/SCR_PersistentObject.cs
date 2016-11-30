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
	private PrimitiveType primitiveType;					/* Accessing what primitive type we are using. */
	private int id;											/* Accessing the ID number of the object. */
	private Material currentMaterial = null;				/* The current material attached to the game object, will be used to update the current material of the mesh renderer. */
	private Material glowingMaterial = null;				/* Stores the material used to indicate that the game object has been highlighted. */
	private Material defaultMaterial = null;				/* Stores the default material used originally for the game object. */

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

		FocusSwitch();

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

		VRTriggerResponse(FocusSwitch);

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

}