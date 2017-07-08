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
    private NVRInteractableItem currentIntItem;

    private bool growing = false;
    private Vector3 goalSize;

    [SerializeField]
    private bool handSpawner = true;

    private void OnEnable()
    {
        if (currentPrefab != null && growing)
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
            if (currentPrefab == null)
            {
                currentPrefab = Instantiate(spawningPrefab, transform.position, transform.rotation);
                currentPrefab.transform.parent = transform;
                goalSize = spawningPrefab.transform.localScale;

                currentPrefab.transform.localScale = Vector3.zero;

                StartCoroutine(GrowObject());
            }
            else if ((currentPrefab.transform.position - transform.position).sqrMagnitude > .5f)
            {
                currentPrefab.transform.parent = null;
                currentIntItem = null;

                currentPrefab = Instantiate(spawningPrefab, transform.position, transform.rotation);
                currentPrefab.transform.parent = transform;
                goalSize = spawningPrefab.transform.localScale;

                currentPrefab.transform.localScale = Vector3.zero;

                StartCoroutine(GrowObject());
            }
            else
            {
                if (handSpawner && parentHand.Inputs[NVRButtons.Trigger].PressDown && growing == false)
                {
                    Rigidbody rb = currentPrefab.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    currentPrefab.AddComponent<NVRInteractableItem>();
                    currentPrefab.transform.parent = null;
                    currentPrefab = null;
                }
                else if (!handSpawner)
                {
                    if (currentIntItem == null)
                    {
                        currentIntItem = currentPrefab.GetComponent<NVRInteractableItem>();
                    }

                    if (currentIntItem != null)
                    {
                        if (currentIntItem.AttachedHand != null)
                        {
                            currentPrefab.transform.parent = null;
                            currentIntItem = null;
                        }
                    }
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

        Rigidbody rb = currentPrefab.GetComponent<Rigidbody>();

        if(rb != null)
        {
            rb.isKinematic = true;
        }

        while (curTime < totalTime)
        {
            yield return new WaitForEndOfFrame();

            curTime += Time.deltaTime;

            if (curTime > totalTime)
            {
                curTime = totalTime;
            }

            currentPrefab.transform.localScale = Vector3.Lerp(Vector3.zero, goalSize, curTime / totalTime);
        }

        currentPrefab.transform.localPosition = Vector3.zero;

        if (rb != null && !handSpawner)
        {
            rb.isKinematic = false;
        }

        col.enabled = true;
        growing = false;
    }
}