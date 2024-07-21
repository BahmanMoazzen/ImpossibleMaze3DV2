using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
[CreateAssetMenu(fileName = "NewBall 0", menuName = "Impossible Maze 2/new Ball", order = 1)]
public class BallInfo : ScriptableObject
{
    public string BallName;
    public AssetReferenceGameObject BallMesh;
}
