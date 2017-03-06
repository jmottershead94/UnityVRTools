using UnityEngine;
using System.Collections;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// A class to store enemy data and provide enemy functionality.
			/// </summary>
			public class SCR_Enemy : SCR_Character 
			{
				/// <summary>
				/// What happens when this character dies.
				/// </summary>
				override protected void onDead()
				{
					Destroy(gameObject);
				}
			}
		}
	}
}