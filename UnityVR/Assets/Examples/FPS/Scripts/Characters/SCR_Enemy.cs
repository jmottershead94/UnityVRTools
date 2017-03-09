using UnityEngine;
using System.Collections;
using IndieJayVR.Examples.FPS;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// A class to store enemy data and provide enemy functionality.
			/// </summary>
			public class SCR_Enemy : SCR_Character 
			{
				[SerializeField]	private float rayDistance = 100.0f;
				private SCR_Player player = null;
				private bool setupFireRate = false;
				private bool resetRepeating = false;
				private RaycastHit raycastTarget;
				private Ray ray;

				/// <summary>
				/// Start this instance.
				/// </summary>
				protected void Start()
				{
					gun = transform.FindChild ("PRE_Gun").GetComponent<SCR_Gun> ();
					gun.InfiniteBullets = true;
					gun.FireRate = Random.Range(1.0f, 6.0f);
					//InvokeRepeating("FireAtPlayer", SCR_GameControl.StartDelayTime, gun.FireRate * (1.0f / SCR_GameControl.SpeedFactor));
				}

				/// <summary>
				/// What happens when this character dies.
				/// </summary>
				override protected void onDead()
				{
					Destroy(gameObject);
				}

				/// <summary>
				/// Provides a delay for firing bullets.
				/// </summary>
				/// <returns>The rate.</returns>
				void FireAtPlayer()
				{
					gun.Fire();
				}

				/// <summary>
				/// Provides the enemy with the ability to have line of sight.
				/// </summary>
				void LineOfSight()
				{
					gun.transform.LookAt(player.transform.position);

					// Make the enemy look at the player.
					Vector3 tempDirection = player.transform.position - transform.position;
					tempDirection.Normalize();

					// Cast a ray from the enemy to the player.
					ray = Camera.main.ScreenPointToRay(transform.position);
					ray.origin = transform.position;
					ray.direction = tempDirection;
					Vector3 point = ray.origin + (tempDirection * rayDistance);

					Debug.DrawRay(ray.origin, ray.direction, Color.red);

					// If the ray has hit something.
					if (Physics.Raycast (ray, out raycastTarget, rayDistance)) 
					{
						// If the ray has hit the player.
						if (raycastTarget.collider.tag == "MainCamera") 
						{
							// Set the enemy to fire at the player.
							if(!player.HasStoppedTime)
							{
								if(resetRepeating)
								{
									InvokeRepeating("FireAtPlayer", gun.FireRate, gun.FireRate * (1.0f / SCR_GameControl.SpeedFactor) * 2.0f);
									resetRepeating = false;
								}
							}
						}
						// Otherwise, the ray has hit something else.
						else
						{
							if(!resetRepeating)
							{
								resetRepeating = true;
								CancelInvoke();
							}
						}
					}
				}

				/// <summary>
				/// Update this instance.
				/// </summary>
				new protected void Update()
				{
					GameObject playerObject = GameObject.FindGameObjectWithTag ("MainCamera");

					if (playerObject == null || !SCR_Player.IsReady)
						return;
					else if (SCR_Player.IsReady) 
						player = playerObject.GetComponent<SCR_Player> ();

					if(SCR_GameControl.IsGameOver || SCR_GameControl.IsPaused || player.HasStoppedTime)
						return;

					base.Update();
					LineOfSight();
				}
			}
		}
	}
}