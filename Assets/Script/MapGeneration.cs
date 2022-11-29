using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    [Header("Room Generation")]
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

    [SerializeField]
    private int room_count_ = 10;

    private List<Vertex> vertex_list_ = new List<Vertex>();
    private List<Triangle> all_tri_list_ = new List<Triangle>();
    private List<Triangle> checked_tri_list_ = new List<Triangle>();

    private LineData[] vertex_graph_;
    private List<LineData> kruskal_graph_ = new List<LineData>();

    private void Start()
    {
        generationRoom(room_count_);

    }

    private void Update()
    {
        foreach (var item in kruskal_graph_)
        {
            item.drawDebug();
        }
    }

    private void generationRoom(int _room_size)
    {
        for (int i = 0; i < _room_size; i++)
        {
            generateRoom();
        }
        onColliderComponent();
        StartCoroutine(offColliderComponentWaitForSec(5f));
    }

    private void onColliderComponent()
    {
        foreach (var room in room_list_)
        {
            room.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void offColliderComponent()
    {
        foreach (var room in room_list_)
        {
            room.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private IEnumerator offColliderComponentWaitForSec(float _delay)
    {

        Time.timeScale = 10f;
        yield return new WaitForSeconds(_delay);
        Time.timeScale = 1f;
        correctRoomPosition();


        Testing test = new Testing();
        room_list_ = test.logic(room_list_, 20);
        offColliderComponent();

        convertRoomToVertex();
        getTriangleComb();
        setupCheckedTriangle();
        setupGraph();
    }

    private void correctRoomPosition()
    {
        foreach (var room in room_list_)
        {
            float corrected_x = 0.32f * Mathf.FloorToInt(room.position.x / 0.32f);
            float corrected_y = 0.32f * Mathf.FloorToInt(room.position.y / 0.32f);

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

    private void convertRoomToVertex()
    {
        var rand_arr = getShuffleArray(room_list_.Count);

        for (int i = 0; i < room_list_.Count; i++)
        {
            var bc = room_list_[i].GetComponent<BoxCollider2D>();
            vertex_list_.Add(new Vertex(i, room_list_[i].position.x + bc.offset.x, room_list_[i].position.y + bc.offset.y));
        }
    }

    private void setupCheckedTriangle()
    {
        foreach (var tri in all_tri_list_)
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
                checked_tri_list_.Add(tri);
            }
        }
    }

    private void getTriangleComb()
    {
        all_tri_list_.Clear();

        int r = 3;
        List<Vertex> comb = new List<Vertex>();

        Combination(vertex_list_, comb, 3, 0);
    }

    private void Combination(List<Vertex> arr, List<Vertex> comb, int r, int depth)
    {
        if (r == 0)
        {
            all_tri_list_.Add(new Triangle(comb[0], comb[1], comb[2]));
        }
        else if (depth == arr.Count)
        {
            return;
        }
        else
        {
            Combination(new List<Vertex>(arr), new List<Vertex>(comb), r, depth + 1);

            comb.Add(arr[depth]);
            Combination(new List<Vertex>(arr), new List<Vertex>(comb), r - 1, depth + 1);
        }
    }

    private void setupGraph()
    {
        float[,] is_value = new float[room_count_, room_count_];
        List<LineData> line_graph_list = new List<LineData>();

        for (int i = 0; i < room_count_; i++)
        {
            for (int j = 0; j < room_count_; j++)
            {
                is_value[i, j] = -1f;
            }
        }

        foreach (var triangle in checked_tri_list_)
        {
            var lines = triangle.getLineData();

            foreach (var line_data in lines)
            {
                if (is_value[line_data.start.no, line_data.end.no] == -1f)
                {
                    line_graph_list.Add(line_data);
                    is_value[line_data.start.no, line_data.end.no] = line_data.getDistance();
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

        vertex_graph_ = line_graph_list.ToArray();

        calculKruskal();

        Debug.Log(kruskal_graph_.Count);
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
        int[] parent = new int[vertex_list_.Count];

        for (int i = 0; i < parent.Length; i++)
        {
            parent[i] = i;
        }

        for (int i = 0; i < vertex_graph_.Length; i++)
        {
            LineData cur_edge = vertex_graph_[i];

            int f = cur_edge.start.no;
            int s = cur_edge.end.no;

            if (findRoot(parent, f) == findRoot(parent, s)) continue;

            kruskal_graph_.Add(cur_edge);
            unionRoot(parent, f, s);

            if (kruskal_graph_.Count == vertex_list_.Count - 1) return;
        }
    }

    public int[] getShuffleArray(int _size)
    {
        int[] shuffle_arr = Enumerable.Range(0, _size).ToArray();
        System.Random random = new System.Random();
        shuffle_arr = shuffle_arr.OrderBy(x => random.Next()).ToArray();
        return shuffle_arr;
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
