using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerState : TurnBaseState
{
    private float timer = 5f;
    private PlayerUnit currentUnit = PlayerUnit.Boss;
    private Player curPlayer = null;

    public override void EnterState(TurnHandler handler)
    {
        if (CheckLoss())
            return;

        Debug.Log("Player Two Turn");
        handler.onPlayerTwoTurn.Invoke();
        do { 
            switch (currentUnit)
            {
                case PlayerUnit.Boss:
                    curPlayer = BattleManager.Instance.robotBoss.GetComponent<Player>();
                    break;
                case PlayerUnit.Minion1:
                    curPlayer = BattleManager.Instance.robotMinion1.GetComponent<Player>();
                    break;
                case PlayerUnit.Minion2:
                    curPlayer = BattleManager.Instance.robotMinion2.GetComponent<Player>();
                    break;
            }

            if (curPlayer.KOd)
            {
                SwapUnit();
            }
        }
        while (curPlayer.KOd);

        handler.StartPlayerTurn(curPlayer);
    }

    public override void UpdateState(TurnHandler handler)
    {
        
    }

    public override string GetStatePlayer()
    {
        return "Two";
    }

    public override void EndTurn()
    {
        SwapUnit();
    }

    public void SwapUnit()
    {
        int newUnit = ((int)currentUnit + 1) % 3;
        currentUnit = (PlayerUnit)newUnit;
    }

    public bool CheckLoss()
    {
        bool bossAlive = BattleManager.Instance.robotBoss.GetComponent<Player>();
        bool minion1Alive = BattleManager.Instance.robotMinion1.GetComponent<Player>();
        bool minion2Alive = BattleManager.Instance.robotMinion2.GetComponent<Player>();

        if (!bossAlive && !minion1Alive && !minion2Alive)
        {
            TurnHandler.Instance.TriggerPlayerVictory(PlayerEnum.P1);
            return true;
        }

        return false;
    }
}
