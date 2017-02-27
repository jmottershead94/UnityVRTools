using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SCR_LoadButton : SCR_3DButton 
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
		
		SCR_SceneData.Instance.Load(SceneManager.GetActiveScene().name);
		
	}

}