using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SceneManagement {
    public class SceneLoader : PersistentSingleton<SceneLoader>
    { 
        [SerializeField] Image _loadingBar;
        [SerializeField] float _fillSpeed = 0.5f;
        [SerializeField] Canvas _loadingCanvas;
        [SerializeField] Camera _loadingCamera;
        [SerializeField] SceneGroup[] _sceneGroups;

        private int _currentLevel;
        public int CurrentLevel
        {
            get { return _currentLevel; }
            set
            {
                _currentLevel = value;
                LoadSceneGroup(_currentLevel);
            }
        }

        private float _targetProgress;
        private bool _isLoading;

        public readonly SceneGroupManager manager = new SceneGroupManager();


        private void Start()
        {
            CurrentLevel = GameSettings.Instance.StartSceneIndex;
        }
        public void LoadNextLevel()
        {
            if (CurrentLevel < RemoteConfigLoader.Instance.TotalLevels)
                CurrentLevel++;
            else CurrentLevel = GameSettings.Instance.FirstLevelSceneIndex;
        }

        void Update() {
            if (!_isLoading) return;
            
            float currentFillAmount = _loadingBar.fillAmount;
            float progressDifference = Mathf.Abs(currentFillAmount - _targetProgress);

            float dynamicFillSpeed = progressDifference * _fillSpeed;
    
            _loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, _targetProgress, Time.deltaTime * dynamicFillSpeed);
        }

        public async UniTask LoadSceneGroup(int index) {
            _loadingBar.fillAmount = 0f;
            _targetProgress = 1f;

            if (index < 0 || index >= _sceneGroups.Length) {
                Debug.LogError("Invalid scene group index: " + index);
                return;
            }

            LoadingProgress progress = new LoadingProgress();
            progress.Progressed += target => _targetProgress = Mathf.Max(target, _targetProgress);
            
            EnableLoadingCanvas();
            await manager.LoadScenes(_sceneGroups[index], progress);
            EnableLoadingCanvas(false);
        }
    
        void EnableLoadingCanvas(bool enable = true) {
            _isLoading = enable;
            _loadingCanvas.gameObject.SetActive(enable);
            _loadingCamera.gameObject.SetActive(enable);
        }
        
    }
    
    public class LoadingProgress : IProgress<float> {
        public event Action<float> Progressed;

        const float ratio = 1f;

        public void Report(float value) {
            Progressed?.Invoke(value / ratio);
        }
    }
}