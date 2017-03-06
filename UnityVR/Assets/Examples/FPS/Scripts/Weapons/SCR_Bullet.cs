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
				[SerializeField]	private float speed = 10.0f;
				[SerializeField]	private float lifeSpan = 100.0f;
				private float currentLifeTime = 0.0f;
				private int damage = 25;
				private Vector3 direction = Vector3.zero;

				/// <summary>
				/// Initialise the specified gunDamage.
				/// </summary>
				/// <param name="gunDamage">Gun damage.</param>
				public void Initialise(int gunDamage, Vector3 dir)
				{
					damage = gunDamage;
					currentLifeTime = 0.0f;

					GameObject cursor = GameObject.Find("Cursor");

					if(cursor != null)
					{
						SCR_Crosshair crosshair = cursor.GetComponent<SCR_Crosshair>();
						//direction = transform.forward;
//						if(crosshair.RayHitSomething)
//							direction = crosshair.Raycast.point - transform.position;
//						else
//						{
//							Vector3 crossHairEnd = new Vector3(crosshair.transform.position.x, crosshair.transform.position.y, crosshair.transform.position.z + crosshair.RayDistance);
//							direction = crossHairEnd - transform.position;
//						}
//
//						direction.Normalize();
					}

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