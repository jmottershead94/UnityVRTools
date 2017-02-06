using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

public class SCR_VREditorWindow : EditorWindow 
{

	/* Attributes. */
	private SCR_SceneData sceneData = null;
	private SCR_SceneEditor sceneEditor = null;

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

	void OnEnable()
	{

		titleContent.text = "VR Editor";

		if(GameObject.Find("Scene Data") != null)
		{

			sceneData = GameObject.Find("Scene Data").GetComponent<SCR_SceneData>();

		}
		else
		{

			GameObject newGameObject = new GameObject("Scene Data");
			newGameObject.AddComponent<SCR_SceneData>();

			Debug.Log("Making new Scene Data script.");

		}

		if(GameObject.Find("Scene Editor") != null)
		{

			sceneEditor = GameObject.Find("Scene Editor").GetComponent<SCR_SceneEditor>();

		}
		else
		{

			GameObject newGameObject = new GameObject("Scene Editor");
			newGameObject.AddComponent<SCR_SceneEditor>();

			Debug.Log("Making new Scene Editor script.");

		}

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

	void LoadLatestVRScene()
	{

		if(!EditorApplication.isPlaying)
		{

			if(GameObject.Find("Scene Data") != null)
			{

				sceneData.Load();

				Debug.Log("Should load scene?");

			}

		}

	}

	void WindowButtons()
	{

		if(GUILayout.Button("Use VR"))
		{

			PlayVREditor();

		}

		GUILayout.Space(10.0f);

		if(GUILayout.Button("Load Latest Scene"))
		{

			LoadLatestVRScene();

		}

	}

	void OnGUI()
	{

		GUILayout.Label("VR Editor", EditorStyles.boldLabel);

		GUILayout.Space(10.0f);

		WindowButtons();

	}

}