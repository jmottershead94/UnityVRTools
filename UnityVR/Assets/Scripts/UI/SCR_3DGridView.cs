using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_3DGridView : SCR_BaseUIElement 
{

	/* Attributes. */
	[SerializeField]	private int numberOfColumns = 2;
	[SerializeField]	private int numberOfRows = 2;
	[SerializeField]	private float cellSize = 1.0f;
	[SerializeField]	private List<GameObject> gridObjects = null;

	/* Methods. */


	// Use this for initialization
	void Start () 
	{

		for(int row = 0; row < numberOfRows; row++)
		{

			for(int column = 0; column < numberOfColumns; column++)
			{

				

			}

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<GameObject> GridObjects
	{
		set { gridObjects = value; }
	}
}