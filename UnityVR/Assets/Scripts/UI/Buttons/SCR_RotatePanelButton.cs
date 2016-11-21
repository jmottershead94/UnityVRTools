using UnityEngine;
using System.Collections;

public class SCR_RotatePanelButton : SCR_3DButton 
{

	/* Attributes. */
	[Header ("Panel Control")]
	[SerializeField]	private Vector3 rotationValue = Vector3.zero;
	[SerializeField]	private Vector3 rotationSpeed = Vector3.zero;
	private SCR_3DMenu menu = null;
	private int currentRotation = 0;
	private const int targetAngle = 90;
	private bool shouldRotate = false;

	/* Methods. */
	new void Awake()
	{

		base.Awake();

		menu = GameObject.Find("PRE_3DMenu").GetComponent<SCR_3DMenu>();

	}

	void ArrowResponse()
	{

		if(name == "Right Arrow")
		{
			//destinationRotation = new Vector3(0.0f, 90.0f, 0.0f);
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

	/* This will allow us to define a specific button response. */
	override protected void ButtonPressResponse()
	{

		/* Rotate all of the panels. */
		menu.transform.FindChild("Panels").Rotate(rotationValue);

		ArrowResponse();

		/*shouldRotate = true;*/

	}

	void LerpRotation()
	{

		if(currentRotation < targetAngle)
		{

			currentRotation++;

			/* menu.transform.FindChild("Panels").Rotate(rotationSpeed); */
		}
		else
		{

			currentRotation = 0;


			shouldRotate = false;
		}

	}

//	new void Update()
//	{
//
//		base.Update();
//
//		if(shouldRotate)
//		{
//
//			LerpRotation();
//
//		}
//
//	}

	private bool LastPanel
	{
		get { return ((menu.CurrentPanelIndex == (menu.Panels.Count - 1)) || (menu.CurrentPanelIndex == 0)); }
	}

}