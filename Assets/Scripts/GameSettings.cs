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
    public int MaxClustersUIPerSlot = 4;
    public float OnDraggableOffsetXRatio = 1;
}
