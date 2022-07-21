using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBridgeManager : MonoBehaviour
{
    [Header("General Bridge Settings")]
    [SerializeField] GameObject bridgePrefab;
    [SerializeField] float paintTimer;
    [SerializeField] float cylinderDecrementValue;

    BridgeSpawner _bridgeSpawner;
    PlayerController _playerController;

    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
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
        while (_playerController.isGameActive)
        {
            //Reduce riding cylinder volume while on invisible bridge
            RidingCylinderManager.Current.ManageCylinderVolume(cylinderDecrementValue);

            //Instantiate pieces to paint invisible bridge
            GameObject paintedBridge = Instantiate(bridgePrefab);

            //Calculate direction for instantiated pieces
            Vector3 direction = _bridgeSpawner.endReference.position - _bridgeSpawner.startReference.position;
            float distance = direction.magnitude;
            direction = direction.normalized;
            paintedBridge.transform.forward = direction;

            //Calculate how the character far away from the bridge start position (for placing pieces)
            float characterDistance = transform.position.z - _bridgeSpawner.startReference.position.z;
            characterDistance = Mathf.Clamp(characterDistance, 0, distance);

            //Adjust instantiated piece position according above calculated position
            Vector3 paintingPosition = _bridgeSpawner.startReference.position + direction * characterDistance;
            paintingPosition = new Vector3(transform.position.x, -0.35f, paintingPosition.z);
            paintedBridge.transform.position = paintingPosition;

            yield return new WaitForSeconds(paintTimer);
        }
    }
}
