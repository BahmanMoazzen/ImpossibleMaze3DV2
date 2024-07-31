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
            if(currentIngameinfo == null)
            {
                currentIngameinfo = Resources.Load<InGameInfo>("InGame");
            }
            return currentIngameinfo;
        }
    }
    public event UnityAction OnGameFinished;
    public event UnityAction<Vector3> OnMazeRotated;
    public event UnityAction<float,float> OnMazeRotatedSlider;
    public bool IsGameFinished
    {
        set
        {
            if (value)
                OnGameFinished?.Invoke();
        }
    }
    public Vector3 MazeRotation
    {
        set
        {
            OnMazeRotated?.Invoke(value);
            //float xSlider = value.x/DefaultRotatorLimit.Xlimit.;
        }
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
