using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] GameObject _gameMaze;
    [SerializeField] InputManagerAbstract[] _inputs;
    IEnumerator Start()
    {
        yield return null;
        GameObject parentMaze = new GameObject();
        parentMaze.name = "LevelMaze";
        GameObject mazeSkleton =
        Instantiate(_gameMaze, parentMaze.transform);
        mazeSkleton.AddComponent<MeshCollider>();
        Rigidbody mazeBody =
        parentMaze.AddComponent<Rigidbody>();
        mazeBody.angularDrag = mazeBody.drag = 0;
        mazeBody.isKinematic = true;
        mazeBody.mass = 1;
        mazeBody.useGravity = false;
        MazeRotationLimit newLimit = new MazeRotationLimit(
            new RotationLimit(-15, 15),
            new RotationLimit(0, 0),
            new RotationLimit(-15, 15)
            );

        parentMaze.AddComponent<MazeRotator>()._SetupRotator(newLimit, 50f);
        foreach (var inp in _inputs)
            inp._Setup(parentMaze.GetComponent<MazeRotator>());
    }


}
