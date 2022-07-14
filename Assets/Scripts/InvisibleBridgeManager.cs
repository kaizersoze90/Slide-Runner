using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBridgeManager : MonoBehaviour
{
    [SerializeField] GameObject bridgePrefab;
    [SerializeField] float paintTimer;
    [SerializeField] float cylinderDecrementValue;

    BridgeSpawner _bridgeSpawner;
    bool _isPaintingBridge;


    void Update()
    {
        if (_isPaintingBridge)
        {
            StartCoroutine(nameof(PaintBridge));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartBridge"))
        {
            StartPaintingBridge(other.GetComponentInParent<BridgeSpawner>());
        }
        else if (other.CompareTag("EndBridge"))
        {
            StopPaintingBridge();
        }
    }

    IEnumerator PaintBridge()
    {
        while (true)
        {
            RidingCylinderManager.Current.ApplyCylinderVolume(cylinderDecrementValue);

            GameObject paintedBridge = Instantiate(bridgePrefab);

            Vector3 direction = _bridgeSpawner.endReference.position - _bridgeSpawner.startReference.position;
            float distance = direction.magnitude;
            direction = direction.normalized;
            paintedBridge.transform.forward = direction;

            float characterDistance = transform.position.z - _bridgeSpawner.startReference.position.z;
            characterDistance = Mathf.Clamp(characterDistance, 0, distance);



            yield return new WaitForSeconds(paintTimer);
        }
    }

    public void StartPaintingBridge(BridgeSpawner spawner)
    {
        _bridgeSpawner = spawner;
        _isPaintingBridge = true;
    }

    public void StopPaintingBridge()
    {
        _isPaintingBridge = false;
    }
}
