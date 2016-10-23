// Unity includes here.
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SCR_SceneCustomEditor : EditorWindow 
{

	[MenuItem("Window/Scene Editor/Enable")]
	public static void Enable()
	{
		SceneView.onSceneGUIDelegate += OnScene;
		Debug.Log("Scene Enabled");
	}

	[MenuItem("Window/Scene Editor/Disable")]
	public static void Disable()
	{
		SceneView.onSceneGUIDelegate -= OnScene;
		Debug.Log("Scene Disabled");
	}

	[MenuItem("Window/Scene Editor/Play Scene")]
	public static void BuildAssetBundles()
	{

		EditorApplication.isPlaying = true;
		EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);

	}

	private static void OnScene(SceneView sceneView)
	{

		Handles.BeginGUI();

		if(GUILayout.Button("PRESS ME!"))
		{

			Debug.Log("Got it to work.");

		}

		Handles.EndGUI();

	}

}
