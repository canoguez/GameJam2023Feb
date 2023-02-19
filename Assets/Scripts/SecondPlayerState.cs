using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerState : TurnBaseState
{
    private float timer = 5f;
    
    public override void EnterState(TurnHandler handler)
    {
        Debug.Log("Player Two Turn");
        handler.onPlayerTwoTurn.Invoke();
    }

    public override void UpdateState(TurnHandler handler)
    {
        //DEBUG REMOVE THIS AFTER TESTING
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            handler.SwitchState(handler.player1State);
            timer = 5f;
        }
    }
}
