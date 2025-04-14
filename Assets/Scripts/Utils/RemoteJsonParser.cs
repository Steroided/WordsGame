using UnityEngine;
using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;

public class RemoteJsonParser
{
    private GameSettings _gameSettings;
    public RemoteJsonParser(GameSettings settings)
    {
        _gameSettings = settings;
    }



    struct EmptyStruct { }
    public async UniTask<WordsForLevels> FetchAsync()
    {
        RuntimeConfig res;
        try
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(_gameSettings.FetchTimeoutMs);
            res = await Fetch(cts.Token);
            return(JsonConvert.DeserializeObject<WordsForLevels>(res.GetJson("Words")));
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Failed to fetch configs, is your project linked and configuration deployed?");
        }
        return null;
    }
   
    public async UniTask<RuntimeConfig> Fetch(CancellationToken token)
    {   // Initialize          
        token.ThrowIfCancellationRequested();
        await UnityServices.InitializeAsync();

        // Sign in
        token.ThrowIfCancellationRequested();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        //Fetch configuration settings from the remote service:
        token.ThrowIfCancellationRequested();
        return await RemoteConfigService.Instance.FetchConfigsAsync(new EmptyStruct(), new EmptyStruct());
    }
}

