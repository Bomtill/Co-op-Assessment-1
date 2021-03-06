using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
	//This character movement input class is an example of how to get input from a keyboard to control the character;
    public class CharacterKeyboardInput : CharacterInput
    {
		public string horizontalInputAxis = "Horizontal";
		public string verticalInputAxis = "Vertical";
		public KeyCode activateKey = KeyCode.E;
		public KeyCode powerKey = KeyCode.Q;

		//If this is enabled, Unity's internal input smoothing is bypassed;
		public bool useRawInput = true;

        public override float GetHorizontalMovementInput()
		{
			if (useRawInput)
				return Input.GetAxisRaw(horizontalInputAxis);
			else
				return Input.GetAxis(horizontalInputAxis);
		}

		public override float GetVerticalMovementInput()
		{
			if (useRawInput)
				return Input.GetAxisRaw(verticalInputAxis);
			else
				return Input.GetAxis(verticalInputAxis);

		}

		public override bool IsPowerKeyPressed()
        {
			
			return Input.GetKeyDown(powerKey);
        }
        public override bool IsPowerKeyReleased()
        {
			
			return Input.GetKeyUp(powerKey);
        }
        public override bool IsActivateKeyPressed()
        {
			return Input.GetKeyDown(activateKey);
        }
    }
}
