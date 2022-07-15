using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] LevelManager levelManager;

    [Header("UI Elements")]
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas youWinCanvas;
    [SerializeField] Slider levelProgressSlider;
    [SerializeField] TextMeshProUGUI gameOverScore, youWinScore, youWinScoreCalc, scoreText;

    [Header("FX Elements")]
    [SerializeField] AudioClip victorySFX;
    [SerializeField] AudioClip failSFX;

    void Update()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("00000");
        levelProgressSlider.value = 1 - levelManager.LevelProgress();
    }

    public void ProcessGameOver()
    {
        scoreText.enabled = false;
        levelProgressSlider.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);
        gameOverScore.text = $"score: {scoreKeeper.GetScore()}";
        AudioSource.PlayClipAtPoint(failSFX, scoreKeeper.transform.position, 1f);
    }

    public void ProcessYouWin()
    {
        scoreText.enabled = false;
        levelProgressSlider.gameObject.SetActive(false);
        youWinCanvas.gameObject.SetActive(true);
        youWinScore.text = $"score: {scoreKeeper.GetScore()}";
        youWinScoreCalc.text = $"{scoreKeeper._score} x {scoreKeeper._prizeMultiplier}";
        AudioSource.PlayClipAtPoint(victorySFX, scoreKeeper.transform.position, 1f);
    }

    public void ReloadScene()
    {
        gameOverCanvas.gameObject.SetActive(false);
        youWinCanvas.gameObject.SetActive(false);
        levelProgressSlider.gameObject.SetActive(true);
        scoreText.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
