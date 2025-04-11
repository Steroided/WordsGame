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
        foreach (string ss in Words.AllWords["LevelDefault"])
        {
            Debug.Log(ss);
        }
    }
    public void Init()
    {

    }
    public async Task DoFetch()
    {
        RemoteJsonParser parser = new RemoteJsonParser();


        var fetchSucess = await parser.FetchAsync();
        string[] s = null;
        if (fetchSucess != null)
        {
            Words = fetchSucess;
            s = Words.AllWords["Level1"];
        }
        else s = Words.AllWords["LevelDefault"];

        Fetched = true;

        foreach (string ss in s)
        {
            Debug.Log(ss);
        }
    }
    async void Start()
    {
        await DoFetch();
    }
}
