using UnityEngine;
using System.Collections;
using Valve.VR;

namespace IndieJayVR
{
	namespace Examples
	{
		/// <summary>
		/// Provides access to Steam VR controls.
		/// </summary>
		public class SCR_VRController : MonoBehaviour 
		{
			private float deadCentre = 0.25f;
			private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;
			private SteamVR_TrackedObject trackedObject = null;
			private SteamVR_Controller.Device device = null;

			/// <summary>
			/// Awake this instance.
			/// </summary>
			void Awake()
			{
				trackedObject = GetComponent<SteamVR_TrackedObject>();
			}

			/// <summary>
			/// Update this instance.
			/// </summary>
			void Update()
			{
				device = SteamVR_Controller.Input((int)trackedObject.index);
			}

			/// <summary>
			/// Gets the device.
			/// </summary>
			/// <value>The device.</value>
			public SteamVR_Controller.Device Device
			{
				get { return device; }
			}

			/// <summary>
			/// Checks if the controller trigger has been pressed.
			/// </summary>
			/// <returns><c>true</c>, if the trigger was pressed, <c>false</c> otherwise.</returns>
			public bool TriggerPressed()
			{
				if(device != null)
					return device.GetPressDown(triggerButton);

				return false;
			}

			/// <summary>
			/// Checks if the controller trigger is being held down.
			/// </summary>
			/// <returns><c>true</c>, if the trigger was held, <c>false</c> otherwise.</returns>
			public bool TriggerHeld()
			{
				if(device != null)
					return device.GetPress(triggerButton);

				return false;
			}

			/// <summary>
			/// Checks if the controller trigger has been released.
			/// </summary>
			/// <returns><c>true</c>, if the trigger was released, <c>false</c> otherwise.</returns>
			public bool TriggerRelease()
			{
				if(device != null)
					return device.GetPressUp(triggerButton);

				return false;
			}

			/// <summary>
			/// If the user is pressing up on the track pad.
			/// </summary>
			/// <returns><c>true</c>, if up was pressed, <c>false</c> otherwise.</returns>
			public bool UpPressed()
			{
				if(device != null)
					return (device.GetAxis().y > deadCentre);

				return false;
			}

			/// <summary>
			/// If the user is pressing right on the track pad.
			/// </summary>
			/// <returns><c>true</c>, if right was pressed, <c>false</c> otherwise.</returns>
			public bool RightPressed()
			{
				if(device != null)
					return (device.GetAxis().x > deadCentre);

				return false;
			}

			/// <summary>
			/// If the user is pressing down on the track pad.
			/// </summary>
			/// <returns><c>true</c>, if down was pressed, <c>false</c> otherwise.</returns>
			public bool DownPressed()
			{
				if(device != null)
					return (device.GetAxis().y < -deadCentre);

				return false;
			}

			/// <summary>
			/// If the user is pressing left on the track pad.
			/// </summary>
			/// <returns><c>true</c>, if left was pressed, <c>false</c> otherwise.</returns>
			public bool LeftPressed()
			{
				if(device != null)
					return (device.GetAxis().x < -deadCentre);

				return false;
			}
		}
	}
}