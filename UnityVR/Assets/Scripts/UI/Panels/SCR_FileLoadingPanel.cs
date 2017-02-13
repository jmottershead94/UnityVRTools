/* Unity includes here. */
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;

public class SCR_FileLoadingPanel : SCR_Panel 
{

	/* Attributes. */
	protected enum FileExtensions
	{
		Audio,
		Prefabs,
		Sprites
	};

	protected const string fileExtensionToIgnore = ".meta";
	protected string filePathToAssets = "";

	[Header ("File Loading Panel Properties")]
	[SerializeField]	protected FileExtensions fileExtension = FileExtensions.Prefabs;
	[SerializeField]	protected string folderToCheck = "";
	[SerializeField]	protected string filesToLookFor = "";

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
		switch(fileExtension)
		{
			case FileExtensions.Audio:
			{
				folderToCheck = "Audio";
				filesToLookFor = "*.wav";
				break;
			}
			case FileExtensions.Prefabs:
			{
				folderToCheck = "Prefabs";
				filesToLookFor = "*.prefab";
				break;
			}
			case FileExtensions.Sprites:
			{
				folderToCheck = "Sprites";
				filesToLookFor = "*.png";
				break;
			}
		}

		filePathToAssets = "Assets/" + folderToCheck + "/";

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}