using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Progress Settings")]
    [SerializeField] Transform finishLine;
    [SerializeField] RidingCylinderManager player;

    float _maxDistance;
    float _distance;
    int _currentLevel;

    void Awake()
    {
        _maxDistance = finishLine.position.z - player.transform.position.z;
        _currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        _distance = finishLine.position.z - player.transform.position.z;
    }

    public float LevelProgress()
    {
        return _distance / _maxDistance;
    }

    public int CurrentLevel()
    {
        return _currentLevel;
    }
}
