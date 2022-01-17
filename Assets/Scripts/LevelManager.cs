using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GridSystem Grid;

    private GameObject[,] Objects;
    private GameObject[,] GroundObjects;
    [SerializeField] private GameObject Player;

    [Header("Animation")]
    [SerializeField] private LeanTweenType animationType;
    [SerializeField] private float animationTime = .1f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (GameManager.Instance == null)
            LoadFromFile(0);
        else LoadFromFile(GameManager.Instance.SelectedLevel);
    }

    public void LoadFromFile(int levelName)
    {
        Grid = GridSystem.LoadFromFile(levelName);
        GridSystem.OnMoveEvent += OnMove;

        Objects = new GameObject[Grid.Width, Grid.Height];
        GroundObjects = new GameObject[Grid.Width, Grid.Height];

        for (int y = 0; y < Grid.Height; y++)
        {
            for (int x = 0; x < Grid.Width; x++)
            {
                Vector3 pos = GetCenteredWorldPosition(x, y);
                Vector3 groundPos = new Vector3(pos.x, pos.y - .5f, pos.z);
                switch (Grid.GetValue(x, y))
                {
                    case LevelValue.Wall:
                        // Create Ground
                        GroundObjects[x, y] = Instantiate(
                            GameAssets.Instance.Ground,
                            groundPos,
                            Quaternion.identity,
                      transform.Find("Grounds")
                        );

                        // Create Wall
                        TilesetEdit.Tile wallTile = Grid.GetWallTile(x, y);
                        Objects[x, y] = Instantiate(
                            GameAssets.Instance.Walls[wallTile.Index],
                            pos,
                     Quaternion.AngleAxis(wallTile.Rotation, Vector3.up),
                      transform.Find("Objects")
                        );
                        break;

                    case LevelValue.Box:
                        // Create Ground
                        GroundObjects[x, y] = Instantiate(
                            GameAssets.Instance.Ground,
                            groundPos,
                            Quaternion.identity,
                      transform.Find("Grounds")
                        );

                        // Create Box
                        Objects[x, y] = Instantiate(
                            GameAssets.Instance.Box,
                            pos,
                            Quaternion.identity,
                      transform.Find("Objects")
                        );
                        Objects[x, y].GetComponent<BoxOutline>().SetRed();
                        break;

                    case LevelValue.Goal:
                        // Create Goal
                        Objects[x, y] = Instantiate(
                            GameAssets.Instance.Goal,
                            groundPos,
                            Quaternion.identity,
                      transform.Find("Objects")
                        );
                        break;

                    case LevelValue.GoalBox:
                        // Create Goal
                        Objects[x, y] = Instantiate(
                            GameAssets.Instance.Goal,
                            groundPos,
                            Quaternion.identity,
                            transform.Find("Objects")
                        );

                        // Create Box
                        Objects[x, y] = Instantiate(
                            GameAssets.Instance.Box,
                            pos,
                            Quaternion.identity,
                            transform.Find("Objects")
                        );
                        Objects[x, y].GetComponent<BoxOutline>().SetGreen();
                        break;

                    case LevelValue.Player:
                        // Create Ground
                        GroundObjects[x, y] = Instantiate(
                            GameAssets.Instance.Ground,
                            groundPos,
                            Quaternion.identity,
                            transform.Find("Grounds")
                        );

                        // Create Player
                        Player.transform.SetPositionAndRotation(pos, Quaternion.identity);
                        break;

                    case LevelValue.GoalPlayer:
                        // Create Goal
                        Objects[x, y] = Instantiate(
                            GameAssets.Instance.Goal,
                            groundPos,
                            Quaternion.identity,
                            transform.Find("Objects")
                        );

                        // Create Player
                        Player.transform.SetPositionAndRotation(pos, Quaternion.identity);
                        break;

                    case LevelValue.None:
                        // Create Ground
                        GroundObjects[x, y] = Instantiate(
                            GameAssets.Instance.Ground,
                            groundPos,
                            Quaternion.identity,
                      transform.Find("Grounds")
                        );
                        break;
                }
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        var pos = transform.position;
        var halfWidth = Grid.Width / 2f;
        var halfHeight = Grid.Height / 2f;
        return new Vector3((x) - halfWidth + pos.x, pos.y, (y) - halfHeight + pos.z);
    }

    public Vector3 GetCenteredWorldPosition(int x, int y)
    {
        var pos = GetWorldPosition(x, y);
        return new Vector3(
            pos.x + .5f,
            pos.y,
            pos.z + .5f
        );
    }

    public void OnMove(LevelValue type, Vector2Int gridElement, Vector2Int finalPos)
    {
        if (type == LevelValue.Player)
        {
            LeanTween.moveLocal(Player, GetCenteredWorldPosition(finalPos.x, finalPos.y), animationTime)
                .setEase(animationType);
        }
        else if (type == LevelValue.Box)
        {
            GameObject box = Objects[gridElement.x, gridElement.y];
            LeanTween.moveLocal(box, GetCenteredWorldPosition(finalPos.x, finalPos.y), animationTime)
                .setEase(animationType).setOnComplete(() =>
                    {
                        // Set Outline
                        if (Grid.GetValue(finalPos.x, finalPos.y) == LevelValue.GoalBox) Objects[finalPos.x, finalPos.y].GetComponent<BoxOutline>().SetGreen();
                        else Objects[finalPos.x, finalPos.y].GetComponent<BoxOutline>().SetRed();
                    }
                );
            Objects[gridElement.x, gridElement.y] = null;
            Objects[finalPos.x, finalPos.y] = box;
        }
    }

    void OnDisable()
    {
        GridSystem.OnMoveEvent -= OnMove;
    }
}
