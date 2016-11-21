using UnityEngine;
using System.Collections;

public class SCR_BaseUIElement : MonoBehaviour 
{

	/* Attributes. */
	protected bool isInFocus = false;

	/* Methods. */
	public bool InFocus
	{
		get { return isInFocus; }
		set { isInFocus = value; }
	}

}
