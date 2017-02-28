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

	public static Vector3 PositionInRelationToCam(Vector3 position)
	{
		Vector3 result = Vector3.zero;
		result = Camera.main.transform.InverseTransformDirection(position - Camera.main.transform.position);
		return result;
	}

	public static void MoveInRelationToCam(Transform goTransform, Vector3 translation, bool invertHeadTilt)
	{
		Vector3 axis = translation;

		if (invertHeadTilt)
			axis = Camera.main.transform.TransformDirection (axis);
		else 
		{
			Vector3 invertedHeadTilt = translation;
			invertedHeadTilt = Camera.main.transform.TransformDirection (invertedHeadTilt);
			axis = new Vector3 (-invertedHeadTilt.x, -invertedHeadTilt.y, invertedHeadTilt.z);
		}
		
		goTransform.position += axis;
	}

	public static void RotateInRelationToCam(Transform goTransform, Vector3 rotation)
	{
		Vector3 rot = rotation;
		goTransform.RotateAround(goTransform.position, Camera.main.transform.right, rot.x);
		goTransform.RotateAround(goTransform.position, Camera.main.transform.up, rot.y);
		goTransform.RotateAround(goTransform.position, Camera.main.transform.forward, rot.z);
	}

//	public static void ScaleInRelationToCam(Transform goTransform, Vector3 scale)
//	{
//		Vector3 axis = scale;
//		axis = Camera.main.transform.TransformVector(axis);
//		goTransform.localScale = axis;
//	}

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
		movement.x = Input.GetAxis("Horizontal") * speed.x;
		movement.y = Input.GetAxis("Vertical") * speed.y;
		movement.z = Input.GetAxis("Depth") * speed.z;

		MoveInRelationToCam(transform, movement, true);
		Rotation();

		if(Input.GetMouseButtonDown(1))
			transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
	}

	private void VRControls()
	{

		if(GameObject.Find("Controller (right)") == null)
			return;

		rightController = GameObject.Find ("Controller (right)").GetComponent<SCR_VRControllerInput> ();

		// This needs testing out!!
		movement = Vector3.zero;

		if(rightController.UpPressed())
			movement.z += speed.z;

		if(rightController.DownPressed())
			movement.z += (speed.z * -1.0f);

		if(rightController.LeftPressed())
			movement.x += (speed.x * -1.0f);

		if(rightController.RightPressed())
			movement.x += speed.x;

		MoveInRelationToCam(transform, movement, true);
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
		PCControls();
		VRControls();
	}
}