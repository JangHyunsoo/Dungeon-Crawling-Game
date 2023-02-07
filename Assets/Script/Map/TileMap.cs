using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private int stage_no_;
    private int world_width_;
    private int world_height_;
    private Room[] room_arr_;
    private Vector2Int world_tile_min_pos_;
    private Tile[,] world_tile_arr_;
    private List<Stair> stair_list_ = new List<Stair>();

    private float tile_size_ = 0.32f;

    public int stage_no { get => stage_no_; }
    public int stair_count { get => stair_list_.Count; }
    public List<Stair> stair_list { get => stair_list_; }

    public void setStageNo(int _stage_no)
    {
        stage_no_ = _stage_no;
    }

    public void setRoomArr(List<Room> _room_list)
    {
        room_arr_ = _room_list.ToArray();
    }

    public Room getRoom(int _idx)
    {
        return room_arr_[_idx];
    }

    public int getRoomLength()
    {
        return room_arr_.Length;
    }

    public Stair getStair(int _idx)
    {
        return stair_list_[_idx];
    }

    public bool isOnMap(Vector2Int pos)
    {
        return pos.x >= world_tile_min_pos_.x && pos.x < world_width_ + world_tile_min_pos_.x && pos.y >= world_tile_min_pos_.y && pos.y < world_height_ + world_tile_min_pos_.y;
    }

    public bool isOnMap(int x, int y)
    {
        return x >= world_tile_min_pos_.x && x < world_width_ + world_tile_min_pos_.x && y >= world_tile_min_pos_.y && y < world_height_ + world_tile_min_pos_.y;
    }

    public List<Tile> getNeighbourTile(Vector2Int pos)
    {
        List<Tile> neighbours = new List<Tile>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                var cur_pos = new Vector2Int(pos.x + x, pos.y + y);

                if (isOnMap(cur_pos))
                {
                    var cur_tile_pos = convertTilePos2ArrayPos(cur_pos);
                    neighbours.Add(world_tile_arr_[cur_tile_pos.x, cur_tile_pos.y]);
                }
            }
        }

        return neighbours;
    }

    public void setWorldPos(int _world_width, int _world_height, Vector2Int _world_tile_min_pos)
    {
        world_width_ = _world_width;
        world_height_ = _world_height;
        world_tile_min_pos_ = _world_tile_min_pos;
        world_tile_arr_ = new Tile[world_width_, world_height_];

        for (int i = 0; i < world_width_; i++)
        {
            for (int j = 0; j < world_height_; j++)
            {
                var tile = TileDataBase.instance.createTile(convertArrayPos2RealPos(i, j), 0);
                tile.name = (new Vector2Int(i, j) + world_tile_min_pos_).ToString();
                tile.transform.SetParent(transform);
                world_tile_arr_[i, j] = tile.GetComponent<Tile>();
                world_tile_arr_[i,j].setTilePos(new Vector2Int(i, j) + world_tile_min_pos_);
            }
        }
    }

    public Tile getTileByTilePos(Vector2Int pos)
    {
        var temp = convertTilePos2ArrayPos(pos);
        return world_tile_arr_[temp.x, temp.y];
    }

    public Vector2 getRealPosByTilePos(Vector2Int pos)
    {
        return getTileByTilePos(pos).transform.position;
    }


    public void setTileByArrayPos(Vector2Int pos, int _tile_type)
    {
        var temp = convertTilePos2ArrayPos(pos);
        world_tile_arr_[temp.x, temp.y].setType(_tile_type);
    }

    public void setTileByArrayPos(int x, int y, int _tile_type)
    {
        setTileByArrayPos(new Vector2Int(x, y), _tile_type);
    }

    public void setTileByWorldPos(Vector2 pos, int _tile_type)
    {
        var temp = convertWorldPos2ArrayPos(pos);
        world_tile_arr_[temp.x, temp.y].setType(_tile_type);
    }

    public Vector2 convertArrayPos2RealPos(Vector2Int pos)
    {
        return new Vector2((pos.x + world_tile_min_pos_.x) * tile_size_ + tile_size_ / 2, (pos.y + world_tile_min_pos_.y) * tile_size_ + tile_size_ / 2);
    }

    public Vector2 convertArrayPos2RealPos(int x, int y)
    {
        return new Vector2((x + world_tile_min_pos_.x) * tile_size_ + tile_size_ / 2, (y + world_tile_min_pos_.y) * tile_size_ + tile_size_ / 2);
    }

    public Vector2Int convertTilePos2ArrayPos(int x, int y)
    {
        return new Vector2Int(x, y) - world_tile_min_pos_;
    }

    public Vector2Int convertTilePos2ArrayPos(Vector2Int pos)
    {
        return pos - world_tile_min_pos_;
    }

    public Vector2Int convertWorldPos2TilePos(Vector2 pos)
    {
        return new Vector2Int(Mathf.RoundToInt((pos.x - tile_size_ / 2) / tile_size_), Mathf.RoundToInt((pos.y - tile_size_ / 2) / tile_size_));
    }

    public Vector2Int convertWorldPos2ArrayPos(Vector2 pos)
    {
        return convertTilePos2ArrayPos(convertWorldPos2TilePos(pos));
    }

    public Vector2Int getIncludeTilePos(Vector2 pos)
    {
        return new Vector2Int(Mathf.RoundToInt((pos.x - tile_size_ / 2) / tile_size_), Mathf.RoundToInt((pos.y - tile_size_ / 2) / tile_size_));
    }

    public Vector2Int getRandomRoomTilePos()
    {
        int rand_room_idx = Random.RandomRange(0, room_arr_.Length);
        Room rand_room = room_arr_[rand_room_idx];
        Vector2 rand_room_pos = rand_room.getRandomRealPos();
        Vector2Int include_tile_pos = getIncludeTilePos(rand_room_pos);
        return include_tile_pos;
    }

    public Tile getRandomRoomTile()
    {
        var tile_pos = getRandomRoomTilePos();
        return getTileByTilePos(tile_pos);
    }

    // create stair
    public Stair createStairRanndom()
    {
        var go = TileDataBase.instance.createStair();
        var tile = getRandomRoomTile();
        var stair = go.GetComponent<Stair>();
        stair.setMyInfo(stage_no_, stair_list_.Count);
        tile.addObject(stair);
        stair_list_.Add(stair);
        return stair;
    }

    // A* path finding
    public List<Vector2Int> findPath(Vector2Int startPos, Vector2Int targetPos, int vision)
    {
        if (getDistance(startPos, targetPos) >= vision) return new List<Vector2Int>();

        Tile startNode = getTileByTilePos(startPos);
        Tile targetNode = getTileByTilePos(targetPos);

        List<Tile> openSet = new List<Tile>();
        HashSet<Tile> closedSet = new HashSet<Tile>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Tile tile = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].path_data.fCost < tile.path_data.fCost || openSet[i].path_data.fCost == tile.path_data.fCost)
                {
                    if (openSet[i].path_data.hCost < tile.path_data.hCost)
                        tile = openSet[i];
                }
            }

            openSet.Remove(tile);
            closedSet.Add(tile);

            if (tile == targetNode)
            {
                return retracePath(startNode, targetNode);
            }

            foreach (Tile neighbour in getNeighbourTile(tile.tile_pos))
            {
                if (!neighbour.walkable_without_player || closedSet.Contains(neighbour))
                {
                    continue;
                }

                if(Vector2Int.Distance(startNode.tile_pos, neighbour.tile_pos) >= 5f)
                {
                    continue;
                }

                int newCostToNeighbour = tile.path_data.gCost + getDistance(tile, neighbour);

                if (newCostToNeighbour < neighbour.path_data.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.path_data.gCost = newCostToNeighbour;
                    neighbour.path_data.hCost = getDistance(neighbour, targetNode);
                    neighbour.path_data.parent = tile;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        return new List<Vector2Int>();
    }

    private List<Vector2Int> retracePath(Tile start_tile, Tile end_tile)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Tile current_tile = end_tile;

        while (current_tile != start_tile)
        {
            path.Add(current_tile.tile_pos);
            current_tile = current_tile.path_data.parent;
        }
        path.Reverse();
        return path;
    }

    private int getDistance(Tile one, Tile other)
    {
        int dstX = Mathf.Abs(one.tile_pos.x - other.tile_pos.x);
        int dstY = Mathf.Abs(one.tile_pos.y - other.tile_pos.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    private int getDistance(Vector2Int one, Vector2Int other)
    {
        int dstX = Mathf.Abs(one.x - other.x);
        int dstY = Mathf.Abs(one.y - other.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}