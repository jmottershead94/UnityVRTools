// Unity includes here.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Scene editor IS A game object, therefore inherits from it.
public class SCR_SceneEditor : MonoBehaviour 
{

	// Attributes.
	[SerializeField]	private List<SCR_PersistentObject> sceneObjects = null;		// The current list of scene objects.

	[Header ("Object Manipulation")]
	[SerializeField]	private Vector3 translationSpeed = Vector3.zero;
	[SerializeField]	private Vector3 scaleIncrement = Vector3.zero;

	private SCR_DetailedWindow detailedWindow = null;
	private enum TransformState
	{
		translation,
		rotation,
		scale
	};
	private TransformState currentTransformState = TransformState.translation;

	// Methods.
	void Awake()
	{

		// Initialising our attributes.
		sceneObjects = new List<SCR_PersistentObject>();
		detailedWindow = GameObject.Find("PRE_DetailedWindow").GetComponent<SCR_DetailedWindow>();

	}

	/*
	*
	*	Overview
	*	--------
	*	This function will spawn in any primitive object we want at a 
	*	specified spawn location.
	*
	*	Params
	*	------
	*	primtiveType - 	This is the current primitive type of the object
	*					that the user wishes to spawn (cube, sphere etc).
	*
	*	spawnPosition - This is the current spawn position of the object
	*					that the user wishes to spawn.
	*
	*/
	public void SpawnObject(PrimitiveType primitiveType, Vector3 spawnPosition)
	{

		// Creating a temporary instance of a cube game object.
        GameObject tempGameObject = GameObject.CreatePrimitive(primitiveType);

        // Creating a temporary instance of a persistent object and adding it onto the temporary game object.
		SCR_PersistentObject tempPersistentObject = tempGameObject.AddComponent<SCR_PersistentObject>();
		tempPersistentObject.transform.position = spawnPosition;
		tempPersistentObject.transform.parent = GameObject.Find(name).transform;
		tempPersistentObject.ObjectType = primitiveType;
		tempPersistentObject.ID = sceneObjects.Count;

        // Add the cube into the list of game objects.
        sceneObjects.Add(tempPersistentObject);

	}

	void CheckRotation(SCR_PersistentObject persistentObject)
	{

		

	}

	void CheckScale(SCR_PersistentObject persistentObject)
	{

		Vector3 scale = persistentObject.transform.localScale;

		if(Input.GetKey(KeyCode.I))
		{

			scale.Set(scale.x, scale.y, scale.z + scaleIncrement.z);

		}

		if(Input.GetKey(KeyCode.J))
		{

			scale.Set(scale.x - scaleIncrement.x, scale.y, scale.z);

		}

		if(Input.GetKey(KeyCode.K))
		{

			scale.Set(scale.x, scale.y, scale.z - scaleIncrement.z);

		}

		if(Input.GetKey(KeyCode.L))
		{

			scale.Set(scale.x + scaleIncrement.x, scale.y, scale.z);

		}

		persistentObject.transform.localScale = scale;

	}

	void CheckTranslations(SCR_PersistentObject persistentObject)
	{

		if(Input.GetKey(KeyCode.I))
		{

			persistentObject.transform.Translate(0.0f, 0.0f, translationSpeed.z);

		}

		if(Input.GetKey(KeyCode.J))
		{

			persistentObject.transform.Translate(-translationSpeed.x, 0.0f, 0.0f);

		}

		if(Input.GetKey(KeyCode.K))
		{

			persistentObject.transform.Translate(0.0f, 0.0f, -translationSpeed.z);

		}

		if(Input.GetKey(KeyCode.L))
		{

			persistentObject.transform.Translate(translationSpeed.x, 0.0f, 0.0f);

		}

		/* Down. */
		if(Input.GetKey(KeyCode.U))
		{

			persistentObject.transform.Translate(0.0f, -translationSpeed.y, 0.0f);

		}

		if(Input.GetKey(KeyCode.O))
		{

			persistentObject.transform.Translate(0.0f, translationSpeed.y, 0.0f);

		}

	}

	void ManipulateObject(SCR_PersistentObject persistentObject)
	{

		if(currentTransformState == TransformState.translation)
		{
			CheckTranslations(persistentObject);
		}
		else if(currentTransformState == TransformState.rotation)
		{
			CheckRotation(persistentObject);	
		}
		else if(currentTransformState == TransformState.scale)
		{
			CheckScale(persistentObject);
		}


	}

	void CheckForHighlightedObjects()
	{

		/* Loop through each of the scene objects.*/
		foreach(SCR_PersistentObject tempPersistentObject in sceneObjects)
		{

			/* If the current scene object is in focus (i.e. the player is aiming at this object). */
			if(tempPersistentObject.InFocus)
			{

				/* Display some transform information in the detailed window. */
				ManipulateObject(tempPersistentObject);

			}

		}

	}

	void CheckCurrentTransformState()
	{

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(currentTransformState != TransformState.translation)
			{
				print("Translating.");
				currentTransformState = TransformState.translation;
			}
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			if(currentTransformState != TransformState.rotation)
			{
				print("Rotating.");
				currentTransformState = TransformState.rotation;
			}
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			if(currentTransformState != TransformState.scale)
			{
				print("Scaling.");
				currentTransformState = TransformState.scale;
			}
		}

	}

	void Update()
	{

		CheckCurrentTransformState();

		CheckForHighlightedObjects();

	}

	// Getters.
	// This will allow us to get/set the current list of persistent objects in the scene.
	public List<SCR_PersistentObject> Objects
	{
		get { return sceneObjects; }
		set { sceneObjects = value; }
	}

}