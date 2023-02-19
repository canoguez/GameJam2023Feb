using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TurnHandler : Singleton<TurnHandler>
{
    TurnBaseState currentState;
    public FirstPlayerState player1State = new FirstPlayerState();
    public SecondPlayerState player2State = new SecondPlayerState();

    public UnityEvent onPlayerOneTurn;
    public UnityEvent onPlayerTwoTurn;

    public Text playerTurnText;
    public GameObject defendMovePanel;
    
    private GameObject activePlayerCrown;

    // Turn variables
    public const int ACTIONS_PER_TURN = 2;
    public int actionsLeft = 0;
    Player activePlayer = null;

    public override void Awake()
    {
        base.Awake();
        activePlayerCrown = GameObject.Instantiate(Resources.Load<GameObject>("ActivePlayer"));
        activePlayerCrown.SetActive(false);
    }

    public void StartTurns()
    {
        currentState = player1State;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateState(this);
    }

    public void SwitchState(TurnBaseState state)
    {
        currentState?.EndTurn();
        currentState = state;
        state.EnterState(this);
    }

    public void StartPlayerTurn(Player p)
    {
        activePlayer = p;
        actionsLeft = ACTIONS_PER_TURN;

        activePlayerCrown.SetActive(true);
        activePlayerCrown.transform.parent = activePlayer.transform;
        activePlayerCrown.transform.localPosition = new Vector3();

        playerTurnText.text = string.Format("Player {0}'s Turn. Actions Left: [{1}]",currentState.GetStatePlayer(), actionsLeft);

        ToggleDefendMovePanel(true);
    }

    public void MoveDecided()
    {
        actionsLeft--;
        if (actionsLeft <= 0)
            EndPlayerTurn();
        else
            ToggleDefendMovePanel(true);
    }

    public void ToggleDefendMovePanel(bool show)
    {
        defendMovePanel.SetActive(show);
    }

    public void DefendChosen()
    {
        Debug.Log("Defend chosen!");
        activePlayer?.Defend();
        EndPlayerTurn();
    }

    public void MoveChosen()
    {
        Debug.Log("Move chosen!");
    }

    public void EndPlayerTurn()
    {
        if (currentState.GetStatePlayer() == "One")
            SwitchState(player2State);
        else
            SwitchState(player1State);
    }
}