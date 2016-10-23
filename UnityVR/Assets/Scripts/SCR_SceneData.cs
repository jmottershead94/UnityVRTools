// Unity includes here.
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

// Scene data IS A game object, therefore inherits from it.
public class SCR_SceneData : MonoBehaviour 
{

	// Attributes.
	private DateTime now = DateTime.Now;						// What date/time it is now.
	private string format = "dd mm yyyy  hh:mm";				// The format to be used for the data.
	private string loadDate;									// The current load date of the data.
	private string saveDate;									// The current save date of the data.
	private List<SCR_PersistentObject> savedPersistentObjects;	// A list of the persistent objects in the scene.	

	private static SCR_SceneData sceneDataInstance;				// The current instance of scene data for a singleton design and to allow access between scenes.

	// Methods.
	void Awake()
	{

		// If we don't current have a singleton instance of the scene data class.
		if(sceneDataInstance == null)
		{

			// Don't destroy this game object when we load in.
			DontDestroyOnLoad(gameObject);

			// Initialise the scene data instance to this.
			sceneDataInstance = this;

		}
		// Otherwise, we already have a singleton instance of the scene data class.
		else
		{

			// Destroy this game object.
			Destroy(gameObject);

		}

	}

	public void Save()
	{

		// Record the save date.
		saveDate = (now.ToString(format));

		// Initialising our local attributes.
		BinaryFormatter binary = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/SaveGame.dat");

		//data.transform = 

		//binary.Serialize(file, data);

	}

	public void Load()
	{

		

	}

	// Getters.
	// This will return the current static instance of our scene data class.
	public static SCR_SceneData Instance
	{
		get { return sceneDataInstance; }
	}

	// This will return the current list of persistent objects in the scene.
	public List<SCR_PersistentObject> SceneObjects
	{
		get { return savedPersistentObjects; }
	}

}
