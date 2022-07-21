using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] LevelManager levelManager;
    [SerializeField] PlayerController playerController;

    [Header("UI Elements")]
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas youWinCanvas;
    [SerializeField] Slider levelProgressSlider;
    [SerializeField] TextMeshProUGUI gameOverScore, youWinScore, youWinScoreCalc, scoreText, currentLevelText, nextLevelText;

    [Header("FX Elements")]
    [SerializeField] AudioClip victorySFX;
    [SerializeField] AudioClip failSFX;

    void Start()
    {
        currentLevelText.text = levelManager.CurrentLevel().ToString();
        nextLevelText.text = (levelManager.CurrentLevel() + 1).ToString();
    }

    void Update()
    {
        if (playerController.isGameActive)
        {
            scoreText.text = scoreKeeper.GetScore().ToString("00000");
            levelProgressSlider.value = 1 - levelManager.LevelProgress();
        }
    }

    public void ProcessGameOver()
    {
        scoreText.enabled = false;
        levelProgressSlider.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);
        gameOverScore.text = $"score: {scoreKeeper.GetLastScore()}";
        scoreKeeper.ResetLastScore();
        AudioSource.PlayClipAtPoint(failSFX, scoreKeeper.transform.position, 1f);
    }

    public void ProcessYouWin()
    {
        scoreText.enabled = false;
        levelProgressSlider.gameObject.SetActive(false);
        youWinCanvas.gameObject.SetActive(true);
        youWinScore.text = $"score: {scoreKeeper.GetLastScore()}";
        youWinScoreCalc.text = $"{scoreKeeper._score} x {scoreKeeper._prizeMultiplier}";
        AudioSource.PlayClipAtPoint(victorySFX, scoreKeeper.transform.position, 1f);
    }

    public void ProcessNewScene()
    {
        gameOverCanvas.gameObject.SetActive(false);
        youWinCanvas.gameObject.SetActive(false);
        levelProgressSlider.gameObject.SetActive(true);
        scoreText.enabled = true;
        Time.timeScale = 1;
    }
}
