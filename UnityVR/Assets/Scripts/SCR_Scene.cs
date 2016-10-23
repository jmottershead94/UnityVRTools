// Unity includes here.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Scene IS A game object, therefore inherits from it.
public class SCR_Scene : MonoBehaviour 
{

	// Attributes.
	[SerializeField]	private List<GameObject> sceneObjects;		// The current list of scene objects.

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
		sceneObjects = new List<GameObject>();

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
	void SpawnObject(PrimitiveType primitiveType, Vector3 spawnPosition)
	{

		// Creating a temporary instance of a cube game object.
        GameObject tempCube = GameObject.CreatePrimitive(primitiveType);
        tempCube.transform.position = spawnPosition;
        tempCube.transform.parent = GameObject.Find(name).transform;

        // Creating a temporary instance of a persistent object and adding it onto the temporary game object.
		SCR_PersistentObject tempPersistentObject = tempCube.AddComponent<SCR_PersistentObject>();
		tempPersistentObject.ObjectType = primitiveType;
		tempPersistentObject.ID = sceneObjects.Count;

        // Add the cube into the list of game objects.
        sceneObjects.Add(tempCube);

	}

	/*
	*
	*	Overview
	*	--------
	*	This will be called for drawing UI elements.
	*
	*/
	void OnGUI() 
	{

		// If the user presses on the cube button.
        if (GUI.Button(new Rect(10, 10, 80, 50), "CUBE"))
        {

            // Spawn a cube.
        	SpawnObject(PrimitiveType.Cube, new Vector3(2.0f, 0.0f, 0.0f));

        }

        // If the user presses on the sphere button.
        if (GUI.Button(new Rect(10, 70, 80, 50), "SPHERE"))
        {

			// Spawn a sphere.
			SpawnObject(PrimitiveType.Sphere, new Vector3(-2.0f, 0.0f, 0.0f));

        }
        
    }

	/*
	*
	*	Overview
	*	--------
	*	This will be used to update every frame.
	*
	*/
	void Update () 
	{

		

	}

}