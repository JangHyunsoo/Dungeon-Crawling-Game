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

    public static int[] getShuffleArray(int _size)
    {
        int[] shuffle_arr = Enumerable.Range(0, _size).ToArray();
        System.Random random = new System.Random();
        shuffle_arr = shuffle_arr.OrderBy(x => random.Next()).ToArray();
        return shuffle_arr;
    }
}
