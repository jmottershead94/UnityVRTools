using UnityEngine;
using System.Collections;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// Destroys this game object.
			/// </summary>
			public class SCR_Destroy : MonoBehaviour 
			{
				/// <summary>
				/// Start this instance.
				/// </summary>
				void Start () 
				{
					Destroy(gameObject);
				}
			}
		}
	}
}