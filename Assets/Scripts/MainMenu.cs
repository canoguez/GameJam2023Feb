using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {
    public List<Button> ButtonList;
    public int SelectedButton = 0;
    public void Awake(){
        
    }
    public void Update(){
        if(Input.GetKeyDown(KeyCode.Keypad2)){
            SelectedButton++;
            SelectedButton = SelectedButton % ButtonList.Count;
        }else if(Input.GetKeyDown(KeyCode.Keypad5)){
            SelectedButton--;
            if(SelectedButton < 0){
                SelectedButton = ButtonList.Count-1;
            }
        }
        ChangeSelection(SelectedButton);
    }

    public void ChangeSelection(int SelectedButton){
        for(int i = 0; i < ButtonList.Count;i++){
            if(i == SelectedButton){
                ButtonList[i].Select();
            }
        }

    }

}