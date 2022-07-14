using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int scoreValue;

    int _score;

    void Update()
    {
        scoreText.text = _score.ToString("00000");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cylinder"))
        {
            _score += scoreValue;
        }
    }

    public int GetScore()
    {
        return _score;
    }
}
