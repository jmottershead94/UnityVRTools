/*
*
*	Spawning Primitive Button Class
*	===============================
*
*	Created: 	2016/11/21
*	Filter:		Scripts/UI/Buttons
*	Class Name: SCR_SpawningPrimitiveButton
*	Base Class: SCR_3DButton
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	This class will allow the user to spawn in primitive game objects depending
*				on what button is pressed, and depending on what that button spawns.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;

/* Spawning primitive button IS A 3D button, therefore inherits from it. */
public class SCR_SpawningPrimitiveButton : SCR_3DButton 
{

	/* Attributes. */
	[Header ("Spawning Object Properties")]
	[SerializeField]	private PrimitiveType primitiveType = PrimitiveType.Sphere;	/* Used to indicate what primitive to spawn in with this button. */

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This will allow us to define a specific button response.
	*
	*/
	override public void ButtonPressResponse()
	{

		/* Spawns in a primitive game object at the spawn point. */
		sceneEditor.SpawnPrimitive(primitiveType, spawner.position);
		
	}

	/*
	*
	*	Overview
	*	--------
	*	This will be called every frame.
	*
	*/
	new private void Update()
	{

		/* If the panel that this button belongs to is in focus. */
		if(parentPanel.InFocus)
		{

			/* Handles base update method call. */			
			base.Update();

		}

	}

}