using UnityEngine;
using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class RemoteJsonParser
{
    public int LevelNum = 1;

    public WordsSave WordsSave;
    struct EmptyStruct { }
    // Start is called before the first frame update
    public async void FetchAsync()
    {
        RuntimeConfig res;
        try
        {
            res = await Fetch().TimeoutWithResult(GameSettings.Instance.FetchTimeoutMs); 
       
            WordsSave = JsonConvert.DeserializeObject<WordsSave>(res.GetJson("WordsLevel" + LevelNum));
            Debug.Log(WordsSave.Words6Letter[0]);

        }
        catch (Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Failed to fetch configs, is your project linked and configuration deployed?");
        }
    }
   
    public async Task<RuntimeConfig> Fetch()
    {
        // Initialize 
        await UnityServices.InitializeAsync();
        // Sign in
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        //Fetch configuration settings from the remote service:
        return await RemoteConfigService.Instance.FetchConfigsAsync(new EmptyStruct(), new EmptyStruct());
    }
}

