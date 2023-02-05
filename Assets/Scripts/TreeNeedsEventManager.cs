using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Rothnag
{
    public sealed class TreeNeedsEventManager : MonoBehaviour
    {
        public event Action<TreeNeedsEvent> OnEventStarted;

        [Header("config")]
        [SerializeField]
        private float timeUntilNextEventStartValue;

        [SerializeField]
        private float spawnTimerFactor;

        public float timeUntilNextEvent { get; private set; }

        private bool stopped = false;

        [SerializeField]
        private GameObject[] treeNeedsEventPrefabs;

        private HashSet<GameObject> _treeNeedsEventPrefabsNotInUse;
        public List<TreeNeedsEvent> treeNeedsEventsInUse { get; } = new();

    #region Singleton

        private static TreeNeedsEventManager _instance;

        public static TreeNeedsEventManager instance
        {
            get
            {
                if (_instance is not null)
                    return _instance;

                var obj = new GameObject { name = nameof(TreeNeedsEventManager) };
                _instance = obj.AddComponent<TreeNeedsEventManager>();
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

        private void Awake()
        {
            // for singleton
            if (!EnsureInstantiatedOnlyOnce())
                return; // abort
        }

        public void Stop()
        {
            stopped = true;
        }

        public void Restart()
        {
            if (stopped)
            {
                Start();
            }
        }

        private void Start()
        {
            stopped = false;
            _treeNeedsEventPrefabsNotInUse = treeNeedsEventPrefabs.ToHashSet();
            timeUntilNextEvent = timeUntilNextEventStartValue;
            SpawnRandomEvent();
        }

        private void SpawnRandomEvent()
        {
            if (stopped) return;
            // schedule next spawn
            UpdateTimer();
            Invoke(nameof(SpawnRandomEvent), timeUntilNextEvent);

            // pick a random Event prefab to spawn that's not used already, if any exist
            Random random = new Random();
            if (_treeNeedsEventPrefabsNotInUse.Count == 0)
                return;

            Invoke(nameof(SpawnRandomEvent), timeUntilNextEvent);
            GameObject selectedPrefab =
                    _treeNeedsEventPrefabsNotInUse.ElementAt(random.Next(0, _treeNeedsEventPrefabsNotInUse.Count - 1));
            // spawn
            _treeNeedsEventPrefabsNotInUse.Remove(selectedPrefab);
            var treeNeedsEventInstance = Instantiate(selectedPrefab).GetComponent<TreeNeedsEvent>();
            treeNeedsEventsInUse.Add(treeNeedsEventInstance);
            // setup
            treeNeedsEventInstance.OnCompleted += ProcessEventEnded;
            treeNeedsEventInstance.OnFailed += ProcessEventEnded;
            treeNeedsEventInstance.Init(selectedPrefab);
            // invoke listener
            OnEventStarted?.Invoke(treeNeedsEventInstance);
        }

        private void ProcessEventEnded(TreeNeedsEvent treeNeedsEvent)
        {
            _treeNeedsEventPrefabsNotInUse.Add(treeNeedsEvent.originPrefab);
            treeNeedsEventsInUse.Remove(treeNeedsEvent);
        }

        /// <summary>
        /// algorithm for scaling the spawn timers for difficulty
        /// </summary>
        private void UpdateTimer()
            => timeUntilNextEvent *= spawnTimerFactor;

    }
}
