using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameMenus : MonoBehaviour
{
    public static bool isPaused;
    public static bool couldPause;
    [SerializeField] Score score;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] Health NPC;
    [SerializeField] float timeToDisplayGameOver;


    void Start()
    {
        isPaused = false;
        couldPause = true;
        Time.timeScale = 1f;
        AudioManager.instance.PlaySound(SoundName.GameMusic);
    }

    private void OnEnable()
    {
        NPC.OnDead += OnNPCDead;
    }

    private void OnDisable()
    {
        NPC.OnDead -= OnNPCDead;
    }

    void Update()
    {
        if (couldPause)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                //Debug.Log("key...");
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            /*
            if (Input.GetKeyDown(KeyCode.Delete))
            { //temporal en lo que se define como se pierde
                GameOver();
            }
            */
        }
    }

    private void OnNPCDead()
    {
        Invoke(nameof(GameOver), timeToDisplayGameOver);
        score.stop();
    }


    public void ToMainMenu()
    {
        AudioManager.instance.StopSound(SoundName.GameMusic);
        //Regresa al menú principal
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Resume()
    {
        //Debug.Log("Resuming...");
        //se desactiva el menú de pausa, se reanuda el tiempo y se pone isPaused en falso
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    protected void Pause()
    {
        //Debug.Log("pausing...");
        //Lo mismo de Resume() pero al revés
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void GameOver()
    {
        //Muestra la pantalla de game over y se detiene el juego
        Time.timeScale = 0f;
        couldPause = false;
        gameOverUI.SetActive(true);
    }

}
