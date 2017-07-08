using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (i.transform.GetChild(0).GetComponent<NewtonVR.NVRInteractableItem>().AttachedHand != null || i.data.type != IngredientType.BURGER_PATTY)
            return;

        i.data.isChopped = true;
        i.transform.GetChild(0).gameObject.SetActive(false);
        i.transform.GetChild(1).gameObject.SetActive(true);
    }
}
