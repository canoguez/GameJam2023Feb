using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x, y;
    public bool isPlatform = false;

    // Surrounding tiles 0 is up, then go clockwise
    public Tile[] tiles;

    public List<GameObject> objects;

    public void Instantiate(int _x, int _y, bool _isPlatform)
    {
        x = _x;
        y = _y;
        isPlatform = _isPlatform;

        tiles = new Tile[8];
        objects = new List<GameObject>();

        transform.position = new Vector3Int(_x, _y, 0);

        SetupTile();
    }

    public void SetupTile()
    {
        // For now just hide any block that's not a platform
        GetComponent<Renderer>().enabled = isPlatform;
    }

    public void LinkTile(Tile _t, int dir)
    {
        tiles[dir] = _t;
        _t.tiles[(dir + 4) % 8] = this;
    }

    public void OnObjectEnter(GameObject _go)
    {
        if(!objects.Contains(_go))
            objects.Add(_go);

        _go.transform.position = transform.position;

        Player p = _go.GetComponent<Player>();
        if (p)
            p.currentTile = this;
    }

    public void OnObjectLeave(GameObject _go)
    {
        if (objects.Contains(_go))
            objects.Remove(_go);
    }
}
