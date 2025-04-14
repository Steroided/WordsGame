using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {
    [Header("Buttons")]
    [SerializeField]
    private Button _optionsButton;
    [SerializeField]
    private Button _soundButton;
    [Header("Sliders")]
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private Slider _soundSlider;
    [Header("Panels")]
    [SerializeField]
    private GameObject _pausePanel;

    public void Start()
    {
        Init();
    }
    public void Init()
    {
        SoundManager.PlayMusic("Swinging Pants");

        _optionsButton.onClick.AddListener(() => SoundManagerButtonsProvider.PlaySound("Click1"));
        _optionsButton.onClick.AddListener(() => TogglePause());
        _soundButton.onClick.AddListener(() => SoundManagerButtonsProvider.ToggleMusicMuted());
        _soundButton.onClick.AddListener(() => SoundManagerButtonsProvider.ToggleSoundMuted());
    }
    public void TogglePause()
    {
        bool needPause = Time.timeScale > 0.5f;
        Time.timeScale = needPause ? 0 : 1;

        _pausePanel.SetActive(needPause);
    }

    void OnEnable()
    {
        SoundManager.LoadSound("phaserUp1");
    }

    void OnDisable()
    {
        SoundManager.UnloadSound("phaserUp1");
    }
}
