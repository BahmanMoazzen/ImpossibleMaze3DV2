using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepInputManager : MonoBehaviour
{
    Vector3 _dragCenter = Vector3.zero;
    [SerializeField] MazeRotator _rotator;
    private void OnDisable()
    {

    }
    private void OnEnable()
    {
        BAHMANSweepManager.OnStartDragging += BAHMANSweepManager_OnStartDragging;
        BAHMANSweepManager.OnDragging += BAHMANSweepManager_OnDragging;
    }

    private void BAHMANSweepManager_OnDragging(Vector3 iDragPosition)
    {
        Vector3 moveVector = iDragPosition - _dragCenter;
        moveVector.Normalize();
        _rotator._RotateMaze(new Vector3(-moveVector.y, 0, moveVector.x));

    }

    private void BAHMANSweepManager_OnStartDragging(Vector3 iStartPosition)
    {
        _dragCenter = iStartPosition;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
