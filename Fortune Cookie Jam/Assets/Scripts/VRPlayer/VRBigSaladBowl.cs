using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;
using TMPro;

public class VRBigSaladBowl : MonoBehaviour
{
    List<NVRInteractableItem> ingredients = new List<NVRInteractableItem>();

    [SerializeField]
    private NVRInteractableItem myNVRII;

    private float shakeAmount = 0;

    private Vector3 lastPos;

    [SerializeField]
    private TextMeshPro text;

    [SerializeField]
    private GameObject finishedSalad;

    [SerializeField]
    private GameObject unfinishedSalad;

    public void OnTriggerEnter(Collider other)
    {
        NVRInteractableItem nvrii = other.GetComponent<NVRInteractableItem>();

        if(nvrii == null)
        {
            nvrii = other.GetComponentInParent<NVRInteractableItem>();
        }

        if (nvrii == null)
            return;

        if(nvrii.AttachedHand)
        {
            nvrii.AttachedHand.EndInteraction(nvrii);
        }

        nvrii.Rigidbody.isKinematic = true;

        nvrii.transform.parent = unfinishedSalad.transform;

        ingredients.Add(nvrii);
    }

    private void Update()
    {
        for(int i = ingredients.Count - 1; i >= 0; i--)
        {
            if(ingredients[i].AttachedHand != null)
            {
                ingredients[i].transform.parent = null;
            }

            ingredients.RemoveAt(i);
        }

        if(myNVRII.AttachedHand != null)
        {
            float dist = Vector3.Distance(transform.position, lastPos) / (Time.deltaTime * 100);

            shakeAmount += dist;

            if(shakeAmount > 10)
            {
                NVRHand hand = myNVRII.AttachedHand;
                hand.EndInteraction(myNVRII);

                finishedSalad.SetActive(true);
                unfinishedSalad.SetActive(false);
                text.text = "Ingredients used here";
            }
        }

        lastPos = transform.position;
    }
}