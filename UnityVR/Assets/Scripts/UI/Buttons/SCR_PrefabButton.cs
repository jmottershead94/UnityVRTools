using UnityEngine;
using System.Collections;

public class SCR_PrefabButton : SCR_3DButton 
{

	/* Attributes. */
	private SCR_PrefabsPanel prefabPanel = null;
	private bool visible = true;

	/* Methods. */
	new protected void Start()
	{
		base.Start();
		prefabPanel = transform.parent.parent.GetComponent<SCR_PrefabsPanel>();
		tag = "DontDestroy";
		Enable ();
	}

	public void Enable()
	{
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<BoxCollider>().enabled = true;
		IsInteractable = true;
		visible = true;

		Debug.Log ("Enabled " + name);
	}

	public void Disable()
	{
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<BoxCollider>().enabled = false;
		IsInteractable = false;
		visible = false;

		Debug.Log ("Disabled " + name);
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
		Debug.Log ("Clicked on...");

		if (prefabPanel.Prefabs.Count < 0)
			return;
		
		Debug.Log ("Prefabs are available...");

		/* Looping through each of our prefabs. */
		foreach(GameObject tempPrefab in prefabPanel.Prefabs)
		{
			/* If the name of the prefab is equal to the name of the button. */
			if(tempPrefab.name == name)
			{
				Debug.Log ("Found prefab " + tempPrefab.name);

				if(tempPrefab != null)
				{
					Debug.Log ("Spawning prefab.");

					GameObject tempGameObject = Instantiate(tempPrefab, spawner.position, Quaternion.identity) as GameObject;
					tempGameObject.name = tempPrefab.name;
					tempGameObject.transform.SetParent(sceneEditor.transform.FindChild("GO"));

					ObjectIdentifier tempObjectIdentifier = tempGameObject.AddComponent<ObjectIdentifier>();
					tempObjectIdentifier.name = tempPrefab.name;
					tempObjectIdentifier.prefabName = tempPrefab.name;
					//tempObjectIdentifier.idParent;
					tempObjectIdentifier.dontSave = false;

					Debug.Log ("Should have spawned?");
				}
			}
		}
	}

	public bool Visible
	{
		get { return visible; }
		set { visible = value; }
	}
}