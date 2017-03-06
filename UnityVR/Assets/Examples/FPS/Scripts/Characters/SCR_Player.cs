using UnityEngine;
using System.Collections;

public class SCR_Player : MonoBehaviour 
{
	[SerializeField]	private Vector3 rotationSpeed = Vector3.zero;

	private void Rotation()
	{
		Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

		if(mouseMovement.x > 0.25f || mouseMovement.x < -0.25f)
		{
			Vector3 rotate = new Vector3(0.0f, mouseMovement.x * rotationSpeed.y, 0.0f);
			transform.Rotate(rotate);
		}
	}
	
	private void PCControls()
	{
		if(Input.GetMouseButton(1))
		{
			Cursor.lockState = CursorLockMode.Locked;
			Rotation();
			Cursor.lockState = CursorLockMode.None;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		PCControls();
	}

}