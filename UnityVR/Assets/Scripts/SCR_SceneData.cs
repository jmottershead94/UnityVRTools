/*
*
*	Scene Data Class
*	================
*
*	Created: 	2016/11/21
*	Filter:		Scripts
*	Class Name: SCR_SceneData
*	Base Class: Monobehaviour
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	The purpose of this class is to store data about the objects in the current scene
*				and also provide saving/loading functionality for VR scene editing.
*
*/

/* Unity includes here.*/
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

/* Scene data IS A game object, therefore inherits from it. */
public class SCR_SceneData : MonoBehaviour 
{

	/* Attributes. */
	private string sceneDataFilename = "";				/* The file name of the scene that we are saving. */
	private string format = "dd mm yyyy  hh:mm";							/* The format to be used for the data. */
	private string loadDate;												/* The current load date of the data. */
	private string saveDate;												/* The current save date of the data. */
	private DateTime now = DateTime.Now;									/* What date/time it is now. */
	private SCR_SceneEditor scene;											/* Accessing the current scene. */
	private static SCR_SceneData sceneDataInstance;							/* The current instance of scene data for a singleton design and to allow access between scenes. */
	private string filePath = "";
	private SaveLoadMenu sceneDataControls = null;
	private string sceneName = "";

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This will be called before initialisation.
	*
	*/
	private void Awake()
	{

		/* If there is a scene editor game object in the current scene. */
		if(GameObject.Find("Scene Editor") != null)
		{

			/* Find the scene editor script in the current scene and assign our local attribute. */
			scene = GameObject.Find("Scene Editor").GetComponent<SCR_SceneEditor>();

		}

		/* If we don't current have a singleton instance of the scene data class. */
		if(sceneDataInstance == null)
		{

			/* Don't destroy this game object when we load in. */
			DontDestroyOnLoad(gameObject);

			/* Initialise the scene data instance to this. */
			sceneDataInstance = this;

		}
		/* Otherwise, we already have a singleton instance of the scene data class. */
		else
		{

			/* Destroy this game object. */
			Destroy(gameObject);

		}

		sceneName = SceneManager.GetActiveScene().name;
		sceneDataFilename = "/" + sceneName + ".dat";
		filePath = Application.dataPath + sceneDataFilename;
		sceneDataControls = GetComponent<SaveLoadMenu>();

	}

	/*
	*
	*	Overview
	*	--------
	*	Here we will save the current persistent data values into our
	*	persistent object data class.
	*
	*	Having this as a separate function allows me to directly decide
	*	what data I want to save easier, and makes the code more readible
	*	in my opinion.
	*
	*	Params
	*	------
	*	ref PersistentObjectData savingPersistentObject -	This will allow us to pass a reference
	*								 						through to the function and directly set
	*								 						the saving values.	
	*
	*	SCR_PersistentObject referencePersistentObject 	-	With this parameter we are using a reference
	*														persistent object to base our saving values
	*														off.
	*
	*/
	private void SavePersistentObjectData(ref PersistentObjectData savingPersistentObject, SCR_PersistentObject referencePersistentObject)
	{

		/* Saving the current position of the persistent object. */
		savingPersistentObject.PositionX = referencePersistentObject.transform.position.x;
		savingPersistentObject.PositionY = referencePersistentObject.transform.position.y;
		savingPersistentObject.PositionZ = referencePersistentObject.transform.position.z;

		/* Saving the current rotation of the persistent object. */
		savingPersistentObject.RotationX = referencePersistentObject.transform.eulerAngles.x;
		savingPersistentObject.RotationY = referencePersistentObject.transform.eulerAngles.y;
		savingPersistentObject.RotationZ = referencePersistentObject.transform.eulerAngles.z;

		/* Saving the current scale of the persistent object. */
		savingPersistentObject.ScaleX = referencePersistentObject.transform.localScale.x;
		savingPersistentObject.ScaleY = referencePersistentObject.transform.localScale.y;
		savingPersistentObject.ScaleZ = referencePersistentObject.transform.localScale.z;

		/* Saving the current persistent object data (Primitive Type and Object ID). */
		savingPersistentObject.ObjectType 	= referencePersistentObject.ObjectType;
		savingPersistentObject.ID 			= referencePersistentObject.ID;
		savingPersistentObject.Red			= referencePersistentObject.DefaultMaterial.color.r;
		savingPersistentObject.Green		= referencePersistentObject.DefaultMaterial.color.g;
		savingPersistentObject.Blue			= referencePersistentObject.DefaultMaterial.color.b;
		savingPersistentObject.Name			= referencePersistentObject.name;

	}

