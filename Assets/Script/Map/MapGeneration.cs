using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGeneration : MonoBehaviour
{
    [Header("Room Generation")]
    [SerializeField]
    private GameObject map_prefab_;
    [SerializeField]
    private GameObject room_prefab_;
    [SerializeField]
    private TileBase tile_base_;
    [SerializeField]
    private GameObject road_tile_;
    [SerializeField]
    private Transform grid_parent_;

    private float tile_size_ = 0.32f;

    private StageData stage_data_;

    private TileMap tile_map_;
    public TileMap tile_map { get => tile_map_; }

    private List<Transform> room_tr_list_ = new List<Transform>();
    private List<Vertex> vertex_list_ = new List<Vertex>();
    private Triangle[] triangle_arr_;
    private List<LineData> vertex_graph_ = new List<LineData>();
    private List<LineData> unselected_lines_ = new List<LineData>();

    public IEnumerator generate(int _stage_no, StageData _stage_data)
    {
        yield return StartCoroutine(logic(_stage_no, _stage_data));
    }

    private IEnumerator logic(int _stage_no, StageData _stage_data)
    {
        tile_map_ = GameObject.Instantiate(map_prefab_, Vector3.zero, Quaternion.identity).GetComponent<TileMap>();
        tile_map_.transform.SetParent(transform);
        stage_data_ = _stage_data;
        tile_map_.setStageNo(_stage_no);
        clearGenerationInfo();
        generateRoom();
        controlColliderComponent(true);
        Time.timeScale = 20f;
        yield return new WaitForSeconds(20f);
        Time.timeScale = 1f;
        correctRoomPosition();
        selectRoom();
        convertRoomToVertex();
        getTriangleComb();
        setCheckedTriangle();        
        setGraph();        
        calculKruskal();
        selectRandomRoute();
        setTileArray();
        linkRooms();
        createStair();
    }

    private void clearGenerationInfo()
    {
        room_tr_list_.Clear();
        vertex_list_.Clear();
        triangle_arr_ = null;
        vertex_graph_.Clear();
        unselected_lines_.Clear();
    }

    private void generateRoom()
    {
        int room_count = stage_data_.target_room_count * 5;

        for (int i = 0; i < room_count; i++)
        {
            int width = Random.RandomRange(stage_data_.room_min_width, stage_data_.room_max_width);
            int height = Random.RandomRange(stage_data_.room_min_height, stage_data_.room_max_height);
            int random_angle = Random.RandomRange(0, 360);

            // tile_map_prefab 네이밍 수정 고려
            var go = GameObject.Instantiate(room_prefab_, new Vector3(Mathf.Cos(random_angle), Mathf.Sin(random_angle), 0), Quaternion.identity);
            var col = go.GetComponent<BoxCollider2D>();
            col.size = new Vector2(width * tile_size_, height * tile_size_);
            col.offset = new Vector2(col.size.x / 2f, col.size.y / 2f);

            // 룸 부모 수정
            go.transform.SetParent(grid_parent_);

            room_tr_list_.Add(go.transform);
        }
    }

    private void controlColliderComponent(bool _value)
    {
        foreach (var room in room_tr_list_)
        {
            room.GetComponent<BoxCollider2D>().enabled = _value;
        }
    }

    private void correctRoomPosition()
    {
        foreach (var room in room_tr_list_)
        {
            float corrected_x = 0.32f * Mathf.FloorToInt(room.position.x / tile_size_);
            float corrected_y = 0.32f * Mathf.FloorToInt(room.position.y / tile_size_);

            room.position = new Vector3(corrected_x, corrected_y, 0);
        }
    }

    private void selectRoom()
    {
        var random_integer_arr = Utility.getShuffleArray(room_tr_list_.Count);
        var selected_room_list = new List<Transform>();
        int selected_room_count = 0;

        for (int i = 0; i < room_tr_list_.Count && selected_room_count < stage_data_.target_room_count; i++)
        {
            var cur_room_tr_ = room_tr_list_[i];

            if (cur_room_tr_.gameObject.active)
            {
                selected_room_list.Add(cur_room_tr_);
                selected_room_count++;
                var box_collider = cur_room_tr_.GetComponent<BoxCollider2D>();

                cur_room_tr_.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                var col_size = box_collider.size + Vector2.one * (2 * tile_size_ + 0.01f);
                var room_pivot = cur_room_tr_.transform.position + new Vector3(box_collider.offset.x, box_collider.offset.y);

                var hit_rooms = Physics2D.OverlapBoxAll(room_pivot, col_size, 0f);

                foreach (var room in hit_rooms)
                {
                    room.transform.gameObject.SetActive(false);
                }
            }
        }

        foreach (var room_tr in room_tr_list_)
        {
            room_tr.gameObject.active = false;
        }

        foreach (var seledted_room_tr in selected_room_list)
        {
            seledted_room_tr.gameObject.active = true;
        }

        foreach (var room_tr in room_tr_list_)
        {
            if (!room_tr.gameObject.active)
            {
                Destroy(room_tr.gameObject);
            }
        }

        room_tr_list_ = selected_room_list;
    }

    private void convertRoomToVertex()
    {
        List<Room> room_list = new List<Room>();

        for (int i = 0; i < room_tr_list_.Count; i++)
        {
            room_list.Add(new Room(i, room_tr_list_[i]));
            vertex_list_.Add(room_list[i].getVertex());
        }

        tile_map_.setRoomArr(room_list);
    }

    private void getTriangleComb()
    {
        int r = 3;
        List<Triangle> result = new List<Triangle>();

        combination(result, new List<Vertex>(), r, 0);

        triangle_arr_ = result.ToArray();
    }

    private void combination(List<Triangle> result, List<Vertex> comb, int r, int depth)
    {
        if (r == 0)
        {
            result.Add(new Triangle(comb[0], comb[1], comb[2]));
        }
        else if (depth == vertex_list_.Count)
        {
            return;
        }
        else
        {
            combination(result, new List<Vertex>(comb), r, depth + 1);

            comb.Add(vertex_list_[depth]);
            combination(result, new List<Vertex>(comb), r - 1, depth + 1);
        }
    }

    private void setCheckedTriangle()
    {
        List<Triangle> result = new List<Triangle>();

        foreach (var tri in triangle_arr_)
        {
            bool flag = true;

            foreach (var vertex in vertex_list_)
            {
                if (tri.isInclude(vertex))
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                result.Add(tri);
            }
        }

        triangle_arr_ = result.ToArray();
    }

    private void setGraph()
    {
        bool[,] is_value = new bool[stage_data_.target_room_count, stage_data_.target_room_count];
        List<LineData> line_graph_list = new List<LineData>();

        for (int i = 0; i < stage_data_.target_room_count; i++)
        {
            for (int j = 0; j < stage_data_.target_room_count; j++)
            {
                is_value[i, j] = false;
            }
        }

        foreach (var triangle in triangle_arr_)
        {
            var lines = triangle.getLineData();

            foreach (var line_data in lines)
            {
                if (!is_value[line_data.start.no, line_data.end.no])
                {
                    line_graph_list.Add(line_data);
                    is_value[line_data.start.no, line_data.end.no] = true;
                }
            }
        }

        line_graph_list.Sort(delegate (LineData _one, LineData _other)
        {
            var distance = _one.getDistance() - _other.getDistance();
            if (distance > 0) return 1;
            else if (distance < 0) return -1;
            else return 0;
        });

        vertex_graph_ = line_graph_list;
    }

    private int findRoot(int[] parent, int x)
    {
        if (parent[x] == x) return x;
        return parent[x] = findRoot(parent, parent[x]);
    }

    private void unionRoot(int[] parent, int x, int y)
    {
        x = findRoot(parent, x);
        y = findRoot(parent, y);

        if (x != y) parent[y] = x;
    }

    private void calculKruskal()
    {
        List<LineData> result = new List<LineData>();
        int[] parent = new int[vertex_list_.Count];

        for (int i = 0; i < parent.Length; i++)
        {
            parent[i] = i;
        }

        for (int i = 0; i < vertex_graph_.Count; i++)
        {
            LineData cur_edge = vertex_graph_[i];

            int f = cur_edge.start.no;
            int s = cur_edge.end.no;

            if (findRoot(parent, f) == findRoot(parent, s))
            {
                continue;
            }

            result.Add(cur_edge);
            vertex_graph_.Remove(cur_edge);
            unionRoot(parent, f, s);

            if (result.Count == vertex_list_.Count - 1) break;
        }

        unselected_lines_ = vertex_graph_;
        vertex_graph_ = result;
    }

    private void selectRandomRoute()
    {
        foreach (var unselected_line in unselected_lines_)
        {
            if (Random.RandomRange(0f, 100f) < stage_data_.additional_route_chance)
            {
                vertex_graph_.Add(unselected_line);
            }
        }
    }

    private Vector2Int getTilePos(Vector2 _real_pos)
    {
        return new Vector2Int(Mathf.RoundToInt((_real_pos.x - tile_size_ / 2) / tile_size_), Mathf.RoundToInt((_real_pos.y - tile_size_ / 2) / tile_size_));
    }

    private Vector2 getRealPos(Vector2Int _tile_pos)
    {
        return new Vector2(_tile_pos.x * tile_size_ + tile_size_ / 2, _tile_pos.y * tile_size_ + tile_size_ / 2);
    }

    private Vector2 getRealPos(int x, int y)
    {
        return new Vector2(x * tile_size_ + tile_size_ / 2, y * tile_size_ + tile_size_ / 2);
    }

    private Vector2 getIncludeTilePos(Direction _dir, Vector2 _pos)
    {
        var correct_vec = _pos + Utility.direction_to_vector[_dir] * 0.01f;
        var temp = new Vector2Int(Mathf.RoundToInt((correct_vec.x - tile_size_ / 2) / tile_size_), Mathf.RoundToInt((correct_vec.y - tile_size_ / 2) / tile_size_));
        return getRealPos(temp);
    }

    private void setTileArray()
    {
        float map_min_x = float.MaxValue;
        float map_min_y = float.MaxValue;
        float map_max_x = float.MinValue;
        float map_max_y = float.MinValue;

        int room_count = tile_map_.getRoomLength(); 
        
        for (int i = 0; i < room_count; i++)
        {
            Room room = tile_map_.getRoom(i);
            map_min_x = Mathf.Min(map_min_x, room.position.x);
            map_min_y = Mathf.Min(map_min_y, room.position.y);

            map_max_x = Mathf.Max(map_max_x, room.position.x + room.room_size.x);
            map_max_y = Mathf.Max(map_max_y, room.position.y + room.room_size.y);
        }

        int world_width = Mathf.RoundToInt((map_max_x - map_min_x) / tile_size_) + 10;
        int world_height = Mathf.RoundToInt((map_max_y - map_min_y) / tile_size_) + 10;
        Vector2Int world_tile_origin = new Vector2Int(Mathf.RoundToInt((map_min_x) / tile_size_) - 5, Mathf.RoundToInt((map_min_y) / tile_size_) - 5);


        tile_map_.setWorldPos(world_width, world_height, world_tile_origin);

        for (int i = 0; i < room_count; i++)
        {
            Room room = tile_map_.getRoom(i);
            int x_size = Mathf.RoundToInt(room.room_size.x / tile_size_);
            int y_size = Mathf.RoundToInt(room.room_size.y / tile_size_);

            for (int x = 0; x < x_size; x++)
            {
                for (int y = 0; y < y_size; y++)
                {
                    Vector2Int curr_pos = new Vector2Int(
                        Mathf.RoundToInt((room.position.x) / tile_size_ + x),
                        Mathf.RoundToInt((room.position.y) / tile_size_ + y));
                    tile_map_.setTileByArrayPos(curr_pos, 1);
                }
            }
        }

        foreach (var room in room_tr_list_)
        {
            Destroy(room.gameObject);
        }
    }

    private void linkRooms()
    {
        foreach (var line in vertex_graph_)
        {
            linkRoom(line);
        }
    }

    private void linkRoom(LineData _line)
    {
        Room start_room = tile_map_.getRoom(_line.start.no);
        Room end_room = tile_map_.getRoom(_line.end.no);
        Vector3 start_room_pos = _line.start.getVecter3();
        Vector3 end_room_pos = _line.end.getVecter3();

        // start pos
        Vector3 dir = end_room_pos - start_room_pos;
        float dir_angle = Mathf.Atan2(dir.y, dir.x);

        var start_door = getRoadStartInfo(start_room, dir_angle);

        //end pos
        dir = start_room_pos - end_room_pos;
        dir_angle = Mathf.Atan2(dir.y, dir.x);

        var end_door = getRoadStartInfo(end_room, dir_angle);

        connectVertex(start_door, end_door);
    }

    private void connectVertex(KeyValuePair<Direction, Vector2> start, KeyValuePair<Direction, Vector2> end)
    {
        var dir = end.Value - start.Value;
        var dir_int = getTilePos(end.Value) - getTilePos(start.Value);

        if ((start.Key == Direction.LEFT && end.Key == Direction.RIGHT) || (start.Key == Direction.RIGHT && end.Key == Direction.LEFT))
        {
            int curr_x = 0;
            int incr_x = dir_int.x > 0 ? 1 : -1;
            int curr_y = 0;
            int incr_y = dir_int.y > 0 ? 1 : -1;
            while (true)
            {
                tile_map_.setTileByWorldPos(start.Value + new Vector2(curr_x, curr_y) * tile_size_, 1);

                if (curr_y == dir_int.y && curr_x == dir_int.x) break;

                if (curr_x == dir_int.x / 2)
                {
                    if (curr_y == dir_int.y)
                    {
                        curr_x += incr_x;
                    }
                    else
                    {
                        curr_y += incr_y;
                    }
                }
                else
                {
                    curr_x += incr_x;
                }
            }
        }
        else if ((start.Key == Direction.BOTTOM && end.Key == Direction.TOP) || (start.Key == Direction.TOP && end.Key == Direction.BOTTOM))
        {
            int curr_x = 0;
            int incr_x = dir_int.x > 0 ? 1 : -1;
            int curr_y = 0;
            int incr_y = dir_int.y > 0 ? 1 : -1;
            while (true)
            {
                tile_map_.setTileByWorldPos(start.Value + new Vector2(curr_x, curr_y) * tile_size_, 1);

                if (curr_y == dir_int.y && curr_x == dir_int.x) break;

                if (curr_y == dir_int.y / 2)
                {
                    if (curr_x == dir_int.x)
                    {
                        curr_y += incr_y;
                    }
                    else
                    {
                        curr_x += incr_x;
                    }
                }
                else
                {
                    curr_y += incr_y;
                }
            }
        }
        else
        {
            if (start.Key == Direction.BOTTOM || start.Key == Direction.TOP)
            {
                createRoad(start.Key, start.Value, Mathf.Abs(dir_int.y));
            }
            else
            {
                createRoad(start.Key, start.Value, Mathf.Abs(dir_int.x));
            }
            if (end.Key == Direction.BOTTOM || end.Key == Direction.TOP)
            {
                createRoad(end.Key, end.Value, Mathf.Abs(dir_int.y) + 1);
            }
            else
            {
                createRoad(end.Key, end.Value, Mathf.Abs(dir_int.x) + 1);
            }
        }
    }

    private void createRoad(Direction _direction, Vector2 _pos, int _length)
    {
        for (int i = 0; i < _length; i++)
        {
            tile_map_.setTileByWorldPos(_pos + Utility.direction_to_vector[_direction] * i * tile_size_, 1);
        }
    }

    private Direction checkRoomDirection(Room _room, float angle)
    {
        float lr_side = Mathf.Atan2(_room.room_size.y, _room.room_size.x);
        float tb_side = Mathf.PI / 2 - lr_side;

        if (angle < 0) angle += 2 * Mathf.PI;

        if ((angle <= lr_side && angle >= 0) || (2 * Mathf.PI - lr_side <= angle && angle <= 2 * Mathf.PI))
        {
            return Direction.RIGHT;
        }
        else if (angle > lr_side && angle <= lr_side + 2 * tb_side)
        {
            return Direction.TOP;
        }
        else if (angle > lr_side + 2 * tb_side && angle < 3 * lr_side + 2 * tb_side)
        {
            return Direction.LEFT;
        }
        else
        {
            return Direction.BOTTOM;
        }
    }

    private KeyValuePair<Direction, Vector2> getRoadStartInfo(Room _room, float angle)
    {
        Direction dir = checkRoomDirection(_room, angle);
        Vector2 origin_pos = _room.getVector2();
        Vector2 pos = Vector2.zero;

        switch (dir)
        {
            case Direction.TOP:
                {
                    float x = origin_pos.x + (_room.room_size.y / 2) / Mathf.Tan(angle) * (1f - 1e-4f);
                    pos = new Vector2(x, origin_pos.y + _room.room_size.y / 2);
                    break;
                }
            case Direction.BOTTOM:
                {
                    float x = origin_pos.x + (_room.room_size.y / 2) / Mathf.Tan(angle) * (1f - 1e-4f) * -1;
                    pos = new Vector2(x, origin_pos.y - _room.room_size.y / 2);
                    break;
                }
            case Direction.LEFT:
                {
                    float y = origin_pos.y + (_room.room_size.x / 2) * Mathf.Tan(angle) * (1f - 1e-4f) * -1;
                    pos = new Vector2(origin_pos.x - _room.room_size.x / 2, y);
                    break;
                }
            case Direction.RIGHT:
                {
                    float y = origin_pos.y + (_room.room_size.x / 2) * Mathf.Tan(angle) * (1f - 1e-4f);
                    pos = new Vector2(origin_pos.x + _room.room_size.x / 2, y);
                    break;
                }
            default:
                break;
        }
        pos = getIncludeTilePos(dir, pos);

        return new KeyValuePair<Direction, Vector2>(dir, pos);
    }

    public void createStair()
    {
        int stair_count = Random.Range(2, 3);
        
        if(tile_map_.stage_no != 0)
        {
            var pre_tile_map = MapManager.instance.getTileMap(tile_map_.stage_no - 1);
            for (int i = 0; i < stair_count; i++)
            {
                Stair cur_stair = tile_map_.createStairRanndom();
                Stair pre_stair = pre_tile_map.createStairRanndom();
                cur_stair.setTargetInfo(pre_stair.my_floor, pre_stair.my_idx);
                pre_stair.setTargetInfo(cur_stair.my_floor, cur_stair.my_idx);
            }
        }
    }
}

