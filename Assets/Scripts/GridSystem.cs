using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public enum Direction
{
    North,
    South,
    West,
    East
}

public struct Move
{
    public LevelValue EmitterType;
    public LevelValue FromType;
    public LevelValue ToType;
    public Vector2Int From;
    public Vector2Int To;

    public Move(LevelValue emitterType, LevelValue fromType, LevelValue toType, Vector2Int from, Vector2Int to)
    {
        EmitterType = emitterType;
        FromType = fromType;
        ToType = toType;
        From = from;
        To = to;
    }
}

public class GridSystem
{
    public static List<TilesetEdit.Tile> WallTiles = new()
    {
        new(0, 0),
        new(11, 180),
        new(0, 0),
        new(11, 180),
        new(11, 270),
        new(9, 270),
        new(11, 270),
        new(12, 270),
        new(0, 0),
        new(11, 180),
        new(0, 0),
        new(11, 180),
        new(11, 270),
        new(9, 270),
        new(11, 270),
        new(12, 270),
        new(11, 0),
        new(3, 0),
        new(11, 0),
        new(3, 0),
        new(9, 0),
        new(8, 0),
        new(9, 0),
        new(14, 0),
        new(11, 0),
        new(3, 0),
        new(11, 0),
        new(3, 0),
        new(12, 0),
        new(10, 0),
        new(12, 0),
        new(7, 0),
        new(0, 0),
        new(11, 180),
        new(0, 0),
        new(11, 180),
        new(11, 270),
        new(9, 270),
        new(11, 270),
        new(12, 270),
        new(0, 0),
        new(11, 180),
        new(0, 0),
        new(11, 180),
        new(11, 270),
        new(9, 270),
        new(11, 270),
        new(12, 270),
        new(11, 0),
        new(3, 0),
        new(11, 0),
        new(3, 0),
        new(9, 0),
        new(8, 0),
        new(9, 0),
        new(14, 0),
        new(11, 0),
        new(3, 0),
        new(11, 0),
        new(3, 0),
        new(12, 0),
        new(10, 0),
        new(12, 0),
        new(7, 0),
        new(11, 90),
        new(9, 180),
        new(11, 90),
        new(9, 180),
        new(3, 90),
        new(8, 270),
        new(3, 270),
        new(10, 270),
        new(11, 90),
        new(9, 180),
        new(11, 90),
        new(9, 180),
        new(3, 90),
        new(8, 270),
        new(3, 90),
        new(10, 270),
        new(9, 90),
        new(8, 180),
        new(9, 90),
        new(8, 180),
        new(8, 90),
        new(2, 0),
        new(8, 90),
        new(13, 90),
        new(9, 90),
        new(8, 180),
        new(9, 90),
        new(8, 180),
        new(14, 90),
        new(13, 180),
        new(14, 90),
        new(5, 180),
        new(11, 90),
        new(9, 180),
        new(11, 90),
        new(9, 180),
        new(3, 90),
        new(8, 270),
        new(3, 90),
        new(10, 270),
        new(11, 90),
        new(9, 180),
        new(11, 90),
        new(9, 180),
        new(3, 270),
        new(8, 270),
        new(3, 90),
        new(10, 270),
        new(12, 90),
        new(14, 180),
        new(12, 90),
        new(14, 180),
        new(10, 90),
        new(13, 270),
        new(10, 90),
        new(4, 90),
        new(12, 90),
        new(14, 180),
        new(12, 90),
        new(14, 180),
        new(7, 90),
        new(5, 270),
        new(7, 90),
        new(6, 180),
        new(0, 0),
        new(11, 180),
        new(0, 0),
        new(11, 180),
        new(11, 270),
        new(9, 270),
        new(11, 270),
        new(9, 270),
        new(0, 0),
        new(11, 180),
        new(0, 0),
        new(11, 180),
        new(11, 270),
        new(9, 270),
        new(11, 270),
        new(12, 270),
        new(11, 0),
        new(3, 0),
        new(11, 0),
        new(3, 0),
        new(9, 0),
        new(8, 0),
        new(9, 0),
        new(14, 0),
        new(11, 0),
        new(3, 0),
        new(11, 0),
        new(3, 0),
        new(12, 0),
        new(10, 0),
        new(12, 0),
        new(7, 0),
        new(0, 0),
        new(11, 180),
        new(0, 0),
        new(11, 180),
        new(11, 270),
        new(9, 270),
        new(11, 270),
        new(12, 270),
        new(0, 0),
        new(11, 180),
        new(0, 0),
        new(11, 180),
        new(11, 270),
        new(9, 270),
        new(11, 270),
        new(12, 270),
        new(11, 0),
        new(3, 0),
        new(11, 0),
        new(3, 0),
        new(9, 0),
        new(8, 0),
        new(9, 0),
        new(14, 0),
        new(11, 0),
        new(3, 0),
        new(11, 0),
        new(3, 0),
        new(12, 0),
        new(10, 0),
        new(12, 0),
        new(7, 0),
        new(11, 90),
        new(12, 180),
        new(11, 90),
        new(12, 180),
        new(3, 90),
        new(14, 270),
        new(3, 90),
        new(7, 270),
        new(11, 90),
        new(12, 180),
        new(11, 90),
        new(12, 180),
        new(3, 90),
        new(14, 270),
        new(3, 90),
        new(7, 270),
        new(9, 90),
        new(10, 180),
        new(9, 90),
        new(10, 180),
        new(8, 90),
        new(13, 0),
        new(8, 90),
        new(5, 90),
        new(9, 90),
        new(10, 180),
        new(9, 90),
        new(10, 180),
        new(14, 90),
        new(4, 0),
        new(14, 90),
        new(6, 90),
        new(11, 90),
        new(12, 180),
        new(11, 90),
        new(12, 180),
        new(3, 90),
        new(14, 270),
        new(3, 90),
        new(7, 270),
        new(11, 90),
        new(12, 180),
        new(11, 90),
        new(12, 180),
        new(3, 90),
        new(14, 270),
        new(3, 90),
        new(7, 270),
        new(12, 90),
        new(7, 180),
        new(12, 90),
        new(7, 180),
        new(10, 90),
        new(5, 0),
        new(10, 90),
        new(6, 0),
        new(12, 90),
        new(7, 180),
        new(12, 90),
        new(7, 180),
        new(7, 90),
        new(6, 270),
        new(7, 90),
        new(1, 0)
    };

