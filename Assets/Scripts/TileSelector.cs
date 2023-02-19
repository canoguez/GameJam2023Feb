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
    int currentPlayer = 0;

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

        if(currentPlayer == 0)
        {
            // Left
            if(Input.GetKeyDown(KeyCode.Keypad1))
            {
                MoveSelectorTo(currentTile.tiles[6]);
            }
            // Up
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                MoveSelectorTo(currentTile.tiles[0]);
            }
            // Down
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                MoveSelectorTo(currentTile.tiles[4]);
            }
            // Right
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                MoveSelectorTo(currentTile.tiles[2]);
            }

            // Confirm
            if (Input.GetKeyDown(KeyCode.KeypadPeriod))
            {
                ConfirmSelection();
            }
            // Cancel
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                CancelSelection();
            }
        }
        else if (currentPlayer == 1)
        {
            // Left
            if(Input.GetKeyDown(KeyCode.Keypad7))
            {
                MoveSelectorTo(currentTile.tiles[6]);
            }
            // Up
            if (Input.GetKeyDown(KeyCode.KeypadDivide))
            {
                MoveSelectorTo(currentTile.tiles[0]);
            }
            // Down
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                MoveSelectorTo(currentTile.tiles[4]);
            }
            // Right
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                MoveSelectorTo(currentTile.tiles[2]);
            }

            // Confirm
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                ConfirmSelection();
            }
            // Cancel
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                CancelSelection();
            }
        }
    }

    public void StartSelection(int _player, Tile _tile, Action<Tile> _onSelect)
    {
        if (isSelecting)
            return;

        currentPlayer = _player;
        onSelect = _onSelect;

        isSelecting = true;
        ToggleSelector(true);
        MoveSelectorTo(_tile);
    }

    void ConfirmSelection()
    {
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
        currentPlayer = 0;
        isSelecting = false;
        ToggleSelector(false);
    }

    void ToggleSelector(bool enabled)
    {
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

    public void TestSelector()
    {
        StartSelection(1, gridManager.GetTileAt(1, 1), (Tile t) => { Debug.Log(t); });
    }
}
