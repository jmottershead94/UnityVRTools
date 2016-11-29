using UnityEngine;
using System.Collections;

public class SCR_SaveButton : SCR_3DButton 
{

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
		
		/* Sets the new transform state. */
		SCR_SceneData.Instance.Save();

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

		base.Update();

		//transform.LookAt(Camera.main.transform.position);

	}

}