using UnityEngine;

namespace Rothnag
{
    public sealed class CatchAnimal : Interactable
    {
        public GameObject owner;

        protected override void InteractionBehaviour()
        {
            PlayerInventory.instance.animal++;
            Destroy(owner);
        }
    }
}