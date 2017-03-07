using UnityEngine;
using System.Collections;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// A spawner class for spawning in prefabs.
			/// </summary>
			public class SCR_Spawner : MonoBehaviour 
			{
				[SerializeField]	private GameObject prefab = null;

				/// <summary>
				/// Awake this instance.
				/// </summary>
				void Awake()
				{
					GameObject go = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
					go.name = prefab.name;
					Destroy(gameObject);
				}
			}
		}
	}
}