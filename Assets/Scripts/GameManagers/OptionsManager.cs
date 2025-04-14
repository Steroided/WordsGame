using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OptionsManager : MonoBehaviour {
    [Header("Buttons")]
    [SerializeField]
    private Button _optionsButton;
    [SerializeField]
    private Button _soundButton;
    [SerializeField]
    private Button _backButton;
    [Header("Sliders")]
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private Slider _soundSlider;
    [Header("Panels")]
    [SerializeField]
    private GameObject _pausePanel;

    private SoundManagerSettings _soundManagerSettings;
    private SoundManagerButtonsProvider _soundManagerButtonsProvider;
    private SoundManager _soundManager;
    [Inject]
    private void Construct(SoundManagerButtonsProvider soundManagerButtonsProvider, SoundManager soundManager, SoundManagerSettings soundManagerSettings)
    {
        _soundManagerSettings = soundManagerSettings;
        _soundManagerButtonsProvider = soundManagerButtonsProvider;
        _soundManager = soundManager;
    }
  
    

    public void Start()
    {
        Init();
    }
    public void Init()
    {
        _soundManager.PlayMusic("Swinging Pants");

        _optionsButton.onClick.AddListener(() => _soundManagerButtonsProvider.PlaySound("Click1"));
        _optionsButton.onClick.AddListener(() => TogglePause());
        _backButton.onClick.AddListener(() => TogglePause());
        _soundButton.onClick.AddListener(() => _soundManagerButtonsProvider.ToggleMusicMuted());
        _soundButton.onClick.AddListener(() => _soundManagerButtonsProvider.ToggleSoundMuted());
    }
    public void TogglePause()
    {
        bool needPause = Time.timeScale > 0.5f;
        Time.timeScale = needPause ? 0 : 1;

        _pausePanel.SetActive(needPause);
    }

    void OnEnable()
    {
        _soundManager.LoadSound("phaserUp1");
    }

    void OnDisable()
    {
        _soundManager.UnloadSound("phaserUp1");
    }
}
