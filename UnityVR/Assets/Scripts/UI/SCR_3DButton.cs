using UnityEngine;
using System.Collections;

public class SCR_3DButton : MonoBehaviour 
{

	/* Attributes. */
	/*[SerializeField]*/	private Vector3 originalPosition = Vector3.zero;
	/*[SerializeField]*/	private Vector3 destination = Vector3.zero;
	/*[SerializeField]*/	private Vector3 originalScale = Vector3.zero;
	/*[SerializeField]*/	private Vector3 destinationScale = Vector3.zero;
	[SerializeField]	private float scaleUpFactor = 1.05f;
	[SerializeField]	private float speed = 1.0f;
	private bool isInFocus = false;

	/* Methods. */
	void Awake()
	{

		/* Initialising our attributes. */
		originalPosition = transform.position;
		originalScale = transform.localScale;

		destination = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z - 0.2f);
		destinationScale = new Vector3(originalScale.x * scaleUpFactor, originalScale.y * scaleUpFactor, originalScale.z * scaleUpFactor);

	}

	/* Would need to use this functions with VR input logic. */
	void ButtonInFocus()
	{

		if(!isInFocus)
		{
			isInFocus = true;
		}

	}

	/* Would need to use this functions with VR input logic. */
	void ButtonOutOfFocus()
	{

		if(isInFocus)
		{

			isInFocus = false;

		}

	}

	void OnMouseOver()
	{

		ButtonInFocus();

	}

	void OnMouseExit()
	{

		ButtonOutOfFocus();

	}

	void VRControls()
	{}

	void CheckFocus()
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

	void Update()
	{

		/* VR Control Logic Here? */
		/* VRControls(); */

		CheckFocus();

	}

	public bool InFocus
	{
		get { return isInFocus; }
	}

}