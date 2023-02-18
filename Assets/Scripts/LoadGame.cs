using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadGame : MonoBehaviour
{
        public static void SwitchScene() {
            Debug.Log("Starting Game.");
            SceneManager.LoadScene(1);
        }
}
