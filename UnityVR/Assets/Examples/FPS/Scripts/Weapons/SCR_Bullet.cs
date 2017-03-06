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
				[SerializeField]	private float lifeSpan = 100.0f;
				private float currentLifeTime = 0.0f;
				private int damage = 25;
				private Vector3 direction = Vector3.zero;

				/// <summary>
				/// Initialise the specified gunDamage and dir.
				/// </summary>
				/// <param name="gunDamage">Gun damage, the damage this bullet should inflict.</param>
				/// <param name="dir">Dir, the direction of the bullet.</param>
				public void Initialise(int gunDamage, Vector3 dir)
				{
					damage = gunDamage;
					currentLifeTime = 0.0f;
					direction = dir;
				}

				/// <summary>
				/// Provides a way of monitoring how long these bullets are alive for.
				/// </summary>
				/// <returns>The life time counter.</returns>
				IEnumerator LifeTimeCounter()
				{
					yield return new WaitForSeconds(1.0f);
					currentLifeTime++;
				}

				/// <summary>
				/// Raises the collision enter event.
				/// </summary>
				/// <param name="collision">Collision.</param>
				void OnCollisionEnter(Collision collision)
				{
					Debug.Log("Hit something!");

					SCR_Character character = collision.gameObject.GetComponent<SCR_Character>();

					if(character != null)
					{
						Debug.Log("Hit a character!");

						Destroy(gameObject);
						character.Health -= damage; 
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
					Vector3 velocity = (direction * speed) * Time.deltaTime;
					transform.position += velocity;

					// Keep the bullet life time counter going.
					StartCoroutine(LifeTimeCounter());

					// If the bullet has lived for too long, destroy it.
					if(currentLifeTime >= lifeSpan)
						Destroy(gameObject);
				}
			}
		}
	}
}