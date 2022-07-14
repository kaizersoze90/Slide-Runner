using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBridgeManager : MonoBehaviour
{
    [SerializeField] GameObject bridgePrefab;
    [SerializeField] float paintTimer;
    [SerializeField] float cylinderDecrementValue;

    BridgeSpawner _bridgeSpawner;

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

    public void StartPaintingBridge(BridgeSpawner spawner)
    {
        _bridgeSpawner = spawner;

        StartCoroutine(nameof(PaintBridge));
    }

    public void StopPaintingBridge()
    {
        StopCoroutine(nameof(PaintBridge));
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

            Vector3 paintingPosition = _bridgeSpawner.startReference.position + direction * characterDistance;
            paintingPosition = new Vector3(transform.position.x, -0.3f, paintingPosition.z);
            paintedBridge.transform.position = paintingPosition;

            yield return new WaitForSeconds(paintTimer);
        }
    }
}
