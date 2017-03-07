using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// Class to store player data and functionality.
			/// </summary>
			public class SCR_Player : SCR_Character 
			{
				[Header ("Player Properties")]
				[SerializeField]	private Vector3 rotationSpeed = Vector3.zero;
				[SerializeField]	private float speedFactor = 0.15f;
				private bool timeStop = false;
				private float segment = 0.0f;
				private Text ammoDisplay = null;
				private Image ammoBar = null;
				private SCR_Crosshair crosshair = null;

				/// <summary>
				/// Awake this instance.
				/// </summary>
				new protected void Awake()
				{
					base.Awake();

					GameObject cursor = transform.FindChild("Cursor").gameObject;
					if(cursor != null)
						crosshair = cursor.GetComponent<SCR_Crosshair>();

					Time.timeScale = speedFactor;
					ammoDisplay = GameObject.Find("Ammo Display").GetComponent<Text>();
					ammoBar = GameObject.Find("Ammo Bar").GetComponent<Image>();

				}

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
				/// Stops the time.
				/// </summary>
				void StopTime()
				{
					Time.timeScale = 0.0f;
					timeStop = true;
				}

				/// <summary>
				/// Resumes the time.
				/// </summary>
				void ResumeTime()
				{
					Time.timeScale = speedFactor;
					timeStop = false;
				}

				/// <summary>
				/// Controls used for PC (so this won't apply to VR controls).
				/// </summary>
				void PCControls()
				{
					if(Input.GetKeyDown(KeyCode.Escape))
					{
						if(!SCR_GameControl.IsPaused)
							SCR_GameControl.Pause();
						else
							SCR_GameControl.UnPause(speedFactor);
					}

					if(Input.GetMouseButton(1))
					{
						SCR_GameControl.LockCursor();
						Rotation();
						SCR_GameControl.UnlockCursor();
					}

					ammoDisplay.text = "Ammo x" + gun.Ammo;

					if(gun.Ammo > 0)
					{
						segment = (1.0f / 6.0f) * gun.Ammo;
						ammoBar.transform.localScale = new Vector3(segment, ammoBar.transform.localScale.y, 1.0f);
					}
					else
					{
						ammoBar.transform.localScale = new Vector3(0.0f, ammoBar.transform.localScale.y, 1.0f);
					}

					if(SCR_GameControl.IsPaused)
						return;

					gun.transform.LookAt(crosshair.transform.position);

					if(Input.GetMouseButtonDown(0))
						gun.Fire();

					if(Input.GetKeyDown(KeyCode.R))
						StartCoroutine(gun.ReloadDelay());

					if(Input.GetKeyDown(KeyCode.P))
					{
						if(timeStop)
							ResumeTime();
						else
							StopTime();
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