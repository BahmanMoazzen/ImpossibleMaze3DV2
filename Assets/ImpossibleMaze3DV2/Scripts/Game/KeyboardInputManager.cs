using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : MonoBehaviour
{
    [SerializeField] MazeRotator _mazeRotator;

    // Update is called once per frame
    void Update()
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
        _mazeRotator._RotateMaze(newRotation);




    }
}
