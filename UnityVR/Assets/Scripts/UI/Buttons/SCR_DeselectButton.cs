using UnityEngine;
using System.Collections;

public class SCR_DeselectButton : SCR_3DButton 
{
	override public void ButtonPressResponse()
	{
		SCR_SceneEditor.DeselectAllObjects();
	}
}