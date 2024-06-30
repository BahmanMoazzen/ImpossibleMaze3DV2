using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class CameraControllerAbstract : MonoBehaviour
{
    protected Transform _target;
    public virtual void _SetupCamera(Transform iBallTransform)
    {
        _target = iBallTransform;
    }

    protected virtual void Update()
    {

    }
    

}
