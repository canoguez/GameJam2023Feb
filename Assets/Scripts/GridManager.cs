using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public TileSelector tileSelector;

    public GameObject tilePrefab;
    public GameObject dinoBossPrefab, robotBossPrefab;
    public GameObject dinoMinion1Prefab, dinoMinion2Prefab, robotMinion1Prefab, robotMinion2Prefab;

    public int width;
    public int height;

    public Vector2Int dinoBossSpawn, robotBossSpawn;
    public Vector2Int dinoMinion1Spawn, dinoMinion2Spawn, robotMinion1Spawn, robotMinion2Spawn;

    private Tile[,] tiles;

    private void Awake()
    {
        GenerateGrid();
        InstantiatePlayers();
        tileSelector.TestSelector();
    }

    void GenerateGrid()
    {
        // Generate Grid
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tileObject = Instantiate(tilePrefab, transform);
                Tile tile = tileObject.GetComponent<Tile>();
                tile.Instantiate(x, y, IsPlatform(x, y));
                tiles[x, y] = tile;

                bool _atTop = (y == 0);
                bool _atLeft = (x == 0);
                bool _atBottom = (y == height-1);

                // Link north tile
                if (!_atTop)
                {
                    tile.LinkTile(tiles[x, y - 1], 4);
                }

                // Link west tile
                if (!_atLeft)
                {
                    tile.LinkTile(tiles[x - 1, y], 6);

                    // Link NorthWest tile
                    if (!_atTop)
                    {
                        tile.LinkTile(tiles[x - 1, y - 1], 5);
                    }

                    // Link SouthWest tile
                    if (!_atBottom)
                    {
                        tile.LinkTile(tiles[x - 1, y + 1], 7);
                    }
                }
            }
        }
    }

    public Tile GetTileAt(Vector2Int pos)
    {
        return GetTileAt(pos.x, pos.y);
    }

    public Tile GetTileAt(int x, int y)
    {
        if (x < 0 || x > width || y < 0 || y > height)
            return null;

        return tiles[x, y];
    }

    void InstantiatePlayers()
    {
        GameObject dinoBoss = GameObject.Instantiate(dinoBossPrefab);
        GetTileAt(dinoBossSpawn).OnObjectEnter(dinoBoss);

        GameObject robotBoss = GameObject.Instantiate(robotBossPrefab);
        GetTileAt(robotBossSpawn).OnObjectEnter(robotBoss);

        GameObject dinoMinion1 = GameObject.Instantiate(dinoMinion1Prefab);
        GetTileAt(dinoMinion1Spawn).OnObjectEnter(dinoMinion1);

        GameObject dinoMinion2 = GameObject.Instantiate(dinoMinion2Prefab);
        GetTileAt(dinoMinion2Spawn).OnObjectEnter(dinoMinion2);

        GameObject robotMinion1 = GameObject.Instantiate(robotMinion1Prefab);
        GetTileAt(robotMinion1Spawn).OnObjectEnter(robotMinion1);

        GameObject robotMinion2 = GameObject.Instantiate(robotMinion2Prefab);
        GetTileAt(robotMinion2Spawn).OnObjectEnter(robotMinion2);
    }

    bool IsPlatform(int _x, int _y)
    {
        if (_y == 0)
            return true;

        return false;
    }
}