using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// the time scale of the game
    /// </summary>
    [Range(0, 5)]
    [SerializeField] float _gameSpeed = 1;
    /// <summary>
    /// all the input handlers of the game
    /// </summary>
    [SerializeField] InputManagerAbstract[] _inputs;
    /// <summary>
    /// level to load by MazeSpawner
    /// </summary>
    [SerializeField] GameObject _levelToLoad;

    [SerializeField] GameObject _ball;

    /// <summary>
    /// the camera manager which manages the game camera
    /// </summary>
    [SerializeField] CameraManager _cameraManager;

    private void Awake()
    {

        Time.timeScale = _gameSpeed;

        
    }
    private void Start()
    {
        MazeSpawner._Instance._SpawnMaze(_levelToLoad);
    }
    private void OnDisable()
    {
        MazeSpawner.OnLevelSpawned -= MazeSpawner_OnLevelSpawned;
    }
    private void OnEnable()
    {
        MazeSpawner.OnLevelSpawned += MazeSpawner_OnLevelSpawned;
    }

    private void MazeSpawner_OnLevelSpawned(MazeRotator iMazeRotator)
    {
        foreach (var inp in _inputs)
            inp._Setup(iMazeRotator);
        _cameraManager._SetupCameras(_ball.transform);
    }
}
