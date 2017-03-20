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
				/// What happens when the player beats a level.
				/// </summary>
				void Victory()
				{
					// Add the current time for the player into the list of times.
					SCR_GameControl.PlayerTimes.Add(SCR_Player.PlayersTime);

					// If we have reached the last level, write the times to a file.
					if(SceneManager.GetActiveScene().name == "SCN_Level6")
					{
						Debug.Log("Writing Times to File.");
						SCR_GameControl.WriteTimesToFile();
					}

					// Move onto the next level.
					int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
					SceneManager.LoadScene(nextLevel);
				}

				/// <summary>
				/// Update this instance.
				/// </summary>
				void Update () 
				{
					// Check how many enemies there are in the scene.
					GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

					// If all of the enemies have been defeated, go to the next scene.
					if(enemies.Length <= 0)
						Victory();
				}
			}
		}
	}
}