using UnityEngine;
using System.Collections;

public class SCR_BaseUIElement : MonoBehaviour 
{

	/* Attributes. */
	protected bool isInFocus = false;

	/* Virtual Methods. */
	/* All UI elements must check focus. */
	protected virtual void CheckFocus(){}

	/* Methods. */
	public bool InFocus
	{
		get { return isInFocus; }
		set { isInFocus = value; }
	}

}
