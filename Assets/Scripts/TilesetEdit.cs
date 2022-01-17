using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesetEdit : MonoBehaviour
{
    [Serializable]
    public class Tile
    {
        public int Index;
        public int Rotation;

        public Tile(int index, int rotation)
        {
            Index = index;
            Rotation = rotation;
        }

        public override string ToString()
        {
            return Index + "-" + Rotation;
        }
    }

    public List<Tile> Tiles = new();
    public int CurrentIndex = 0;

    string GetBits()
    {
        return Convert.ToString(CurrentIndex, 2).PadLeft(8, '0');
    }

    void SetGizmosColor(char bit)
    {
        if (bit == '0') Gizmos.color = Color.gray;
        else Gizmos.color = Color.white;
    }

    /* void OnDrawGizmos()
    {
        string bits = GetBits();

        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(-2, 0, 0), Vector3.one * .5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector3(0, 0, 2), Vector3.one * .5f);

        SetGizmosColor(bits[7]);
        Gizmos.DrawCube(new Vector3(-1, 0, 0), Vector3.one * .9f);

        SetGizmosColor(bits[6]);
        Gizmos.DrawCube(new Vector3(-1, 0, 1), Vector3.one * .9f);

        SetGizmosColor(bits[5]);
        Gizmos.DrawCube(new Vector3(0, 0, 1), Vector3.one * .9f);

        SetGizmosColor(bits[4]);
        Gizmos.DrawCube(new Vector3(1, 0, 1), Vector3.one * .9f);

        SetGizmosColor(bits[3]);
        Gizmos.DrawCube(new Vector3(1, 0, 0), Vector3.one * .9f);

        SetGizmosColor(bits[2]);
        Gizmos.DrawCube(new Vector3(1, 0, -1), Vector3.one * .9f);

        SetGizmosColor(bits[1]);
        Gizmos.DrawCube(new Vector3(0, 0, -1), Vector3.one * .9f);

        SetGizmosColor(bits[0]);
        Gizmos.DrawCube(new Vector3(-1, 0, -1), Vector3.one * .9f);
    }*/
}
