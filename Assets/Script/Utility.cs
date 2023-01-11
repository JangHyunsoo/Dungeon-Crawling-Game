using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Direction
{
    TOP,
    BOTTOM,
    LEFT,
    RIGHT
}

public class Utility
{
    public static Dictionary<Direction, Vector2> direction_to_vector = new Dictionary<Direction, Vector2>
    {
        { Direction.BOTTOM, Vector2.down },
        { Direction.TOP, Vector2.up },
        { Direction.LEFT, Vector2.left },
        { Direction.RIGHT, Vector2.right }
    };

    public static Dictionary<Direction, Vector2Int> direction_to_vector_int = new Dictionary<Direction, Vector2Int>
    {
        { Direction.BOTTOM, Vector2Int.down },
        { Direction.TOP, Vector2Int.up },
        { Direction.LEFT, Vector2Int.left },
        { Direction.RIGHT, Vector2Int.right }
    };

    public static Vector2Int[] int_to_vector_int = new Vector2Int[]
    {
        Vector2Int.down,
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.right
    };

    public static Vector2Int[] int_to_vector_int8 = new Vector2Int[]
    {
        Vector2Int.down,
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.right,
        new Vector2Int(1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, 1),
        new Vector2Int(-1, -1)
    };

    public static int[] getShuffleArray(int _size)
    {
        int[] shuffle_arr = Enumerable.Range(0, _size).ToArray();
        System.Random random = new System.Random();
        shuffle_arr = shuffle_arr.OrderBy(x => random.Next()).ToArray();
        return shuffle_arr;
    }

    public static Vector2Int convertVector2ToVector2Int(Vector2 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }

    public static AptitudeType convertAptitudeTypeToWeaponType(WeaponType _weapon_type)
    {
        return (AptitudeType)(int)_weapon_type;
    }
}
