/*
*
*	Scene Editor Class
*	==================
*
*	Created: 	2016/11/21
*	Filter:		Scripts
*	Class Name: SCR_SceneEditor
*	Base Class: Monobehaviour
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	The purpose of this class is to allow for object manipulation in the scene, 
*				and also for storing all of the current scene objects.
*
*/

/* Unity includes here. */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Scene editor IS A game object, therefore inherits from it. */
public class SCR_SceneEditor : MonoBehaviour 
{
#region Attributes
	/* Attributes. */
	[SerializeField]	private static List<SCR_PersistentObject> sceneObjects = null;		/* The current list of scene objects. */

	[Header ("Object Manipulation")]
	[SerializeField]	private Vector3 translationSpeed = Vector3.zero; 			/* Used to determine how fast objects will move for translations in each axis. */
	[SerializeField]	private Vector3 scaleIncrement = Vector3.zero; 				/* Used to determine how fast objects scale in each axis. */
	[SerializeField]	private Vector3 rotationIncrement = Vector3.zero;			/* Used to determine how fast objects rotate in each axis. */
	[SerializeField]	private float controllerDistanceForManipulation = 2.0f;

	public enum TransformState 													/* Defining transform states for the user. */
	{
		translation,	/* The user is translating an object. */
		rotation, 		/* The user is rotating an object. */
		scale 			/* The user is scaling an object. */
	};
	private TransformState currentTransformState = TransformState.translation;	 	/* Used to determine what transform state the user is currently in for object manipulation. */
	private SCR_VRControllerInput leftController = null;
	private SCR_VRControllerInput rightController = null;
	private Vector3 currentRotation = Vector3.zero;
	private Vector3 currentScale = Vector3.zero;
	private Vector3 currentTranslation = Vector3.zero;
#endregion

#region Awake
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
		/* Initialising our attributes. */
		sceneObjects = new List<SCR_PersistentObject>();
	}
#endregion

#region Spawn Primitive
	/*
	*
	*	Overview
	*	--------
	*	This function will spawn in any primitive object we want at a 
	*	specified spawn location.
	*
	*	Params
	*	------
	*	PrimitiveType primtiveType 	- 	This is the current primitive type of the object
	*									that the user wishes to spawn (cube, sphere etc).
	*
	*	Vector3 spawnPosition 		- 	This is the current spawn position of the object
	*									that the user wishes to spawn.
	*
	*/
	public void SpawnPrimitive(PrimitiveType primitiveType, Vector3 spawnPosition)
	{
		/* Creating a temporary instance of a cube game object. */
        GameObject tempGameObject = GameObject.CreatePrimitive(primitiveType);
        tempGameObject.tag = "DontDestroy";

		/* Creating a temporary instance of a persistent object and adding it onto the temporary game object. */
		SCR_PersistentObject tempPersistentObject = tempGameObject.AddComponent<SCR_PersistentObject>();
		tempPersistentObject.transform.position = spawnPosition;
		tempPersistentObject.transform.parent = GameObject.Find(name).transform;
		tempPersistentObject.ObjectType = primitiveType;
		tempPersistentObject.ID = sceneObjects.Count;

		/* Add the cube into the list of game objects. */
        sceneObjects.Add(tempPersistentObject);
	}
#endregion

#region Rotation
	/*
	*
	*	Overview
	*	--------
	*	This method will be used to set the individual rotation values of the 
	*	object based on user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being rotated.
	*
	*/
	private void CheckRotation(SCR_PersistentObject persistentObject)
	{
		currentRotation = Vector3.zero;

		if(Input.GetKey(KeyCode.I))
			currentRotation.x += rotationIncrement.x;

		if(Input.GetKey(KeyCode.K))
			currentRotation.x += (rotationIncrement.x * -1.0f);

		if(Input.GetKey(KeyCode.J))
			currentRotation.y += rotationIncrement.y;

		if(Input.GetKey(KeyCode.L))
			currentRotation.y += (rotationIncrement.y * -1.0f);

		if(Input.GetKey(KeyCode.U))
			currentRotation.z += rotationIncrement.z;

		if(Input.GetKey(KeyCode.O))
			currentRotation.z += (rotationIncrement.z * -1.0f);

		SCR_Camera.RotateInRelationToCam(persistentObject.transform, currentRotation);
	}
