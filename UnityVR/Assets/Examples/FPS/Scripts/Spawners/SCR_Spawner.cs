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
				[SerializeField]	private bool execute = false;

				/// <summary>
				/// Awake this instance.
				/// </summary>
				void Awake()
				{
					// If we are using this in editor, do not execute.
					if(execute == false)
						return;

					GameObject go = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
					go.name = prefab.name;
					Destroy(gameObject);
				}
			}
		}
	}
}