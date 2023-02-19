using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    
    // p1 is agressor
    public void Clash(Player p1, AttackChosen p1Attack, Player p2, AttackChosen p2Attack, int atkDirection)
    {
        // Track Player1 atk
        bool atk1Success = false;
        // Track Player2 atk
        bool atk2Success = false;
        // Track Player1 def
        bool def1Success = false;
        // Track Player2 def
        bool def2Success = false;


        if (p1Attack == AttackChosen.Attack)
            atk1Success = UnityEngine.Random.Range(0, 1f) < p1.AttackPercent;

        if (p2Attack == AttackChosen.Attack)
            atk2Success = UnityEngine.Random.Range(0, 1f) < p2.AttackPercent;

        if (p1Attack == AttackChosen.Defend)
            p1.defending = true;

        if (p2Attack == AttackChosen.Defend)
            p2.defending = true;


        // Clash = No damage, just knockback
        if (atk1Success && atk2Success)
        {
            // P1, the agressor will be launched back
            p1.InflictKnockback(1,(atkDirection+4)%8);
            p2.InflictKnockback(1,atkDirection);
            ClashDone();
            return;
        }

        if (atk1Success)
        {
            float dmg = p1.AttackDamage;
            dmg *= (def2Success ? p2.DefensePercent : 1f);
            p2.InflictDamage(dmg);

            if (!def2Success)
                p2.InflictKnockback(1, atkDirection);
        }
        if (atk2Success)
        {
            float dmg = p2.AttackDamage;
            dmg *= (def1Success ? p1.DefensePercent : 1f);
            p1.InflictDamage(dmg);

            if (!def2Success)
                p2.InflictKnockback(1, (atkDirection + 4) % 8);
        }

        p1.defending = false;
        p2.defending = false;

        ClashDone();
    }

    public void ClashDone()
    {
        TurnHandler.Instance.MoveDecided();
    }

    public void UpdatePlayerHealth(Player p, float newPercent)
    {
        p.UpdatePercent(newPercent);
    }
}

public enum AttackChosen
{
    Attack,
    Defend
}