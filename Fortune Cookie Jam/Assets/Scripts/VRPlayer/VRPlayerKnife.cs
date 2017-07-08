using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class VRPlayerKnife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Ingredient i = other.GetComponent<Ingredient>();

        if (i == null)
        {
            i = other.GetComponentInParent<Ingredient>();
        }

        if (i == null)
            return;

        NVRInteractableItem nvrii = i.GetComponent<NVRInteractableItem>();

        if (nvrii == null)
            return;

        if (nvrii.AttachedHand != null || i.data.type == IngredientType.BURGER_BUN || i.data.type == IngredientType.BURGER_PATTY)
            return;

        i.data.isChopped = true;
        i.transform.GetChild(0).gameObject.SetActive(false);
        i.transform.GetChild(1).gameObject.SetActive(true);
    }
}