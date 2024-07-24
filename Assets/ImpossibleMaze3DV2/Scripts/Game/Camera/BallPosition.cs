using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour
{
    [SerializeField] Transform _ballTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = _ballTransform.position;
    }
}
