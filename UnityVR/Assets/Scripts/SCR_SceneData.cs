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
	private const string sceneDataFilename = "/SceneData.dat";

	private DateTime now = DateTime.Now;						// What date/time it is now.
	private string format = "dd mm yyyy  hh:mm";				// The format to be used for the data.
	private string loadDate;									// The current load date of the data.
	private string saveDate;									// The current save date of the data.
	private SCR_SceneEditor scene;								// Accessing the current scene.	

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

		// Find the scene editor script in the current scene.
		scene = GameObject.Find("Scene Editor").GetComponent<SCR_SceneEditor>();

		// Record the save date.
		saveDate = (now.ToString(format));

		// Initialising our local attributes.
		BinaryFormatter binary = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + sceneDataFilename);
		List<PersistentObjectData> sceneObjectData = new List<PersistentObjectData>();

		// Looping through the amount of objects there are in the current scene.
		for(int i = 0; i < scene.Objects.Count; i++)
		{
			PersistentObjectData objectData = new PersistentObjectData();

			objectData.PositionX = scene.Objects[i].transform.position.x;
			objectData.PositionY = scene.Objects[i].transform.position.y;
			objectData.PositionZ = scene.Objects[i].transform.position.z;

			objectData.RotationX = scene.Objects[i].transform.eulerAngles.x;
			objectData.RotationY = scene.Objects[i].transform.eulerAngles.y;
			objectData.RotationZ = scene.Objects[i].transform.eulerAngles.z;

			objectData.ScaleX = scene.Objects[i].transform.localScale.x;
			objectData.ScaleY = scene.Objects[i].transform.localScale.y;
			objectData.ScaleZ = scene.Objects[i].transform.localScale.z;

			objectData.ObjectType = scene.Objects[i].ObjectType;
			objectData.ID = scene.Objects[i].ID;

			sceneObjectData.Add(objectData);

		}

		binary.Serialize(file, sceneObjectData);

		Debug.Log("Saved!");

	}

	public void Load()
	{

		// Find the scene editor script in the current scene.
		scene = GameObject.Find("Scene Editor").GetComponent<SCR_SceneEditor>();

		// If the scene data file exists.
		if(File.Exists(Application.persistentDataPath + sceneDataFilename))
		{
			
			BinaryFormatter binary = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + sceneDataFilename, FileMode.Open);
			List<PersistentObjectData> sceneObjectData = (List<PersistentObjectData>)binary.Deserialize(file);
			file.Close();

			// If we currently have objects in our scene.
			if(scene.Objects.Count > 0)
			{

				for(int i = 0; i < scene.Objects.Count; i++)
				{

					Destroy(scene.Objects[i].gameObject);

				}

				// Clear the current scene, because we are about to load and don't want duplicates.
				scene.Objects.Clear();
				
			}

			//scene.Objects = sceneObjectData;

			Debug.Log(sceneObjectData.Count);

			for(int i = 0; i < sceneObjectData.Count; i++)
			{

				GameObject tempGameObject = GameObject.CreatePrimitive(sceneObjectData[i].ObjectType);

				SCR_PersistentObject tempNewPersistentObject = tempGameObject.AddComponent<SCR_PersistentObject>();
				//tempNewPersistentObject.transform = sceneObjectData[i].ObjectTransform;
				//tempNewPersistentObject.transform.position = sceneObjectData[i].Position;
				//tempNewPersistentObject.transform.eulerAngles = sceneObjectData[i].Rotation;
				//tempNewPersistentObject.transform.localScale = sceneObjectData[i].Scale;
				tempNewPersistentObject.transform.position = new Vector3(sceneObjectData[i].PositionX, sceneObjectData[i].PositionY, sceneObjectData[i].PositionZ);
				tempNewPersistentObject.transform.eulerAngles = new Vector3(sceneObjectData[i].RotationX, sceneObjectData[i].RotationY, sceneObjectData[i].RotationZ);
				tempNewPersistentObject.transform.localScale = new Vector3(sceneObjectData[i].ScaleX, sceneObjectData[i].ScaleY, sceneObjectData[i].ScaleZ);
				tempNewPersistentObject.transform.parent = GameObject.Find("Scene Editor").transform;
				tempNewPersistentObject.ObjectType = sceneObjectData[i].ObjectType;
				tempNewPersistentObject.ID = sceneObjectData[i].ID;

				scene.Objects.Add(tempNewPersistentObject);

			}

			Debug.Log("Loaded!");

		}

	}

	// Getters.
	// This will return the current static instance of our scene data class.
	public static SCR_SceneData Instance
	{
		get { return sceneDataInstance; }
	}

}

[Serializable]
// Persistent object data is just a standard class.
class PersistentObjectData
{

	// Attributes.
	private float positionX, positionY, positionZ;
	private float rotationX, rotationY, rotationZ;
	private float scaleX, scaleY, scaleZ;
	private PrimitiveType primitiveType;	// This is used to store the current primitive type of the object.
	private int id;							// This is used to store the current ID of the object.

	// Methods.
	// Getters/Setters.
	// This will allow us to get/set the current position.
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

	// This will allow us to get/set the current position.
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

	// This will allow us to get/set the current position.
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

	// This will allow us to get/set the primitive type of the game object.
	public PrimitiveType ObjectType
	{
		get { return primitiveType; }
		set { primitiveType = value; }
	}

	// This will allow us to get/set the current ID number.
	public int ID
	{
		get { return id; }
		set { id = value; }
	}

}