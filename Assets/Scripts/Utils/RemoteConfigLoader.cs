using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
public class RemoteConfigLoader : MonoBehaviour
{
    public WordsForLevels Words;
    [HideInInspector]
    public int TotalLevels = 1;
    public bool Fetched = false;

    [Inject]
    private GameSettings _gameSettings;
    // Start is called before the first frame update
    private void Awake()
    {
        Words.AllWords.Add("LevelDefault", _gameSettings.WordsLevelDefault);
    }
    public void Init()
    {

    }
    public async UniTask DoFetch()
    {
        RemoteJsonParser parser = new RemoteJsonParser(_gameSettings);

        var fetchSucess = await parser.FetchAsync();
        Words = fetchSucess ?? Words;
        TotalLevels = Words.AllWords.Count;
        Fetched = true;
        print("Fetched");
    }
    async void Start()
    {
        await DoFetch();
    }
}
