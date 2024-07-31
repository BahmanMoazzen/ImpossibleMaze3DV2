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
    bool _attachRigidbody;

    UnityAction<GameObject> _completeAction;

    Vector3 _spawnOffset = new Vector3(0, 2, 0);
    GameObject _ballGameObject;

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
    public void _SpawnBall(AssetReferenceGameObject iBall, Transform iBallLocation, bool iAttachRotator, bool iAttachRigidbody, UnityAction<GameObject> iCompleteAction)
    {
        _ballAssetReference = iBall;
        _spawnLocation = iBallLocation;
        _attachRotator = iAttachRotator;
        _completeAction = iCompleteAction;
        _attachRigidbody = iAttachRigidbody;
        _ballAssetReference.LoadAssetAsync<GameObject>().Completed += BallSpawner_Completed;
        //StartCoroutine(startSpawn(iMaze,iOnSpawnDone));

    }
    public void _UnloadBall()
    {
        /// Destroy the ball
        Destroy(_ballGameObject);

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
        Instantiate(iAsyncResult.Result, _spawnLocation.position + _spawnOffset, Quaternion.identity);
        if (_attachRigidbody)
        {
            newBall.AddComponent<Rigidbody>().mass = 100;
        }
        if (_attachRotator)
        {
            newBall.AddComponent<BallDisplayRotator>()._SetRotation(Vector3.one * DEFAULT_ROTATION_SPEED);
        }
        _ballGameObject = newBall;
        _completeAction?.Invoke(_ballGameObject);

    }
    private void OnDestroy()
    {
        _UnloadBall();
    }
}
