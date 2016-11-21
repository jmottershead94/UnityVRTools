/* Unity includes here. */
using UnityEngine;
using System.Collections;

/* 3D Button is just a standard game object. */
public class SCR_3DButton : SCR_BaseUIElement 
{

	/* Attributes. */
	protected SCR_SceneEditor sceneEditor = null;

	[Header ("3D Button Transition Properties")]
	[SerializeField]	private float scaleUpFactor = 1.05f;
	[SerializeField]	private float speed = 1.0f;

	private Vector3 originalPosition = Vector3.zero;
	private Vector3 destination = Vector3.zero;
	private Vector3 originalScale = Vector3.zero;
	private Vector3 destinationScale = Vector3.zero;
	private Vector3 originalDistanceDifference = Vector3.zero;
	private bool isInteractable = true;
	protected SCR_Panel parentPanel = null;

	/* Virtual Methods. */
	/* Each inheriting button class must implement a response. */
	protected virtual void ButtonPressResponse(){}
	//protected virtual void ButtonHeldResponse(){}

	/* Methods. */
	protected void Awake()
	{

		/* Initialising our attributes. */
		sceneEditor = GameObject.Find("Scene Editor").GetComponent<SCR_SceneEditor>();
		originalPosition = transform.position;
		originalScale = transform.localScale;
		destination = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z - 0.2f);
		destinationScale = new Vector3(originalScale.x * scaleUpFactor, originalScale.y * scaleUpFactor, originalScale.z * scaleUpFactor);
		parentPanel = transform.parent.GetComponent<SCR_Panel>();

		originalDistanceDifference = Camera.main.transform.position - transform.position;

	}

	/* Would need to use this function with VR input logic. */
	void ButtonInFocus()
	{

		if(!isInFocus)
		{

			isInFocus = true;

		}

	}

	/* Would need to use this function with VR input logic. */
	void ButtonOutOfFocus()
	{

		if(isInFocus)
		{

			isInFocus = false;

		}

	}

	/* VR Controller Equivalent: Aiming at this object and pressing the Right Hand Controller Trigger. */
	void OnMouseDown()
	{

		/* Perform a specific button response. */
		ButtonPressResponse();

	}

	/* VR Controller Equivalent: Aiming at this object with the Right Hand Controller. */
	void OnMouseOver()
	{

		ButtonInFocus();

		//ButtonHeldResponse();

	}

	/* VR Controller Equivalent: Not aiming at this object with the Right Hand Controller. */
	void OnMouseExit()
	{

		ButtonOutOfFocus();

	}

	void VRControls()
	{
		
		/* 

		If the VR hand controller is aiming at this button. 

		isInFocus = true;

		Else, the VR hand controller is not aiming at this button.

		isInFocus = false;

		*/

	}

	override protected void CheckFocus()
	{

		

		if(!isInFocus)
		{
			
			transform.position = Vector3.Lerp(transform.position, originalPosition, speed);
			transform.localScale = Vector3.Lerp(transform.localScale, originalScale, speed);

		}
		else
		{

			transform.position = Vector3.Lerp(transform.position, destination, speed);
			transform.localScale = Vector3.Lerp(transform.localScale, destinationScale, speed);
			
		}

	}

	void UpdateUIPositions()
	{

		originalPosition = Camera.main.transform.position - originalDistanceDifference;
		destination = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z - 0.2f);

	}

	protected void Update()
	{

		/* VR Control Logic Here? */
		/* VRControls(); */

		UpdateUIPositions();

		/* If this button is interactable. */
		if(isInteractable)
		{

			/* Keep checking the focus of this object. */
			CheckFocus();

		}

	}

	/* Getters/Setters. */
	/* This will allow us to get/set the current interactive state of this button. */
	public bool IsInteractable
	{
		get { return isInteractable; }
		set { isInteractable = value; }
	}

}