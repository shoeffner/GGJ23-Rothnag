using System;
using UnityEngine;

namespace Rothnag
{
    public abstract class TreeNeedsEvent : MonoBehaviour
    {
        public event Action<int> OnProgressChanged;
        public event Action<TreeNeedsEvent> OnCompleted;
        public event Action<TreeNeedsEvent> OnFailed;

        public float timeLimit => _timeLimit; // in seconds

        public float timeLeft => _timeLeft;

        public int progressNeeded => _progressNeeded;

        public int progress
        {
            get => _progress;
            set
            {
                OnProgressChanged?.Invoke(value);
                _progress = value;
                if (Mathf.Approximately(_progress, progressNeeded))
                {
                    OnCompleted?.Invoke(this);
                    Destroy(gameObject);
                }
            }
        }

        public GameObject originPrefab { get; private set; }

        // ReSharper disable InconsistentNaming
        [SerializeField]
        private float _timeLimit;

        [SerializeField]
        private float _timeLeft;

        [SerializeField]
        private int _progressNeeded;

        [SerializeField]
        private int _progress;
        // ReSharper restore InconsistentNaming

        private bool _initialized = false;

        // ReSharper disable once ParameterHidesMember
        public void Init(GameObject originPrefab)
        {
            if (_initialized)
                return;
            _initialized = true;
            this.originPrefab = originPrefab;
        }

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected void Update()
        {
            _timeLeft -= Time.deltaTime;
            if (timeLeft < Mathf.Epsilon)
            {
                OnFailed?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}