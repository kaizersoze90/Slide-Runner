using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] TextMeshProUGUI lastScoreText, scoreText;

    public void ProcessGameOver()
    {
        scoreText.enabled = false;
        gameOverCanvas.gameObject.SetActive(true);
        lastScoreText.text = $"score: {scoreKeeper.GetScore()}";
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverCanvas.gameObject.SetActive(false);
        scoreText.enabled = true;
        Time.timeScale = 1;
    }
}
