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
	[SerializeField]	private float rayDistance = 2.0f;
	[SerializeField]	private bool shouldUseRays = false;
	private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;	/* Used to test if the trigger button has been pressed. */
	private SteamVR_TrackedObject trackedObject = null;							/* Stores the current tracked object. */
	private SteamVR_Controller.Device device = null;							/* Stores the current hand controller. */
	private RaycastHit raycastTarget;
	private Ray ray;
	private LineRenderer lineRenderer = null;									

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
		lineRenderer = GetComponent<LineRenderer> ();

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
		//device.GetAxis ().x < 0.5f;

	}

	private void ShootRays()
	{

		/* Initialising local attributes. */
		Vector3 tempDirection = transform.TransformDirection(Vector3.forward);

		ray = Camera.main.ScreenPointToRay(transform.position);
		ray.origin = transform.position;
		ray.direction = tempDirection;
		Vector3 point = ray.origin + (tempDirection * rayDistance);

		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, point);

		if (Physics.Raycast (ray, out raycastTarget, rayDistance)) 
		{

			if (raycastTarget.transform.GetComponent<SCR_BaseUIElement> () != null) 
			{

				SCR_BaseUIElement tempBaseUIElement = raycastTarget.transform.GetComponent<SCR_BaseUIElement> ();

				if (device != null) 
				{
					
					//device.TriggerHapticPulse (500);

				}

				/* This works for pressing on buttons, but hopefully my solution in the SCR_3DButton class will sort this out. */
//				if (tempBaseUIElement.GetComponent<SCR_3DButton> () != null) 
//				{
//
//					SCR_3DButton button = tempBaseUIElement.GetComponent<SCR_3DButton> ();
//
//					if (TriggerPressed ()) 
//					{
//
//						button.ButtonPressResponse ();
//
//					}
//
//				}

			}

		}

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

		if (shouldUseRays) 
		{
			
			ShootRays ();

		}

	}

	public bool TriggerPressed()
	{

		if (device != null) 
		{
			
			return device.GetPressDown(triggerButton);

		}

		return false;

	}

	public bool UpPressed()
	{

		if (device != null) 
		{

			return (device.GetAxis().y > 0.25f);

		}

		return false;

	}

	public bool RightPressed()
	{

		if (device != null) 
		{
			
			return (device.GetAxis().x > 0.25f);

		}

		return false;

	}

	public bool LeftPressed()
	{

		if (device != null) 
		{

			return (device.GetAxis().x < -0.25f);

		}

		return false;

	}

	public bool DownPressed()
	{

		if (device != null) 
		{

			return (device.GetAxis().y < -0.25f);

		}

		return false;

	}

	/* Getters. */
	public bool IsAimingAtSomething
	{
		get { return (Physics.Raycast (ray, out raycastTarget, rayDistance)); }
	}

	public RaycastHit Target
	{
		get { return raycastTarget; }
	}

}