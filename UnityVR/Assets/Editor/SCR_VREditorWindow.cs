using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class SCR_VREditorWindow : EditorWindow 
{

	/* Attributes. */
	private SCR_SceneData sceneData = null;
	private SCR_SceneEditor sceneEditor = null;
	private SaveLoadMenu prefabData = null;
	[SerializeField]	private string sceneName = "";
	[SerializeField]	private bool normalEditing = false;

	/* Methods. */
	[MenuItem("Window/VR Editor/Show")]
	static void Init()
	{

		SCR_VREditorWindow window = (SCR_VREditorWindow)EditorWindow.GetWindow(typeof (SCR_VREditorWindow));
		window.Show();

	}

	[MenuItem("Window/VR Editor/Close")]
	static void Remove()
	{

		SCR_VREditorWindow window = (SCR_VREditorWindow)EditorWindow.GetWindow(typeof (SCR_VREditorWindow));
		window.Close();

	}

	void SetupAttributes()
	{
		if(GameObject.Find("Scene Data") != null)
		{

			sceneData = GameObject.Find("Scene Data").GetComponent<SCR_SceneData>();

		}
		else
		{

			GameObject newGameObject = new GameObject("Scene Data");
			newGameObject.AddComponent<SCR_SceneData>();
			Debug.Log("Making new Scene Data game object.");

		}

		if(GameObject.Find("Scene Editor") != null)
		{
			sceneEditor = GameObject.Find("Scene Editor").GetComponent<SCR_SceneEditor>();
		}
		else
		{
			GameObject newGameObject = new GameObject("Scene Editor");
			newGameObject.AddComponent<SCR_SceneEditor>();
			Debug.Log("Making new Scene Editor game object.");
		}

		if(sceneData != null)
		{
			if(prefabData == null)
			{
				if(sceneData.GetComponent<SaveLoadMenu>() == null)
					prefabData = sceneData.gameObject.AddComponent<SaveLoadMenu>();
				else
					prefabData = sceneData.GetComponent<SaveLoadMenu>();
			}
		}
	}

	void OnEnable()
	{

		titleContent.text = "VR Editor";

		if(!normalEditing)
			SetupAttributes();

	}

	void PlayVREditor()
	{

		if(!EditorApplication.isPlaying)
		{

			EditorApplication.isPlaying = true;

			/* This would add a new scene to the hierarchy. */
			/* May want to use this at some other point. */
			/* EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive); */

		}

	}

	void LoadPrefabs(string filePath)
	{
		/* Loading prefabs from the standard file path. */
		string searchPattern = "*.prefab";
		SearchOption searchOption = SearchOption.AllDirectories;
		string[] filePaths = Directory.GetFiles(filePath, searchPattern, searchOption);
		List<GameObject> prefabs = new List<GameObject>();
		prefabData.prefabDictionary = new Dictionary<string, GameObject>();

		if(filePaths.Length > 0)
		{
			foreach(string path in filePaths)
			{
				if(path.EndsWith(".meta")) continue;

				GameObject tempPrefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;

				if(tempPrefab != null)
				{
					prefabs.Add(tempPrefab);
				}
			}
		}

		/* Loading prefabs from resources folder. */
		GameObject[] resourcePrefabs = Resources.LoadAll<GameObject>("Prefabs");

		if(resourcePrefabs.Length > 0)
		{
			foreach(GameObject tempPrefab in resourcePrefabs)
			{
				prefabs.Add(tempPrefab);
			}
		}

		if(prefabs.Count > 0)
		{
			Debug.Log("THERE ARE THINGS.");

			foreach(GameObject loadedPrefab in prefabs) {
				if(loadedPrefab.GetComponent<ObjectIdentifier>()) {
					
					prefabData.prefabDictionary.Add (loadedPrefab.name,loadedPrefab);
					//Debug.Log("Added GameObject to prefabDictionary: " + loadedPrefab.name);
				}
			}
		}
	}

	void LoadLatestVRScene()
	{

		if(!EditorApplication.isPlaying)
		{
			GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
			string[] previousTags = new string[objectsInScene.Length];

			// Obtain the previous tags and make sure these objects are not destroyed.
			for(int i = 0; i < previousTags.Length; ++i)
			{
				previousTags[i] = objectsInScene[i].tag;
				objectsInScene[i].tag = "DontDestroy";
			}

			SetupAttributes();

			if(GameObject.Find("Scene Data") != null)
			{
				string filePathToAssets = "Assets/Prefabs/";
				LoadPrefabs(filePathToAssets);
				sceneData.Load(sceneName);
			}

			for(int i = 0; i < objectsInScene.Length; ++i)
				objectsInScene[i].tag = previousTags[i];

		}

	}

	void WindowButtons()
	{

//		if(GUI.Button(new Rect(new Vector2(0.0f, 150.0f), new Vector2(200.0f, 100.0f)), "Use VR"))
//		{
//
//			PlayVREditor();
//
//		}

		if(GUI.Button(new Rect(new Vector2(0.0f, 170.0f), new Vector2(200.0f, 100.0f)), "Load Latest Scene"))
		{

			LoadLatestVRScene();

		}

	}

	void OnGUI()
	{

		GUILayout.Label("VR Editor", EditorStyles.boldLabel);

		GUILayout.Space(10.0f);

		GUILayout.Label("Loading Objects from VR Scene", EditorStyles.boldLabel);

		GUILayout.Space(10.0f);

		GUILayout.Label("If you want to populate a new scene with the same scene you created in VR, you can use the 'Load Latest Scene' button to load the last VR scene you worked on into whatever current scene you have open.", EditorStyles.wordWrappedLabel);

		GUILayout.Space(10.0f);

		ScriptableObject target = this;
		SerializedObject so = new SerializedObject(target);
		SerializedProperty stringProperty = so.FindProperty("sceneName");
		EditorGUILayout.PropertyField(stringProperty);
		SerializedProperty boolProperty = so.FindProperty("normalEditing");
		EditorGUILayout.PropertyField(boolProperty);
		so.ApplyModifiedProperties();

		WindowButtons();

		if(normalEditing)
		{
			
		}

	}

}