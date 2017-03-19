using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// Handles big game events. I.e Victory conditions.
			/// </summary>
			public class SCR_GameEvents : MonoBehaviour 
			{
				/// <summary>
				/// Update this instance.
				/// </summary>
				void Update () 
				{
					// Check how many enemies there are in the scene.
					GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

					// If all of the enemies have been defeated, go to the next scene.
					if(enemies.Length <= 0)
					{
						int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
						SceneManager.LoadScene(nextLevel);
					}
				}
			}
		}
	}
}