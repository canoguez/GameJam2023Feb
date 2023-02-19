using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendMovePanel : MonoBehaviour
{
    Choice currentChoice = Choice.Defend;

    public GameObject Defend, Move, Select;

    public void Start()
    {
        Select.transform.position = currentChoice == Choice.Defend ? Defend.transform.position : Move.transform.position;
    }

    private void Update()
    {
        if (InputHandler.Instance.Left() || InputHandler.Instance.Right())
            ChangeChoice();

        if (InputHandler.Instance.Confirm())
            SelectChoice();

    }

    public void ChangeChoice()
    {
        currentChoice = (currentChoice == Choice.Defend) ? Choice.Move : Choice.Defend;
        Select.transform.position = currentChoice == Choice.Defend ? Defend.transform.position : Move.transform.position;
    }

    public void SelectChoice()
    {
        gameObject.SetActive(false);

        if (currentChoice == Choice.Defend)
            TurnHandler.Instance.DefendChosen();
        else
            TurnHandler.Instance.MoveChosen();
    }

    enum Choice
    {
        Defend,
        Move
    }
}

