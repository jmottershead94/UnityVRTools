/* Unity includes here. */
using System;
using UnityEngine;

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

	[Header ("File Loading")]
	[SerializeField]	protected FileExtensions fileExtension = FileExtensions.Prefabs;
	protected string folderToCheck = "";
	protected string filesToLookFor = "";
	protected UnityEngine.Object assetType;

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

}