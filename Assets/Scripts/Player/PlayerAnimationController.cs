using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator _animator;
    PlayerController _playerController;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        //Adjusts running animation to be direct or inverse according sliding
        if (RidingCylinderManager.Current.cylinders.Count == 0)
        {
            _animator.SetFloat("Direction", 1f);
        }
        else
        {
            _animator.SetFloat("Direction", -1f);
        }

        //Play dancing animation when player wins
        if (!_playerController.isGameActive)
        {
            _animator.SetBool("isDancing", true);
        }
    }
}