    public const int MinWidth = 3;
    public const int MaxWidth = 50;
    public const int MinHeight = 3;
    public const int MaxHeight = 25;
    public const int DefaultWidth = 5;
    public const int DefaultHeight = 3;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public Vector2Int PlayerPos;

    public delegate void MoveEvent(LevelValue type, Vector2Int gridElement, Vector2Int finalPos);
    public static event MoveEvent OnMoveEvent;

    private LevelValue[,] Values;
    private List<List<Move>> MoveHistory;

    public GridSystem() : this(DefaultWidth, DefaultHeight) {}
    public GridSystem(int width, int height)
    {
        Width = width;
        Height = height;
        Values = new LevelValue[Width, Height];
        MoveHistory = new List<List<Move>>();
    }

    public static GridSystem LoadFromFile(int levelName)
    {
        string levelFilePath = Application.streamingAssetsPath + "/Levels/" + levelName + ".lvl";
        string[] lines = File.ReadAllLines(levelFilePath);

        int width = int.Parse(lines[0]);
        int height = int.Parse(lines[1]);
        GridSystem grid = new(width, height);

        for (int y = 0; y < height; y++)
        {
            string[] values = lines[y + 2].Split(",");
            for (int x = 0; x < width; x++)
            {
                LevelValue value = LevelValueExtention.From(values[x]);
                if (value == LevelValue.Player || value == LevelValue.GoalPlayer) grid.PlayerPos = new Vector2Int(x, y);
                grid.SetValue(value, x, y);
            }
        }

        GameManager.Instance.SetGoals(grid.GetGoals());
        GameManager.Instance.SetCompleteGoals(grid.GetCompleteGoals());
        return grid;
    }

