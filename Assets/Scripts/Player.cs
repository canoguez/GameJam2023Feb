using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string assetName = "";
    public float percent = 0;
    public int knockBack = 2;
    public bool defending = false;

    public void UpdatePercent(float newPercent)
    {
        percent = newPercent;
        GameUIManager.Instance.portraitDictionary[assetName].percent.text = Mathf.RoundToInt(percent).ToString() + "%";
    }

    public void Defend()
    {
        defending = true;
    }
}
