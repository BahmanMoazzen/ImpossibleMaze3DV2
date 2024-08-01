using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "NewIngameInfo", menuName = "Impossible Maze 2/New In Game Information", order = 2)]
public class InGameInfo : ScriptableObject
{
    private static InGameInfo currentIngameinfo;
    public static InGameInfo Instance
    {
        get
        {
            if (currentIngameinfo == null)
            {
                currentIngameinfo = Resources.Load<InGameInfo>("InGame");
            }
            return currentIngameinfo;
        }
    }
    public event UnityAction OnTimeUp;
    public event UnityAction OnGameWon;
    public event UnityAction OnBallDroped;
    public event UnityAction OnBallLoaded;
    public event UnityAction OnLevelLoaded;
    public event UnityAction<Vector3> OnMazeRotated;
    public bool IsGameWon
    {
        set
        {
            if (value)
                OnGameWon?.Invoke();
        }
    }
    public bool IsBallDroped
    {
        set
        {
            if (value)
                OnBallDroped?.Invoke();
        }
    }
    public bool IsTimeUp
    {
        set
        {
            if (value)
                OnTimeUp?.Invoke();
        }
    }
    public Vector3 MazeRotation
    {
        set
        {
            OnMazeRotated?.Invoke(value);
        }
    }

    public Transform StartPointTransform;
    public Transform EndPointTransform;
    public MazeRotator GameMazeRotator;
    public GameObject GameBall;

    public void SetGameMaze(Transform iStartTransform, Transform iEndTransform, MazeRotator iMazeRotator)
    {
        StartPointTransform = iStartTransform;
        EndPointTransform = iEndTransform;
        GameMazeRotator = iMazeRotator;
        OnLevelLoaded?.Invoke();
    }

    public void SetBallLoaded(GameObject iBallObject)
    {
        GameBall = iBallObject;
        OnBallLoaded?.Invoke();
    }




    /// <summary>
    /// the default rotation limit of the rotator
    /// </summary>
    public MazeRotationLimit DefaultRotatorLimit
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


[Serializable]
public struct RotationLimit
{
    public float MinRotation;
    public float MaxRotation;
    public RotationLimit(float iMinRotation, float iMaxRotation)
    {
        MinRotation = iMinRotation;
        MaxRotation = iMaxRotation;
    }
}
[Serializable]
public struct MazeRotationLimit
{
    //public enum RotationAxis { X, Y, Z };
    public RotationLimit Xlimit;
    public RotationLimit Ylimit;
    public RotationLimit Zlimit;
    public MazeRotationLimit(RotationLimit iXLimit, RotationLimit iYLimit, RotationLimit iZLimit)
    {
        Xlimit = iXLimit;
        Ylimit = iYLimit;
        Zlimit = iZLimit;
    }
}
