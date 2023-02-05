using System;
using UnityEngine;

namespace Rothnag
{
    public sealed class GroundCheck : MonoBehaviour
    {
        [Header("Dependencies")]
        public Collider2D footCollider;

        public Collider2D parentCollider;

        public bool isGrounded { get; private set; }
        private int _standingOn;

        private void OnEnable()
        {
            Physics2D.IgnoreCollision(footCollider, parentCollider);
        }

        private void OnDisable()
        {
            Physics2D.IgnoreCollision(footCollider, parentCollider, false);
        }

        private void OnTriggerEnter2D(Collider2D _)
        {
            if (_standingOn == 0)
                isGrounded = true;
            _standingOn++;
        }

        private void OnTriggerExit2D(Collider2D _)
        {
            if (_standingOn == 1)
                isGrounded = false;
            _standingOn--;
        }
    }
}