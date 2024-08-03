using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour
{
    const float MINIMUM_Y_TO_LOOSE = -10;
    public static BallPosition _Instance;

    Transform _ballTransform;
    bool _isChasing = true;


    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void FixedUpdate()
    {
        if (_ballTransform != null && _isChasing)
        {
            transform.position = _ballTransform.position;
            if(transform.position.y<MINIMUM_Y_TO_LOOSE)
            {
                InGameInfo.Instance.IsBallDroped = true;
                _isChasing = false;
            }
        }

    }

    public void _SetBall(Transform iBallTransform)
    {
        _ballTransform = iBallTransform;
    }


}
