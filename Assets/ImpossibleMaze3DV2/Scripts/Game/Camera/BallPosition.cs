using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour
{
    public static BallPosition _Instance;

    Transform _ballTransform;


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
        if (_ballTransform != null)
        {
            transform.position = _ballTransform.position;
        }

    }

    public void _SetBall(Transform iBallTransform)
    {
        _ballTransform = iBallTransform;
    }


}
