using UnityEngine;
using System.Collections;

public class SCR_Player : MonoBehaviour 
{

	[SerializeField]	private Vector3 rotationSpeed = Vector3.zero;
	[SerializeField]	private bool clampVertical = true;
	private float rotationX = 0.0f;

	private void Rotation()
	{
		Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

		if(mouseMovement.x > 0.25f || mouseMovement.x < -0.25f)
		{
			Vector3 rotate = new Vector3(0.0f, mouseMovement.x * rotationSpeed.y, 0.0f);
			transform.Rotate(rotate);
		}

		if(mouseMovement.y != 0.0f)
		{
			rotationX += (mouseMovement.y * -rotationSpeed.x);
			if(clampVertical)
				rotationX = Mathf.Clamp(rotationX, -40.0f, 40.0f);
			transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
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

		if(Input.GetMouseButtonDown(1))
			transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
	}

	// Update is called once per frame
	void Update () 
	{
		PCControls();
	}

}