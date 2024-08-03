using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
    //Transform _endTransform;
    GameObject _ballObject;
    [SerializeField] Text _ballNameText;
    [SerializeField] Text _levelNameText;
    [SerializeField] GameObject _loadingImage;

    [SerializeField] Slider _xSlider;
    [SerializeField] Slider _zSlider;

    [SerializeField] UnityEvent OnBallDropped;
    [SerializeField] UnityEvent OnGameFinished;
    [SerializeField] UnityEvent OnTimeUp;


    private void Awake()
    {
        Time.timeScale = _gameSpeed;
        
    }
    private void OnDisable()
    {
        InGameInfo.Instance.OnGameWon -= _gameInfo_OnGameFinished;
        InGameInfo.Instance.OnMazeRotated -= _gameInfo_OnMazeRotated;
        InGameInfo.Instance.OnBallDroped -= _gameInfo_OnBallDroped;
    }
    private void OnEnable()
    {
        InGameInfo.Instance.OnGameWon += _gameInfo_OnGameFinished;
        InGameInfo.Instance.OnMazeRotated += _gameInfo_OnMazeRotated;
        InGameInfo.Instance.OnBallDroped += _gameInfo_OnBallDroped;
    }

    private void _gameInfo_OnBallDroped()
    {
        OnBallDropped?.Invoke();
        
    }

    public void _ReloadScene()
    {
        BAHMANLoadingManager._INSTANCE._LoadScene(AllScenes.GameScene);
    }

    private void _gameInfo_OnMazeRotated(Vector3 iRotation)
    {
        _xSlider.value = iRotation.x;
        _zSlider.value = -iRotation.z;
    }

    private void _gameInfo_OnGameFinished()
    {
        GameSettingInfo.Instance.CurrentGameLevel++;
        Debug.Log("Game Finished");
        OnGameFinished?.Invoke();
        

    }

    private void Start()
    {
        //_gameStats[_currentGameStat]._Started();
        MazeSpawner._Instance._SpawnMaze(GameSettingInfo.Instance.CurrentLevelSkeletone.LevelSkeletone, MazeSpawner_OnLevelSpawned);
        _levelNameText.text = GameSettingInfo.Instance.CurrentLevelSkeletone.LevelName;

    }
    private void MazeSpawner_OnLevelSpawned(MazeRotator iMazeRotator,Transform iStartTransform)
    {
        _startTransform = iStartTransform;
        // setting up all the input controllers available
        foreach (var inp in _inputs)
            inp._Setup(iMazeRotator);

        // getting start and end Transform
        //_startTransform = GameObject.Find("Start").transform;
        //_endTransform = GameObject.Find("End").transform;

        // spawn ball
        BallSpawner._Instance._SpawnBall(GameSettingInfo.Instance.CurrentBallInfo.BallMesh, _startTransform, false, true, _spawnBallCompleted);
        _ballNameText.text = GameSettingInfo.Instance.CurrentBallInfo.BallName;
    }
    void _spawnBallCompleted(GameObject iBall)
    {
        _loadingImage.SetActive(false);
        _ballObject = iBall;
        //_ballObject.SetActive(false);
        BallPosition._Instance._SetBall(_ballObject.transform);
        // targeting all cameras to the ball
        _cameraManager._SetupCameras(_ballObject.transform, BallPosition._Instance.transform, _startTransform);

    }
    public void _LoadBall()
    {
        _ballObject.SetActive(true);
    }
    //public void _ChangeStat(GameStats iNewStat)
    //{
    //    _gameStats[_currentGameStat]._Ended();
    //    _currentGameStat =((int)iNewStat);
    //    _gameStats[_currentGameStat]._Started();
    //}

    public void _NextBall(int iDirection)
    {
        BallSpawner._Instance._UnloadBall();
        // go to next index based on direction
        GameSettingInfo.Instance.NextBall(iDirection);
        // spawn ball
        _loadingImage.SetActive(true);
        BallSpawner._Instance._SpawnBall(GameSettingInfo.Instance.CurrentBallInfo.BallMesh, _startTransform, false, true, _spawnBallCompleted);
        _ballNameText.text = GameSettingInfo.Instance.CurrentBallInfo.BallName;
        //_LoadBall();
    }

    private void Update()
    {
        //_gameStats[_currentGameStat]._Perform();
    }
}


public enum GameStats { LoadScene, SelectBall, StartGame, WinGame, LooseGame }