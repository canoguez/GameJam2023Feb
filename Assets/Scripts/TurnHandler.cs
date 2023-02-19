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

    public TileSelector tileSelector;
    public Text playerTurnText;
    public GameObject defendMovePanel;
    public GameObject clashPanel;

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

        UpdateTurnText();

        if (currentState.GetStatePlayer() == "One")
            InputHandler.Instance.SetCurPlayer(PlayerEnum.P1);
        else
            InputHandler.Instance.SetCurPlayer(PlayerEnum.P2);

        p.StartTurn();

        ToggleDefendMovePanel(true);
    }

    public void MoveDecided()
    {
        actionsLeft--;

        if (actionsLeft <= 0)
            EndPlayerTurn();
        else
            ToggleDefendMovePanel(true);

        UpdateTurnText();
    }

    public void ToggleDefendMovePanel(bool show)
    {
        defendMovePanel.SetActive(show);
    }

    public void DefendChosen()
    {
        Debug.Log(activePlayer.assetName + " - Defend chosen!");
        activePlayer?.Defend();
        EndPlayerTurn();
    }

    public void MoveChosen()
    {
        Debug.Log("Move chosen!");

        tileSelector.StartSelection(activePlayer, 2, (Tile t)=> {
            if (t == null)
            {
                ToggleDefendMovePanel(true);
            }
            else if (activePlayer.currentTile == t)
            {
                Debug.Log("Player chose same tile.");
                MoveChosen();
            }
            else
            {
                if (t.objects.Count > 0)
                {
                    // We shouldnt have more than 1 object on a tile at the moment
                    GameObject go = t.objects[0];

                    Player p = go.GetComponent<Player>();
                    if (p != null)
                    {
                        if (p.team == activePlayer.team)
                        {
                            ToggleDefendMovePanel(true);
                        }
                        else
                        {
                            // Establish which direction we're attacking from
                            int dir = (t.x < activePlayer.currentTile.x) ? 2 : 6;

                            // CLASH
                            activePlayer.currentTile.OnObjectLeave(activePlayer.gameObject);
                            t.OnObjectEnter(activePlayer.gameObject);
                            clashPanel.SetActive(true);
                            clashPanel.GetComponent<ClashPanel>().ClashStart(activePlayer, p, dir);
                        }
                        return;
                    }
                }

                activePlayer.currentTile.OnObjectLeave(activePlayer.gameObject);
                t.OnObjectEnter(activePlayer.gameObject);
                MoveDecided();
            }
        });
    }

    public void EndPlayerTurn()
    {
        if (currentState.GetStatePlayer() == "One")
            SwitchState(player2State);
        else
            SwitchState(player1State);
    }

    private void UpdateTurnText()
    {
        playerTurnText.text = string.Format("Player {0}'s Turn. Actions Left: [{1}]", currentState.GetStatePlayer(), actionsLeft);
    }
}