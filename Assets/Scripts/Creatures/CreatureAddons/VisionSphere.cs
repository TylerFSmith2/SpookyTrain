using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSphere : MonoBehaviour
{
    public bool canSeePlayer;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSeePlayer = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSeePlayer = false;
        }
    }

    public bool GetCanSeePlayer()
    {
        return canSeePlayer;
    }
}
