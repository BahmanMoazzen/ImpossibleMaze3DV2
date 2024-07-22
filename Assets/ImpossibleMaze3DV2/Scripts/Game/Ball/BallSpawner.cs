using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using UnityEngine.Events;
using System;

public class BallSpawner : MonoBehaviour
{
    /// <summary>
    /// the default rotation speed of the game
    /// </summary>
    const float DEFAULT_ROTATION_SPEED = 50f;

    /// <summary>
    /// singletone porpuses
    /// </summary>
    public static BallSpawner _Instance;

    Transform _spawnLocation;


    AssetReferenceGameObject _ballAssetReference;

    bool _attachRotator;

    private void Awake()
    {
        /// initializing the _Instance
        if (_Instance == null) _Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// public method to be called from other codes to instantiate a level
    /// </summary>
    /// <param name="iMaze">the prefab of the level. all the components will be set up automatically</param>
    public void _SpawnBall(AssetReferenceGameObject iBall, Transform iBallLocation,bool iAttachRotator)
    {
        _ballAssetReference = iBall;
        _spawnLocation = iBallLocation;
        _attachRotator = iAttachRotator;
        _ballAssetReference.LoadAssetAsync<GameObject>().Completed += BallSpawner_Completed;
        //StartCoroutine(startSpawn(iMaze,iOnSpawnDone));

    }
    public void _UnloadBall()
    {
        /// releasing the maze asset
        _ballAssetReference.ReleaseAsset();

    }
    /// <summary>
    /// calls after trying to load the addressable asset
    /// </summary>
    /// <param name="iAsyncResult">the result of the operation</param>
    private void BallSpawner_Completed(AsyncOperationHandle<GameObject> iAsyncResult)
    {
        GameObject newBall =
        Instantiate(iAsyncResult.Result, _spawnLocation.position, Quaternion.identity);
        if (_attachRotator)
        {
            newBall.AddComponent<BallDisplayRotator>()._SetRotation(Vector3.one * DEFAULT_ROTATION_SPEED);
        }

    }
    private void OnDestroy()
    {
        _UnloadBall();
    }
}
