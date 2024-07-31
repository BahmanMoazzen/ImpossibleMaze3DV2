using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "NewSettings", order = 2, menuName = "BAHMAN Unity Assets/New Game Settings")]
public class GameSettingInfo : ScriptableObject
{
    private static GameSettingInfo gameSetting;
    public static GameSettingInfo Instance
    {

        get
        {
            if (gameSetting == null)
            {
                gameSetting = Resources.Load<GameSettingInfo>("GameSettings");
            }
            return gameSetting;
        }
    }
    const string SOUNDFXTAG = "SoundFXTag", ANTIALIASINGTAG = "AntiAliasingTag", MUSICTAG = "MUSICTag";
    public static event UnityAction<bool> OnSoundFXChange;
    public static event UnityAction<bool> OnMusicChange;
    public static event UnityAction<bool> OnAntialiasingChange;

    [SerializeField] bool antiAliasing = true;
    [SerializeField] bool soundFX = true;
    [SerializeField] bool music = true;
    public bool SoundFX
    {
        get
        {
            return A.Tools.IntToBool(PlayerPrefs.GetInt(SOUNDFXTAG, A.Tools.BoolToInt(soundFX)));

        }
        set
        {
            OnSoundFXChange?.Invoke(value);
            PlayerPrefs.SetInt(SOUNDFXTAG, A.Tools.BoolToInt(value));
        }
    }
    public bool Music
    {
        get
        {
            return A.Tools.IntToBool(PlayerPrefs.GetInt(MUSICTAG, A.Tools.BoolToInt(music)));

        }
        set
        {
            OnMusicChange?.Invoke(value);
            PlayerPrefs.SetInt(MUSICTAG, A.Tools.BoolToInt(value));
        }
    }
    public bool AntiAliasing
    {
        get
        {

            return A.Tools.IntToBool(PlayerPrefs.GetInt(ANTIALIASINGTAG, A.Tools.BoolToInt(antiAliasing)));

        }
        set
        {
            OnAntialiasingChange?.Invoke(value);
            PlayerPrefs.SetInt(ANTIALIASINGTAG, A.Tools.BoolToInt(value));
        }
    }


    const string GameLevelSaveTag = "GameLevelSaveTag";


    public bool IsGameWon;
    public int ThisRunScore;

    [SerializeField] int currentGameLevel;

    public int CurrentGameLevel
    {
        get
        {
            return PlayerPrefs.GetInt(GameLevelSaveTag, currentGameLevel);
        }
        set
        {
            PlayerPrefs.SetInt(GameLevelSaveTag, value);
        }
    }
    const string BallSaveTag = "BallSaveTag";
    [SerializeField] int currentBall;
    public int CurrentBall
    {
        get
        {
            return PlayerPrefs.GetInt(BallSaveTag, currentBall);
        }
        set
        {
            PlayerPrefs.SetInt(BallSaveTag, value);
        }
    }
    public LevelInfo CurrentLevelSkeletone
    {
        get
        {

            return AllLevels[currentGameLevel];
        }
    }
    public BallInfo CurrentBallInfo
    {
        get
        {

            return AllBalls[CurrentBall];
        }
    }
    public void NextBall(int iDirection)
    {
        int cBall = CurrentBall;
        cBall += iDirection;
        if (cBall < 0)
        {
            cBall = AllBalls.Length - 1;
        }
        else if (cBall >= AllBalls.Length)
        {
            cBall = 0;
        }
        CurrentBall= cBall;
    }
    public LevelInfo[] AllLevels;
    public BallInfo[] AllBalls;

}


