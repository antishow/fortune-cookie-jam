using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class VRPlayerSpatula : MonoBehaviour
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

        NVRInteractableItem nvrii = i.transform.GetChild(0).GetComponent<NewtonVR.NVRInteractableItem>();

        if (nvrii == null)
            return;

        if (nvrii.AttachedHand != null || i.data.type != IngredientType.BURGER_PATTY)
            return;

        if(i.data.isTenderized || i.data.isCooked)
        {
            return;
        }

        i.data.isTenderized = true;
        i.transform.GetChild(0).gameObject.SetActive(false);
        i.transform.GetChild(1).gameObject.SetActive(true);
    }
}
