using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerGrill : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Grill Hit: " + collision.transform.name);
    }
}