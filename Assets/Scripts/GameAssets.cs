using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets Instance
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    // Objects
    public GameObject[] Walls;
    public GameObject Box;
    public GameObject Player;
    public GameObject Goal;
    public GameObject Ground;
}
