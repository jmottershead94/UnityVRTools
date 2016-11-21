using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

public class SCR_VREditorWindow : EditorWindow {

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
			EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
		}

	}

	void OnGUI()
	{

		GUILayout.Label("VR Editor", EditorStyles.boldLabel);

		if(GUILayout.Button("Use VR"))
		{
			PlayVREditor();
		}

	}
}
