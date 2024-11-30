using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highscoreText;
    [SerializeField] private TMP_Text _titleText;

    [SerializeField] private MenuPausa Canvas;

    [SerializeField] private Button _buttonMainMenu;
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonRestart;
    [SerializeField] private Button _buttonQuit;

    [SerializeField] private GameObject pnlTutorial;
    [SerializeField] private GameObject pnlPrincipal;

    private bool tutorialAtivo = false;

    private void Start()
    {
        
    }

    public void SetScoreText()
    {
        _scoreText.text = "Pontuacao: " + ScoreManager.instance.scoreCount;
        _highscoreText.text = "Pontuacao mais alta: " + PlayerPrefs.GetInt("SavedHighscore");
    }
    public void SetWinText()
    {
        _titleText.text = "Voce Venceu!";
    }

    public void CallResumeGames()
    {
        Canvas.PauseSwitch();
    }
    public void CallNewGames()
    {
        SceneManager.LoadScene("Fase1");
        Time.timeScale = 1f;
    }

    public void CallMainMenu()
    {
        SceneManager.LoadScene("Titulo");
        Time.timeScale = 1f;
    }

    public void CallRestartGames()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void CallSwitchMenu()
    {
        if (!tutorialAtivo)
        {
            tutorialAtivo=true;
            pnlTutorial.SetActive(true);
            pnlPrincipal.SetActive(false);
        }
        else
        {
            tutorialAtivo=false;
            pnlTutorial.SetActive(false);
            pnlPrincipal.SetActive(true);
        }
    }

    public void CallQuitGames()
    {
        Application.Quit();
    }
}
