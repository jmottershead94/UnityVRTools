using UnityEngine;
using UnityEngine.SceneManagement;
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
				[SerializeField]	private float speedFactor = 0.05f;
				[SerializeField]	private float crouch = 3.0f;
				private float crouchHeight = 0.0f;
				private float originalHeight = 3.0f;
				private bool timeStop = false;
				private float ammoSegment = 0.0f, healthSegment = 0.0f;
				private Text ammoDisplay = null;
				private Image ammoBar = null;
				private Image healthBar = null;
				private SCR_Crosshair crosshair = null;
				private SCR_VRController rightController = null;
				private SCR_VRController leftController = null;

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
					healthBar = GameObject.Find("Health Bar").GetComponent<Image>();

					originalHeight = transform.position.y;
					crouchHeight = transform.position.y - crouch;
				}

				/// <summary>
				/// Delays the game restart.
				/// </summary>
				/// <returns>The delay.</returns>
				IEnumerator RestartDelay()
				{
					Time.timeScale = 1.0f;

					yield return new WaitForSeconds(deathTimer);

					Time.timeScale = speedFactor;
					SCR_GameControl.IsGameOver = false;
					SCR_GameControl.IsPaused = false;
					timeStop = false;

					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}

				/// <summary>
				/// What happens when this character dies.
				/// </summary>
				override protected void onDead()
				{
					SCR_GameControl.IsGameOver = true;
					StartCoroutine(RestartDelay());
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
				/// Updates for the player UI.
				/// </summary>
				void UIUpdates()
				{
					ammoDisplay.text = "Ammo x" + gun.Ammo;

					ammoSegment = (1.0f / gun.MaximumAmmo) * gun.Ammo;
					ammoBar.transform.localScale = new Vector3(ammoSegment, ammoBar.transform.localScale.y, 1.0f);

					healthSegment = (1.0f / maximumHealth) * health;
					healthBar.transform.localScale = new Vector3(healthSegment, healthBar.transform.localScale.y, 1.0f);
				}

				/// <summary>
				/// Controls used for PC (so this won't apply to VR controls).
				/// </summary>
				void PCControls()
				{
					if(!timeStop)
					{
						if(Input.GetKeyDown(KeyCode.Escape))
						{
							if(!SCR_GameControl.IsPaused)
								SCR_GameControl.Pause();
							else
								SCR_GameControl.UnPause(speedFactor);
						}
					}

					if(Input.GetMouseButton(1))
					{
						SCR_GameControl.LockCursor();
						Rotation();
						SCR_GameControl.UnlockCursor();
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

					if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.DownArrow))
						transform.position = new Vector3(transform.position.x, crouchHeight, transform.position.z);
					else
						transform.position = new Vector3(transform.position.x, originalHeight, transform.position.z);
				}

				/// <summary>
				/// Controls used for VR.
				/// </summary>
				void VRControls()
				{
					GameObject leftControllerObject = GameObject.Find("Controller (left)");
					GameObject rightControllerObject = GameObject.Find("Controller (right)");

					if(leftControllerObject == null || rightControllerObject == null)
						return;

					leftController = leftControllerObject.GetComponent<SCR_VRController>();
					rightController = rightControllerObject.GetComponent<SCR_VRController>();

					// Pausing the game.
					if(leftController.TriggerPressed())
					{
						if(!SCR_GameControl.IsPaused)
							SCR_GameControl.Pause();
						else
							SCR_GameControl.UnPause(speedFactor);
					}

					if(SCR_GameControl.IsPaused)
						return;

					gun.transform.LookAt(crosshair.transform.position);

					if(rightController.TriggerPressed())
						gun.Fire();

					if(rightController.UpPressed())
						StartCoroutine(gun.ReloadDelay());

					if(rightController.RightPressed())
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
					UIUpdates();
					PCControls();
					VRControls();
				}

				/// <summary>
				/// Gets a value indicating whether this instance has stopped time.
				/// </summary>
				/// <value><c>true</c> if this instance has stopped time; otherwise, <c>false</c>.</value>
				public bool HasStoppedTime
				{
					get { return timeStop; }
				}
			}
		}
	}
}