using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) LevelManager.Instance.Grid.MovePlayer(Direction.North);
        if (Input.GetKeyDown(KeyCode.DownArrow)) LevelManager.Instance.Grid.MovePlayer(Direction.South);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) LevelManager.Instance.Grid.MovePlayer(Direction.West);
        if (Input.GetKeyDown(KeyCode.RightArrow)) LevelManager.Instance.Grid.MovePlayer(Direction.East);
    }
}
