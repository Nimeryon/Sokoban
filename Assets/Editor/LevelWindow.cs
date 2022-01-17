using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class LevelWindow : EditorWindow
{
    private GridSystem grid = new();

    int tempWidth = 5;
    int tempHeight = 3;
    
    Vector2 scrollPosition = Vector2.zero;

    [MenuItem("Game/Level")]
    public static void Init()
    {
        GetWindow<LevelWindow>("Level");
    }

    void OnGUI()
    {
        // Header
        GUILayout.Label("Select Level Size");
        GUILayout.BeginHorizontal();
        tempWidth = EditorGUILayout.IntSlider("Width", tempWidth, 3, GridSystem.MaxWidth);
        tempHeight = EditorGUILayout.IntSlider("Height", tempHeight, 3, GridSystem.MaxHeight);
        if (GUILayout.Button("Resize")) { grid.SetSize(tempWidth, tempHeight); }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Reset Values")) { grid.Clear(); }
        if (GUILayout.Button("Reset Size"))
        {
            tempWidth = GridSystem.DefaultWidth;
            tempHeight = GridSystem.DefaultHeight;
            grid.SetSize(tempWidth, tempHeight);
            grid.Clear();
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Editor
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);
        EditorGUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        // North buttons
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("+", GUILayout.Width(32), GUILayout.Height(32))) { grid.AddLine(Direction.North); }
        if (GUILayout.Button("-", GUILayout.Width(32), GUILayout.Height(32))) { grid.RemoveLine(Direction.North); }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(16);

        // Full col buttons
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Box("", GUILayout.Width(32), GUILayout.Height(32));
        for (int x = 0; x < grid.Width; x++)
        {
            if (GUILayout.Button("V", GUILayout.Width(32), GUILayout.Height(32)))
            {
                if (Event.current.button == 1)
                {
                    grid.SetColPrior(x);
                }
                else
                {
                    grid.SetColNext(x);
                }
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        // Setup grid buttons
        int halfHeight = (grid.Height / 2) - 1;
        for (int y = 0; y < grid.Height; y++)
        {
            // Button col
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // West buttons
            if (y == halfHeight)
            {
                if (GUILayout.Button("+", GUILayout.Width(32), GUILayout.Height(32))) { grid.AddLine(Direction.West); }
                GUILayout.Space(16);
            }

            if (y == halfHeight + 1)
            {
                if (GUILayout.Button("-", GUILayout.Width(32), GUILayout.Height(32))) { grid.RemoveLine(Direction.West); }
                GUILayout.Space(16);
            }

            for (int x = 0; x < grid.Width; x++)
            {
                // Full row button
                if (x == 0)
                {
                    ResetBackgroundColor();
                    if (GUILayout.Button("=>", GUILayout.Width(32), GUILayout.Height(32)))
                    {
                        if (Event.current.button == 1)
                        {
                            grid.SetRowPrior(y);
                        }
                        else
                        {
                            grid.SetRowNext(y);
                        }
                    }
                }

                // Single cell button
                grid.GetValue(x, y).SetBackGround();
                if (GUILayout.Button(grid.GetValue(x, y).ToString()[0].ToString(), GUILayout.Width(32), GUILayout.Height(32)))
                {
                    if (Event.current.button == 1)
                    {
                        grid.SetPriorValue(x, y);
                    }
                    else
                    {
                        grid.SetNextValue(x, y);
                    }
                }
            }

            // Est buttons
            ResetBackgroundColor();
            if (y == halfHeight)
            {
                GUILayout.Space(16);
                if (GUILayout.Button("+", GUILayout.Width(32), GUILayout.Height(32))) { grid.AddLine(Direction.East); }
            }

            if (y == halfHeight + 1)
            {
                GUILayout.Space(16);
                if (GUILayout.Button("-", GUILayout.Width(32), GUILayout.Height(32))) { grid.RemoveLine(Direction.East); }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        ResetBackgroundColor();

        // South buttons
        GUILayout.Space(16);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("+", GUILayout.Width(32), GUILayout.Height(32))) { grid.AddLine(Direction.South); }
        if (GUILayout.Button("-", GUILayout.Width(32), GUILayout.Height(32))) { grid.RemoveLine(Direction.South); }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndVertical();
        GUILayout.EndScrollView();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Buttons
        if (GUILayout.Button("Save Level")) { grid.SaveToFile(); }
    }

    private void ResetBackgroundColor() { GUI.backgroundColor = Color.white; }
}
