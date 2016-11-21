using UnityEngine;
using System.Collections;

public class SCR_RotatePanelButton : SCR_3DButton 
{

	/* Attributes. */
	[Header ("Panel Control")]
	[SerializeField]	private Vector3 rotationValue = Vector3.zero;
	private SCR_3DMenu menu = null;

	/* Methods. */
	new void Awake()
	{

		base.Awake();

		menu = GameObject.Find("PRE_3DMenu").GetComponent<SCR_3DMenu>();

	}

	/* This will allow us to define a specific button response. */
	override protected void ButtonPressResponse()
	{

		/* Rotate all of the panels. */
		menu.transform.FindChild("Panels").Rotate(rotationValue);

		if(name == "Right Arrow")
		{
			menu.Panels[menu.CurrentPanelIndex].InFocus = false;

			if(menu.CurrentPanelIndex < (menu.Panels.Count - 1))
			{
				//menu.Panels[menu.CurrentPanelIndex].InFocus = false;
				menu.CurrentPanelIndex++;
			}
			else
			{
				menu.CurrentPanelIndex = 0;
			}
		}
		else
		{
			menu.Panels[menu.CurrentPanelIndex].InFocus = false;

			if(menu.CurrentPanelIndex > 0)
			{
				menu.CurrentPanelIndex--;
			}
			else
			{
				menu.CurrentPanelIndex = (menu.Panels.Count - 1);
			}
		}

	}
}
