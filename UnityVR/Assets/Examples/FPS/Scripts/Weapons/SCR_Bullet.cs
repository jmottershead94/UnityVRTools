using UnityEngine;
using System.Collections;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// A class to store information about bullets and bullet functionality.
			/// </summary>
			public class SCR_Bullet : MonoBehaviour 
			{
				[Header ("Bullet Properties")]
				[SerializeField]	private float speed = 50.0f;
				[SerializeField]	private float lifeSpan = 20.0f;
				private SCR_Gun gun = null;
				[SerializeField]	private float currentLifeTime = 0.0f;
				private int damage = 25;
				private Vector3 direction = Vector3.zero;

				/// <summary>
				/// Initialise the specified gunDamage and dir.
				/// </summary>
				/// <param name="owningGun">Owning gun, the gun that this bullet has been fired from.</param>
				/// <param name="gunDamage">Gun damage, the damage this bullet should inflict.</param>
				/// <param name="dir">Dir, the direction of the bullet.</param>
				public void Initialise(SCR_Gun owningGun, int gunDamage, Vector3 dir)
				{
					gun = owningGun;
					damage = gunDamage;
					currentLifeTime = 0.0f;
					direction = dir;
					InvokeRepeating("LifeTimeCounter", 1.0f, 1.0f);
				}

				/// <summary>
				/// Increments the life counter for the bullet.
				/// </summary>
				void LifeTimeCounter()
				{
					currentLifeTime++;
				}

				/// <summary>
				/// Raises the trigger enter event.
				/// </summary>
				/// <param name="collider">Collider.</param>
				void OnTriggerEnter(Collider collider)
				{
					Transform parentTransform = transform;

					// If the gun still exists (i.e. the character is still alive).
					// Otherwise, the transform will be set to itself.
					if(gun != null)
						parentTransform = gun.transform.parent;

					// If this bullet has hit a character.
					if(collider.tag == "MainCamera" || collider.tag == "Enemy")
					{
						SCR_Character character = collider.gameObject.GetComponent<SCR_Character>();

						// If the character is not itself OR if the character that fired this bullet is currently dead.
						if(character.transform != parentTransform || transform == parentTransform)
						{
							character.Health -= damage; 
							Destroy(gameObject);
						}
					}
				}

				/// <summary>
				/// Update this instance.
				/// </summary>
				void Update()
				{
					if(SCR_GameControl.IsGameOver || SCR_GameControl.IsPaused)
						return;

					// Move the bullet forward.
					Vector3 velocity = (direction * speed) * Mathf.Pow(SCR_GameControl.SpeedFactor, 3.0f);
					transform.position += velocity;

					// If the bullet has lived for too long, destroy it.
					if(currentLifeTime >= lifeSpan)
					{
						Debug.Log("Die bullet!");
						Destroy(gameObject);
					}
				}
			}
		}
	}
}