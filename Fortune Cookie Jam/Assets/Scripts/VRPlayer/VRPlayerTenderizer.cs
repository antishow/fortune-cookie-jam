using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerTenderizer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tenderizer Hit: " + other.name);
    }
}
