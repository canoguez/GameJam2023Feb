using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> portraitList;

    private Dictionary<string, PlayerStatus> portraitDictionary;

    private void Awake()
    {
        portraitDictionary = new Dictionary<string, PlayerStatus>();

        foreach(GameObject go in portraitList)
        {
            PlayerStatus status = go.AddComponent<PlayerStatus>();
            status.Instantiate(go.GetComponentInChildren<Image>(), go.GetComponentInChildren<Text>());
            status.assetName = go.name;
            portraitDictionary[status.assetName] = status;
        }
    }
}

class PlayerStatus : MonoBehaviour
{
    public Image portrait;
    public string assetName;
    public Text percent;

    public void Instantiate(Image _image, Text _text)
    {
        portrait = _image;
        percent = _text;

        UpdatePortrait(PortraitState.Idle);
    }

    public void UpdatePortrait(PortraitState state)
    {
        string imagePath = "Portraits/" + name + state.ToString();
        Debug.Log(imagePath);
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
