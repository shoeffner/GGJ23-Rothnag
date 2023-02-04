using UnityEngine;

namespace Rothnag
{
    public abstract class Interactable : MonoBehaviour
    {
        /// <summary>
        /// triggered only if the player touches the trigger collider
        /// </summary>
        protected abstract void InteractionBehaviour();

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponentInParent<CharacterController>() != null
                || col.gameObject.GetComponentInParent<MapPlayer>() != null)
                InteractionBehaviour();
        }
    }
}