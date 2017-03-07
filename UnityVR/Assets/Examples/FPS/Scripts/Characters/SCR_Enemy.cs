using UnityEngine;
using System.Collections;

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
				private bool resetRepeating = false;

				/// <summary>
				/// Start this instance.
				/// </summary>
				protected void Start()
				{
					player = GameObject.FindGameObjectWithTag("Player").GetComponent<SCR_Player>();
					gun.InfiniteBullets = true;
					gun.FireRate = Random.Range(1.0f, 3.0f);
					InvokeRepeating("FireAtPlayer", 1.0f, gun.FireRate);
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
							InvokeRepeating("FireAtPlayer", 1.0f, gun.FireRate);
							resetRepeating = false;
						}
					}

					base.Update();
					gun.transform.LookAt(player.transform.position);
				}
			}
		}
	}
}