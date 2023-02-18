using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;

    public int width;
    public int height;

    private Tile[,] tiles;

    private void Awake()
    {
        GenerateGrid();
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
                tile.Instantiate(x, y, false);
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
}