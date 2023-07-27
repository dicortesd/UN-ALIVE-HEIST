using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)){
            //Debug.Log("key...");
            if(isPaused){
                Resume();
            }else{
                Pause();
            }
        }
   }
   public void ToMainMenu(){
    //Regresa al menú principal
    Resume();
    SceneManager.LoadScene(0);
   }
    public void Resume(){
        //Debug.Log("Resuming...");
    //se desactiva el menú de pausa, se reanuda el tiempo y se pone isPaused en falso
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    isPaused = false;
   }
    public void Pause(){
        //Debug.Log("pausing...");
    //Lo mismo de Resume() pero al revés
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    isPaused = true;
   }
}
