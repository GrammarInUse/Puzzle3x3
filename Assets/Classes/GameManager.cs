using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Quit(){
        Debug.Log("Quitting...");
        Application.Quit();
    }

    public void Reload(){
        Debug.Log("Reloading...");
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
