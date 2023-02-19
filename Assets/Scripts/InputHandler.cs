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

    private void Start()
    {
        
    }
}
