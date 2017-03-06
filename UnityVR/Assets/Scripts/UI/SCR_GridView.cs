using UnityEngine;
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

//	public void ScrollOffset(Vector3 inputPosition)
//	{
//		SCR_UIConstants.Scrolling = true;
//		offset = prefabView.position.y - inputPosition.y;
//	}
//
//	public void Drag(Vector3 inputPosition)
//	{
//		if(gridObjects.Count <= 0)
//			return;
//
//		float cursorPosition = inputPosition.y + offset;
//		Vector3 movement = new Vector3(0.0f, cursorPosition, 0.0f);
//		prefabView.position = new Vector3(prefabView.transform.position.x, cursorPosition, prefabView.position.z);
//		//Vector3 axis = prefabView.position + (transform.up * cursorPosition);
//		//prefabView.position = new Vector3(prefabView.transform.position.x, axis.y, prefabView.position.z);
//	}

//	public void StopScrolling()
//	{
//		SCR_UIConstants.Scrolling = false;
//	}

//	void OnMouseOver()
//	{
//		//if(allowScroll)
//		//{
//			Vector3 mouseInput = new Vector3(0.0f, Input.mousePosition.y * scrollSpeed, 0.0f);
//			ScrollOffset(Camera.main.ScreenToWorldPoint(mouseInput));
//		//}
//	}
//
//	void OnMouseDrag()
//	{
//		//if(allowScroll)
//		//{
//			Vector3 mouseInput = new Vector3(0.0f, Input.mousePosition.y, 0.0f);
//			Drag(Camera.main.ScreenToWorldPoint(mouseInput));
//		//}
//	}

//	void OnMouseExit()
//	{
//		//if(allowScroll)
//		//{
//			StopScrolling();
//		//}
//	}

	void Update()
	{
//		if(gridObjects.Count > 0)
//		{
//			foreach(GameObject prefab in gridObjects)
//			{
//				if(prefab != null)
//				{
//					if(prefab.GetComponent<SCR_PrefabButton>() != null)
//					{
//						SCR_PrefabButton button = prefab.GetComponent<SCR_PrefabButton>();
//
//						if(button.transform.position.y > upperScrollLimit.position.y)
//							button.Disable();
//						else if(button.transform.position.y < lowerScrollLimit.position.y)
//							button.Disable();
//						else
//							button.Enable();
//					}
//				}
//			}
//		}
	}

	public List<GameObject> GridObjects
	{
		get { return gridObjects; }
		set { gridObjects = value; }
	}
}