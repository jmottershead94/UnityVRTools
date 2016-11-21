using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

public class SCR_VREditorWindow : EditorWindow 
{

	/* Attributes. */


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

				
				SCR_SceneData sceneData = GameObject.Find("Scene Data").GetComponent<SCR_SceneData>();


				sceneData.Load();

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