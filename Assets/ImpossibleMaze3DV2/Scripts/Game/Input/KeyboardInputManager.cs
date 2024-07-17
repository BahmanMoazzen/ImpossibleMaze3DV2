using UnityEngine;

public class KeyboardInputManager : InputManagerAbstract
{

    public override void _Setup(MazeRotator iMazeRotator)
    {
        _rotator = iMazeRotator;
        _enable = true;
    }

    void Update()
    {

        if (_enable)
        {
            Vector3 newRotation = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                newRotation.z = 1;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                newRotation.z = -1;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                newRotation.x = -1;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                newRotation.x = 1;
            }
            _rotator._RotateMaze(newRotation);

        }

    }
}
