using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSpawner : MonoBehaviour
{
    public Transform startReference, endReference;

    [SerializeField] BoxCollider invisibleBridge;

    void Start()
    {
        Vector3 direction = endReference.position - startReference.position;
        float distance = direction.magnitude;
        direction = direction.normalized;

        invisibleBridge.transform.forward = direction;
        invisibleBridge.size = new Vector3(invisibleBridge.size.x, invisibleBridge.size.y, distance);

        invisibleBridge.transform.position = startReference.position + (direction * distance / 2)
                                                + (new Vector3(0, -direction.z, direction.y)
                                                * invisibleBridge.size.y / 2);
    }

}
