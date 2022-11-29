using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Testing : MonoBehaviour
{
    public List<Transform> logic(List<Transform> _tr_list, int _size)
    {
        var random_integer_arr = getShuffleArray(_tr_list.Count);
        var temp_list = new List<Transform>();
        int count = 0;

        int selected_num = 0;

        while (selected_num < _size)
        {
            var cur_room_tr_ = _tr_list[count];

            if (count >= _tr_list.Count) break;
            if (!cur_room_tr_.gameObject.active) { count++; continue; }

            temp_list.Add(cur_room_tr_);
            selected_num++;
            count++;
            var box_collider = cur_room_tr_.GetComponent<BoxCollider2D>();
            box_collider.enabled = false;
            var temp = new Vector2(box_collider.size.x + 0.64f, box_collider.size.y + 0.64f);

            var origin = cur_room_tr_.transform.position + new Vector3(box_collider.offset.x, box_collider.offset.y);

            var hits = Physics2D.OverlapBoxAll(origin, temp, 360f);

            foreach (var hit in hits)
            {
                hit.transform.gameObject.SetActive(false);
            }
            cur_room_tr_.GetComponent<Tilemap>().color = Color.red; // for debug    
        }
        
        foreach (var room_tr in _tr_list)
        {
            room_tr.gameObject.active = false;
        }

        foreach (var seledted_room_tr in temp_list)
        {
            seledted_room_tr.gameObject.active = true;
        }

        foreach (var room_tr in _tr_list)
        {
            if(room_tr.gameObject.active == false)
            {
                Destroy(room_tr.gameObject);
            }
        }

        return temp_list;
    }

    public int[] getShuffleArray(int _size)
    {
        int[] shuffle_arr = Enumerable.Range(0, _size).ToArray();
        System.Random random = new System.Random();
        shuffle_arr = shuffle_arr.OrderBy(x => random.Next()).ToArray();
        return shuffle_arr;
    }
}
