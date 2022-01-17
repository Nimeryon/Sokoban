using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum LevelValue
{
    None,
    Wall,
    Box,
    Goal,
    GoalBox,
    GoalPlayer,
    Player
}

public static class LevelValueExtention
{
    public static LevelValue From(string type)
    {
        return (LevelValue)Enum.Parse(typeof(LevelValue), type);
    }

    public static LevelValue Next<LevelValue>(this LevelValue type) where LevelValue : struct
    {
        LevelValue[] Values = (LevelValue[])Enum.GetValues(type.GetType());
        int index = Array.IndexOf(Values, type) + 1;
        return (Values.Length == index) ? Values[0] : Values[index];
    }

    public static LevelValue Prior<LevelValue>(this LevelValue type) where LevelValue : struct
    {
        LevelValue[] Values = (LevelValue[])Enum.GetValues(type.GetType());
        int index = Array.IndexOf(Values, type) - 1;
        return (index < 0) ? Values[Values.Length - 1] : Values[index];
    }

    public static void SetBackGround<LevelValue>(this LevelValue type) where LevelValue : struct
    {
        switch (type)
        {
            case global::LevelValue.None:
                GUI.backgroundColor = Color.grey;
                break;
            case global::LevelValue.Wall:
                GUI.backgroundColor = Color.yellow;
                break;
            case global::LevelValue.Box:
                GUI.backgroundColor = Color.blue;
                break;
            case global::LevelValue.Goal:
                GUI.backgroundColor = Color.red;
                break;
            case global::LevelValue.GoalBox:
                GUI.backgroundColor = Color.magenta;
                break;
            case global::LevelValue.Player:
                GUI.backgroundColor = Color.green;
                break;
            case global::LevelValue.GoalPlayer:
                GUI.backgroundColor = Color.white;
                break;
        }
    }
}
