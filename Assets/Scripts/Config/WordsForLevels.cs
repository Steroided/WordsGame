
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class WordsForLevels
{
    [JsonProperty]
    public Dictionary<string, string[]> AllWords = new Dictionary<string, string[]>();
}
