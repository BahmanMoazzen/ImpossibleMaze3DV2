using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class InputManagerAbstract : MonoBehaviour
{
    [SerializeField] protected MazeRotator _rotator;
    protected bool _enable = false;
    public abstract void _Setup(MazeRotator iMazeRotator);
}
