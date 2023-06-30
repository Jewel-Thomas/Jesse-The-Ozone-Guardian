using UnityEngine;

using  UnityEngine.InputSystem;


namespace starterAssets
{
    public class startAssetInput : MonoBehaviour 
    {
        [Header("Character Input Values")]
        public bool aim;

        public void OnAim(InputValue value)
        {
            AimInput(value.isPressed);
        }

        public void AimInput(bool newAimState)
        {
            aim = newAimState;
        }
    }  
}