	private void AddObjectsToScene(Scene newScene)
	{

		/* Looping through the amount of objects there are in the current scene. */
		for(int i = 0; i < SCR_SceneEditor.Objects.Count; i++)
		{

			/* Set the game objects from the current scene to the new scene. */
			newScene.GetRootGameObjects().SetValue(SCR_SceneEditor.Objects[i], i);

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	Here we will save the current scene to a text file.
	*
	*/
	public void Save()
	{

		/* Record the save date. */
		saveDate = (now.ToString(format));

		/* Initialising local attributes. */
		BinaryFormatter tempBinary = new BinaryFormatter();
		List<PersistentObjectData> tempSceneObjectData = new List<PersistentObjectData>();

		/* Creating a file to save the local scene data. */
		FileStream tempFile = File.Create(filePath);
		
		/* Looping through the amount of objects there are in the current scene. */
		for(int i = 0; i < SCR_SceneEditor.Objects.Count; i++)
		{

			/* Initialising a temporary instance of the persistent object data. */
			PersistentObjectData tempObjectData = new PersistentObjectData();

			/* Saving the current persistent object data from the scene. */
			SavePersistentObjectData(ref tempObjectData, SCR_SceneEditor.Objects[i]);

			/* Adding the object data into our persistent object data list. */
			tempSceneObjectData.Add(tempObjectData);

		}

		/* Serializing the list of scene objects to a text file. */
		tempBinary.Serialize(tempFile, tempSceneObjectData);

		/* Close the text file. */
		tempFile.Close();

		sceneDataControls.SaveGame(sceneName);

	}

	private void LoadMaterials(ref SCR_PersistentObject loadingPersistentObject, Color colour)
	{

		Renderer renderer = loadingPersistentObject.GetComponent<Renderer>();
		Material tempMaterial = new Material(renderer.sharedMaterial);
		tempMaterial.color = colour;

		if(loadingPersistentObject.CurrentMaterial != null)
		{

			loadingPersistentObject.CurrentMaterial.color = colour;

		}
		else
		{
			
			renderer.sharedMaterial = tempMaterial;
			loadingPersistentObject.CurrentMaterial = tempMaterial;
			loadingPersistentObject.CurrentMaterial.color = tempMaterial.color;

		}

		if(loadingPersistentObject.DefaultMaterial != null)
		{

			loadingPersistentObject.DefaultMaterial.color = colour;

		}
		else
		{

			loadingPersistentObject.DefaultMaterial = tempMaterial;
			loadingPersistentObject.DefaultMaterial.color = tempMaterial.color;

		}

	}

	/*
	*
	*	Overview
	*	--------
	*	Here we will load the current persistent data values into our
	*	persistent object class.
	*
	*	Having this as a separate function allows me to directly decide
	*	what data I want to load easier, and makes the code more readible
	*	in my opinion.
	*
	*	Params
	*	------
	*	ref SCR_PersistentObject loadingPersistentObject 	-	This is the persistent object that we are using
	*															to load the text file values into.	
	*
	*	PersistentObjectData referencePersistentObject		-	With this parameter we are using a reference
	*															persistent data object to base our loading values
	*															off.
	*
	*/
	private void LoadPersistentObjectData(ref SCR_PersistentObject loadingPersistentObject, PersistentObjectData referencePersistentObject)
	{

		/* Loading the transform of the persistent object using the float values stored in the text file. */
		/* Because Vector3s are not serializable by Unity, currently, we have to store each float value into it's own function. */
		loadingPersistentObject.transform.position 		= new Vector3(referencePersistentObject.PositionX, referencePersistentObject.PositionY, referencePersistentObject.PositionZ);
		loadingPersistentObject.transform.eulerAngles 	= new Vector3(referencePersistentObject.RotationX, referencePersistentObject.RotationY, referencePersistentObject.RotationZ);
		loadingPersistentObject.transform.localScale 	= new Vector3(referencePersistentObject.ScaleX, referencePersistentObject.ScaleY, referencePersistentObject.ScaleZ);
		loadingPersistentObject.transform.parent 		= scene.transform;

		/* Loading the current persistent object data (Primitive Type and Object ID). */
		loadingPersistentObject.ObjectType 				= referencePersistentObject.ObjectType;
		loadingPersistentObject.ID 						= referencePersistentObject.ID;

		if(loadingPersistentObject.tag != "DontDestroy")
			loadingPersistentObject.tag = "DontDestroy";

		Color loadedColour = new Color(referencePersistentObject.Red, referencePersistentObject.Green, referencePersistentObject.Blue);
		LoadMaterials(ref loadingPersistentObject, loadedColour);
	}

	/*
	*
	*	Overview
	*	--------
	*	Here we will load the current scene from a text file.
	*
	*/
	public void Load(string fileName)
	{
		if(sceneDataControls == null)
			sceneDataControls = GetComponent<SaveLoadMenu>();

		sceneDataControls.LoadGame(fileName);

#if UNITY_EDITOR
		filePath = Application.dataPath + "/" + fileName + ".dat";
#else
		if(filePath == "")
		{
			filePath = Application.dataPath + "/" + sceneDataFilename + ".dat";
		}
#endif

		/* Find the scene editor script in the current scene. */
		/* Place this in to show that the scene saves correctly, and to allow the application to load using the editor. */
		scene = GameObject.Find("Scene Editor").GetComponent<SCR_SceneEditor>();

		/* Record the load date. */
		loadDate = (now.ToString(format));

		//filePath = Application.dataPath + "/" + fileName + ".dat";
		//Debug.Log("Loading?????" + filePath);

		/* If the scene data file exists. */
		if(File.Exists(filePath))
		{

			Debug.Log("Found file!");

			/* Initialising local attributes. */
			BinaryFormatter tempBinary = new BinaryFormatter();

			/* Opening the file to the current scene being edited. */
			FileStream tempFile = File.Open(filePath, FileMode.Open);

			/* Deserializing the data from the text file, and casting it to a list data structure of persistent object data. */
			/* Basically gaining access to any scene object data we saved previously. */
			List<PersistentObjectData> tempSceneObjectData = (List<PersistentObjectData>)tempBinary.Deserialize(tempFile);

			/* Close the text file. */
			tempFile.Close();

			if(SCR_SceneEditor.Objects != null)
			{
				/* If we currently have objects in our scene. */
				if(SCR_SceneEditor.Objects.Count > 0)
				{

					/* Loop through each object in the current scene. */
					for(int i = 0; i < SCR_SceneEditor.Objects.Count; i++)
					{

						/* If the application is currently playing. */
						if(Application.isPlaying)
						{

							/* Destroy the current game object. */
							Destroy(SCR_SceneEditor.Objects[i].gameObject);

						}
						/* Otherwise, the application is being used in the editor. */
						else
						{

							/* Destroy the current game object. */
							DestroyImmediate(SCR_SceneEditor.Objects[i].gameObject);

						}

					}

					/* Clear the current scene, because we are about to load and don't want duplicates. */
					SCR_SceneEditor.Objects.Clear();
					
				}
			}

			/* Loop through each of the objects from the text file. */
			for(int i = 0; i < tempSceneObjectData.Count; i++)
			{

				/* Initialising a temporary instance of game object with it's primitive type. */
				GameObject tempGameObject = GameObject.CreatePrimitive(tempSceneObjectData[i].ObjectType);

				/* Initialising a temporary instance of a persistent object and adding it to the temporary instance of the above game object. */
				SCR_PersistentObject tempPersistentObject = tempGameObject.AddComponent<SCR_PersistentObject>();

				/* Load the current persistent object ID from the list based off of the text file data. */
				LoadPersistentObjectData(ref tempPersistentObject, tempSceneObjectData[i]);

				/* Adding in the persistent object into our current scene. */
				SCR_SceneEditor.Objects.Add(tempPersistentObject);

			}

		}

	}

	/* Getters.
	/* This will return the current static instance of our scene data class. */
	public static SCR_SceneData Instance
	{
		get { return sceneDataInstance; }
	}

}

/* This is serializable so that we can store this class information into a text file. */
/* Monobehaviour doesn't allow for some fields to be serialized. */
[Serializable]

/* Persistent object data is just a standard class. */
class PersistentObjectData
{

	/* Attributes. */
	private float positionX, positionY, positionZ;	/* Used for storing the current position. */
	private float rotationX, rotationY, rotationZ;	/* Used for storing the current rotation. */
	private float scaleX, scaleY, scaleZ;			/* Used for storing the current scale. */
	private PrimitiveType primitiveType;			/* Used to store the current primitive type of the object. */
	private int id;									/* Used to store the current ID of the object. */
	private float red, green, blue;					/* Used to store the current colour of the object material. */
	private string name;							

	/* Methods. */
	/* Getters/Setters. */
	/* This will allow us to get/set the current position. */
	public float PositionX
	{
		get { return positionX; }
		set { positionX = value; }
	}

	public float PositionY
	{
		get { return positionY; }
		set { positionY = value; }
	}

	public float PositionZ
	{
		get { return positionZ; }
		set { positionZ = value; }
	}

	/* This will allow us to get/set the current rotation. */
	public float RotationX
	{
		get { return rotationX; }
		set { rotationX = value; }
	}

	public float RotationY
	{
		get { return rotationY; }
		set { rotationY = value; }
	}

	public float RotationZ
	{
		get { return rotationZ; }
		set { rotationZ = value; }
	}

	/* This will allow us to get/set the current scale. */
	public float ScaleX
	{
		get { return scaleX; }
		set { scaleX = value; }
	}

	public float ScaleY
	{
		get { return scaleY; }
		set { scaleY = value; }
	}

	public float ScaleZ
	{
		get { return scaleZ; }
		set { scaleZ = value; }
	}

	/* This will allow us to get/set the primitive type of the game object. */
	public PrimitiveType ObjectType
	{
		get { return primitiveType; }
		set { primitiveType = value; }
	}

	/* This will allow us to get/set the current ID number. */
	public int ID
	{
		get { return id; }
		set { id = value; }
	}

	public float Red
	{
		get { return red; }
		set { red = value; }
	}

	public float Green
	{
		get { return green; }
		set { green = value; }
	}

	public float Blue
	{
		get { return blue; }
		set { blue = value; }
	}

	public string Name
	{
		get { return name; }
		set { name = value; }
	}

}