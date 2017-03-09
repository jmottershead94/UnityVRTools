using UnityEngine;
using System.Collections;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// This will allow characters to spawn in via trigger events.
			/// </summary>
			public class SCR_TriggerSpawner : MonoBehaviour 
			{
				[SerializeField]	private GameObject prefab = null;
				private Transform spawn;

				/// <summary>
				/// Awake this instance.
				/// </summary>
				void Awake()
				{
					spawn = transform.FindChild("SpawnLocation");
				}

				/// <summary>
				/// Raises the trigger enter event.
				/// </summary>
				/// <param name="collider">Collider.</param>
				void OnTriggerEnter(Collider collider)
				{
					if(collider.tag == "MainCamera")
					{
						GameObject go = Instantiate(prefab, spawn.position, spawn.rotation) as GameObject;
						go.name = prefab.name;
						Destroy(gameObject);
					}
				}
			}
		}
	}
}