using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingCylinderManager : MonoBehaviour
{
    [SerializeField] GameObject ridingCylinderPrefab;
    [SerializeField] float cylinderIncrementValue;
    [SerializeField] float cylinderDecrementValue;

    public static RidingCylinderManager Current;
    public List<RidingCylinder> cylinders;

    void Start()
    {
        Current = this;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cylinder"))
        {
            ApplyCylinderVolume(cylinderIncrementValue);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            ApplyCylinderVolume(cylinderDecrementValue);
        }
    }

    public void ApplyCylinderVolume(float value)
    {
        if (cylinders.Count == 0)
        {
            if (value > 0)
            {
                CreateCyclinder(value);
            }
            else
            {
                //Gameover
            }
        }
        else
        {
            cylinders[cylinders.Count - 1].IncrementCylinderVolume(value);
        }


    }

    public void CreateCyclinder(float value)
    {
        RidingCylinder createdCyclinder = Instantiate(ridingCylinderPrefab, transform)
                                          .GetComponent<RidingCylinder>();
        cylinders.Add(createdCyclinder);
        createdCyclinder.IncrementCylinderVolume(value);
    }

    public void DestroyCylinder(RidingCylinder cylinder)
    {
        cylinders.Remove(cylinder);
        Destroy(cylinder.gameObject);
    }
}
