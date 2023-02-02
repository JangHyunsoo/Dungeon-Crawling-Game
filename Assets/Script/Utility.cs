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

[System.Serializable]
public class RarlityPercentPair
{
    public int rarlity;
    public float percent;
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

    public static Transform[] getChildsTransform(Transform parent_)
    {
        Transform[] ret = new Transform[parent_.childCount];
        for (int i = 0; i < parent_.childCount; i++)
        {
            ret[i] = parent_.GetChild(i);
        }
        return ret;
    }

    public static int getAmountRandom(float[] amount_arr)
    {
        float total_amount = 0;
        foreach (var amount in amount_arr)
        {
            total_amount += amount;
        }

        float rand_amout = Random.RandomRange(0, total_amount);

        for (int i = 0; i < amount_arr.Length; i++)
        {
            if(amount_arr[i] <= rand_amout)
            {
                return i;
            }
        }
        
        return -1;
    }

    public static int getRandRarlityByRarlityArr(RarlityPercentPair[] rarlity_arr)
    {
        float total_value = 0;
        int select_rarlity = -1;
        foreach (var item in rarlity_arr)
        {
            total_value += item.percent;
        }

        float rand_value = Random.RandomRange(0, total_value);

        foreach (var item in rarlity_arr)
        {
            if (rand_value <= item.percent)
            {
                select_rarlity = item.rarlity;
                break;
            }
            rand_value -= item.percent;
        }

        return select_rarlity;
    }
}
