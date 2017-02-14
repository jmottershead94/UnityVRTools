using UnityEngine;
using System.Collections;

public class SCR_PrefabButton : SCR_3DButton 
{

	/* Attributes. */
	private SCR_PrefabsPanel prefabPanel = null;


	/* Methods. */
	new protected void Start()
	{

		base.Start();
		prefabPanel = parentPanel.GetComponent<SCR_PrefabsPanel>();

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

		/* Looping through each of our prefabs. */
		foreach(GameObject tempPrefab in prefabPanel.Prefabs)
		{
			/* If the name of the prefab is equal to the name of the button. */
			if(tempPrefab.name == name)
			{
				if(tempPrefab != null)
				{
					GameObject tempGameObject = Instantiate(tempPrefab, spawner.position, Quaternion.identity) as GameObject;
					tempGameObject.transform.SetParent(sceneEditor.transform);
				}
			}
		}

	}
	
}