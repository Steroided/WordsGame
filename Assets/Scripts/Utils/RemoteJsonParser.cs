using UnityEngine;
using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;

public class RemoteJsonParser
{
    struct EmptyStruct { }
    // Start is called before the first frame update
    public async Task<WordsForLevels> FetchAsync()
    {
        RuntimeConfig res;
        try
        {
            res = await Fetch().TimeoutWithResult(GameSettings.Instance.FetchTimeoutMs);       
            return(JsonConvert.DeserializeObject<WordsForLevels>(res.GetJson("Words")));
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("Failed to fetch configs, is your project linked and configuration deployed?");
        }
        return null;
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

