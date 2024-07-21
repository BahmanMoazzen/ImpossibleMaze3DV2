using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
[CreateAssetMenu(fileName = "NewLevel 0", menuName = "Impossible Maze 2/new Level", order = 0)]
public class LevelInfo : ScriptableObject
{
    public string LevelName;
    public AssetReferenceGameObject LevelSkeletone;
    public float LevelTime;
    

}
