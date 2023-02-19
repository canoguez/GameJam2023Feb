using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : Singleton<InputHandler>
{
    public const KeyCode P1_Left = KeyCode.Keypad7;
    public const KeyCode P1_Right = KeyCode.Keypad1;
    public const KeyCode P1_Up = KeyCode.Keypad5;
    public const KeyCode P1_Down = KeyCode.Keypad4;
    public const KeyCode P1_Confirm = KeyCode.Keypad2;
    public const KeyCode P1_Cancel = KeyCode.Keypad8;

    public const KeyCode P2_Left = KeyCode.Keypad9;
    public const KeyCode P2_Right = KeyCode.Keypad3;
    public const KeyCode P2_Up = KeyCode.KeypadPlus;
    public const KeyCode P2_Down = KeyCode.Keypad6;
    public const KeyCode P2_Confirm = KeyCode.KeypadEnter;
    public const KeyCode P2_Cancel = KeyCode.KeypadMinus;


    PlayerEnum curPlayer = PlayerEnum.P1 | PlayerEnum.P2;

    public void SetCurPlayer(PlayerEnum _player)
    {
        curPlayer = _player;
    }

    public bool Left()
    {
        return (Input.GetKeyDown(P1_Left) && (curPlayer & PlayerEnum.P1) != 0) || (Input.GetKeyDown(P2_Left) && (curPlayer & PlayerEnum.P2) != 0);
    }
    public bool Right()
    {
        return (Input.GetKeyDown(P1_Right) && (curPlayer & PlayerEnum.P1) != 0) || (Input.GetKeyDown(P2_Right) && (curPlayer & PlayerEnum.P2) != 0);
    }
    public bool Up()
    {
        return (Input.GetKeyDown(P1_Up) && (curPlayer & PlayerEnum.P1) != 0) || (Input.GetKeyDown(P2_Up) && (curPlayer & PlayerEnum.P2) != 0);
    }
    public bool Down()
    {
        return (Input.GetKeyDown(P1_Down) && (curPlayer & PlayerEnum.P1) != 0) || (Input.GetKeyDown(P2_Down) && (curPlayer & PlayerEnum.P2) != 0);
    }
    public bool Confirm()
    {
        return (Input.GetKeyDown(P1_Confirm) && (curPlayer & PlayerEnum.P1) != 0) || (Input.GetKeyDown(P2_Confirm) && (curPlayer & PlayerEnum.P2) != 0);
    }
    public bool Cancel()
    {
        return (Input.GetKeyDown(P1_Cancel) && (curPlayer & PlayerEnum.P1) != 0) || (Input.GetKeyDown(P2_Cancel) && (curPlayer & PlayerEnum.P2) != 0);
    }
}

[Flags]
public enum PlayerEnum
{
    P1 = 1,
    P2 = 2
}
