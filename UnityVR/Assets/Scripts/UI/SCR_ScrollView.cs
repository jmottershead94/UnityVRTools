using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_ScrollView : MonoBehaviour 
{
	[SerializeField]	private bool allowScroll = false;
	[SerializeField]	private float scrollSpeed = 1.25f;
	private float offset;
	private Transform upperScrollLimit, lowerScrollLimit;
	private Transform scrollView;
	private List<GameObject> scrollItems;

	// Use this for initialization
	void Start () 
	{
		//if(!SCR_SceneEditor.InVREditor)
		//	return;

		upperScrollLimit = transform.FindChild("Upper Scroll Limit");
		lowerScrollLimit = transform.FindChild("Lower Scroll Limit");
		scrollView = transform.FindChild("Scroll View");

		if(GetComponent<SCR_GridView>() != null)
			Initialise(GetComponent<SCR_GridView>().GridObjects);
	}

	public void Initialise(List<GameObject> items)
	{
		//if(!SCR_SceneEditor.InVREditor)
		//	return;

		scrollItems = items;
	}

	public void ScrollOffset(Vector3 inputPosition)
	{
		if(!allowScroll)
			return;

		SCR_UIConstants.Scrolling = true;
		offset = (scrollView.position.y - inputPosition.y);
	}

	public void Drag(Vector3 inputPosition)
	{
		if(!allowScroll)
			return;

		if(scrollItems.Count <= 0)
			return;

		float cursorPosition = inputPosition.y + offset;
		Vector3 movement = new Vector3(0.0f, cursorPosition, 0.0f);
		scrollView.position = new Vector3(scrollView.transform.position.x, cursorPosition * scrollSpeed, scrollView.position.z);

		Debug.Log("Offset = " + offset);
		Debug.Log("Scroll View Y = " + scrollView.position.y);
		Debug.Log("Input Y = " + movement.y);
	}

	public void StopScrolling()
	{
		if(!allowScroll)
			return;

		SCR_UIConstants.Scrolling = false;
	}

	void OnMouseOver()
	{
		//if(!SCR_SceneEditor.InVREditor)
		//	return;

		Vector3 mouseInput = new Vector3(0.0f, Input.mousePosition.y, 0.0f);
		ScrollOffset(Camera.main.ScreenToWorldPoint(mouseInput));
	}

	void OnMouseDrag()
	{
		//if(!SCR_SceneEditor.InVREditor)
		//	return;

		Vector3 mouseInput = new Vector3(0.0f, Input.mousePosition.y, 0.0f);
		Drag(Camera.main.ScreenToWorldPoint(mouseInput));
	}

	void OnMouseExit()
	{
		//if(!SCR_SceneEditor.InVREditor)
		//	return;

		StopScrolling();
	}

	// Update is called once per frame
	void Update () 
	{
		//if(!SCR_SceneEditor.InVREditor)
		//	return;

		if(scrollItems.Count > 0)
		{
			foreach(GameObject prefab in scrollItems)
			{
				if(prefab != null)
				{
					if(prefab.GetComponent<SCR_PrefabButton>() != null)
					{
						SCR_PrefabButton button = prefab.GetComponent<SCR_PrefabButton>();

						if(button.transform.position.y > upperScrollLimit.position.y)
							button.Disable();
						else if(button.transform.position.y < lowerScrollLimit.position.y)
							button.Disable();
						else
							button.Enable();
					}
				}
			}
		}
	}
}
