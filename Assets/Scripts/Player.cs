using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Team team;
    public string assetName = "";
    public int knockBack = 2;
    public bool defending = false;
    public int movement = 2;
    public bool KOd = false;

    public Tile currentTile;
    public float HP = 150;
    public float AttackDamage = 30;
    public float AttackPercent = .3f;
    public float DefensePercent = .2f;

    private float curHP;
    private float KnockBackDamage = 10f;

    public float Percent
    {
        get
        {
            return (1-(curHP / HP));
        }
    }

    private void Awake()
    {
        curHP = HP;
    }

    public void InflictKnockback(float strength, int dir)
    {
        // No Knockback if KOd
        if (KOd)
            return;

        // TODO: Setup knockback, for now just moves em
        int modStrength = (int)strength + Mathf.FloorToInt(strength * Percent);

        if (modStrength <= 0)
            return;

        Tile newTile = currentTile;

        for (int i = 0; i < modStrength; ++i)
        {
            Tile nextTile = newTile.tiles[dir];
            if (nextTile)
            {
                if (nextTile.isPlatform || nextTile.GetPlayerOnTile() != null)
                {
                    WallSlam();
                }
                else
                {
                    newTile = nextTile;
                }
            }
            else
            {
                OffScreen((dir + 4) % 8);
            }
        }

        if(newTile != currentTile)
        {
            currentTile?.OnObjectLeave(gameObject);
            newTile?.OnObjectEnter(gameObject);
        }
    }
    
    // Handle getting Knocked Off Screen
    public void OffScreen(int dir)
    {
        KO();
    }

    // Player got slammed into a wall and will take small damge
    public void WallSlam()
    {
        InflictDamage(KnockBackDamage);
    }

    public void InflictDamage(float damage)
    {
        curHP -= damage;
        UpdatePercent(Percent * 100f);

        if(curHP <= 0)
        {
            curHP = 0;
            KO();
        }
    }

    public void UpdatePercent(float newPercent)
    {
        GameUIManager.Instance.portraitDictionary[assetName].percent.text = Mathf.RoundToInt(newPercent).ToString() + "%";
    }

    public void StartTurn()
    {
        defending = false;
    }

    public void Defend()
    {
        defending = true;
    }


    // Handle Removing Player
    public void KO()
    {
        KOd = true;
        gameObject.GetComponent<Renderer>().enabled = false;
        currentTile?.OnObjectLeave(gameObject);
    }
}

public enum Team
{
    Dino,
    Robot
}