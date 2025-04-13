using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SceneManagement {
    public class SceneLoader : PersistentSingleton<SceneLoader>
    { 
        [SerializeField] Image loadingBar;
        [SerializeField] float fillSpeed = 0.5f;
        [SerializeField] Canvas loadingCanvas;
        [SerializeField] Camera loadingCamera;
        [SerializeField] SceneGroup[] sceneGroups;

        private int _currentLevel;

        public int CurrentLevel
        {
            get { return _currentLevel; }
            set 
            {
                _currentLevel = value;
                 LoadLevel(_currentLevel);
            }
        }

        float targetProgress;
        bool isLoading;

        public readonly SceneGroupManager manager = new SceneGroupManager();


        private void Start()
        {
            LoadLevel(0);
        }

        public void LoadLevel(int level)
        {
            LoadSceneGroup(level);
        }
        public void LoadNextLevel()
        {
            //if (sceneGroups[CurrentLevel].GroupName.Contains(char(CurrentLevel++)))
            if (CurrentLevel < GameSettings.Instance.TotalLevels)
                CurrentLevel++;
            else CurrentLevel = 1;
        }

        void Update() {
            if (!isLoading) return;
            
            float currentFillAmount = loadingBar.fillAmount;
            float progressDifference = Mathf.Abs(currentFillAmount - targetProgress);

            float dynamicFillSpeed = progressDifference * fillSpeed;
    
            loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProgress, Time.deltaTime * dynamicFillSpeed);
        }

        public async Task LoadSceneGroup(int index) {
            loadingBar.fillAmount = 0f;
            targetProgress = 1f;

            if (index < 0 || index >= sceneGroups.Length) {
                Debug.LogError("Invalid scene group index: " + index);
                return;
            }

            LoadingProgress progress = new LoadingProgress();
            progress.Progressed += target => targetProgress = Mathf.Max(target, targetProgress);
            
            EnableLoadingCanvas();
            await manager.LoadScenes(sceneGroups[index], progress);
            EnableLoadingCanvas(false);
        }
    
        void EnableLoadingCanvas(bool enable = true) {
            isLoading = enable;
            loadingCanvas.gameObject.SetActive(enable);
            loadingCamera.gameObject.SetActive(enable);
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