#endregion

#region Scale
	/*
	*
	*	Overview
	*	--------
	*	This method will be used to set the individual scale values of the 
	*	object based on user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being scaled.
	*
	*/
	private void CheckScale(SCR_PersistentObject persistentObject)
	{
		currentScale = persistentObject.transform.localScale;

		if(Input.GetKey(KeyCode.I))
			currentScale.Set(currentScale.x, currentScale.y, currentScale.z + scaleIncrement.z);

		if(Input.GetKey(KeyCode.J))
			currentScale.Set(currentScale.x - scaleIncrement.x, currentScale.y, currentScale.z);

		if(Input.GetKey(KeyCode.K))
			currentScale.Set(currentScale.x, currentScale.y, currentScale.z - scaleIncrement.z);

		if(Input.GetKey(KeyCode.L))
			currentScale.Set(currentScale.x + scaleIncrement.x, currentScale.y, currentScale.z);

		if(Input.GetKey(KeyCode.U))
			currentScale.Set(currentScale.x, currentScale.y - scaleIncrement.y, currentScale.z);

		if(Input.GetKey(KeyCode.O))
			currentScale.Set(currentScale.x, currentScale.y + scaleIncrement.y, currentScale.z);
		
		persistentObject.transform.localScale = currentScale;
	}
#endregion

#region Translations
	/*
	*
	*	Overview
	*	--------
	*	This method will be used to set the individual translation values of the 
	*	object based on user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being translated.
	*
	*/
	private void CheckTranslations(SCR_PersistentObject persistentObject)
	{
		currentTranslation = Vector3.zero;

		if(Input.GetKey(KeyCode.I))
			currentTranslation.z += (translationSpeed.z);

		if(Input.GetKey(KeyCode.K))
			currentTranslation.z += (translationSpeed.z * -1.0f);
		
		if(Input.GetKey(KeyCode.L))
			currentTranslation.x += (translationSpeed.x);

		if(Input.GetKey(KeyCode.J))
			currentTranslation.x += (translationSpeed.x * -1.0f);

		if(Input.GetKey(KeyCode.O))
			currentTranslation.y += (translationSpeed.y);

		if(Input.GetKey(KeyCode.U))
			currentTranslation.y += (translationSpeed.y * -1.0f);

		SCR_Camera.MoveInRelationToCam(persistentObject.transform, currentTranslation);
	}
#endregion

#region VR Rotation
	/*
	*
	*	Overview
	*	--------
	*	This method will be used to set the individual rotation values of the 
	*	object based on user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being rotated.
	*
	*/
	private void CheckRotationVR(SCR_PersistentObject persistentObject)
	{
		if(GameObject.Find("Controller (left)") == null && GameObject.Find("Controller (right)") == null)
			return;

		leftController = GameObject.Find("Controller (left)").GetComponent<SCR_VRControllerInput>();
		rightController = GameObject.Find("Controller (right)").GetComponent<SCR_VRControllerInput>();

		if(leftController.TriggerHeld() && rightController.TriggerHeld())
		{
			Vector3 anchorDistance = leftController.PositionToCamera - rightController.PositionToCamera;
			currentRotation = Vector3.zero;

			if(anchorDistance.z > controllerDistanceForManipulation)
				currentRotation.x += rotationIncrement.x;
				//persistentObject.transform.Rotate (0.0f, 0.0f, rotationIncrement.x);

			if(anchorDistance.z < (controllerDistanceForManipulation * -1.0f))
				currentRotation.x += (rotationIncrement.x * -1.0f);
				//persistentObject.transform.Rotate (0.0f, 0.0f, -rotationIncrement.x);

			if(anchorDistance.x < (controllerDistanceForManipulation * -1.0f))
				currentRotation.y += rotationIncrement.y;
				//persistentObject.transform.Rotate (0.0f, rotationIncrement.y, 0.0f);

			if(anchorDistance.x > controllerDistanceForManipulation)
				currentRotation.y += (rotationIncrement.y * -1.0f);
				//persistentObject.transform.Rotate (0.0f, -rotationIncrement.y, 0.0f);
						
			if(anchorDistance.y > controllerDistanceForManipulation)
				currentRotation.z += rotationIncrement.z;
				//persistentObject.transform.Rotate (rotationIncrement.z, 0.0f, 0.0f);

			if(anchorDistance.y < (controllerDistanceForManipulation * -1.0f))
				currentRotation.z += (rotationIncrement.z * -1.0f);
				//persistentObject.transform.Rotate (-rotationIncrement.z, 0.0f, 0.0f);

			SCR_Camera.RotateInRelationToCam(persistentObject.transform, currentRotation);
		}
	}
