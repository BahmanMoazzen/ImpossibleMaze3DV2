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
    /// the camera manager which manages the game camera
    /// </summary>
    [SerializeField] CameraManager _cameraManager;

    [SerializeField] GameStatAbstract[] _gameStats;
    int _currentGameStat = 0;

    Transform _startTransform;
    Transform _endTransform;



    private void Awake()
    {

        Time.timeScale = _gameSpeed;


    }
    private void Start()
    {
        //_gameStats[_currentGameStat]._Started();

        MazeSpawner._Instance._SpawnMaze(GameSettingInfo.Instance.CurrentLevelSkeletone.LevelSkeletone, MazeSpawner_OnLevelSpawned);

    }


    private void MazeSpawner_OnLevelSpawned(MazeRotator iMazeRotator)
    {
        // setting up all the input controllers available
        foreach (var inp in _inputs)
            inp._Setup(iMazeRotator);

        // getting start and end Transform
        _startTransform = GameObject.Find("Start").transform;
        _endTransform = GameObject.Find("End").transform;

        




    }

    void _spawnBallCompleted(GameObject iBall)
    {
        BallPosition._Instance._SetBall(iBall.transform);
        // targeting all cameras to the ball
        _cameraManager._SetupCameras(iBall.transform, BallPosition._Instance.transform,_startTransform);
    }
    public void _LoadBall()
    {
        // spawn ball
        BallSpawner._Instance._SpawnBall(GameSettingInfo.Instance.CurrentBallInfo.BallMesh, _startTransform, false, true, _spawnBallCompleted);

    }
    //public void _ChangeStat(GameStats iNewStat)
    //{
    //    _gameStats[_currentGameStat]._Ended();
    //    _currentGameStat =((int)iNewStat);
    //    _gameStats[_currentGameStat]._Started();
    //}

    private void Update()
    {
        //_gameStats[_currentGameStat]._Perform();
    }
}


public enum GameStats { LoadScene, SelectBall, StartGame, WinGame, LooseGame }