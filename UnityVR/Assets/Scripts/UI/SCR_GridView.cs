﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_GridView : SCR_BaseUIElement 
{

	/* Attributes. */
	[SerializeField] 	private int numberOfColumns = 2;
	[SerializeField] 	private int numberOfRows = 2;
	[SerializeField]	private float cellSize = 1.25f;
	private List<GameObject> gridObjects = null;
	private List<Vector3> gridPositions = null;

	/* Methods. */
	new protected void Awake () 
	{
		if(!SCR_SceneEditor.InVREditor)
			return;

		base.Awake();
		gridObjects = new List<GameObject>();
		gridPositions = new List<Vector3>();

	}

	public void AlignGridElements(Vector3 startingPosition)
	{
		if(gridObjects.Count < 1)
			return;

		int rowsNeeded = 0;

		for(int i = 0; i < gridObjects.Count; i++)
		{
			if(i % 2 == 0)
			{
				rowsNeeded++;
			}
		}

		numberOfRows = rowsNeeded;

		if(numberOfRows > 0 && numberOfColumns > 0)
		{
			Vector3 startingGridPosition = new Vector3((startingPosition.x + (cellSize * 0.5f)), (startingPosition.y - cellSize), startingPosition.z - 0.3f);
			Vector3 currentGridPosition = startingGridPosition;

			for(int row = 1; row <= numberOfRows; row++)
			{
				for(int column = 1; column <= numberOfColumns; column++)
				{
					gridPositions.Add(currentGridPosition);
					currentGridPosition.x -= (column * cellSize);
				}
				
				currentGridPosition.x = startingGridPosition.x;
				currentGridPosition.y -= cellSize;
			}
		}

		if(gridObjects.Count > 0)
		{
			for(int i = 0; i < gridObjects.Count; i++)
			{
				gridObjects[i].transform.position = gridPositions[i];
			}
		}
	}

	public List<GameObject> GridObjects
	{
		get { return gridObjects; }
		set { gridObjects = value; }
	}
}