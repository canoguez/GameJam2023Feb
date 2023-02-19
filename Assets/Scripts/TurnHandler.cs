using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnHandler : MonoBehaviour
{
    TurnBaseState currentState;
    public FirstPlayerState player1State = new FirstPlayerState();
    public SecondPlayerState player2State = new SecondPlayerState();

    public UnityEvent onPlayerOneTurn;
    public UnityEvent onPlayerTwoTurn;

    void Start()
    {
        currentState = player1State;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(TurnBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
