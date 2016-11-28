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
	protected bool isInFocus = false;	/* Used to indicate if this game object is in focus or not. */

	/* Methods. */
	/* Virtual. */
	/* All UI elements must check focus. */
	protected virtual void CheckFocus(){}

	/* Getters/Setters. */
	/* This will allow us to get/set the current focus status of this game object. */
	public bool InFocus
	{
		get { return isInFocus; }
		set { isInFocus = value; }
	}

}