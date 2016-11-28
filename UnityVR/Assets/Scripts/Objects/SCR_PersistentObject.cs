using UnityEngine;
using System;
using System.Collections;

// Persistent object IS A game object, therefore inherits from it.
public class SCR_PersistentObject : SCR_BaseUIElement
{

	// Attributes.
	private PrimitiveType primitiveType;							// Accessing what primitive type we are using.
	private int id;													// Accessing the ID number of the object.
	private Material currentMaterial = null;
	private Material glowingMaterial = null;
	private Material defaultMaterial = null;

	/* Methods. */
	void Awake()
	{

		currentMaterial = GetComponent<MeshRenderer>().materials[0];
		defaultMaterial = currentMaterial;
		glowingMaterial = Resources.Load("Materials/MAT_ObjectSelected") as Material;

	}

	void ObjectInFocus()
	{

		if(currentMaterial != glowingMaterial)
		{

			currentMaterial = glowingMaterial;

			GetComponent<MeshRenderer>().material = currentMaterial;

		}

	}

	void ObjectOutOfFocus()
	{

		if(currentMaterial != defaultMaterial)
		{

			currentMaterial = defaultMaterial;

			GetComponent<MeshRenderer>().material = currentMaterial;

		}

	}

	void OnMouseDown()
	{

		if(isInFocus)
		{

			isInFocus = false;

		}
		else
		{

			isInFocus = true;

		}

	}

	override protected void CheckFocus()
	{

		if(isInFocus)
		{

			ObjectInFocus();

		}
		else
		{

			ObjectOutOfFocus();

		}

	}

	void Update()
	{

		CheckFocus();

	}

	// Getters/Setters.
	public Transform ObjectTransform
	{
		get { return transform; }
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