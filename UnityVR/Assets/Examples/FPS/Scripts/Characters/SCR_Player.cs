﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
				[SerializeField]	private float movementSpeed = 1.0f;
				private float crouchHeight = 0.0f;
				private float originalHeight = 3.0f;
				private bool timeStop = false;
				private float ammoSegment = 0.0f, healthSegment = 0.0f;
				private Transform cam = null;
				private Text timer = null;
				private Text ammoDisplay = null;
				private Image ammoBar = null;
				private Image healthBar = null;
				new private Rigidbody rigidbody = null;
				private SCR_Crosshair crosshair = null;
				private SCR_VRController rightController = null;
				private SCR_VRController leftController = null;
				private static bool start = false;
				private static float time = 0.0f;
				private TextMesh healthText = null, ammoText = null, timeText = null;
				private float startingY = 0.0f;

				/// <summary>
				/// Awake this instance.
				/// </summary>
				new protected void Awake()
				{
					base.Awake();

					if(SCR_GameControl.PlayerTimes == null)
					{
						SCR_GameControl.PlayerTimes = new List<float>();
						Debug.Log("Initialising times list");
					}
					
					ammoDisplay = GameObject.Find("Ammo Display").GetComponent<Text>();
					ammoBar = GameObject.Find("Ammo Bar").GetComponent<Image>();
					healthBar = GameObject.Find("Health Bar").GetComponent<Image>();
					timer = GameObject.Find ("Timer").GetComponent<Text> ();
					originalHeight = transform.position.y;
					crouchHeight = transform.position.y - crouch;
					rigidbody = GetComponent<Rigidbody>();

					time = 0.0f;
					start = false;
					InvokeRepeating ("Timer", 1.0f, 1.0f);
					StartCoroutine (StartingDelay ());

					SCR_GameControl.SpeedFactor = speedFactor;
				}

				/// <summary>
				/// Timer this instance.
				/// </summary>
				void Timer()
				{
					time += 0.5f;
					timer.text = "Time " + time;

					if (timeText != null)
						timeText.text = time.ToString();
				}

				/// <summary>
				/// Startings the delay.
				/// </summary>
				/// <returns>The delay.</returns>
				IEnumerator StartingDelay()
				{
					yield return new WaitForSeconds (SCR_GameControl.StartDelayTime);

					startingY = transform.position.y;

					GameObject cursor = GameObject.Find("Cursor");
					if(cursor != null)
						crosshair = cursor.GetComponent<SCR_Crosshair>();
					
					GameObject camera = GameObject.Find("Camera (eye)");
					if(camera != null)
						gun = camera.transform.FindChild("PRE_Gun").GetComponent<SCR_Gun> ();

					GameObject hpText = GameObject.Find ("Health Text");
					if (hpText != null)
						healthText = hpText.GetComponent<TextMesh> ();

					GameObject aText = GameObject.Find ("Ammo Text");
					if (aText != null)
						ammoText = aText.GetComponent<TextMesh> ();

					GameObject tText = GameObject.Find ("Timer Text");
					if (tText != null)
						timeText = tText.GetComponent<TextMesh> ();

					cam = camera.transform;
					start = true;
					gun.FireRate = speedFactor;
				}

				/// <summary>
				/// Delays the game restart.
				/// </summary>
				/// <returns>The delay.</returns>
				IEnumerator RestartDelay()
				{
					yield return new WaitForSeconds(deathTimer);

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
					Time.timeScale = 1.0f;
					timeStop = false;
				}

				/// <summary>
				/// Updates for the player UI.
				/// </summary>
				void UIUpdates()
				{
					if (gun == null)
						return;
					
					ammoDisplay.text = "Ammo x" + gun.Ammo;

					ammoSegment = (1.0f / gun.MaximumAmmo) * gun.Ammo;
					ammoBar.transform.localScale = new Vector3(ammoSegment, ammoBar.transform.localScale.y, 1.0f);

					healthSegment = (1.0f / maximumHealth) * health;
					healthBar.transform.localScale = new Vector3(healthSegment, healthBar.transform.localScale.y, 1.0f);

					if (timeText == null) {
						GameObject tText = GameObject.Find ("Timer Text");
						if (tText != null)
							timeText = tText.GetComponent<TextMesh> ();
					}
				}

				/// <summary>
				/// Controls used for PC (so this won't apply to VR controls).
				/// </summary>
				void PCControls()
				{
					//if(!timeStop)
					//{
						if(Input.GetKeyDown(KeyCode.Escape))
						{
							if(!SCR_GameControl.IsPaused)
								SCR_GameControl.Pause();
							else
								SCR_GameControl.UnPause(1.0f);
						}
					//}

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

					//if(Input.GetKeyDown(KeyCode.P))
					//{
					//	if(timeStop)
					//		ResumeTime();
					//	else
					//		StopTime();
					//}

					if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.DownArrow))
						transform.position = new Vector3(transform.position.x, crouchHeight, transform.position.z);
					else
						transform.position = new Vector3(transform.position.x, originalHeight, transform.position.z);

					float horizontal = Input.GetAxis("Horizontal");
					float vertical = Input.GetAxis("Depth");

					Vector3 axis = new Vector3(horizontal * movementSpeed, 0.0f, vertical * movementSpeed);
					axis = cam.TransformDirection(axis);
					rigidbody.velocity = axis;
				}

				bool AssignControllers()
				{
					GameObject leftControllerObject = GameObject.Find("Controller (left)");
					GameObject rightControllerObject = GameObject.Find("Controller (right)");

					if(leftControllerObject == null || rightControllerObject == null)
						return false;

					leftController = leftControllerObject.GetComponent<SCR_VRController>();
					rightController = rightControllerObject.GetComponent<SCR_VRController>();

					if ((leftController == null || rightController == null))
						return false;

					//gun = transform.FindChild ("PRE_Gun").GetComponent<SCR_Gun> ();
					GameObject camera = GameObject.Find("Camera (eye)");
					if(camera != null)
						gun = camera.transform.FindChild("PRE_Gun").GetComponent<SCR_Gun> ();

					return true;
				}

				/// <summary>
				/// Controls used for VR.
				/// </summary>
				void VRControls()
				{
					if (!AssignControllers ())
						return;

					// Pausing the game.
					if(leftController.TriggerPressed())
					{
						if(!SCR_GameControl.IsPaused)
							SCR_GameControl.Pause();
						else
							SCR_GameControl.UnPause(1.0f);
					}

					if(SCR_GameControl.IsPaused)
						return;

					gun.transform.LookAt(crosshair.transform.position);

					if(rightController.TriggerPressed())
						gun.Fire();

					if(rightController.UpPressed())
						StartCoroutine(gun.ReloadDelay());

					//if(rightController.RightPressed())
					//{
					//	if(timeStop)
					//		ResumeTime();
					//	else
					//		StopTime();
					//}

					float horizontal = 0.0f;
					float vertical = 0.0f;

					if (leftController.UpPressed ()) 
					{
						vertical = 1.0f;
						Debug.Log ("Should move forward...");
					}
					else if(leftController.DownPressed())
						vertical= -1.0f;

					if(leftController.RightPressed())
						horizontal = 1.0f;
					else if(leftController.LeftPressed())
						horizontal = -1.0f;

					Vector3 axis = new Vector3(horizontal * movementSpeed, 0.0f, vertical * movementSpeed);
					axis = cam.TransformDirection(axis);

					if(rigidbody == null)
						rigidbody = GetComponent<Rigidbody>();
					
					//rigidbody.velocity = axis;
					Vector3 velocity = axis * Time.deltaTime;
					velocity.y = 0.0f;
					GameObject.Find("PRE_VRPlayer").transform.position += velocity;
					Debug.Log (rigidbody.velocity);
				}

				/// <summary>
				/// Update this instance.
				/// </summary>
				new protected void Update () 
				{
					AssignControllers ();
					UIUpdates();
				
					if(SCR_GameControl.IsGameOver || !start)
						return;

					base.Update();
					//PCControls();
					VRControls();

					// Update the player's health, ammo and time.
					healthText.text = health.ToString ();
					ammoText.text = gun.Ammo.ToString ();

				}

				/// <summary>
				/// Gets a value indicating whether this instance has stopped time.
				/// </summary>
				/// <value><c>true</c> if this instance has stopped time; otherwise, <c>false</c>.</value>
				public bool HasStoppedTime
				{
					get { return timeStop; }
				}

				/// <summary>
				/// Gets a value indicating whether this instance is ready.
				/// </summary>
				/// <value><c>true</c> if this instance is ready; otherwise, <c>false</c>.</value>
				public static bool IsReady
				{
					get { return start; }
				}

				/// <summary>
				/// Gets the time.
				/// </summary>
				/// <value>The time.</value>
				public static float PlayersTime
				{
					get { return time; }
				}
			}
		}
	}
}