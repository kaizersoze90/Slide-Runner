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
        int lastScore = _score * _prizeMultiplier;
        return lastScore;
    }
}
