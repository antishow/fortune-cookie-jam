using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class VRToolSpawner : MonoBehaviour
{
    private NVRHand parentHand;

    [SerializeField]
    private GameObject spawningPrefab;

    private GameObject currentPrefab;

    private bool growing = false;
    private Vector3 goalSize;

    [SerializeField]
    private bool handSpawner = true;

    private void OnEnable()
    {
        if(currentPrefab != null && growing)
        {
            StartCoroutine(GrowObject());
        }
    }

    void Update()
    {
        if (handSpawner && parentHand == null)
        {
            parentHand = GetComponentInParent<NVRHand>();
        }
        else
        {
            if (currentPrefab == null || (currentPrefab.transform.position - transform.position).sqrMagnitude > .75f)
            {
                currentPrefab = Instantiate(spawningPrefab, transform.position, transform.rotation);

                currentPrefab.transform.parent = transform;

                goalSize = currentPrefab.transform.localScale;
                currentPrefab.transform.localScale = Vector3.zero;

                StartCoroutine(GrowObject());
            }
            else
            {
                if (handSpawner && parentHand.Inputs[NVRButtons.Trigger].PressDown && growing == false)
                {
                    currentPrefab.AddComponent<Rigidbody>();
                    currentPrefab.AddComponent<NVRInteractableItem>();
                    currentPrefab.transform.parent = null;
                    currentPrefab = null;
                }
            }
        }
    }

    IEnumerator GrowObject()
    {
        growing = true;

        float curTime = 0;
        float totalTime = .25f;

        Collider col = currentPrefab.GetComponentInChildren<Collider>();
        col.enabled = false;

        while(curTime < totalTime)
        {
            yield return new WaitForEndOfFrame();

            curTime += Time.deltaTime;

            if(curTime > totalTime)
            {
                curTime = totalTime;
            }

            currentPrefab.transform.localScale = Vector3.Lerp(Vector3.zero, goalSize, curTime / totalTime);
        }

        col.enabled = true;
        growing = false;
    }
}