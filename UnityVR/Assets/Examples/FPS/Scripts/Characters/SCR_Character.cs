using UnityEngine;
using System.Collections;

// Characters can:
	// Fire their gun.
		// Gun Information:
			// Damage.
			// Fire Rate.
			// Bullets.
			// If it is empty or not.
	// Die.
		// Health.

namespace IndieJayVR
{
	namespace Examples
	{
		namespace SpeedsterGunslinger
		{
			/// <summary>
			/// A class to store base character data and functionality.
			/// </summary>
			public class SCR_Character : MonoBehaviour 
			{
				[Header ("Character Properties")]
				[SerializeField]						protected int health = 100;
				[SerializeField] [Range(0.0f, 5.0f)]	protected float deathTimer = 2.0f;
				protected bool dead = false;

				/// <summary>
				/// Virtual methods for inheriting classes to override.
				/// </summary>
				protected virtual void onDead(){}

				/// <summary>
				/// Update this instance.
				/// </summary>
				protected void Update()
				{
					if(health <= 0)
						dead = true;

					if(dead)
					{
						onDead();
						dead = false;
					}
				}

				/// <summary>
				/// Gets or sets the health.
				/// </summary>
				/// <value>The health.</value>
				public int Health
				{
					get { return health; }
					set { health = value; }
				}
			}
		}
	}
}