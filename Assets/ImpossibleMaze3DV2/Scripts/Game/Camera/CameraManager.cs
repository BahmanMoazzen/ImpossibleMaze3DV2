using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    public static event UnityAction OnCamraSetup;
    [SerializeField] CinemachineVirtualCamera[] _gameCameras;
    /// <summary>
    /// calls from other scrips to setup cameras
    /// </summary>
    /// <param name="iBallTransform">the transform of the ball in game</param>
    /// <param name="iLookAtTransform">the transform of the look at in game</param>
    public void _SetupCameras(Transform iBallTransform,Transform iLookAtTransform)
    {
        
    }

}
