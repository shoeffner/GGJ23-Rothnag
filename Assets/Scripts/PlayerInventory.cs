using UnityEngine;

namespace Rothnag
{
    public sealed class PlayerInventory : MonoBehaviour
    {
    #region Singleton

        private static PlayerInventory _instance;

        public static PlayerInventory instance
        {
            get
            {
                if (_instance is not null)
                    return _instance;

                var obj = new GameObject { name = nameof(PlayerInventory) };
                _instance = obj.AddComponent<PlayerInventory>();
                DontDestroyOnLoad(obj);
                return _instance;
            }
        }

        private bool EnsureInstantiatedOnlyOnce()
        {
            if (_instance is null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                return true;
            }

            Destroy(gameObject);
            return false;
        }

    #endregion

        public int bucket;
        public int animal;
        public int birdHouse;

        private void Awake()
        {
            // for singleton
            if (!EnsureInstantiatedOnlyOnce())
                return;
        }
    }
}