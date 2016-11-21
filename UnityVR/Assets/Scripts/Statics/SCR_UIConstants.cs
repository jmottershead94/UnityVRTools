using UnityEngine;
using System.Collections;

public static class SCR_UIConstants
{

	/* Attributes. */
	private static Vector2 LEFT_OF_SCREEN = new Vector2(Camera.main.pixelWidth * 0.25f, Camera.main.pixelHeight * 0.5f);
	private static Vector2 RIGHT_OF_SCREEN = new Vector2(Camera.main.pixelWidth * 0.75f, Camera.main.pixelHeight * 0.5f);
	private static Vector2 ON_SCREEN = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
	private static Vector2 OFF_SCREEN = new Vector2(Screen.width * 0.5f, Screen.height * 2.0f);

	/* Methods. */
	public static Vector2 LeftOfScreen
	{
		get { return LEFT_OF_SCREEN; }
	}

	public static Vector2 RightOfScreen
	{
		get { return RIGHT_OF_SCREEN; }
	}

	public static Vector2 OnScreen
	{
		get { return ON_SCREEN; }
	}

	public static Vector2 OffScreen
	{
		get { return OFF_SCREEN; }
	}

}