public class Vertex
{
    public int no;
    public float x;
    public float y;

    public Vertex(int _no, float _x, float _y)
    {
        this.no = _no;
        this.x = _x;
        this.y = _y;
    }

    public override string ToString()
    {
        return String.Format("({0}, {1})", x, y);
    }

    public Vector3 getVecter3()
    {
        return new Vector3(x, y, 0);
    }
}

public class Triangle
{
    public Vertex[] vertices;

    public Triangle(Vertex _p1, Vertex _p2, Vertex _p3)
    {
        vertices = new Vertex[3];
        Vertex center = new Vertex(-1, (_p1.x + _p2.x + _p3.x) / 3, (_p1.y + _p2.y + _p3.y) / 3);

        List<KeyValuePair<float, Vertex>> data = new List<KeyValuePair<float, Vertex>>();
        data.Add(new KeyValuePair<float, Vertex>(Mathf.Atan2(center.x - _p1.x, center.y - _p1.y), _p1));
        data.Add(new KeyValuePair<float, Vertex>(Mathf.Atan2(center.x - _p2.x, center.y - _p2.y), _p2));
        data.Add(new KeyValuePair<float, Vertex>(Mathf.Atan2(center.x - _p3.x, center.y - _p3.y), _p3));

        data.Sort(delegate (KeyValuePair<float, Vertex> one, KeyValuePair<float, Vertex> other)
        {
            if (one.Key > other.Key) return -1;
            else if (one.Key == other.Key) return 0;
            else return 1;
        });

        for (int i = 0; i < 3; i++)
        {
            vertices[i] = data[i].Value;
        }
    }

