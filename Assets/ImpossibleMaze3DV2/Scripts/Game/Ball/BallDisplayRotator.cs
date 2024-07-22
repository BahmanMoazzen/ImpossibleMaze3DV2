using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDisplayRotator : MonoBehaviour
{
    [SerializeField] Vector3 _rotationDirection;
    public void _SetRotation(Vector3 iRotationDirection)
    {
        _rotationDirection = iRotationDirection;
    }
    private void FixedUpdate()
    {
        if (_rotationDirection != Vector3.zero)
        {
            transform.Rotate(_rotationDirection * Time.deltaTime);
        }

    }
}
