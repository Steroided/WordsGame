using Systems.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button _start;

    [Inject]
    private SceneLoader _sceneLoader;

     void Awake()
    {
        _start.onClick.AddListener(() => _sceneLoader.LoadNextLevel());
    }
}
