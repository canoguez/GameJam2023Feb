using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Team team;
    public string assetName = "";
    public float percent = 0;
    public int knockBack = 2;
    public bool defending = false;
    public int movement = 2;

    public Tile currentTile;
    public float HP = 150;
    public float AttackDamage = 30;
    public float AttackPercent = .3f;
    public float DefensePercent = .2f;

    public void InflictKnockback(float strength, int dir)
    {
        // TODO: Setup knockback, for now just moves em
        
    }
    
    public void InflictDamage(float damage)
    {
        UpdatePercent(percent + damage);
    }

    public void UpdatePercent(float newPercent)
    {
        percent = newPercent;
        GameUIManager.Instance.portraitDictionary[assetName].percent.text = Mathf.RoundToInt(percent).ToString() + "%";
    }

    public void StartTurn()
    {
        defending = false;
    }

    public void Defend()
    {
        defending = true;
    }
}

public enum Team
{
    Dino,
    Robot
}