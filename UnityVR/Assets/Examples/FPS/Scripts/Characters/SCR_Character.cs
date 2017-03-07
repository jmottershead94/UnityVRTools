using UnityEngine;
using System.Collections;

// Characters can:
	// Fire their gun.
	// Die.
		// Health.

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// A class to store base character data and functionality.
			/// </summary>
			public class SCR_Character : MonoBehaviour 
			{
				[Header ("Character Properties")]
				[SerializeField]						protected int maximumHealth = 100;
				[SerializeField] [Range(0.0f, 5.0f)]	protected float deathTimer = 2.0f;
				protected int health = 100;
				protected bool dead = false;
				protected SCR_Gun gun = null;

				/// <summary>
				/// What happens when this character dies.
				/// </summary>
				protected virtual void onDead(){}

				/// <summary>
				/// Awake this instance.
				/// </summary>
				protected void Awake()
				{
					health = maximumHealth;

					Transform gunTransform = transform.FindChild("PRE_Gun");
					if(gunTransform.gameObject != null)
						gun = transform.FindChild("PRE_Gun").GetComponent<SCR_Gun>();
				}

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