    public bool isInclude(Vertex D)
    {
        Vertex A = vertices[0];
        Vertex B = vertices[1];
        Vertex C = vertices[2];
        float Ax2 = A.x * A.x;
        float Ay2 = A.y * A.y;
        float Bx2 = B.x * B.x;
        float By2 = B.y * B.y;
        float Cx2 = C.x * C.x;
        float Cy2 = C.y * C.y;
        float Dx2 = D.x * D.x;
        float Dy2 = D.y * D.y;

        float det = (A.x - D.x) * (B.y - D.y) * ((Cx2 - Dx2) + (Cy2 - Dy2))
            + (A.y - D.y) * ((Bx2 - Dx2) + (By2 - Dy2)) * (C.x - D.x)
            + ((Ax2 - Dx2) + (Ay2 - Dy2)) * (B.x - D.x) * (C.y - D.y)
            - ((Ax2 - Dx2) + (Ay2 - Dy2)) * (B.y - D.y) * (C.x - D.x)
            - ((Bx2 - Dx2) + (By2 - Dy2)) * (A.x - D.x) * (C.y - D.y)
            - ((Cx2 - Dx2) + (Cy2 - Dy2)) * (A.y - D.y) * (B.x - D.x);
        return det > 0;
    }

    public void drawDebug()
    {
        Debug.DrawLine(vertices[0].getVecter3(), vertices[1].getVecter3(), Color.red);
        Debug.DrawLine(vertices[1].getVecter3(), vertices[2].getVecter3(), Color.red);
        Debug.DrawLine(vertices[0].getVecter3(), vertices[2].getVecter3(), Color.red);
    }

