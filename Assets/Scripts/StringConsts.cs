using System.Collections.Generic;
using UnityEngine;

public class StringConsts 
{
    public const string UP = "Up";
    public const string LEFT = "Left";
    public const string RIGHT = "Right";
    public const string DOWN = "Down";
    public const string SIDE = "Side";
    public const string LUP = "LUp";
    public const string RUP = "RUp";
    public const string LDOWN = "LDown";
    public const string RDOWN = "RDown";
    public const string SNAKE1 = "Snake1";
    public const string SNAKE2 = "Snake2";

    // For copying
    public static readonly Dictionary<KeyCode, Vector2Int> _snake1InputMap = new()
    {
        { KeyCode.W, Vector2Int.up },
        { KeyCode.A, Vector2Int.left },
        { KeyCode.S, Vector2Int.down },
        { KeyCode.D, Vector2Int.right }
    };

    public static readonly Dictionary<KeyCode, Vector2Int> _snake2InputMap = new()
    {
        { KeyCode.UpArrow, Vector2Int.up },
        { KeyCode.LeftArrow, Vector2Int.left },
        { KeyCode.DownArrow, Vector2Int.down },
        { KeyCode.RightArrow, Vector2Int.right }
    };

    // For referencing
    public static readonly Dictionary<Vector2Int, string> _directionTriggerMap = new()
    {
        { Vector2Int.up, UP },
        { Vector2Int.left, LEFT },
        { Vector2Int.down, DOWN },
        { Vector2Int.right, RIGHT }
    };
}