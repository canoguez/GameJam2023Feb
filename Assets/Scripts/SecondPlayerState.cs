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
        Debug.Log("Player Two Turn");
        handler.onPlayerTwoTurn.Invoke();
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
        int newUnit = ((int)currentUnit + 1) % 3;
        currentUnit = (PlayerUnit)newUnit;
    }
}
