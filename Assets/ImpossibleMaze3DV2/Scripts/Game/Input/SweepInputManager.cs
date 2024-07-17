using UnityEngine;

public class SweepInputManager : InputManagerAbstract
{
    Vector3 _dragCenter = Vector3.zero;
    private void OnDisable()
    {
        BAHMANSweepManager.OnStartDragging -= BAHMANSweepManager_OnStartDragging;
        BAHMANSweepManager.OnDragging -= BAHMANSweepManager_OnDragging;
    }
    private void OnEnable()
    {
        BAHMANSweepManager.OnStartDragging += BAHMANSweepManager_OnStartDragging;
        BAHMANSweepManager.OnDragging += BAHMANSweepManager_OnDragging;
    }

    private void BAHMANSweepManager_OnDragging(Vector3 iDragPosition)
    {
        if (_enable)
        {
            Vector3 moveVector = iDragPosition - _dragCenter;
            moveVector.Normalize();
            _rotator._RotateMaze(new Vector3(-moveVector.y, 0, moveVector.x));
        }

    }

    private void BAHMANSweepManager_OnStartDragging(Vector3 iStartPosition)
    {
        _dragCenter = iStartPosition;
    }


    public override void _Setup(MazeRotator iMazeRotator)
    {
        _rotator = iMazeRotator;
        _enable = true;
    }
}
