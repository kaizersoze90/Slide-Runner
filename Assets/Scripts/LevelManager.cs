using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Progress Settings")]
    [SerializeField] Transform finishLine;
    [SerializeField] RidingCylinderManager player;

    float _maxDistance;
    float _distance;


    void Start()
    {
        _maxDistance = finishLine.position.z - player.transform.position.z;
    }

    void Update()
    {
        _distance = finishLine.position.z - player.transform.position.z;
    }

    public float LevelProgress()
    {
        return _distance / _maxDistance;
    }
}
