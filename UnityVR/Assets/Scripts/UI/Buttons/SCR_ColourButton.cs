using UnityEngine;
using System.Collections;

public class SCR_ColourButton : SCR_3DButton 
{

	/* Attributes. */
	[SerializeField]	private Color colour = Color.black;

	/* Methods. */
	new void Awake()
	{
		base.Awake();
	}

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

//	new void Update()
//	{
//		base.Update();
//
//		if(InFocus)
//		{
//
//			for(int i = 0; i < sceneEditor.Objects.Count; i++)
//			{
//
//				if(sceneEditor.Objects[i].InFocus)
//				{
//					
//					sceneEditor.Objects[i].ChangingMaterial.color = colour;
//					sceneEditor.Objects[i].gameObject.GetComponent<Renderer>().material = sceneEditor.Objects[i].ChangingMaterial;
//
//				}
//
//			}
//
//		}
//		else
//		{
//
//			for(int i = 0; i < sceneEditor.Objects.Count; i++)
//			{
//
//				if(sceneEditor.Objects[i].InFocus)
//				{
//
//					if(sceneEditor.Objects[i].CurrentMaterial != sceneEditor.Objects[i].HighLightedMaterial)
//					{
//						sceneEditor.Objects[i].gameObject.GetComponent<Renderer>().material = sceneEditor.Objects[i].HighLightedMaterial;
//						sceneEditor.Objects[i].gameObject.GetComponent<Renderer>().material.color = Color.cyan;
//					}
//
//				}
//
//			}
//
//		}
//	}
}