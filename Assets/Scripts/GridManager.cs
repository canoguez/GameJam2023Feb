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
    private bool[,] platformMap;

    public void GenerateGrid()
    {
        // Generate Grid
        tiles = new Tile[width, height];
        platformMap = new bool[width, height];
        SetUpPlatformMap();

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

    public void InstantiatePlayers()
    {
        BattleManager.Instance.dinoBoss = GameObject.Instantiate(dinoBossPrefab);
        GetTileAt(dinoBossSpawn).OnObjectEnter(BattleManager.Instance.dinoBoss);

        BattleManager.Instance.robotBoss = GameObject.Instantiate(robotBossPrefab);
        GetTileAt(robotBossSpawn).OnObjectEnter(BattleManager.Instance.robotBoss);

        BattleManager.Instance.dinoMinion1 = GameObject.Instantiate(dinoMinion1Prefab);
        GetTileAt(dinoMinion1Spawn).OnObjectEnter(BattleManager.Instance.dinoMinion1);

        BattleManager.Instance.dinoMinion2 = GameObject.Instantiate(dinoMinion2Prefab);
        GetTileAt(dinoMinion2Spawn).OnObjectEnter(BattleManager.Instance.dinoMinion2);

        BattleManager.Instance.robotMinion1 = GameObject.Instantiate(robotMinion1Prefab);
        GetTileAt(robotMinion1Spawn).OnObjectEnter(BattleManager.Instance.robotMinion1);

        BattleManager.Instance.robotMinion2 = GameObject.Instantiate(robotMinion2Prefab);
        GetTileAt(robotMinion2Spawn).OnObjectEnter(BattleManager.Instance.robotMinion2);
    }

    void SetUpPlatformMap()
    {
        int distFromWall = 4;
        int platormWidthMin = 4;
        int platormWidthMax = 7;

        int plat1Length = Random.Range(platormWidthMin, platormWidthMax);
        int plat2Length = Random.Range(platormWidthMin, platormWidthMax);
        int plat2StartX = width - distFromWall - plat2Length;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (y == 0)
                {
                    platformMap[x, y] = true;
                    continue;
                }

                // First set of platforms
                if (y == 5)
                {
                    // 1st plaform
                    if (x > distFromWall && x < distFromWall + plat1Length)
                    {
                        platformMap[x, y] = true;
                        continue;
                    }

                    // 2nd platform
                    if (x > plat2StartX && x < plat2StartX + plat2Length)

                    {
                        platformMap[x, y] = true;
                        continue;
                    }
                }

                // Second set of platforms
                if (y == 10)
                {
                    // 1st plaform

                    if (x >= distFromWall + plat1Length && x <= plat2StartX)
                    {
                        platformMap[x, y] = true;
                        continue;
                    }
                }
            }
        }
    }

    //27 x 18
    bool IsPlatform(int _x, int _y)
    {
        return platformMap[_x, _y];
    }
}