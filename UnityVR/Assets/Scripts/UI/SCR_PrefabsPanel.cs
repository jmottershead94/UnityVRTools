/*
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
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;

/* Prefabs panel IS A panel, therefore inherits from it. */
public class SCR_PrefabsPanel : SCR_Panel 
{

	/* Attributes. */
	private string filePathToStandardPrefabs = "";
	//private string filePathToResourcePrefabs = "";
	[SerializeField]	private List<GameObject> prefabs = null;

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This will be called before initialisation.
	*
	*/
	new private void Awake()
	{
		
		filePathToStandardPrefabs = "Assets/Prefabs/";
		prefabs = new List<GameObject>();
		AddPrefabs(filePathToStandardPrefabs);

	}

	private void AddPrefabs(string filePath)
	{

		string searchPattern = "*";
		SearchOption searchOption = SearchOption.AllDirectories;

		//Debug.Log(file.Name);
		//DirectoryInfo directoryInfo = new DirectoryInfo(filePathToStandardPrefabs);
		//FileInfo[] fileInfo = directoryInfo.GetFiles();

		/* This will search all of teh directories under  */
		string[] filePaths = Directory.GetFiles(filePath, searchPattern, searchOption);

		if(filePaths.Length > 0)
		{
			Debug.Log("Files found.");

			foreach(string path in filePaths)
			{
				if(path.EndsWith(".meta")) continue;

				GameObject tempPrefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;

				if(tempPrefab != null)
				{
					prefabs.Add(tempPrefab);
				}
			}
		}

		GameObject[] resourcePrefabs = Resources.LoadAll<GameObject>("Prefabs");

		if(resourcePrefabs.Length > 0)
		{
			foreach(GameObject tempObject in resourcePrefabs)
			{
				prefabs.Add(tempObject);
			}
		}

	}

	void Start () 
	{}

	void Update () 
	{}

}