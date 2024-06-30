using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MazeSpawner : MonoBehaviour
{
    /// <summary>
    /// the default rotation speed of the game
    /// </summary>
    const float DEFAULT_ROTATION_SPEED = 50f;

    /// <summary>
    /// singletone porpuses
    /// </summary>
    public static MazeSpawner _Instance;

    /// <summary>
    /// the event to fire after the spawing is done. Returns MazeRotator
    /// </summary>
    public static event UnityAction<MazeRotator> OnLevelSpawned;

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
    public void _SpawnMaze(GameObject iMaze)
    {
        StartCoroutine(startSpawn(iMaze));

    }

    /// <summary>
    /// the routine to spawn the level and add components. It sould be called from SpawnMaze method.
    /// </summary>
    /// <returns>nothing</returns>
    IEnumerator startSpawn(GameObject iMaze)
    {
        yield return null;
        GameObject parentMaze = new GameObject();
        parentMaze.name = "LevelMaze";
        GameObject mazeSkleton =
        Instantiate(iMaze, parentMaze.transform);
        mazeSkleton.AddComponent<MeshCollider>();
        Rigidbody mazeBody =
        parentMaze.AddComponent<Rigidbody>();
        mazeBody.angularDrag = mazeBody.drag = 0;
        mazeBody.isKinematic = true;
        mazeBody.mass = 1000;
        mazeBody.useGravity = false;

        MazeRotator newMazeRotator = parentMaze.AddComponent<MazeRotator>();
        newMazeRotator._SetupRotator(newLimit, DEFAULT_ROTATION_SPEED);
        
        yield return new WaitForEndOfFrame();

        OnLevelSpawned?.Invoke(newMazeRotator);

        
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
