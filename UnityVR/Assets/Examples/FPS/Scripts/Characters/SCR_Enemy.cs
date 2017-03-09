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
				private SCR_Player player = null;
				private bool setupFireRate = false;
				private bool resetRepeating = false;

				/// <summary>
				/// Start this instance.
				/// </summary>
				protected void Start()
				{
					gun = transform.FindChild ("PRE_Gun").GetComponent<SCR_Gun> ();
					gun.InfiniteBullets = true;
					gun.FireRate = Random.Range(1.0f, 6.0f);
					InvokeRepeating("FireAtPlayer", SCR_GameControl.StartDelayTime, gun.FireRate * (1.0f / SCR_GameControl.SpeedFactor));
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
				/// Update this instance.
				/// </summary>
				new protected void Update()
				{
					GameObject playerObject = GameObject.FindGameObjectWithTag ("MainCamera");

					if (playerObject == null || !SCR_Player.IsReady)
						return;
					else if (SCR_Player.IsReady) 
						player = playerObject.GetComponent<SCR_Player> ();

					base.Update();
					gun.transform.LookAt(player.transform.position);

					if(SCR_GameControl.IsGameOver || SCR_GameControl.IsPaused || player.HasStoppedTime)
					{
						resetRepeating = true;
						CancelInvoke();
						return;
					}
					else if(!player.HasStoppedTime)
					{
						if(resetRepeating)
						{
							InvokeRepeating("FireAtPlayer", 1.0f * Time.timeScale, gun.FireRate);
							resetRepeating = false;
						}
					}


				}
			}
		}
	}
}