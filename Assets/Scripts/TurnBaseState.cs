using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnBaseState
{
    public abstract void EnterState(TurnHandler handler);
    public abstract void UpdateState(TurnHandler handler);

    public abstract void EndTurn();
    public abstract string GetStatePlayer();
}
