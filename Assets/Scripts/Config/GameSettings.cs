using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Settings")]
public class GameSettings : ScriptableObject
{
    static GameSettings _Instance;

    public static GameSettings Instance
    {
        get
        {
            return _Instance ?? Resources.Load<GameSettings>("GameSettings");
        }
    }
    public int MaxClustersUIPerSlot = 1;
    public float OnDraggableOffsetXRatio = 1;
    public int FetchTimeoutMs = 6000;
    public int MaxWords = 4;
    public int TotalLevels = 4;
    public int MaxSymbolsPerWord = 6;
    public string[] WordsLevelDefault;
}
