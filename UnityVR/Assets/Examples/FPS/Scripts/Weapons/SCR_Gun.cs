using UnityEngine;
using System.Collections;

// Gun Information:
	// Damage.
	// Fire Rate.
	// Bullets.
	// If it is empty or not.

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// A class to hold information about guns and their functionality.
			/// </summary>
			public class SCR_Gun : MonoBehaviour 
			{
				[Header ("Gun Properties")]
				[SerializeField]	protected int damage = 25;
				[SerializeField]	protected float fireRate = 2.0f;
				[SerializeField]	protected float reloadTime = 1.0f;
				[SerializeField]	protected int ammo = 6;
				[SerializeField]	protected GameObject bulletPrefab = null;
				protected bool empty = false;
				protected bool infiniteBullets = false;

				/// <summary>
				/// Awake this instance.
				/// </summary>
				void Start()
				{
					reloadTime *= Time.timeScale;
				}

				/// <summary>
				/// Delays the reload for this gun.
				/// </summary>
				/// <returns>The delay.</returns>
				public IEnumerator ReloadDelay()
				{
					yield return new WaitForSeconds(reloadTime);
					Reload();
				}

				/// <summary>
				/// Reload this gun.
				/// </summary>
				void Reload()
				{
					ammo = 6;

					if(empty)
						empty = false;
				}

				/// <summary>
				/// Fires this gun.
				/// </summary>
				public void Fire()
				{
					if(!empty)
						SpawnBullet();
					else
						StartCoroutine(ReloadDelay());
				}

				/// <summary>
				/// Spawns the bullet.
				/// </summary>
				void SpawnBullet()
				{
					if(ammo <= 0)
					{
						empty = true;
						return;
					}

					GameObject bulletGameObject = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
					SCR_Bullet bullet = bulletGameObject.GetComponent<SCR_Bullet>();
					bullet.Initialise(this, damage, transform.forward);

					if(!infiniteBullets)
						ammo--;
				}

				/// <summary>
				/// Gets the fire rate.
				/// </summary>
				/// <value>The fire rate.</value>
				public float FireRate
				{
					get { return fireRate; }
				}

				/// <summary>
				/// Gets or sets the ammo.
				/// </summary>
				/// <value>The ammo.</value>
				public int Ammo
				{
					get { return ammo; }
					set { ammo = value; }
				}

				/// <summary>
				/// Gets or sets a value indicating whether this gun has any ammo in its' clip.
				/// </summary>
				/// <value><c>true</c> if this gun's clip is empty; otherwise, <c>false</c>.</value>
				public bool IsClipEmpty
				{
					get { return empty; }
					set { empty = value; }
				}

				/// <summary>
				/// Gets or sets a value indicating whether this gun has infinite bullets.
				/// </summary>
				/// <value><c>true</c> if this gun has infinite bullets; otherwise, <c>false</c>.</value>
				public bool InfiniteBullets
				{
					get { return infiniteBullets; }
					set { infiniteBullets = value; }
				}
			}
		}
	}
}
