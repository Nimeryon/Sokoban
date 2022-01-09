using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public enum GridValue {
    None,
    Player,
    Wall,
    Box,
    Goal
}

public class Grid : MonoBehaviour
{
    public static Grid Instance { get; private set; }

    public int Width = 5;
    public int Height = 5;
    public float CellSize = 1f;

    private GridValue[,] _map;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    void OnValidate()
    {
        Width = Math.Clamp(Width, 5, int.MaxValue);
        Height = Math.Clamp(Height, 5, int.MaxValue);
        CellSize = Math.Clamp(CellSize, 1, int.MaxValue);

        if (_map == null) _map = new GridValue[Width, Height];
        else
        {
            var oldWidth = _map.GetLength(0);
            var oldHeight = _map.GetLength(1);
            var curMinWidth = Math.Min(Width, oldWidth);
            var curMinHeight = Math.Min(Width, oldHeight);
            var map = new GridValue[Width, Height];

            for (int y = 0; y < curMinHeight; y++)
            {
                for (int x = 0; x < curMinWidth; x++)
                {
                    try
                    {
                        map[x, y] = _map[x, y];
                    }
                    catch (IndexOutOfRangeException e) {}
                }
            }

            _map = map;
        }
    }

    void OnDrawGizmos()
    {
        for (int x = 0; x < _map.GetLength(0); x++)
        {
            for (int y = 0; y < _map.GetLength(1); y++)
            {
                Gizmos.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y));
                Gizmos.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1));
            }
        }

        Gizmos.DrawLine(GetWorldPosition(0, Height), GetWorldPosition(Width, Height));
        Gizmos.DrawLine(GetWorldPosition(Width, 0), GetWorldPosition(Width, Height));
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * CellSize;
    }

    public void BuildLevel()
    {
        Debug.Log("Building Level!");
    }

    public void SaveLevel()
    {
        Debug.Log("Saving Level!");
    }

    public void LoadLevel()
    {
        Debug.Log("Loading Level!");
    }
}
