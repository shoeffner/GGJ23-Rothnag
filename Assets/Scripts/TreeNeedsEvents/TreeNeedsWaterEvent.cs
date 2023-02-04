using System;
using Input;
using UnityEngine.InputSystem;

namespace Rothnag.TreeNeedsEvents
{
    // TODO: implement properly
    public sealed class TreeNeedsWaterEvent : TreeNeedsEvent
    {
        private RothnagInputActionAsset.CharacterActionMapActions _instanceCharacterActionMap;

        private void Awake()
            => _instanceCharacterActionMap = InputProvider.instance.CharacterActionMap;

        private void OnEnable()
        {
            _instanceCharacterActionMap.CheatEventProgress.performed += Progress;
        }

        private void OnDisable()
        {
            _instanceCharacterActionMap.CheatEventProgress.performed -= Progress;
        }

        private void Progress(InputAction.CallbackContext _) => progress += 1;
    }
}