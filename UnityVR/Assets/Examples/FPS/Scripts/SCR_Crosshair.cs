using UnityEngine;
using System.Collections;

namespace IndieJayVR
{
	namespace Examples
	{
		namespace FPS
		{
			/// <summary>
			/// Provides crosshair functionality and will be updated with the players aim.
			/// </summary>
			public class SCR_Crosshair : MonoBehaviour 
			{
				[SerializeField]	private float rayDistance = 5.0f;
				[SerializeField]	private Color standardColour = Color.black;
				private bool rayHitSomething = false;
				private SpriteRenderer sprite = null;
				private Camera playerCamera = null;
				private RaycastHit hit;

				/// <summary>
				/// Awake this instance.
				/// </summary>
				void Awake()
				{
					sprite = GetComponent<SpriteRenderer>();				
					Cursor.visible = false;
				}

				/// <summary>
				/// Update this instance.
				/// </summary>
				void Update()
				{
					if (!SCR_Player.IsReady)
						return;
					else
					{
						GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
						if(camera != null)
							playerCamera = camera.GetComponent<Camera>();
					}
					
					//SCR_GameControl.FollowMouse(transform);
				}

				/// <summary>
				/// Provides a fixed update cycle, good for physics.
				/// </summary>
				void FixedUpdate()
				{
					rayHitSomething = Physics.Raycast (transform.position, transform.forward, out hit, rayDistance);
				}

				/// <summary>
				/// Gets a value indicating whether this the crosshair ray hit something.
				/// </summary>
				/// <value><c>true</c> if ray hit something; otherwise, <c>false</c>.</value>
				public bool RayHitSomething
				{
					get { return rayHitSomething; }
				}

				/// <summary>
				/// Gets the ray distance.
				/// </summary>
				/// <value>The ray distance.</value>
				public float RayDistance
				{
					get { return rayDistance; }
				}

				/// <summary>
				/// Gets the raycast.
				/// </summary>
				/// <value>The raycast.</value>
				public RaycastHit Raycast
				{
					get { return hit; }
				}
			}
		}
	}
}