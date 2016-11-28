/*
*
*	UI Constants Class
*	==================
*
*	Created: 	2016/11/21
*	Filter:		Scripts/Statics
*	Class Name: SCR_UIConstants
*	Base Class: N/A
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	UI Constants will provide a quick way to access important UI coordinates for
*				any UI element in the application.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;

/* UI Constants is just a standard class. */
public static class SCR_UIConstants
{

	/* Attributes. */
	private static Vector2 LEFT_OF_SCREEN = new Vector2(Screen.width * 0.5f * 0.25f, Screen.height * 0.5f);	/* Stores the location of the left side of the screen. */
	private static Vector2 RIGHT_OF_SCREEN = new Vector2(Screen.width * 0.75f, Screen.height * 0.5f);		/* Stores the location of the right side of the screen. */
	private static Vector2 ON_SCREEN = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);				/* Stores the location of the centre of the screen. */
	private static Vector2 OFF_SCREEN = new Vector2(Screen.width * 0.5f, Screen.height * 2.0f);				/* Stores the location for being off screen. */

	/* Methods. */
	/* This will allow us to get the location of the left side of the screen. */
	public static Vector2 LeftOfScreen
	{
		get { return LEFT_OF_SCREEN; }
	}

	/* This will allow us to get the location of the right side of the screen. */
	public static Vector2 RightOfScreen
	{
		get { return RIGHT_OF_SCREEN; }
	}

	/* This will allow us to get the location of the centre of the screen. */
	public static Vector2 OnScreen
	{
		get { return ON_SCREEN; }
	}

	/* This will allow us to get the location for being off screen. */
	public static Vector2 OffScreen
	{
		get { return OFF_SCREEN; }
	}

}