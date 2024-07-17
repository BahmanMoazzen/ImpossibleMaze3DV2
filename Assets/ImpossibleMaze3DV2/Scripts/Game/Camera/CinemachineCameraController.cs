using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCameraController : CameraControllerAbstract
{
    [SerializeField] CinemachineVirtualCamera _camera;
    public override void _SetupCamera(Transform iBallTransform)
    {
        base._SetupCamera(iBallTransform);

        _camera.LookAt = _target;
        _camera.Follow = _target;

    }
}
