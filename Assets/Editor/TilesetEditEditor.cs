using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilesetEdit))]
public class TilesetEditEditor : Editor
{
    private int tile;
    private int previousTile;
    private int rotation;
    private int objectRotation;

    private GameObject preview;

    public override void OnInspectorGUI()
    {
        previousTile = tile;
        DrawDefaultInspector();

        GUILayout.Space(8);

        TilesetEdit tilesetEdit = (TilesetEdit)target;
        GUILayout.Label("Tile");
        tile = (int)EditorGUILayout.Slider(tile, 0, 14);
        GUILayout.Label("Rotation: " + objectRotation + "°");
        rotation = (int)EditorGUILayout.Slider(rotation, 0, 3);
        objectRotation = rotation * 90;

        if (GUILayout.Button("Add Tile"))
        {
            tilesetEdit.Tiles.Add(new TilesetEdit.Tile(tile, objectRotation));
            tilesetEdit.CurrentIndex++;
            SceneView.RepaintAll();
        }

        if (previousTile != tile)
        {
            if (preview != null)
            {
                DestroyImmediate(preview);
            }

            preview = Instantiate(
                GameAssets.Instance.Walls[tile], 
                Vector3.zero,
         Quaternion.AngleAxis(objectRotation, Vector3.up)
            );
        }

        if (preview != null)
        {
            preview.transform.rotation = Quaternion.AngleAxis(objectRotation, Vector3.up);
        }
    }
}
