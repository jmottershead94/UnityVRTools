using UnityEngine;
using System.Collections;

public class SCR_GameCamera : MonoBehaviour 
{
	[SerializeField]	private Vector3 rotationSpeed = Vector3.zero;
	[SerializeField]	private bool clampVertical = true;
	private float rotationX = 0.0f;

	private void Rotation()
	{
		Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

		if(mouseMovement.x > 0.25f || mouseMovement.x < -0.25f)
		{
			Cursor.lockState = CursorLockMode.Locked;

			Vector3 rotate = new Vector3(0.0f, mouseMovement.x * rotationSpeed.y, 0.0f);
			transform.Rotate(rotate);

			Cursor.lockState = CursorLockMode.None;
		}

		if(mouseMovement.y != 0.0f)
		{
			Cursor.lockState = CursorLockMode.Locked;

			rotationX += (mouseMovement.y * -rotationSpeed.x);
			if(clampVertical)
				rotationX = Mathf.Clamp(rotationX, -40.0f, 40.0f);
			transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);

			Cursor.lockState = CursorLockMode.None;
		}
	}

	private void PCControls()
	{
		if(Input.GetMouseButton(1))
			Rotation();

		if(Input.GetMouseButtonDown(1))
			transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
	}

	// Update is called once per frame
	void Update () 
	{
		PCControls();
	}

}