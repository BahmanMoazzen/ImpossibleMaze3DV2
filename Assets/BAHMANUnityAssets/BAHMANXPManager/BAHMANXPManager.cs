/*
 * BAHMANXPManager v1.1
 * to manage player xp level this class is usefull
 * 
 * just use _SetExperience to add to the current xp and listen to  OnLevelUp<FromLevel,ToLevel> for any level change.
 * also current Level can be obtained by _GetCurrentLevel
 * for display purposes _GetSliderValue returns a float between 0 to 1 which indicates the normalized value of current xp level. 
 * it use to set up any XP slider
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BAHMANXPManager : MonoBehaviour
{
    /// <summary>
    /// the tag by which the level saves on PlayerPrefab
    /// </summary>
    const string TAG = "XPManagerTag";
    /// <summary>
    /// singletone instance
    /// </summary>
    public static BAHMANXPManager _Instance;
    /// <summary>
    /// Invokes whenever Player levels up
    /// int:From int:To
    /// </summary>
    public static event UnityAction<int, int> OnLevelUp;

    List<levelStructure> _levelSteps;
    [SerializeField] int _stepLevel = 2;
    [SerializeField] int _baseLevel = 0;
    [SerializeField] float _baseXP = 0;

    [SerializeField] bool _provideDebugInformation = false;
    /// <summary>
    /// set/get the XP information from PlayerPref
    /// </summary>
    float _currentXP
    {
        get
        {
            return PlayerPrefs.GetFloat(TAG, _baseXP);
        }
        set
        {
            PlayerPrefs.SetFloat(TAG, value);
        }
    }
    /// <summary>
    /// provides debug information 
    /// </summary>
    /// <param name="iMessage">the message to show</param>
    void _dlog(string iMessage)
    {
        if(_provideDebugInformation)
        {
            Debug.Log(iMessage);
        }
    }
    private void Awake()
    {
        #region singleton setup
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion


        // setting up level structure to level 120
        _levelSteps = new List<levelStructure>();
        _levelSteps.Add(new levelStructure(0, _stepLevel));
        for (int i = 1; i < 120; i++)
        {

            _levelSteps.Add(
                new levelStructure(
                _levelSteps[i - 1].EndXP + 1,
                _levelSteps[i - 1].EndXP + Mathf.Pow(_stepLevel, i)
                )
                );

        }
    }
    private void Start()
    {
        Debug.Log("CurrentLevel: " + _GetCurrentLevel() + " CurrentXP: " + _currentXP);
    }
    /// <summary>
    /// calculates the current level of the player
    /// </summary>
    /// <returns>the current level of the player</returns>
    public int _GetCurrentLevel()
    {
        return _XPToLevel(_currentXP);
    }
    /// <summary>
    /// setting XP 
    /// </summary>
    /// <param name="iXPAmount">the amount to set</param>
    public void _SetExperience(int iXPAmount)
    {
        int currentLevel = _XPToLevel(_currentXP);
        _dlog("XP to Set: " + iXPAmount);
        _currentXP += iXPAmount;
        int nextLevel = _XPToLevel(_currentXP);

        if (nextLevel > currentLevel)
        {
            _dlog("Level Up");
            OnLevelUp?.Invoke(currentLevel, nextLevel);
        }

    }
    /// <summary>
    /// The value to set to the slider for diplaying purposes. it ranges from 0 to 1
    /// </summary>
    /// <returns>the slider value</returns>
    public float _GetSliderValue()
    {
        levelStructure _currentLevel = _levelSteps[_GetCurrentLevel()];
        float range = _currentLevel.EndXP - _currentLevel.StartXP;
        return (_currentXP - _currentLevel.StartXP) / range;

    }
    int _XPToLevel(float iCurrentXP)
    {
        int currentLevel = _baseLevel;

        foreach (levelStructure item in _levelSteps)
        {
            if (iCurrentXP >= item.StartXP && iCurrentXP <= item.EndXP)
            {
                return currentLevel;
            }
            currentLevel++;
        }
        return -1;
    }
}
/// <summary>
/// the level structure to show on the script menu
/// </summary>
[System.Serializable]
struct levelStructure
{
    public float StartXP;
    public float EndXP;
    public levelStructure(float iStart, float iEnd)
    {
        StartXP = iStart;
        EndXP = iEnd;
    }
}
