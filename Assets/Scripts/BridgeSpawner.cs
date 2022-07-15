using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSpawner : MonoBehaviour
{
    [SerializeField] BoxCollider invisibleBridge;

    public Transform startReference, endReference;

    void Start()
    {
        //Calculate distance and direction between start and end position of bridge
        Vector3 direction = endReference.position - startReference.position;
        float distance = direction.magnitude;
        direction = direction.normalized;

        //Apply calculated distance and direction to bridge collider
        invisibleBridge.transform.forward = direction;
        invisibleBridge.size = new Vector3(invisibleBridge.size.x, invisibleBridge.size.y, distance);

        //Adjust bridge collider position
        invisibleBridge.transform.position = startReference.position + (direction * distance / 2)
                                                + (new Vector3(0, -direction.z, direction.y)
                                                * invisibleBridge.size.y / 2);
    }

}
