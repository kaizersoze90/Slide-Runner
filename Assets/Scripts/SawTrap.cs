using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] AudioClip sawSound;

    void Update()
    {
        transform.Rotate(Vector3.left);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(sawSound, other.transform.position);
        }
    }
}
