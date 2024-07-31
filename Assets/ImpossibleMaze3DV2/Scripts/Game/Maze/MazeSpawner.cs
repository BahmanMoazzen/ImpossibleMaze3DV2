using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using UnityEngine.Events;
using System;

public class MazeSpawner : MonoBehaviour
{
    /// <summary>
    /// the default rotation speed of the game
    /// </summary>
    const float DEFAULT_ROTATION_SPEED = 50f;
    const float DEFAULT_MAZE_MASS = 1000f;

    /// <summary>
    /// singletone porpuses
    /// </summary>
    public static MazeSpawner _Instance;

    /// <summary>
    /// reference to the maze rotator to enable and disable it
    /// </summary>
    MazeRotator _mazeRotator;

    UnityAction<MazeRotator, Transform> _actionAfterDone;

    AssetReferenceGameObject _mazeAssetReference;
    [SerializeField]
    AssetReferenceGameObject _endPointReference;

    GameObject _parentMaze;

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
    public void _SpawnMaze(AssetReferenceGameObject iMaze, UnityAction<MazeRotator, Transform> iOnSpawnDone)
    {
        _actionAfterDone = iOnSpawnDone;
        _mazeAssetReference = iMaze;

        _mazeAssetReference.LoadAssetAsync<GameObject>().Completed += MazeSpawner_Completed;
        //StartCoroutine(startSpawn(iMaze,iOnSpawnDone));

    }
    public void _UnloadMaze()
    {
        Destroy(_parentMaze);
        /// releasing the maze asset
        _mazeAssetReference.ReleaseAsset();

    }

    /// <summary>
    /// calls after trying to load the addressable asset
    /// </summary>
    /// <param name="iAsyncResult">the result of the operation</param>
    private void MazeSpawner_Completed(AsyncOperationHandle<GameObject> iAsyncResult)
    {
        /// creating empty object
        _parentMaze = new GameObject();
        /// rename it 
        _parentMaze.name = "LevelMaze";
        /// adding the maze mesh underneath the empty parent
        GameObject mazeSkleton =
        Instantiate(iAsyncResult.Result, _parentMaze.transform);
        /// adding mesh collider to the maze skletone
        mazeSkleton.AddComponent<MeshCollider>();
        /// adding rigidbody
        _addRigidBody();
        /// adding rotator
        _addMazeRotator();

        // load end point
        _endPointReference.LoadAssetAsync<GameObject>().Completed += MazeSpawner_EndPointLoaded; ;

        /// job done event
        _actionAfterDone?.Invoke(_mazeRotator, GameObject.Find("Start").transform);
    }

    private void MazeSpawner_EndPointLoaded(AsyncOperationHandle<GameObject> iAsyncTask)
    {
        if (iAsyncTask.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(iAsyncTask.Result, GameObject.Find("End").transform);
        }

    }

    /// <summary>
    /// the routine to spawn the level and add components. It sould be called from SpawnMaze method.
    /// </summary>
    /// <returns>nothing</returns>
    //IEnumerator startSpawn(AssetReferenceGameObject iMaze,UnityAction<MazeRotator> iOnSpawnDone)
    //{
    //    yield return null;
    //    /// creating empty object
    //    GameObject parentMaze = new GameObject();
    //    /// rename it 
    //    parentMaze.name = "LevelMaze";

    //    iMaze.LoadAssetAsync<GameObject>().Completed += (iAsyncresult) =>
    //    {
    //        if( iAsyncresult.Status == AsyncOperationStatus.Succeeded)
    //        {
    //            /// adding the maze mesh underneath the empty parent
    //            GameObject mazeSkleton =
    //            Instantiate(iAsyncresult.Result, parentMaze.transform);
    //            /// adding mesh collider to the maze skletone
    //            mazeSkleton.AddComponent<MeshCollider>();
    //            /// adding rigidbody
    //            _addRigidBody(parentMaze);
    //            /// adding rotator
    //            _addMazeRotator(parentMaze);


    //            /// job done event
    //            iOnSpawnDone?.Invoke(_mazeRotator);
    //        }
    //    }; 

    //}



    /// <summary>
    /// enabeling the rotator for game
    /// </summary>
    public void _EnableMazeRotator()
    {
        _mazeRotator.enabled = true;
    }
    /// <summary>
    /// disabling the rotator for game
    /// </summary>
    public void _DisableMazeRotator()
    {
        _mazeRotator.enabled = false;
    }

    /// <summary>
    /// adding a MazeRotator with difault parameters to the game object and disable it
    /// </summary>
    /// <param name="iObject">the game object to add MazeRotator</param>
    void _addMazeRotator()
    {
        _mazeRotator = _parentMaze.AddComponent<MazeRotator>();
        _mazeRotator._SetupRotator(InGameInfo.Instance.DefaultRotatorLimit, DEFAULT_ROTATION_SPEED);
        _DisableMazeRotator();
    }

    /// <summary>
    /// adding a rigidbody with difault parameters to the game object
    /// </summary>
    /// <param name="iObject">the game object to add rigidbody</param>
    void _addRigidBody()
    {
        Rigidbody mazeBody =
        _parentMaze.AddComponent<Rigidbody>();
        mazeBody.angularDrag = mazeBody.drag = 0;
        mazeBody.isKinematic = true;
        mazeBody.mass = DEFAULT_MAZE_MASS;
        mazeBody.useGravity = false;
    }

    

    private void OnDestroy()
    {
        _UnloadMaze();
    }


}
