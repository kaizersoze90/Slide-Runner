using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RidingCylinderManager : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] GameObject ridingCylinderPrefab;
    [SerializeField] float cylinderIncrementValue;

    [Header("Other Settings")]
    [SerializeField] UnityEvent processGameOver;
    [SerializeField] UnityEvent processYouWin;
    [SerializeField] AudioClip pickupSFX;

    [HideInInspector] public List<RidingCylinder> cylinders;
    public static RidingCylinderManager Current;

    PlayerController _playerController;
    bool _isFinishPassed;

    void Awake()
    {
        Current = this;
        _playerController = GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cylinder"))
        {
            ManageCylinderVolume(cylinderIncrementValue);
            AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("FinishLine"))
        {
            _isFinishPassed = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            ManageCylinderVolume(-Time.fixedDeltaTime);
        }
    }

    public void ManageCylinderVolume(float value)
    {
        if (cylinders.Count == 0)
        {
            if (value > 0)
            {
                CreateCyclinder(value);
            }
            else
            {
                if (_isFinishPassed)
                {
                    _playerController.canMove = false;
                    processYouWin.Invoke();
                }
                else
                {
                    Time.timeScale = 0;
                    processGameOver.Invoke();
                }
            }
        }
        else
        {
            cylinders[cylinders.Count - 1].AdjustCylinderVolume(value);
        }
    }

    public void CreateCyclinder(float value)
    {
        RidingCylinder createdCyclinder = Instantiate(ridingCylinderPrefab, transform)
                                          .GetComponent<RidingCylinder>();
        cylinders.Add(createdCyclinder);
        createdCyclinder.AdjustCylinderVolume(value);
    }

    public void DestroyCylinder(RidingCylinder cylinder)
    {
        cylinders.Remove(cylinder);
        Destroy(cylinder.gameObject);
    }
}
