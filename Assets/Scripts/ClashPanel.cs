using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClashPanel : MonoBehaviour
{
    bool p1Chose, p2Chose;
    AttackChosen p1Option, p2Option;
    Player p1, p2;
    int atkDir;

    public void ClashStart(Player _p1, Player _p2, int _atkDir)
    {
        p1 = _p1;
        p2 = _p2;
        atkDir = _atkDir;
    }

    private void Update()
    {
        if(Input.GetKeyDown(InputHandler.P1_Left))
        {
            p1Chose = true;
            p1Option = AttackChosen.Defend;
        }
        else if (Input.GetKeyDown(InputHandler.P1_Right))
        {
            p1Chose = true;
            p1Option = AttackChosen.Attack;
        }
        if (Input.GetKeyDown(InputHandler.P2_Left))
        {
            p2Chose = true;
            p2Option = AttackChosen.Defend;
        }
        else if (Input.GetKeyDown(InputHandler.P2_Right))
        {
            p2Chose = true;
            p2Option = AttackChosen.Attack;
        }

        if(p1Chose && p2Chose)
        {
            p1Chose = false;
            p2Chose = false;
            BattleManager.Instance.Clash(p1, p1Option, p2, p2Option, atkDir);
            gameObject.SetActive(false);
        }
    }
}
