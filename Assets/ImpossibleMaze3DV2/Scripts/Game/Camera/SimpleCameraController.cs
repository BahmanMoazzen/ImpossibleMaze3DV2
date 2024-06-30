using UnityEngine;

public class SimpleCameraController : CameraControllerAbstract
{
    [SerializeField] Camera _camera;
    Vector3 _offset;
    bool _enabled = false;
    public override void _SetupCamera(Transform iBallTransform)
    {
        base._SetupCamera(iBallTransform);
        _offset = transform.position - _target.position;
        _enabled = true;
    }

    protected override void Update()
    {
        base.Update();
        if (_enabled)
        {
            transform.position = new Vector3(_target.position.x + _offset.x, transform.position.y, _target.position.z + _offset.z);
        }
    }

}
