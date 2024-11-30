using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public bool gameIsPaused = false;
    public bool gameIsOver = false;
    public GameObject Pausa;
    public GameObject GameOver;

    private void Start()
    {
        GameOver.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsOver)
        {
            PauseSwitch();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void PauseSwitch()
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Pause()
    {
        Pausa.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    void Resume()
    {
        Pausa.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public IEnumerator EndGameAppears()
    {
        yield return new WaitForSeconds(1.5f);
        GameOver.SetActive(true);
        ScoreManager.instance.HighscoreUpdate();
        GameOver.GetComponent<Menu>().SetScoreText();
        GameOver.GetComponent<Menu>().SetWinText();
        gameIsOver = true;
        Time.timeScale = 0f;
    }

    public IEnumerator GameOverAppears()
    {
        yield return new WaitForSeconds(1.5f);
        GameOver.SetActive(true);
        ScoreManager.instance.HighscoreUpdate();
        GameOver.GetComponent<Menu>().SetScoreText();
        gameIsOver = true;
        Time.timeScale = 0f;
    }
}
