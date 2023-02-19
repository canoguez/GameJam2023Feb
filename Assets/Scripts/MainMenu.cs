using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour {
    public List<Button> ButtonList;
    public int SelectedButton = 0;
    public void Awake(){
        ChangeSelection(SelectedButton);
    }

    public void Update(){
        if(InputHandler.Instance.Left()){
            SelectedButton++;
            SelectedButton = SelectedButton % ButtonList.Count;
            ChangeSelection(SelectedButton);

        }
        else if(Input.GetKeyDown(KeyCode.Keypad5)){
            SelectedButton--;
            if(SelectedButton < 0){
                SelectedButton = ButtonList.Count-1;
            }
            ChangeSelection(SelectedButton);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            ButtonList[SelectedButton].onClick?.Invoke();
        }
    }

    public void ChangeSelection(int SelectedButton){
        for(int i = 0; i < ButtonList.Count;i++){
            ButtonList[i].interactable = (i == SelectedButton);
        }

    }

    public void CloseGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}