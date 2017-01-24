using UnityEngine;
using System.Collections;

public class SCR_ColourButton : SCR_3DButton 
{

	/* Attributes. */
	[SerializeField]	private Color colour = Color.black;

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

		for(int i = 0; i < sceneEditor.Objects.Count; i++)
		{

			if(sceneEditor.Objects[i].InFocus)
			{
				
				sceneEditor.Objects[i].gameObject.GetComponent<Renderer>().material.color = colour;
				sceneEditor.Objects[i].DefaultMaterial.color = colour;

			}

		}

	}
}