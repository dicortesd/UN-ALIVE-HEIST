using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
   public void PlayGame(){
    //pendiente de añadir la siguiente escena colocada una de ejemplo
    SceneManager.LoadScene(1);
   }
   public void QuitGame(){
    Debug.Log("Quitting game...");
    //Saca del juego comentario en consola extra para verificar que funciona
    Application.Quit();
   }
}
