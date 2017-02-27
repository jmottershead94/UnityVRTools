using UnityEngine;
using System.Collections;

public class SCR_GrabButton : SCR_3DButton 
{
	
	public enum VRTransformType
	{
		grab,
		freeForm
	};
	private VRTransformType transformType = VRTransformType.freeForm;
	private Material defaultMaterial = null;
	private Material clickedMaterial = null;

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This will be called before initialisation.
	*
	*/
	new protected void Awake()
	{

		base.Awake();

		defaultMaterial = GetComponent<MeshRenderer>().materials[0];
		clickedMaterial = Resources.Load("Materials/MAT_FadedBlack") as Material;

	}

	/*
	*
	*	Overview
	*	--------
	*	This will allow us to define a specific button response.
	*
	*/
	override public void ButtonPressResponse()
	{

		if (transformType == VRTransformType.grab) 
		{
			SCR_AppearenceChanger.ChangeMaterial (gameObject, defaultMaterial);
			transformType = VRTransformType.freeForm;
		} 
		else if (transformType == VRTransformType.freeForm) {
			SCR_AppearenceChanger.ChangeMaterial (gameObject, clickedMaterial);
			transformType = VRTransformType.grab;
		}

	}

	public VRTransformType TransformType
	{
		get { return transformType; }
	}
}
