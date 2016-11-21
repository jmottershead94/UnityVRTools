using UnityEngine;
using System.Collections;

public class SCR_SpawningObjectButton : SCR_3DButton 
{

	/* Attributes. */
	[Header ("Spawning Object Properties")]
	[SerializeField]	private PrimitiveType primitiveType = PrimitiveType.Sphere;

	/* Methods. */
	/* This will allow us to define a specific button response. */
	override protected void ButtonPressResponse()
	{
		
		sceneEditor.SpawnObject(primitiveType, Vector3.zero);
		
	}

	new void Update()
	{

		if(parentPanel.InFocus)
		{
			base.Update();
		}

	}

}