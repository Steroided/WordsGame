using Cysharp.Threading.Tasks;
public class RemoteConfigLoader : PersistentSingleton<RemoteConfigLoader>
{
    public WordsForLevels Words;
    public int TotalLevels = 1;
    public bool Fetched = false;

    // Start is called before the first frame update
    private void Awake()
    {
        Words.AllWords.Add("LevelDefault", GameSettings.Instance.WordsLevelDefault);
        print(Words.AllWords.Count);
    }
    public void Init()
    {

    }
    public async UniTask DoFetch()
    {
        RemoteJsonParser parser = new RemoteJsonParser();

        var fetchSucess = await parser.FetchAsync();
        Words = fetchSucess ?? Words;
        TotalLevels = Words.AllWords.Count;
        Fetched = true;
        print("FETCHED");
    }
    async void Start()
    {
        await DoFetch();
    }
}
