using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    public static event UnityAction OnCamraSetup;
    [SerializeField] CinemachineVirtualCamera[] _gameCameras;

    int _cameraIndex = 0;
    /// <summary>
    /// calls from other scrips to setup cameras
    /// </summary>
    /// <param name="iBallTransform">the transform of the ball in game</param>
    /// <param name="iLookAtTransform">the transform of the look at in game</param>
    public void _SetupCameras(Transform iBallTransform, Transform iLookAtTransform)
    {
        foreach (var cam in _gameCameras)
        {
            cam.Follow = iBallTransform;
            cam.LookAt = iLookAtTransform;
            cam.Priority = 0;
        }
        _gameCameras[_cameraIndex].Priority = 1;
    }
    public void _SwapCamera()
    {
        _gameCameras[_cameraIndex].Priority = 0;
        _cameraIndex++;
        if (_cameraIndex >= _gameCameras.Length)
        {
            _cameraIndex = 0;
        }

        _gameCameras[_cameraIndex].Priority = 1;

    }

}
