/*
*
*	Rotate Panel Button Class
*	=========================
*
*	Created: 	2016/11/21
*	Filter:		Scripts/UI/Buttons
*	Class Name: SCR_RotatePanelButton
*	Base Class: SCR_3DButton
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	Rotate panel button will provide specific functionality for navigating the
*				3D menu using the 3D arrow buttons.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;

/* Rotate panel button IS A 3D button, therefore inherits from it. */
public class SCR_RotatePanelButton : SCR_3DButton
{

	/* Attributes. */
	[Header ("Panel Control")]
	[SerializeField]	private Vector3 rotationValue = Vector3.zero;	/* The angles and axis this should rotate the panel by. */
	[SerializeField]	private Vector3 rotationSpeed = Vector3.zero;	/* How fast the panel should lerp to the rotation value. */
	//private SCR_3DMenu menu = null;										/* Used to access the 3D menu. */
	private int currentRotation = 0;									/* Stores the current rotation value. */
	private const int targetAngle = 90;									/* Used to set the current target angle for the rotation. */
	private bool shouldRotate = false;									/* Indicates if the button should rotate the panel or not. */

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This method provides a response for the rotation arrow being pressed.
	*
	*/
	private void ArrowResponse()
	{

		/* If this is the right arrow. */
		if(name == "Right Arrow")
		{

			/* Used for lerping the rotation - needs work still. */
			/* destinationRotation = new Vector3(0.0f, 90.0f, 0.0f); */

			/* The current menu panel is going out of focus because of the rotation. */
			menu.Panels[menu.CurrentPanelIndex].InFocus = false;

			/* If the current index is less than the amount of panels we have. */
			if(menu.CurrentPanelIndex < (menu.Panels.Count - 1))
			{

				/* Increment to the next panel. */
				menu.CurrentPanelIndex++;

			}
			/* Otherwise, the current index is greater than the amount of panels we have. */
			else
			{

				/* Reset the current panel index. */
				menu.CurrentPanelIndex = 0;

			}

		}
		/* Otherwise, this is the left arrow. */
		else
		{

			/* Used for lerping the rotation - needs work still. */
			/* destinationRotation = new Vector3(0.0f, -90.0f, 0.0f); */

			/* The current menu panel is going out of focus because of the rotation. */
			menu.Panels[menu.CurrentPanelIndex].InFocus = false;

			/* If the current index is greater than the first panel. */
			if(menu.CurrentPanelIndex > 0)
			{

				/* Decrement the current panel index. */
				menu.CurrentPanelIndex--;

			}
			/* Otherwise, the current index is less than the first panel index. */
			else
			{

				/* Reset the current panel index. */
				menu.CurrentPanelIndex = (menu.Panels.Count - 1);

			}

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	This will allow us to define a specific button response.
	*
	*/
	override public void ButtonPressResponse()
	{

		/* Rotate all of the panels. */
		menu.transform.FindChild("Panels").Rotate(rotationValue);

		/* Check what arrow is being pressed. */
		ArrowResponse();

	}

	/*
	*
	*	Overview
	*	--------
	*	This method will provide a way to lerp the rotation of the 3D menu.
	*
	*/
	private void LerpRotation()
	{

		/* If the current rotation value is less than the target angle value. */
		if(currentRotation < targetAngle)
		{

			/* Increment the current rotation value. */
			currentRotation++;

			/* Rotate the menu around. */
			menu.transform.Rotate(rotationSpeed);

		}
		/* Otherwise, the current rotation value is less than the target angle value. */
		else
		{

			/* Reset the current rotation value. */
			currentRotation = 0;

			/* The menu should stop rotating now. */
			shouldRotate = false;

		}

	}

	/* This needs testing out. */
	protected override void VRControls()
	{

		/* Check to see if the left controller is in the tracking area. */
		if (GameObject.Find ("Controller (left)") != null) 
		{

			/* Update the value for the left controller. */
			leftController = GameObject.Find ("Controller (left)").GetComponent<SCR_VRControllerInput>();

			/* If the user presses left OR right on the left controller. */
			if (leftController.LeftPressed() || leftController.RightPressed()) 
			{

				/* Perform a specific button response. */
				ButtonPressResponse();

			}

		}


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

		/* Handles base update method call. */
		base.Update();

		/* This needs testing out. */
		VRControls();

		/* If the menu should rotate. */
		if(shouldRotate)
		{

			/* Rotate the menu. */
			LerpRotation();

		}

	}

}