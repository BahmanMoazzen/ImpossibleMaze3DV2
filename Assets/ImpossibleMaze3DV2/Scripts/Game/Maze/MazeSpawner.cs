using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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
    public void _SpawnMaze(GameObject iMaze,UnityAction<MazeRotator> iOnSpawnDone)
    {
        StartCoroutine(startSpawn(iMaze,iOnSpawnDone));

    }

    /// <summary>
    /// the routine to spawn the level and add components. It sould be called from SpawnMaze method.
    /// </summary>
    /// <returns>nothing</returns>
    IEnumerator startSpawn(GameObject iMaze,UnityAction<MazeRotator> iOnSpawnDone)
    {
        yield return null;
        /// creating empty object
        GameObject parentMaze = new GameObject();
        /// rename it 
        parentMaze.name = "LevelMaze";
        /// adding the maze mesh underneath the empty parent
        GameObject mazeSkleton =
        Instantiate(iMaze, parentMaze.transform);
        /// adding mesh collider to the maze skletone
        mazeSkleton.AddComponent<MeshCollider>();
        /// adding rigidbody
        _addRigidBody(parentMaze);
        /// adding rotator
        _addMazeRotator(parentMaze);

        yield return new WaitForEndOfFrame();
        /// job done event
        iOnSpawnDone?.Invoke(_mazeRotator);

    }
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
    void _addMazeRotator(GameObject iObject)
    {
        _mazeRotator = iObject.AddComponent<MazeRotator>();
        _mazeRotator._SetupRotator(newLimit, DEFAULT_ROTATION_SPEED);
        _DisableMazeRotator();
    }

    /// <summary>
    /// adding a rigidbody with difault parameters to the game object
    /// </summary>
    /// <param name="iObject">the game object to add rigidbody</param>
    void _addRigidBody(GameObject iObject)
    {
        Rigidbody mazeBody =
        iObject.AddComponent<Rigidbody>();
        mazeBody.angularDrag = mazeBody.drag = 0;
        mazeBody.isKinematic = true;
        mazeBody.mass = DEFAULT_MAZE_MASS;
        mazeBody.useGravity = false;
    }

    /// <summary>
    /// the default rotation limit of the rotator
    /// </summary>
    MazeRotationLimit newLimit
    {
        get
        {
            return new MazeRotationLimit(
            new RotationLimit(-15, 15),
            new RotationLimit(0, 0),
            new RotationLimit(-15, 15)
            );
        }
    }




}
