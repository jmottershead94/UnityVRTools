using UnityEngine;
using System.Collections;
using IndieJayVR.Examples.SpeedsterGunslinger;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace SpeedsterGunslinger
		{
			/// <summary>
			/// Class to store player data and functionality.
			/// </summary>
			public class SCR_Player : SCR_Character 
			{
				[Header ("Player Properties")]
				[SerializeField]	private Vector3 rotationSpeed = Vector3.zero;

				/// <summary>
				/// What happens when this character dies.
				/// </summary>
				override protected void onDead()
				{
					SCR_GameControl.IsGameOver = true;
				}

				/// <summary>
				/// Provides rotation controls for the player.
				/// </summary>
				void Rotation()
				{
					Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

					if(mouseMovement.x > 0.25f || mouseMovement.x < -0.25f)
					{
						Vector3 rotate = new Vector3(0.0f, mouseMovement.x * rotationSpeed.y, 0.0f);
						transform.Rotate(rotate);
					}
				}

				/// <summary>
				/// Controls used for PC (so this won't apply to VR controls).
				/// </summary>
				void PCControls()
				{
					if(Input.GetMouseButton(1))
					{
						SCR_GameControl.LockCursor();
						Rotation();
						SCR_GameControl.UnlockCursor();
					}
				}

				/// <summary>
				/// Update this instance.
				/// </summary>
				new protected void Update () 
				{
					if(SCR_GameControl.IsGameOver)
						return;

					base.Update();
					PCControls();
				}
			}
		}
	}
}