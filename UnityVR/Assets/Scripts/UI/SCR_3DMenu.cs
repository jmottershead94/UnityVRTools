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
        if (GUI.Button(new Rect(10, 10, 80, 50), "SAVE"))
        {

			SCR_SceneData.Instance.Save();

        }

        // If the user presses on the sphere button.
        if (GUI.Button(new Rect(10, 70, 80, 50), "LOAD"))
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

    void RotateWithMouse()
    {

    	Vector3 tempCurrentMousePosition = Input.mousePosition;
    	tempCurrentMousePosition.z = 10.0f;

    	Vector3 tempObjectPosition = Camera.main.WorldToScreenPoint(transform.position);
    	tempCurrentMousePosition.x = tempCurrentMousePosition.x - tempObjectPosition.x;
    	tempCurrentMousePosition.y = tempCurrentMousePosition.y - tempObjectPosition.y;

    	float tempAngle = Mathf.Atan2(tempCurrentMousePosition.y, tempCurrentMousePosition.x) * Mathf.Rad2Deg;
    	Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, tempAngle));

    }

	// Update is called once per frame
	void Update () 
	{

		CheckPanelFocus();

		//RotateWithMouse();

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