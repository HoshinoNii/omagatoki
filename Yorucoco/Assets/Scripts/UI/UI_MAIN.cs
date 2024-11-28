using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MAIN : MonoBehaviour
{
    private bool gameEnded = false;

    public GameObject gameOverScreen;

    public TowerBlueprint blueprint;

    [Header("Pause")]
    public GameObject pauseScreen;
    public GameObject pauseButton;


    [Header("References")]
    [HideInInspector]
    public BuildManager buildManager;

    void Start()
    {
        buildManager = GetComponent<BuildManager>();

        if (pauseScreen != null)
            pauseScreen.SetActive(false);

        if(gameOverScreen != null) 
            gameOverScreen.SetActive(false);   
    }

    public void QuitGame() //Quit Button
    {
        Application.Quit();
    }
    public void Credits() //Credits Button
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((int)SceneIndexes.CREDITS);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        pauseButton.SetActive(false);

        if (Blueprint.Instance != null) Blueprint.Instance.OnObjectTaskCompleted();
    }

    public void MainMenu() //Main Menu Button
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((int)SceneIndexes.MAIN_MENU);
    }
    //Game References
    public void LoadLevel(int levelIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelIndex.ToString());
    }

    public void LoadLevel(string levelIndex) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelIndex);
    }


    public void POS()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((int)SceneIndexes.POS);
    }

    public void Level01()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((int)SceneIndexes.LEVEL_1);
    }

    public void Restart() //Restart Button
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume() //Resume Button
    {
        PauseResume(true);

        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
    }
    public void PauseResume(bool Pause = true)
    {
        Time.timeScale = Pause ? 1.0f : 0f;
    }

    

    private void Awake()
    {
        Time.timeScale = 1f;
    }

}