    public LineData[] getLineData()
    {
        LineData[] lines = new LineData[3]
        {
            new LineData(vertices[0], vertices[1]),
            new LineData(vertices[1], vertices[2]),
            new LineData(vertices[0], vertices[2])
        };
        return lines;
    }

    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", vertices[0].ToString(), vertices[1].ToString(), vertices[2].ToString());
    }
}

public class LineData
{
    public Vertex start;
    public Vertex end;

    public LineData(Vertex _start, Vertex _end)
    {
        if (_start.no >= _end.no)
        {
            start = _start;
            end = _end;
        }
        else
        {
            start = _end;
            end = _start;
        }
    }

    public float getDistance()
    {
        return Vector2.Distance(start.getVecter3(), end.getVecter3());
    }

    public void drawDebug()
    {
        Debug.DrawLine(start.getVecter3(), end.getVecter3(), Color.red);
    }
}

public class Room
{
    private int room_no_ = 0;
    private Vector2 room_pos_;
    private Vector2 room_offset_;
    private Vector2 room_size_;

    public Vector2 position { get => room_pos_; }
    public Vector2 room_offset { get => room_offset_; }
    public Vector2 room_size { get => room_size_; }

    public Room(int _room_no, Transform _room_tr)
    {
        var bc = _room_tr.GetComponent<BoxCollider2D>();
        room_no_ = _room_no;
        room_pos_ = _room_tr.position;
        room_offset_ = bc.offset;
        room_size_ = bc.size;
    }

    public Vertex getVertex()
    {
        return new Vertex(room_no_, room_pos_.x + room_offset_.x, room_pos_.y + room_offset_.y);
    }

    public Vector2 getVector2()
    {
        return new Vector2(room_pos_.x + room_offset_.x, room_pos_.y + room_offset_.y);
    }

    public Vector2 getRandomRealPos()
    {
        float x = Random.RandomRange(room_pos_.x, room_pos_.x + room_size_.x);
        float y = Random.RandomRange(room_pos_.y, room_pos_.y + room_size_.y);
        return new Vector2(x, y);
    }
}