#endregion

#region VR Scale
	/*
	*
	*	Overview
	*	--------
	*	This method will be used to set the individual scale values of the 
	*	object based on user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being scaled.
	*
	*/
	private void CheckScaleVR(SCR_PersistentObject persistentObject)
	{
		if(GameObject.Find("Controller (left)") == null && GameObject.Find("Controller (right)") == null)
			return;
		
		leftController = GameObject.Find("Controller (left)").GetComponent<SCR_VRControllerInput>();
		rightController = GameObject.Find("Controller (right)").GetComponent<SCR_VRControllerInput>();

		if(leftController.TriggerHeld() && rightController.TriggerHeld())
		{
			Vector3 anchorDistance = leftController.PositionToCamera - rightController.PositionToCamera;

			if(anchorDistance.z > controllerDistanceForManipulation)
				currentScale.Set(currentScale.x, currentScale.y, currentScale.z + (scaleIncrement.z));

			if(anchorDistance.z < (controllerDistanceForManipulation * -1.0f))
				currentScale.Set(currentScale.x, currentScale.y, currentScale.z - (scaleIncrement.z));

			if(anchorDistance.x > controllerDistanceForManipulation)
				currentScale.Set(currentScale.x + (scaleIncrement.x), currentScale.y, currentScale.z);

			if(anchorDistance.x < (controllerDistanceForManipulation * -1.0f))
				currentScale.Set(currentScale.x - (scaleIncrement.x), currentScale.y, currentScale.z);

			if(anchorDistance.y > controllerDistanceForManipulation)
				currentScale.Set(currentScale.x, currentScale.y + (scaleIncrement.y), currentScale.z);

			if(anchorDistance.y < (controllerDistanceForManipulation * -1.0f))
				currentScale.Set(currentScale.x, currentScale.y - (scaleIncrement.y), currentScale.z);
		}

		persistentObject.transform.localScale = currentScale;
	}
#endregion

#region VR Translations
	/*
	*
	*	Overview
	*	--------
	*	This method will be used to set the individual translation values of the 
	*	object based on user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being translated.
	*
	*/
	private void CheckTranslationsVR(SCR_PersistentObject persistentObject)
	{
		if(GameObject.Find("Controller (left)") == null && GameObject.Find("Controller (right)") == null)
			return;

		leftController = GameObject.Find("Controller (left)").GetComponent<SCR_VRControllerInput>();
		rightController = GameObject.Find("Controller (right)").GetComponent<SCR_VRControllerInput>();

		// THIS NEEDS TESTING.
		if(leftController.TriggerHeld() && rightController.TriggerHeld())
		{
			Vector3 anchorDistance = leftController.PositionToCamera - rightController.PositionToCamera;
			currentTranslation = Vector3.zero;

			if(anchorDistance.z > controllerDistanceForManipulation)
				currentTranslation.z += (translationSpeed.z);
				//persistentObject.transform.Translate (0.0f, 0.0f, (translationSpeed.z));

			if(anchorDistance.z < (controllerDistanceForManipulation * -1.0f))
				currentTranslation.z += (translationSpeed.z * -1.0f);
				//persistentObject.transform.Translate (0.0f, 0.0f, (-translationSpeed.z));

			if(anchorDistance.x > controllerDistanceForManipulation)
				currentTranslation.x += (translationSpeed.x);
				//persistentObject.transform.Translate((translationSpeed.x), 0.0f, 0.0f);

			if(anchorDistance.x < (controllerDistanceForManipulation * -1.0f))
				currentTranslation.x += (translationSpeed.x * -1.0f);
				//persistentObject.transform.Translate((-translationSpeed.x), 0.0f, 0.0f);

			if(anchorDistance.y > controllerDistanceForManipulation)
				currentTranslation.y += (translationSpeed.y);
				//persistentObject.transform.Translate(0.0f, (translationSpeed.y), 0.0f);

			if(anchorDistance.y < (controllerDistanceForManipulation * -1.0f))
				currentTranslation.y += (translationSpeed.y * -1.0f);
				//persistentObject.transform.Translate(0.0f, (-translationSpeed.y), 0.0f);

			SCR_Camera.MoveInRelationToCam(persistentObject.transform, currentTranslation);
		}
	}
