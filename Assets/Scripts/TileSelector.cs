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
    Tile jumpableTile;
    Tile droppableTile;

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
            if (droppableTile != null && currentTile == droppableTile)
            {
                MoveSelectorTo(currentPlayer.currentTile);
            }
            else if (jumpableTile!=null)
            {
                MoveSelectorTo(jumpableTile);
            }
        }
        // Down
        if (InputHandler.Instance.Down())
        {
            if(jumpableTile!=null && currentTile == jumpableTile)
            {
                MoveSelectorTo(currentPlayer.currentTile);
            }
            else if(droppableTile != null)
            {
                MoveSelectorTo(droppableTile);
            }
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
        jumpableTile = GetJumpableTile(p.currentTile);
        droppableTile = GetDroppableTile(p.currentTile);

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

            if(jumpableTile)
            {
                GameObject goodTile = GameObject.Instantiate(goodTilePrefab, selectableRangeParent.transform);
                goodTile.transform.position = jumpableTile.transform.position;
            

                if (jumpableTile.objects.Count > 0)
                {
                    Player p = jumpableTile.objects[0].GetComponent<Player>();
                    if (p != null)
                    {
                        SpriteRenderer tileRend = goodTile.GetComponentInChildren<SpriteRenderer>();
                        // Yellow = Teammate
                        if (p.team == currentPlayer.team)
                        {
                            tileRend.sprite = Resources.Load<Sprite>("Tiles/YellowTile");
                        }
                        else
                        {
                            tileRend.sprite = Resources.Load<Sprite>("Tiles/RedTile");
                        }
                    }
                }
            }

            if (droppableTile)
            {
                GameObject goodTile = GameObject.Instantiate(goodTilePrefab, selectableRangeParent.transform);
                goodTile.transform.position = droppableTile.transform.position;

                if (droppableTile.objects.Count > 0)
                {
                    Player p = droppableTile.objects[0].GetComponent<Player>();
                    if (p != null)
                    {
                        SpriteRenderer tileRend = goodTile.GetComponentInChildren<SpriteRenderer>();
                        // Yellow = Teammate
                        if (p.team == currentPlayer.team)
                        {
                            tileRend.sprite = Resources.Load<Sprite>("Tiles/YellowTile");
                        }
                        else
                        {
                            tileRend.sprite = Resources.Load<Sprite>("Tiles/RedTile");
                        }
                    }
                }
            }

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
                        SpriteRenderer tileRend = goodTile.GetComponentInChildren<SpriteRenderer>();
                        // Yellow = Teammate
                        if (p.team == currentPlayer.team)
                        {
                            tileRend.sprite = Resources.Load<Sprite>("Tiles/YellowTile");
                        }
                        else
                        {
                            tileRend.sprite = Resources.Load<Sprite>("Tiles/RedTile");
                        }
                    }
                }
            }
        }
    }

    void ConfirmSelection()
    {
        if(currentTile == jumpableTile)
        {
            TurnHandler.Instance.actionsLeft--;
        }

        if (currentTile.isPlatform)
        {
            Debug.LogWarning("Invalid tile selected.");
            return;
        }

        ClearSelector();
        onSelect?.Invoke(currentTile);
    }

    void CancelSelection()
    {
        ClearSelector();
        onSelect?.Invoke(null);
    }

    void ClearSelector()
    {
        currentPlayer = null;
        selectableRange = new List<Tile>();
        isSelecting = false;
        jumpableTile = null;
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
            if (tile != null && !curList.Contains(tile) && tile.IsSteppable())
            {
                curList.Add(tile);
                GetAllTilesWithingRange(tile, range-1, curList);
            }
        }

        return curList;
    }

    Tile GetJumpableTile(Tile t)
    {
        if (TurnHandler.Instance.actionsLeft < 2)
        {
            return null;
        }

        // Check above
        Tile nextTile = t.tiles[0];
        while (nextTile != null)
        {
            // Right
            Tile rightTile = nextTile.tiles[2];
            if (rightTile && rightTile.IsSteppable())
            {
                return rightTile;
            }

            // Left
            Tile leftTile = nextTile.tiles[6];
            if (leftTile && leftTile.IsSteppable())
            {
                return leftTile;
            }

            nextTile = nextTile.tiles[0];
        }

        return null;
    }

    Tile GetDroppableTile(Tile t)
    {
        // Right
        Tile rightTile = t.tiles[2];
        if(rightTile && !rightTile.IsSteppable())
            if (rightTile && !rightTile.isPlatform && rightTile.tiles[4] != null)
            {
                Tile nextTile = rightTile;
                while (nextTile != null)
                {
                    if (nextTile.IsSteppable())
                        return nextTile;
                    nextTile = nextTile.tiles[4];
                }
            }

        // Left
        Tile leftTile = t.tiles[6];
        if (leftTile && !leftTile.IsSteppable())
            if (leftTile && !leftTile.isPlatform && leftTile.tiles[4] != null)
            {
                Tile nextTile = leftTile;
                while (nextTile != null)
                {
                    if(nextTile.IsSteppable())
                        return nextTile;
                    nextTile = nextTile.tiles[4];
                }
            }

        return null;
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

        if (!selectableRange.Contains(t) && !jumpableTile && !droppableTile)
            return;

        selector.transform.position = t.transform.position;
        currentTile = t;
    }
}
