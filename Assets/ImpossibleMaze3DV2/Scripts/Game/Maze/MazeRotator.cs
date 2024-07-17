using System;
using UnityEngine;

public class MazeRotator : MonoBehaviour
{
    
    /// <summary>
    /// Maximum rotation of the maze
    /// </summary>
    [SerializeField] MazeRotationLimit _maxRotation;
    /// <summary>
    /// stores the current rotation of the maze
    /// </summary>
    Vector3 _currentRotation;
    /// <summary>
    /// the speed in wich the rotation should be handled
    /// </summary>
    [SerializeField] float _rotationSpeed;

    private void Awake()
    {
        
        _currentRotation = transform.rotation.eulerAngles;
        Debug.Log(_currentRotation.x);
    }
    public void _RotateMaze(Vector3 iRotation)
    {
        iRotation *= Time.deltaTime * _rotationSpeed * -1;
        _currentRotation += iRotation;
        _checkRotationBounds();
        transform.rotation = Quaternion.Euler(_currentRotation);
    }
    public void _SetRotationLimit(MazeRotationLimit iLimits)
    {
        _maxRotation = iLimits;
    }
    public void _SetupRotator(MazeRotationLimit iLimits, float iRotationSpeed)
    {
        _rotationSpeed = iRotationSpeed;
        _maxRotation = iLimits;
    }
    void _checkRotationBounds()
    {
        if (_currentRotation.x > _maxRotation.Xlimit.MaxRotation)
        {
            _currentRotation.x = _maxRotation.Xlimit.MaxRotation;
        }
        else if (_currentRotation.x < _maxRotation.Xlimit.MinRotation)
        {
            _currentRotation.x = _maxRotation.Xlimit.MinRotation;
        }

        if (_currentRotation.z > _maxRotation.Zlimit.MaxRotation)
        {
            _currentRotation.z = _maxRotation.Zlimit.MaxRotation;
        }
        else if (_currentRotation.z < _maxRotation.Zlimit.MinRotation)
        {
            _currentRotation.z = _maxRotation.Zlimit.MinRotation;
        }
        if (_currentRotation.y > _maxRotation.Ylimit.MaxRotation)
        {
            _currentRotation.y = _maxRotation.Ylimit.MaxRotation;
        }
        else if (_currentRotation.y < _maxRotation.Ylimit.MinRotation)
        {
            _currentRotation.y = _maxRotation.Ylimit.MinRotation;
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
