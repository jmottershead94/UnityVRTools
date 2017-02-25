/*
*
*	Camera Class
*	============
*
*	Created: 	2016/11/21
*	Filter:		Scripts
*	Class Name: SCR_Rotate
*	Base Class: Monobehaviour
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	Camera will provide debugging/testing capabilities whilst not having 
*				access to the VR equipment at university. This will allow the user to 
*				use standard keyboard controls to navigate the scene.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;

/* Look into the SteamVR class for VR character movement. */
/* Camera IS A game object, therefore inherits from it. */
public class SCR_Camera : MonoBehaviour
{

	/* Attributes. */
	[SerializeField]	private Vector3 speed = Vector3.zero;	/* This will store how fast the camera will move. */
	[SerializeField]	private Vector3 rotationSpeed = Vector3.zero;
	[SerializeField]	private bool clampVertical = true;
	private SCR_VRControllerInput leftController = null;		/* Provides access to the left hand controller (and will allow access to input on this controller). */
	private SCR_VRControllerInput rightController = null;		/* Provides access to the right hand controller (and will allow access to input on this controller). */
	private Vector3 movement = Vector3.zero;
	private float rotationX = 0.0f;

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
		if(GameObject.Find("Controller (left)") != null)
		{
			leftController = GameObject.Find("Controller (left)").GetComponent<SCR_VRControllerInput>();
		}

		if(GameObject.Find("Controller (right)") != null)
		{
			rightController = GameObject.Find("Controller (right)").GetComponent<SCR_VRControllerInput>();
		}
	}

	private void Movement()
	{
		movement.x = Input.GetAxis("Horizontal") * speed.x;
		movement.y = Input.GetAxis("Vertical") * speed.y;
		movement.z = Input.GetAxis("Depth") * speed.z;

		Vector3 axis = movement;
		axis = transform.TransformDirection(axis);
		transform.Translate(axis);
	}

	private void Rotation()
	{
		Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

		if(mouseMovement.x > 0.25f || mouseMovement.x < -0.25f)
		{
			Vector3 rotate = new Vector3(0.0f, mouseMovement.x * rotationSpeed.y, 0.0f);
			transform.Rotate(rotate);
		}

		if(clampVertical)
		{
			rotationX += (mouseMovement.y * -rotationSpeed.x);
			rotationX = Mathf.Clamp(rotationX, -40.0f, 40.0f);
			transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
		}
	}

	/*
	*
	*	Overview
	*	--------
	*	This method will check user input to provide camera movement.
	*
	*/
	private void PCControls()
	{
		Movement();
		Rotation();
	}

	private void VRControls()
	{

		/* Need to test this for y axis movement. */
		/*
		if(GameObject.Find ("Camera (eye)") != null)
		{

			transform.forward = GameObject.Find ("Camera (eye)").transform.forward;

		}
		*/

		if(GameObject.Find("Controller (right)") == null)
			return;

		rightController = GameObject.Find ("Controller (right)").GetComponent<SCR_VRControllerInput> ();

		/* VR Equivalent: If the user pushes up on the left touch pad? */
		if(rightController.UpPressed())
		{

			/* Move the camera. */
			//transform.Translate(0.0f, 0.0f, speed.z);
			transform.position += new Vector3 (0.0f, 0.0f, speed.z);

		}

		/* VR Equivalent: If the user pushes left on the left touch pad? */
		if(rightController.LeftPressed())
		{

			/* Move the camera. */
			//transform.Translate(speed.x * -1.0f, 0.0f, 0.0f);
			transform.position += new Vector3 (speed.x * -1.0f, 0.0f, 0.0f);

		}

		/* VR Equivalent: If the user pushes down on the left touch pad? */
		if(rightController.DownPressed())
		{

			/* Move the camera. */
			//transform.Translate(0.0f, 0.0f, speed.z * -1.0f);
			transform.position += new Vector3 (0.0f, 0.0f, speed.z * -1.0f);

		}

		/* VR Equivalent: If the user pushes right on the left touch pad? */
		if(rightController.RightPressed())
		{

			/* Move the camera. */
			//transform.Translate(speed.x, 0.0f, 0.0f);
			transform.position += new Vector3 (speed.x, 0.0f, 0.0f);

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

		/*

			if(inUniversity)

				VRControls();

			else

				PCControls();

		*/

		/* Handles user input for camera control. */
		PCControls();

		/* Comment this in for University. */
		VRControls();

	}

}