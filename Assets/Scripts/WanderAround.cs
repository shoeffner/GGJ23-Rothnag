using UnityEngine;
using Random = UnityEngine.Random;

namespace Rothnag
{
    public sealed class WanderAround : MonoBehaviour
    {
        [Header("references")]
        [SerializeField]
        private Rigidbody2D rb;

        [Header("config")]
        [SerializeField]
        private float wanderTimePerDirection;

        [SerializeField]
        private float wanderSpeed;

        private void Awake()
        {
            ChangeWanderDirection();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            // run away from collision
            var position = transform.position;
            Vector2 newDirection = (new Vector2(position.x, position.y) - col.GetContact(0).point).normalized;
            rb.velocity = newDirection * wanderSpeed;
        }

        private void ChangeWanderDirection()
        {
            rb.velocity = Random.insideUnitCircle.normalized * wanderSpeed;
            Invoke(nameof(ChangeWanderDirection), wanderTimePerDirection);
        }
    }
}