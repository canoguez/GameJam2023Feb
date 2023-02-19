using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileSelector : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject selectorPrefab;
    GameObject selector;

    Action<Tile> onSelect;
    Tile currentTile;
    Player currentPlayer;
    int maxRange = 0;

    bool isSelecting;

    private void Awake()
    {
        selector = GameObject.Instantiate(selectorPrefab);
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

        isSelecting = true;
        ToggleSelector(true);
        MoveSelectorTo(p.currentTile);
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
        isSelecting = false;
        ToggleSelector(false);
    }

    void ToggleSelector(bool enabled)
    {
        Debug.Log("Toggle Selector: " + enabled);
        selector.GetComponent<Renderer>().enabled = enabled;
    }

    void MoveSelectorTo(Tile t)
    {
        Debug.Log(t);
        if (t == null)
            return;

        selector.transform.position = t.transform.position;
        currentTile = t;
    }
}
