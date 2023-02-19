using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanel : MonoBehaviour
{
    public GameObject bgCam;

    private void Start()
    {
        bgCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler.Instance.SetCurPlayer(PlayerEnum.P1 | PlayerEnum.P2);

        if (InputHandler.Instance.Confirm())
        {
            SceneManager.LoadScene(1);
        }

        if (InputHandler.Instance.Cancel())
        {
            SceneManager.LoadScene(0);
        }
    }
}
