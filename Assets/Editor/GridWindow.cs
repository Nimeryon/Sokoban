using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridWindow : EditorWindow
{
    int Width = 5;
    int Height = 5;

    [MenuItem("Game/Edit Level")]
    public static void ShowWindow()
    {
        GetWindow<GridWindow>("Edit Level");
    }

    void OnGUI()
    {
        for (int y = 0; y < Height; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < Width; x++)
            {
                GUILayout.Label(string.Format("X: {0}, Y: {1}", x, y), EditorStyles.boldLabel);
                EditorGUILayout.EnumPopup(GridValue.None);
            }
            GUILayout.EndHorizontal();
        }
    }
}
