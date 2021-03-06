﻿/*
*
*	Prefabs Panel Class
*	===================
*
*	Created: 	2016/11/21
*	Filter:		Scripts/UI
*	Class Name: SCR_PrefabsPanel
*	Base Class: SCR_Panel
*	Author: 	1300455 Jason Mottershead
*
*	Purpose:	This will allow the menu to choose if the user wants to use any
*				standard assets, or there own assets.
*
*/

/* Unity includes here. */
using UnityEngine;

#if UNITY_EDITOR
	using UnityEditor;
#endif

using System;
using System.IO;
using System.Collections.Generic;

/* Prefabs panel IS A panel, therefore inherits from it. */
public class SCR_PrefabsPanel : SCR_FileLoadingPanel 
{

	/* Attributes. */
	[HideInInspector]	[SerializeField]	private List<GameObject> prefabs = null;
	[HideInInspector]	[SerializeField]	private List<Texture2D> prefabPreviews = null;
	[SerializeField]	private bool inVR = false;
	private SCR_GridView gridView = null;

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This will be called for initialisation.
	*
	*/
	protected void Start()
	{
		//if(!SCR_SceneEditor.InVREditor)
		//	return;

		prefabs = new List<GameObject>();
		prefabPreviews = new List<Texture2D>();
		gridView = GetComponent<SCR_GridView>();
		AddPrefabs(filePathToAssets);
	}

	public void AddPrefabs(string filePath)
	{

		/* Loading prefabs from the standard file path. */
		string searchPattern = filesToLookFor;
		SearchOption searchOption = SearchOption.AllDirectories;
		string[] filePaths = Directory.GetFiles(filePath, searchPattern, searchOption);
		Vector3 startingPosition = transform.FindChild("Label").transform.position;

		if(filePaths.Length > 0)
		{
			foreach(string path in filePaths)
			{
				if(path.EndsWith(fileExtensionToIgnore)) continue;

				#if UNITY_EDITOR
					GameObject tempPrefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
					prefabPreviews.Add(AssetPreview.GetAssetPreview(tempPrefab));

					if(tempPrefab != null)
					{
						prefabs.Add(tempPrefab);
					}
				#endif
			}
		}


		/* Loading prefabs from resources folder. */
		GameObject[] resourcePrefabs = Resources.LoadAll<GameObject>(folderToCheck);

		if(resourcePrefabs.Length > 0)
		{
			foreach(GameObject tempPrefab in resourcePrefabs)
			{
				#if UNITY_EDITOR
					prefabPreviews.Add(AssetPreview.GetAssetPreview(tempPrefab));
					prefabs.Add(tempPrefab);
				#endif
			}
		}

		for(int i = 0; i < prefabPreviews.Count; i++)
		{
			GameObject prefabPreview = Resources.Load("Standard VR Assets/PRE_PrefabButton", typeof (GameObject)) as GameObject;
			prefabPreview = Instantiate(prefabPreview, transform.position, Quaternion.identity) as GameObject;
			prefabPreview.name = prefabs[i].name;
			prefabPreview.transform.localScale = GameObject.Find("PRE_3DMenu").GetComponent<Transform>().localScale;
			prefabPreview.transform.SetParent(transform.FindChild("Scroll View"));
			prefabPreview.GetComponent<Renderer>().materials[0].mainTexture = (Texture)prefabPreviews[i];

			if(gridView != null && gridView.GridObjects != null)
			{
				gridView.GridObjects.Add(prefabPreview);
			}
		}

		if(gridView != null)
		{
			if (inVR) 
			{
				startingPosition.x -= 0.00125f;
				startingPosition.z += 0.25f;
			}

			// Align the grid elements.
			gridView.AlignGridElements(startingPosition);
		}
	}

	public List<GameObject> Prefabs
	{
		get { return prefabs; }
	}

}