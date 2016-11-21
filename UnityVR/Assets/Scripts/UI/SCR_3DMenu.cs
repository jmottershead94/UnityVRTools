using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_3DMenu : MonoBehaviour 
{

	/* Attributes. */
	//[SerializeField] [Range (0.0f, 5.0f)]	private float rotationSpeed = 1.0f;
	private SCR_SceneEditor sceneEditor = null;
	private List<SCR_Panel> panels = null;
	private SCR_Panel currentFocusPanel = null;
	private int panelIndex = 0;

	/* Methods. */
	void Awake()
	{

		/* Initialising our attributes. */
		//transform.position = new Vector3(SCR_UIConstants.LeftOfScreen.x, SCR_UIConstants.LeftOfScreen.y, transform.position.z);
		sceneEditor = GameObject.Find("Scene Editor").GetComponent<SCR_SceneEditor>();
		panels = new List<SCR_Panel>();

		SCR_Panel[] tempChildrenPanels = transform.FindChild("Panels").GetComponentsInChildren<SCR_Panel>();

		foreach(SCR_Panel tempPanel in tempChildrenPanels)
		{
			panels.Add(tempPanel);
		}

		panels[panelIndex].InFocus = true;
		currentFocusPanel = panels[panelIndex];

	}

	// Use this for initialization
	void Start () 
	{

		

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
        	sceneEditor.SpawnObject(PrimitiveType.Cube, new Vector3(2.0f, 0.0f, 0.0f));

        }

        // If the user presses on the sphere button.
        if (GUI.Button(new Rect(10, 70, 80, 50), "SPHERE"))
        {

			// Spawn a sphere.
			sceneEditor.SpawnObject(PrimitiveType.Sphere, new Vector3(-2.0f, 0.0f, 0.0f));

        }

		// If the user presses on the save button.
        if (GUI.Button(new Rect(500, 10, 200, 50), "SAVE SCENE"))
        {

			SCR_SceneData.Instance.Save();

        }

		// If the user presses on the sphere button.
        if (GUI.Button(new Rect(500, 70, 200, 50), "LOAD SCENE"))
        {

			SCR_SceneData.Instance.Load();

        }
        
    }

    void CheckPanelFocus()
    {

    	if(!panels[panelIndex].InFocus)
    	{
			panels[panelIndex].InFocus = true;
    	}

    }

	// Update is called once per frame
	void Update () 
	{

		CheckPanelFocus();

	}

	public List<SCR_Panel> Panels
	{
		get { return panels; }
	}

	public SCR_Panel CurrentPanel
	{
		get { return currentFocusPanel; }
		set { currentFocusPanel = value; }
	}

	public int CurrentPanelIndex
	{
		get { return panelIndex; }
		set { panelIndex = value; }
	}

}