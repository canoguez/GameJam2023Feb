using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : Singleton<GameUIManager>
{
    [SerializeField]
    private List<GameObject> portraitList;

    public Dictionary<string, PlayerStatus> portraitDictionary;

    private void Awake()
    {
        base.Awake();

        portraitDictionary = new Dictionary<string, PlayerStatus>();

        foreach(GameObject go in portraitList)
        {
            PlayerStatus status = go.AddComponent<PlayerStatus>();
            status.Instantiate(go.GetComponentInChildren<Image>(), go.GetComponentInChildren<Text>(), go.name);
            portraitDictionary[status.assetName] = status;
        }
    }
}

public class PlayerStatus : MonoBehaviour
{
    public Image portrait;
    public string assetName;
    public Text percent;

    public void Instantiate(Image _image, Text _text, string _name)
    {
        portrait = _image;
        percent = _text;
        assetName = _name.Replace("Portrait", "");

        UpdatePortrait(PortraitState.Idle);
    }

    public void UpdatePortrait(PortraitState state)
    {
        string imagePath = "Portraits/" + assetName + "Portrait" + state.ToString();
        Sprite charTex = Resources.Load<Sprite>(imagePath);

        if (charTex)
            portrait.sprite = charTex;
    }
}

public enum PortraitState
{
    Idle,
    Hit,
    Attack
}
