using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerState : TurnBaseState
{
    private PlayerUnit currentUnit = PlayerUnit.Boss;
    private Player curPlayer = null;

    public override void EnterState(TurnHandler handler)
    {
        Debug.Log("Player One Turn");
        handler.onPlayerOneTurn.Invoke();

        switch(currentUnit)
        {
            case PlayerUnit.Boss:
                curPlayer = BattleManager.Instance.dinoBoss.GetComponent<Player>();
                break;
            case PlayerUnit.Minion1:
                curPlayer = BattleManager.Instance.dinoMinion1.GetComponent<Player>();
                break;
            case PlayerUnit.Minion2:
                curPlayer = BattleManager.Instance.dinoMinion2.GetComponent<Player>();
                break;
        }


        handler.StartPlayerTurn(curPlayer);
    }

    public override void UpdateState(TurnHandler handler)
    {
        
    }

    public override string GetStatePlayer()
    {
        return "One";
    }

    public override void EndTurn()
    {
        int newUnit = ((int)currentUnit+1) % 3;
        currentUnit = (PlayerUnit)newUnit;
    }
}
