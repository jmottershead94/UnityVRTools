using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_Scene : MonoBehaviour 
{

	// Attributes.
	[SerializeField]	private List<GameObject> sceneObjects;		// The current list of scene objects.

	// Use this for initialization
	void Start () 
	{

		// Initialising our attributes.
		sceneObjects = new List<GameObject>();
		name = "SceneEditor";

	}

	void OnGUI() 
	{

        if (GUI.Button(new Rect(10, 10, 80, 50), "CUBE"))
        {

            Debug.Log("Clicked the button for a cube.");

            // Creating a temporary instance 
            GameObject tempCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            tempCube.transform.position = new Vector3(2.0f, 0.0f, 0.0f);
            tempCube.transform.parent = GameObject.Find(name).transform;

            sceneObjects.Add(tempCube);

        }
        
        if (GUI.Button(new Rect(10, 70, 80, 50), "SPHERE"))
        {

			Debug.Log("Clicked the button for a sphere.");

			GameObject tempSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			tempSphere.transform.position = new Vector3(-2.0f, 0.0f, 0.0f);
			tempSphere.transform.parent = GameObject.Find(name).transform;

			sceneObjects.Add(tempSphere);

        }
        
    }

	// Update is called once per frame
	void Update () 
	{
	
	}
}
