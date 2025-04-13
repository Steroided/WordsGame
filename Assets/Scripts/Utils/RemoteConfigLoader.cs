using System.Threading.Tasks;
using UnityEngine;

public class RemoteConfigLoader : PersistentSingleton<RemoteConfigLoader>
{
    public WordsForLevels Words;
    public bool Fetched = false;
    // Start is called before the first frame update
    private void Awake()
    {
        Words.AllWords.Add("LevelDefault", GameSettings.Instance.WordsLevelDefault);
    }
    public void Init()
    {

    }
    public async Task DoFetch()
    {
        RemoteJsonParser parser = new RemoteJsonParser();


        var fetchSucess = await parser.FetchAsync();

        if (fetchSucess != null)
        {
            Words = fetchSucess;
        }

        Fetched = true;
    }
    async void Start()
    {
        await DoFetch();
    }
}
