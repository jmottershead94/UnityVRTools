﻿/*
*
*	3D Menu Class
*	=============
*
*	Created: 	2016/11/21
*	Filter:		Scripts/UI
*	Class Name: SCR_3DMenu
*	Base Class: SCR_BaseUIElement
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	3D Menu will provide a way for the user to interact with the scene and 
*				contain panels which will allow the user to perform different scene editing.
*
*				This class will provide a way to store the different panels for the 3D
*				menu, and also keep track of what current panel is in focus.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 3D menu IS A UI element, therefore inherits from it. */
public class SCR_3DMenu : SCR_BaseUIElement
{

	/* Attributes. */
	private List<SCR_Panel> panels = null;				/* Used to store the different panels that the 3D menu has access to. */
	private SCR_Panel currentFocusPanel = null;			/* What current panel is in focus. */
	private int panelIndex = 0;							/* The current panel index that is in focus, used to update the current focussed panel. */
	private Vector3 offScreenPosition = Vector3.zero;
	private Vector3 originalPosition = Vector3.zero;
	private Vector3 originalDistance = Vector3.zero;

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This will be called before initialisation.
	*
	*/
	new private void Awake()
	{
		if(!SCR_SceneEditor.InVREditor)
		{
			gameObject.SetActive(false);
			return;
		}

		/* Initialising our attributes. */
		panels = new List<SCR_Panel>();
		offScreenPosition = new Vector3(SCR_UIConstants.OffScreen.x, SCR_UIConstants.OffScreen.y, transform.position.z);

		/* Initialising local attributes. */
		SCR_Panel[] tempChildrenPanels = transform.FindChild("Panels").GetComponentsInChildren<SCR_Panel>();

		/* Looping through each panel in the 3D menu. */
		foreach(SCR_Panel tempPanel in tempChildrenPanels)
		{

			/* Adding the panels to the panels list. */
			panels.Add(tempPanel);

		}

		/* Setting which panel is currently in focus. */
		panels[panelIndex].InFocus = true;
		currentFocusPanel = panels[panelIndex];

	}

	/* This needs testing out. */
	private void VRControls()
	{

		if(GameObject.Find("Controller (left)") != null)
		{

			leftController = GameObject.Find("Controller (left)").GetComponent<SCR_VRControllerInput>();

		}

		if(leftController != null)
		{

			originalPosition = GameObject.Find("Menu Holder").transform.position;
			//transform.forward = leftController.transform.forward;
			//transform.LookAt(leftController.transform);

			if(leftController.TriggerHeld())
			{

				if(transform.position != offScreenPosition)
				{

					transform.position = offScreenPosition;

				}

			}
			else
			{

				if(transform.position != originalPosition)
				{
					transform.position = originalPosition;
					//transform.position = new Vector3(leftController.transform.position.x, leftController.transform.position.y, leftController.transform.position.z + 0.5f);
					//transform.rotation = leftController.transform.rotation;

				}

			}

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will update what panel is currently in focus, if this
	*	panel has not already been set.
	*
	*/
    private void CheckPanelFocus()
    {

    	/* If the currently selected panel is not in focus. */
    	if(!panels[panelIndex].InFocus)
    	{

    		/* Set the panel so that it is in focus. */
			panels[panelIndex].InFocus = true;

    	}

    }

	/*
	*
	*	Overview
	*	--------
	*	This will be called every frame.
	*
	*/
	private void Update () 
	{
		if(!SCR_SceneEditor.InVREditor)
			return;

		/* Handles any updates with the panel that is currently in focus. */
		CheckPanelFocus();

		/* This needs testing out. */
		VRControls();

	}

	/* Getters/Setters. */
	/* This will allow us to get the current list of panels in the 3D menu. */
	public List<SCR_Panel> Panels
	{
		get { return panels; }
	}

	/* This will allow us to get/set the current panel index which is in focus. */
	public int CurrentPanelIndex
	{
		get { return panelIndex; }
		set { panelIndex = value; }
	}

}