#endregion

#region Object Manipulation	
	/*
	*
	*	Overview
	*	--------
	*	This method will be used to check the current user transform state and 
	*	provide the correct transform manipulation response with user input.
	*
	*	Params
	*	------
	*	SCR_PersistentObject persistentObject 	- 	This is the object currently being translated.
	*
	*/
	private void ManipulateObject(SCR_PersistentObject persistentObject)
	{
		if(currentTransformState == TransformState.translation)
		{
			CheckTranslations(persistentObject);
			CheckTranslationsVR (persistentObject);
		}
		else if(currentTransformState == TransformState.rotation)
		{
			CheckRotation(persistentObject);	
			CheckRotationVR (persistentObject);
		}
		else if(currentTransformState == TransformState.scale)
		{
			CheckScale(persistentObject);
			CheckScaleVR (persistentObject);
		}
	}
#endregion

#region Highlighted Object Check
	/*
	*
	*	Overview
	*	--------
	*	This method will be responsible for handling any object manipulation
	*	in the scene, checking if the object is actually in focus first.
	*
	*/
	private void CheckForHighlightedObjects()
	{

		/* Loop through each of the scene objects.*/
		foreach(SCR_PersistentObject tempPersistentObject in sceneObjects)
		{

			/* If the current scene object is in focus (i.e. the player is aiming at this object). */
			if(tempPersistentObject.InFocus)
			{

				/* Manipulate the object based on user input. */
				ManipulateObject(tempPersistentObject);

			}

		}

	}
#endregion

#region Transform State Check
	/*
	*
	*	Overview
	*	--------
	*	This method will be responsible for checking the current transform 
	*	state of the user, allowing the user to switch what type of object
	*	manipulation they are performing.
	*
	*/
	private void CheckCurrentTransformState()
	{

		/* If the Num1 key has been pressed. */
		/* VR Equivalent: If the user is selects the translation button along the top of the screen with the right hand controller. */
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{

			/* If the current transform state has not been set to translation. */
			if(currentTransformState != TransformState.translation)
			{

				/* Set the current transform state to translation. */
				currentTransformState = TransformState.translation;

			}

		}
		/* Otherwise, the Num2 key has been pressed. */
		/* VR Equivalent: If the user is selects the rotation button along the top of the screen with the right hand controller. */
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{

			/* If the current transform state has not been set to rotation. */
			if(currentTransformState != TransformState.rotation)
			{

				/* Set the current transform state to rotation. */
				currentTransformState = TransformState.rotation;

			}

		}
		/* Otherwise, the Num3 key has been pressed. */
		/* VR Equivalent: If the user is selects the scale button along the top of the screen with the right hand controller. */
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{

			/* If the current transform state has not been set to scale. */
			if(currentTransformState != TransformState.scale)
			{

				/* Set the current transform state to scale. */
				currentTransformState = TransformState.scale;

			}

		}

	}
#endregion

	public static void DeselectAllObjects()
	{
		foreach(SCR_PersistentObject obj in sceneObjects)
			obj.InFocus = false;
	}

#region Update
	/*
	*
	*	Overview
	*	--------
	*	This will be called every frame.
	*
	*/
	private void Update()
	{

		/* Handles updates to the user transform state. */
		CheckCurrentTransformState();

		/* Handles updates to any highlighted objects. */
		CheckForHighlightedObjects();

	}
#endregion

#region Getters / Setters
	/* Getters. */
	/* This will allow us to get/set the current list of persistent objects in the scene. */
	public static List<SCR_PersistentObject> Objects
	{
		get { return sceneObjects; }
		set { sceneObjects = value; }
	}

	public TransformState CurrentTransformState
	{
		get { return currentTransformState; }
		set { currentTransformState = value; }
	}
#endregion

}