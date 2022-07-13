using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingCylinder : MonoBehaviour
{
    RidingCylinderManager _manager;

    bool _filled;
    float _value;
    int _cyclinderCount;

    void Start()
    {
        _manager = RidingCylinderManager.Current;
        _cyclinderCount = RidingCylinderManager.Current.cylinders.Count;
    }

    public void IncrementCylinderVolume(float value)
    {
        _value += value;

        if (_value > 1)
        {
            float surplusValue = _value - 1;

            _manager.CreateCyclinder(surplusValue);

            AdjustFilledCylinder();
        }
        else if (_value < 0)
        {
            _manager.DestroyCylinder(this);
        }
        else
        {
            AdjustUnfilledCyclinder();
        }
    }

    void AdjustFilledCylinder()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,
                                    -0.5f * (_cyclinderCount - 1) - 0.25f,
                                    transform.localPosition.z);
        transform.localScale = new Vector3(0.5f, transform.localScale.y, 0.5f);
    }

    void AdjustUnfilledCyclinder()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,
                                    -0.5f * (_cyclinderCount - 1) - 0.25f * _value,
                                    transform.localPosition.z);
        transform.localScale = new Vector3(0.5f * _value, transform.localScale.y, 0.5f * _value);
    }
}
