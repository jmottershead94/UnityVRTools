using UnityEngine;
using System.Collections;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace SpeedsterGunslinger
		{
			/// <summary>
			/// Provides access to common game functionality.
			/// </summary>
			public class SCR_GameControl
			{
				private static bool paused = false;
				private static bool gameOver = false;

				/// <summary>
				/// Locks the cusor.
				/// </summary>
				public static void LockCursor()
				{
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
				}

				/// <summary>
				/// Unlocks the cursor.
				/// </summary>
				public static void UnlockCursor()
				{
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}

				/// <summary>
				/// Gets or sets a value indicating if the game is paused.
				/// </summary>
				/// <value><c>true</c> if paused; otherwise, the game is running <c>false</c>.</value>
				public static bool IsPause
				{
					get { return paused; }
					set { paused = value; }
				}

				/// <summary>
				/// Gets or sets a value indicating is game over.
				/// </summary>
				/// <value><c>true</c> if the game is over; otherwise, the game is still going <c>false</c>.</value>
				public static bool IsGameOver
				{
					get{ return gameOver; }
					set{ gameOver = value; }
				}
			}
		}
	}
}