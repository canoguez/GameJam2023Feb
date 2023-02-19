using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileSelector : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject selectorPrefab;
    public GameObject goodTilePrefab;
    GameObject selector;

    Action<Tile> onSelect;
    Tile currentTile;
    Player currentPlayer;
    int maxRange = 0;

    bool isSelecting;

    GameObject selectableRangeParent;
    List<Tile> selectableRange;

    private void Awake()
    {
        selector = GameObject.Instantiate(selectorPrefab);
        selectableRange = new List<Tile>();
        ToggleSelector(false);
    }

    private void Update()
    {
        if (!isSelecting)
            return;

        // Left
        if (InputHandler.Instance.Left())
        {
            MoveSelectorTo(currentTile.tiles[6]);
        }
        // Up
        if (InputHandler.Instance.Up())
        {
            MoveSelectorTo(currentTile.tiles[0]);
        }
        // Down
        if (InputHandler.Instance.Down())
        {
            MoveSelectorTo(currentTile.tiles[4]);
        }
        // Right
        if (InputHandler.Instance.Right())
        {
            MoveSelectorTo(currentTile.tiles[2]);
        }

        // Confirm
        if (InputHandler.Instance.Confirm())
        {
            ConfirmSelection();
        }
        // Cancel
        if (InputHandler.Instance.Cancel())
        {
            CancelSelection();
        }
    }

    public void StartSelection(Player p, int movement, Action<Tile> _onSelect)
    {
        if (isSelecting)
            return;

        currentPlayer = p;
        onSelect = _onSelect;
        maxRange = movement;

        selectableRange = GetAllTilesWithingRange(p.currentTile, maxRange, selectableRange);
        ToggleSelectableRange(true);

        isSelecting = true;
        ToggleSelector(true);
        MoveSelectorTo(p.currentTile);
    }

    void ToggleSelectableRange(bool show)
    {
        if (selectableRangeParent == null)
            selectableRangeParent = new GameObject("Selectable Range Parent");

        // Destroy all selectors whenever we call this
        while (selectableRangeParent.transform.childCount > 0)
        {
            DestroyImmediate(selectableRangeParent.transform.GetChild(0).gameObject);
        }

        if(show)
        {            
            foreach (Tile t in selectableRange)
            {
                GameObject goodTile = GameObject.Instantiate(goodTilePrefab, selectableRangeParent.transform);
                goodTile.transform.position = t.transform.position;

                // Default = Green = Good

                if(t.objects.Count > 0)
                {
                    Player p = t.objects[0].GetComponent<Player>();
                    if (p != null)
                    {
                        // Yellow = Teammate
                        if (p.team == currentPlayer.team)
                        {
                            goodTile.GetComponent<Renderer>().material.color = Color.yellow - new Color(0,0,0,0.5f);
                        }
                        else
                        {
                            goodTile.GetComponent<Renderer>().material.color = Color.red - new Color(0, 0, 0, 0.5f);
                        }
                    }
                }
            }
        }
    }

    void ConfirmSelection()
    {
        if (currentTile.isPlatform)
        {
            Debug.LogWarning("Invalid tile selected.");
            return;
        }

        onSelect?.Invoke(currentTile);
        ClearSelector();
    }

    void CancelSelection()
    {
        onSelect?.Invoke(null);
        ClearSelector();
    }

    void ClearSelector()
    {
        onSelect = null;
        currentTile = null;
        currentPlayer = null;
        selectableRange = new List<Tile>();
        isSelecting = false;
        ToggleSelector(false);
        ToggleSelectableRange(false);
    }

    List<Tile> GetAllTilesWithingRange(Tile t, int range, List<Tile> curList)
    {
        if (range == 0)
            return curList;

        for(int i = 0; i < 8;++i)
        {
            Tile tile = t.tiles[i];

            // Only add steppable platforms.
            // TODO: add vertical movement
            if (tile != null && !curList.Contains(tile) && tile.IsSteppable())
            {
                curList.Add(tile);
                GetAllTilesWithingRange(tile, range-1, curList);
            }
        }

        return curList;
    }

    void ToggleSelector(bool enabled)
    {
        Debug.Log("Toggle Selector: " + enabled);
        selector.GetComponentInChildren<Renderer>().enabled = enabled;
    }

    void MoveSelectorTo(Tile t)
    {
        Debug.Log(t);
        if (t == null)
            return;

        if (!selectableRange.Contains(t))
            return;

        selector.transform.position = t.transform.position;
        currentTile = t;
    }
}
