using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    public static event UnityAction OnCamraSetup;
    [SerializeField] CameraControllerAbstract[] _gameCameras;
    /// <summary>
    /// calls from other scrips to setup cameras
    /// </summary>
    /// <param name="iBallTransform">the transform of the ball in game</param>
    public void _SetupCameras(Transform iBallTransform)
    {
        foreach (var cam in _gameCameras)
        {
            cam._SetupCamera(iBallTransform);
        }
        OnCamraSetup?.Invoke();
    }

}
