using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [Tooltip("Enter score value per cylinder pickup")]
    [SerializeField] int scoreValue;

    [HideInInspector] public int _score;
    [HideInInspector] public int _prizeMultiplier = 1;

    PlayerController _playerController;

    int _lastScore;
    int _currentScore;

    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        _lastScore = PlayerPrefs.GetInt("LastScore");
        _score = _lastScore;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cylinder"))
        {
            _score += scoreValue;
        }
        else if (other.CompareTag("Prize"))
        {
            _prizeMultiplier = other.GetComponent<PrizeMultiplier>().value;
        }
    }

    public int GetScore()
    {
        _currentScore = _score * _prizeMultiplier;
        return _currentScore;
    }

    public int GetLastScore()
    {
        _lastScore = _score * _prizeMultiplier;
        PlayerPrefs.SetInt("LastScore", _lastScore);
        return _lastScore;
    }

    public void ResetLastScore()
    {
        PlayerPrefs.SetInt("LastScore", 0);
    }
}
