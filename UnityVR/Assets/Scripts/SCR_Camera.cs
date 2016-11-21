using UnityEngine;
using System.Collections;

/* Look into the SteamVR class. */
public class SCR_Camera : MonoBehaviour
{

	/* Attributes. */
	[SerializeField]	private Vector3 speed = Vector3.zero;

	/* Methods. */
	void Controls()
	{

		/* Maybe pushing up on the touch pad? */
		if(Input.GetKey(KeyCode.W))
		{

			transform.Translate(0.0f, 0.0f, speed.z);

			//transform.position.z+=speed.z;

		}

		/* Maybe pushing left on the touch pad? */
		if(Input.GetKey(KeyCode.A))
		{

			transform.Translate(speed.x * -1.0f, 0.0f, 0.0f);

		}

		/* Maybe pushing down on the touch pad? */
		if(Input.GetKey(KeyCode.S))
		{

			transform.Translate(0.0f, 0.0f, speed.z * -1.0f);

		}

		/* Maybe pushing right on the touch pad? */
		if(Input.GetKey(KeyCode.D))
		{

			transform.Translate(speed.x, 0.0f, 0.0f);

		}

		/* If the users head is up. */
		if(Input.GetKey(KeyCode.E))
		{

			transform.Translate(0.0f, speed.y, 0.0f);

		}

		/* If the users head is down. */
		if(Input.GetKey(KeyCode.Q))
		{

			transform.Translate(0.0f, speed.y * -1.0f, 0.0f);

		}

	}
	
	// Update is called once per frame
	void Update () 
	{

		Controls();

	}

}