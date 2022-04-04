using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
    public class PlayerActivate : Controller
    {
        protected CharacterInput characterInput;
        // get inputs from either player
        PlayerPowers powerClass;
        
        public static event Action InteractEvent;
        //public static event Action PowerEvent;

       
        // Start is called before the first frame update
        void Start()
        {
            powerClass = GetComponent<PlayerPowers>();
            characterInput = GetComponent<CharacterInput>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleActivateInputs();
            HandlePowerInput();
            // activate power.
        }

        private void HandleActivateInputs()
        {
            bool newActivatePressed = IsActivateKeyPressed();
            if (newActivatePressed)
            {
                InteractEvent?.Invoke();
            }
        }

        private void HandlePowerInput()
        {
            bool newPowerPressed = IsPowerKeyPressed();
            bool newPowerReleased = IsPowerKeyReleased();
            if (newPowerPressed) powerClass.ActivatePower();
            if (newPowerReleased) powerClass.DeactivatePower();
        }

        protected virtual bool IsActivateKeyPressed()
        {
            if (characterInput == null) return false;
            return characterInput.IsActivateKeyPressed();
        }
        protected virtual bool IsPowerKeyPressed()
        {
            if(characterInput == null) return false;
            return characterInput.IsPowerKeyPressed();
        }
        protected virtual bool IsPowerKeyReleased()
        {
            if (characterInput == null) return false;
            return characterInput.IsPowerKeyReleased();
        }


        #region Null input checks
        public override Vector3 GetVelocity()
        {
            throw new System.NotImplementedException();
        }

        public override Vector3 GetMovementVelocity()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsGrounded()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
