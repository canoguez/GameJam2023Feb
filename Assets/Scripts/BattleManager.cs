using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField]
    TileSelector tileSelector;

    [SerializeField]
    TurnHandler turnHandler;

    [SerializeField]
    GridManager gridManager;

    public GameObject dinoBoss, dinoMinion1, dinoMinion2, robotBoss, robotMinion1, robotMinion2;

    void Start()
    {
        gridManager.GenerateGrid();
        gridManager.InstantiatePlayers();

        dinoBoss.GetComponent<Player>().UpdatePercent(0);
        dinoMinion1.GetComponent<Player>().UpdatePercent(0);
        dinoMinion2.GetComponent<Player>().UpdatePercent(0);
        robotBoss.GetComponent<Player>().UpdatePercent(0);
        robotMinion1.GetComponent<Player>().UpdatePercent(0);
        robotMinion2.GetComponent<Player>().UpdatePercent(0);

        turnHandler.StartTurns();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            turnHandler.EndPlayerTurn();
    }

    public void StartMove(Player p)
    {
        Debug.Log("STARTING TO MOVE");
    }

    public void UpdatePlayerHealth(Player p, float newPercent)
    {
        p.UpdatePercent(newPercent);
    }
}
