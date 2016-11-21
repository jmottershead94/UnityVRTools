// Unity includes here.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Scene editor IS A game object, therefore inherits from it.
public class SCR_SceneEditor : MonoBehaviour 
{

	// Attributes.
	[SerializeField]	private List<SCR_PersistentObject> sceneObjects;		// The current list of scene objects.

	// Methods.
	/*
	*
	*	Overview
	*	--------
	*	This will be called for initialisation.
	*
	*/
	void Start () 
	{

		// Initialising our attributes.
		sceneObjects = new List<SCR_PersistentObject>();

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

	// Getters.
	// This will allow us to get/set the current list of persistent objects in the scene.
	public List<SCR_PersistentObject> Objects
	{
		get { return sceneObjects; }
		set { sceneObjects = value; }
	}

}