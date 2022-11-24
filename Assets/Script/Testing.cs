using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Testing : MonoBehaviour
{
    [SerializeField]
    private GameObject tile_map_;

    [SerializeField]
    private TileBase tile_base_;

    [SerializeField]
    private Transform grid_parent_;

    private List<Transform> room_list_ = new List<Transform>();

    private float tile_size_ = 32;

    [SerializeField]
    private int min_width_;
    [SerializeField]
    private int min_height_;
    [SerializeField]
    private int max_width_;
    [SerializeField]
    private int max_height_;

    public void getMainPosition()
    {
        foreach (var room in room_list_)
        {
            string testtext = (room.position.x / 2).ToString() + ", " + (room.position.y / 2).ToString();
            Debug.Log(testtext);
        }
    }


    public void setRoom(int _room_size)
    {
        for (int i = 0; i < _room_size; i++)
        {
            generateRoom();
        }
    }

    public void onColliderComponent()
    {
        foreach (var room in room_list_)
        {
            room.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void offColliderComponent()
    {
        foreach (var room in room_list_)
        {
            room.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void correctRoomPosition()
    {
        foreach (var room in room_list_)
        {
            float corrected_x = 0.32f * Mathf.RoundToInt(room.position.x / 0.32f);
            float corrected_y = 0.32f * Mathf.RoundToInt(room.position.y / 0.32f);

            room.position = new Vector3(corrected_x, corrected_y, 0);
        }
    }

    private void generateRoom()
    {
        int width = Random.RandomRange(min_width_, max_width_);
        int height = Random.RandomRange(min_height_, max_height_);
        int random_angle = Random.RandomRange(0, 360);

        var go = GameObject.Instantiate(tile_map_, new Vector3(Mathf.Cos(random_angle), Mathf.Sin(random_angle), 0), Quaternion.identity);
        var col = go.GetComponent<BoxCollider2D>();
        var tile_map = go.GetComponent<Tilemap>();
        col.size = new Vector2(width * 0.32f, height * 0.32f);
        col.offset = new Vector2(col.size.x / 2f, col.size.y / 2f);
        go.transform.SetParent(grid_parent_);

        room_list_.Add(go.transform);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tile_map.SetTile(new Vector3Int(i, j, 0), tile_base_);
            }
        }
    }
}