    // Getters
    public LevelValue GetValue(int x, int y)
    {
        if (!IsBounded(x, y)) return LevelValue.None;
        return Values[x, y];
    }

    // Setters
    public void SetValue(LevelValue value, int x, int y)
    {
        if (!IsBounded(x, y)) return;
        Values[x, y] = value;
    }
    public void SetNextValue(int x, int y)
    {
        if (!IsBounded(x, y)) return;
        SetValue(GetValue(x, y).Next(), x, y);
    }
    public void SetPriorValue(int x, int y)
    {
        if (!IsBounded(x, y)) return;
        SetValue(GetValue(x, y).Prior(), x, y);
    }
    public void SetRow(LevelValue value, int y)
    {
        if (!IsBounded(0, y)) return;
        for (int x = 0; x < Width; x++)
        {
            SetValue(value, x, y);
        }
    }
    public void SetRowNext(int y)
    {
        if (!IsBounded(0, y)) return;
        for (int x = 0; x < Width; x++)
        {
            SetNextValue(x, y);
        }
    }
    public void SetRowPrior(int y)
    {
        if (!IsBounded(0, y)) return;
        for (int x = 0; x < Width; x++)
        {
            SetPriorValue(x, y);
        }
    }
    public void SetCol(LevelValue value, int x)
    {
        if (!IsBounded(x, 0)) return;
        for (int y = 0; y < Height; y++)
        {
            SetValue(value, x, y);
        }
    }
    public void SetColNext(int x)
    {
        if (!IsBounded(x, 0)) return;
        for (int y = 0; y < Height; y++)
        {
            SetNextValue(x, y);
        }
    }
    public void SetColPrior(int x)
    {
        if (!IsBounded(x, 0)) return;
        for (int y = 0; y < Height; y++)
        {
            SetPriorValue(x, y);
        }
    }
    public void SetSize(int width, int height)
    {
        if (width > 0 && height > 0 && width == Width && height == Height) return;

        Width = width;
        Height = height;

        LevelValue[,] newValues = new LevelValue[Width, Height];
        int minWidth = Math.Min(Width, Values.GetLength(0));
        int minHeight = Math.Min(Height, Values.GetLength(1));
        for (int y = 0; y < minHeight; y++)
        {
            for (int x = 0; x < minWidth; x++)
            {
                newValues[x, y] = GetValue(x, y);
            }
        }

        Values = newValues;
    }

