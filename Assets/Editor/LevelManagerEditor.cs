using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    Vector2 scrollPos;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelManager levelManager = (LevelManager)target;

        if (levelManager.Grid == null) return;
        GUILayout.Space(16);
        GUILayout.Label("Player Postion: x" + levelManager.Grid.PlayerPos.x + " y" + levelManager.Grid.PlayerPos.y);
        GUILayout.Label("Grid");
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        for (int y = 0; y < levelManager.Grid.Height; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < levelManager.Grid.Width; x++)
            {
                levelManager.Grid.GetValue(x, y).SetBackGround();
                GUILayout.Button("", GUILayout.Width(16), GUILayout.Height(16));
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }
}
