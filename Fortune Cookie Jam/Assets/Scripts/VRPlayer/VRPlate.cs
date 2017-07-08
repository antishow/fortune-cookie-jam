using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;
using TMPro;

public class VRPlate : MonoBehaviour
{
    private List<GameObject> buns = new List<GameObject>();

    private List<GameObject> nonBuns = new List<GameObject>();

    [SerializeField]
    private TextMeshPro text;

    private List<Ingredient> ing = new List<Ingredient>();

    private Recipe myRecipe = new Recipe();

    private void OnTriggerEnter(Collider other)
    {
        Ingredient i = other.GetComponent<Ingredient>();

        if (i == null)
        {
            i = other.GetComponentInParent<Ingredient>();
        }

        if (i == null)
            return;

        bool added = false;

        if (i.data.type == IngredientType.BURGER_BUN)
        {
            buns.Add(i.gameObject);
            added = true;
        }
        else if (i.data.type == IngredientType.BURGER_PATTY && i.data.isTenderized && i.data.isCooked)
        {
            nonBuns.Add(i.gameObject);
            ing.Add(i);
            added = true;
        }
        else if((i.data.type != IngredientType.BURGER_BUN && i.data.type != IngredientType.BURGER_PATTY) && i.data.isChopped)
        {
            nonBuns.Add(i.gameObject);
            ing.Add(i);
            added = true;
        }

        if (!added)
            return;

        NVRInteractableItem[] items = i.GetComponentsInChildren<NVRInteractableItem>();

        foreach (NVRInteractableItem ii in items)
        {
            if (ii.AttachedHand != null)
            {
                ii.AttachedHand.EndInteraction(ii);
            }

            Destroy(ii);
        }

        Collider[] cols = i.GetComponentsInChildren<Collider>();

        foreach(Collider c in cols)
        {
            Destroy(c);
        }

        i.gameObject.transform.parent = transform;
        i.gameObject.transform.localRotation = Quaternion.identity;

        Destroy(i.GetComponent<Rigidbody>());

        int count = 0;

        if(buns.Count > 0)
        {
            buns[0].transform.localPosition = transform.localPosition + transform.up * (.025f * count++);
        }

        foreach(GameObject nb in nonBuns)
        {
            nb.transform.localPosition = transform.localPosition + transform.up * (.025f * count++);
        }

        if(buns.Count > 1)
        {
            buns[1].transform.localPosition = transform.localPosition + transform.up * (.025f * count++);
        }

        string recip = "";

        List<IngredientData> ingData = new List<IngredientData>();

        for (int j = 0; j < ing.Count; j++)
        {
            ingData.Add(ing[j].data);

            if (string.IsNullOrEmpty(recip))
            {
                recip = ing[j].name;
            }
            else
            {
                recip += "\n" + ing[j].name;
            }
        }

        if(buns.Count > 1)
        {
            IngredientData ig = new IngredientData();

            ig.name = "Bun";
            ig.type = IngredientType.BURGER_BUN;

            ingData.Add(ig);
        }

        myRecipe.ingredients = ingData;

        text.text = recip;

        text.gameObject.transform.localPosition = transform.localPosition + transform.up * (.025f * count++);

        Destroy(i);
    }
}