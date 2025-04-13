using Systems.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button _start;

     void Awake()
    {
        _start.onClick.AddListener(() => SceneLoader.Instance.LoadNextLevel());
    }
}