    // Utilities
    public int GetGoals()
    {
        int goalCount = 0;
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (GetValue(x, y) == LevelValue.Goal || GetValue(x, y) == LevelValue.GoalBox) goalCount++;
            }
        }
        return goalCount;
    }
    public int GetCompleteGoals()
    {
        int completeGoalCount = 0;
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (GetValue(x, y) == LevelValue.GoalBox) completeGoalCount++;
            }
        }
        return completeGoalCount;
    }
    public Vector2Int GetNextPos(int x, int y, Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                if (IsBounded(x, y + 1)) return new Vector2Int(x, y + 1);
                break;

            case Direction.South:
                if (IsBounded(x, y - 1)) return new Vector2Int(x, y - 1);
                break;

            case Direction.West:
                if (IsBounded(x - 1, y)) return new Vector2Int(x - 1, y);
                break;

            case Direction.East:
                if (IsBounded(x + 1, y)) return new Vector2Int(x + 1, y);
                break;
        }
        return new Vector2Int(x, y);
    }
    public void MovePlayer(Direction direction)
    {
        List<Move> moves = new List<Move>();

        Vector2Int basePlayerPos = PlayerPos;

        Vector2Int nextPos = GetNextPos(PlayerPos.x, PlayerPos.y, direction);
        LevelValue goalValue = GetValue(nextPos.x, nextPos.y);

        Vector2Int nextNextPos;
        LevelValue nextGoalValue;

        switch (goalValue)
        {
            case LevelValue.Box:
                nextNextPos = GetNextPos(nextPos.x, nextPos.y, direction);
                nextGoalValue = GetValue(nextNextPos.x, nextNextPos.y);

                if (nextGoalValue == LevelValue.None)
                {
                    Debug.Log("1");
                    SetValue(LevelValue.Box, nextNextPos.x, nextNextPos.y);
                    moves.Add(new Move(LevelValue.Box, LevelValue.Box, nextGoalValue, nextNextPos, nextPos));

                    if (GetValue(PlayerPos.x, PlayerPos.y) == LevelValue.GoalPlayer) SetValue(LevelValue.Goal, PlayerPos.x, PlayerPos.y);
                    else SetValue(LevelValue.None, PlayerPos.x, PlayerPos.y);
                    moves.Add(new Move(LevelValue.Player, LevelValue.Player, goalValue, nextPos, basePlayerPos));

                    SetValue(LevelValue.Player, nextPos.x, nextPos.y);

                    PlayerPos = nextPos;
                    OnMoveEvent?.Invoke(LevelValue.Box, nextPos, nextNextPos);
                    OnMoveEvent?.Invoke(LevelValue.Player, basePlayerPos, nextPos);
                    
                    MoveHistory.Add(moves);
                    UIManager.Instance.SetMoveText(MoveHistory.Count);
                }
                else if (nextGoalValue == LevelValue.Goal)
                {
                    Debug.Log("2");
                    SetValue(LevelValue.GoalBox, nextNextPos.x, nextNextPos.y);
                    moves.Add(new Move(LevelValue.Box, LevelValue.GoalBox, nextGoalValue, nextNextPos, nextPos));

                    if (GetValue(PlayerPos.x, PlayerPos.y) == LevelValue.GoalPlayer) SetValue(LevelValue.Goal, PlayerPos.x, PlayerPos.y);
                    else SetValue(LevelValue.None, PlayerPos.x, PlayerPos.y);
                    moves.Add(new Move(LevelValue.Player, LevelValue.Player, goalValue, nextPos, basePlayerPos));

                    SetValue(LevelValue.Player, nextPos.x, nextPos.y);

                    PlayerPos = nextPos;
                    OnMoveEvent?.Invoke(LevelValue.Box, nextPos, nextNextPos);
                    OnMoveEvent?.Invoke(LevelValue.Player, basePlayerPos, nextPos);
                    
                    MoveHistory.Add(moves);
                    UIManager.Instance.SetMoveText(MoveHistory.Count);
                }
                break;

            case LevelValue.GoalBox:
                nextNextPos = GetNextPos(nextPos.x, nextPos.y, direction);
                nextGoalValue = GetValue(nextNextPos.x, nextNextPos.y);

                if (nextGoalValue == LevelValue.None)
                {
                    Debug.Log("3");
                    SetValue(LevelValue.Box, nextNextPos.x, nextNextPos.y);
                    moves.Add(new Move(LevelValue.Box, LevelValue.Box, nextGoalValue, nextNextPos, nextPos));

                    if (GetValue(PlayerPos.x, PlayerPos.y) == LevelValue.GoalPlayer)
                        SetValue(LevelValue.Goal, PlayerPos.x, PlayerPos.y);
                    else SetValue(LevelValue.None, PlayerPos.x, PlayerPos.y);
                    moves.Add(new Move(LevelValue.Player, LevelValue.GoalPlayer, goalValue, nextPos, basePlayerPos));

                    SetValue(LevelValue.GoalPlayer, nextPos.x, nextPos.y);

                    PlayerPos = nextPos;
                    OnMoveEvent?.Invoke(LevelValue.Box, nextPos, nextNextPos);
                    OnMoveEvent?.Invoke(LevelValue.Player, basePlayerPos, nextPos);
                    
                    MoveHistory.Add(moves);
                    UIManager.Instance.SetMoveText(MoveHistory.Count);
                }
                else if (nextGoalValue == LevelValue.Goal)
                {
                    Debug.Log("4");
                    SetValue(LevelValue.GoalBox, nextNextPos.x, nextNextPos.y);
                    moves.Add(new Move(LevelValue.Box, LevelValue.GoalBox, nextGoalValue, nextNextPos, nextPos));

                    if (GetValue(PlayerPos.x, PlayerPos.y) == LevelValue.GoalPlayer)
                        SetValue(LevelValue.Goal, PlayerPos.x, PlayerPos.y);
                    else SetValue(LevelValue.None, PlayerPos.x, PlayerPos.y);
                    moves.Add(new Move(LevelValue.Player, LevelValue.GoalPlayer, goalValue, nextPos, basePlayerPos));

                    SetValue(LevelValue.GoalPlayer, nextPos.x, nextPos.y);

                    PlayerPos = nextPos;
                    OnMoveEvent?.Invoke(LevelValue.Box, nextPos, nextNextPos);
                    OnMoveEvent?.Invoke(LevelValue.Player, basePlayerPos, nextPos);
                    
                    MoveHistory.Add(moves);
                    UIManager.Instance.SetMoveText(MoveHistory.Count);
                }
                break;

            case LevelValue.None:
                Debug.Log("5");
                if (GetValue(PlayerPos.x, PlayerPos.y) == LevelValue.GoalPlayer) SetValue(LevelValue.Goal, PlayerPos.x, PlayerPos.y);
                else SetValue(LevelValue.None, PlayerPos.x, PlayerPos.y);
                moves.Add(new Move(LevelValue.Player, LevelValue.Player, goalValue, nextPos, PlayerPos));

                SetValue(LevelValue.Player, nextPos.x, nextPos.y);

                PlayerPos = nextPos;
                OnMoveEvent?.Invoke(LevelValue.Player, basePlayerPos, nextPos);

                MoveHistory.Add(moves);
                UIManager.Instance.SetMoveText(MoveHistory.Count);
                break;

            case LevelValue.Goal:
                Debug.Log("6");
                if (GetValue(PlayerPos.x, PlayerPos.y) == LevelValue.GoalPlayer) SetValue(LevelValue.Goal, PlayerPos.x, PlayerPos.y);
                else SetValue(LevelValue.None, PlayerPos.x, PlayerPos.y);
                moves.Add(new Move(LevelValue.Player, LevelValue.GoalPlayer, goalValue, nextPos, basePlayerPos));

                SetValue(LevelValue.GoalPlayer, nextPos.x, nextPos.y);

                PlayerPos = nextPos;
                OnMoveEvent?.Invoke(LevelValue.Player, basePlayerPos, nextPos);
                
                MoveHistory.Add(moves);
                UIManager.Instance.SetMoveText(MoveHistory.Count);
                break;
        }
        GameManager.Instance.SetCompleteGoals(GetCompleteGoals());
    }
    public void Undo()
    {
        if (MoveHistory.Count == 0) return;

        List<Move> lastMoves = MoveHistory[MoveHistory.Count - 1];
        foreach (var move in lastMoves)
        {
            SetValue(move.FromType, move.To.x, move.To.y);
            SetValue(move.ToType, move.From.x, move.From.y);

            if (move.EmitterType == LevelValue.Player) PlayerPos = move.To;

            OnMoveEvent?.Invoke(move.EmitterType, move.From, move.To);
        }

        MoveHistory.RemoveAt(MoveHistory.Count - 1);
        UIManager.Instance.SetMoveText(MoveHistory.Count);
    }
    public void AddLine(Direction direction)
    {
        LevelValue[,] newValues;
        switch (direction)
        {
            case Direction.North:
                if (Height + 1 > MaxHeight) return;

                Height++;
                newValues = new LevelValue[Width, Height];
                for (int y = 1; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        newValues[x, y] = GetValue(x, y - 1);
                    }
                }
                Values = newValues;
                break;

            case Direction.South:
                if (Height + 1 > MaxHeight) return;

                Height++;
                newValues = new LevelValue[Width, Height];
                for (int y = 0; y < Height - 1; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        newValues[x, y] = GetValue(x, y);
                    }
                }
                Values = newValues;
                break;

            case Direction.West:
                if (Width + 1 > MaxWidth) return;

                Width++;
                newValues = new LevelValue[Width, Height];
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 1; x < Width; x++)
                    {
                        newValues[x, y] = GetValue(x - 1, y);
                    }
                }
                Values = newValues;
                break;

            case Direction.East:
                if (Width + 1 > MaxWidth) return;

                Width++;
                newValues = new LevelValue[Width, Height];
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width - 1; x++)
                    {
                        newValues[x, y] = GetValue(x, y);
                    }
                }
                Values = newValues;
                break;
        }
    }
    public void RemoveLine(Direction direction)
    {
        LevelValue[,] newValues;
        switch (direction)
        {
            case Direction.North:
                if (Height - 1 < MinHeight) return;

                newValues = new LevelValue[Width, Height - 1];
                for (int y = 0; y < Height - 1; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        newValues[x, y] = GetValue(x, y + 1);
                    }
                }
                Height--;
                Values = newValues;
                break;

            case Direction.South:
                if (Height - 1 < MinHeight) return;

                newValues = new LevelValue[Width, Height - 1];
                for (int y = 0; y < Height - 1; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        newValues[x, y] = GetValue(x, y);
                    }
                }
                Height--;
                Values = newValues;
                break;

            case Direction.West:
                if (Width - 1 < MinWidth) return;

                newValues = new LevelValue[Width - 1, Height];
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width - 1; x++)
                    {
                        newValues[x, y] = GetValue(x + 1, y);
                    }
                }
                Width--;
                Values = newValues;
                break;

            case Direction.East:
                if (Width - 1 < MinWidth) return;

                newValues = new LevelValue[Width - 1, Height];
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width - 1; x++)
                    {
                        newValues[x, y] = GetValue(x, y);
                    }
                }
                Width--;
                Values = newValues;
                break;
        }
    }
    public void Clear()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                SetValue(LevelValue.None, x, y);
            }
        }
    }
    public async void SaveToFile()
    {
        if (!Directory.Exists(Application.streamingAssetsPath + "/Levels")) Directory.CreateDirectory(Application.streamingAssetsPath + "/Levels");
        int levelCount = Directory.GetFiles(Application.streamingAssetsPath + "/Levels", "*.lvl").Length;
        string levelFilePath = Application.streamingAssetsPath + "/Levels/" + levelCount + ".lvl";
        List<String> lines = new List<string>();
        lines.Add(Width.ToString());
        lines.Add(Height.ToString());
        for (int y = 0; y < Height; y++)
        {
            string line = "";
            for (int x = 0; x < Width; x++)
            {
                if (x != 0) line += ",";
                line += GetValue(x, y).ToString();
            }
            lines.Add(line);
        }

        await File.WriteAllLinesAsync(levelFilePath, lines);
        Debug.Log("Level " + levelCount + " created!");
    }

    // Helpers
    public TilesetEdit.Tile GetWallTile(int x, int y)
    {
        string bits = "";

        /* O O O
         * X   O
         * O O O */
        bits += GetValue(x - 1, y) == LevelValue.Wall ? "1" : "0";
        /* X O O
         * O   O
         * O O O */
        bits += GetValue(x - 1, y + 1) == LevelValue.Wall ? "1" : "0";
        /* O X O
         * O   O
         * O O O */
        bits += GetValue(x, y + 1) == LevelValue.Wall ? "1" : "0";
        /* O O X
         * O   O
         * O O O */
        bits += GetValue(x + 1, y + 1) == LevelValue.Wall ? "1" : "0";
        /* O O O
         * O   X
         * O O O */
        bits += GetValue(x + 1, y) == LevelValue.Wall ? "1" : "0";
        /* O O O
         * O   O
         * O O X */
        bits += GetValue(x + 1, y - 1) == LevelValue.Wall ? "1" : "0";
        /* O O O
         * O   O
         * O X O */
        bits += GetValue(x, y - 1) == LevelValue.Wall ? "1" : "0";
        /* O O O
         * O   O
         * X O O */
        bits += GetValue(x - 1, y - 1) == LevelValue.Wall ? "1" : "0";

        // Reverse bits
        char[] bitsChar = bits.ToCharArray();
        Array.Reverse(bitsChar);
        string reverseBits = new string(bitsChar);

        return WallTiles[Convert.ToInt32(reverseBits, 2)];
    }

    private bool IsBounded(int x, int y)
    {
        return x >= 0 && y >= 0 && x < Width && y < Height;
    }
}