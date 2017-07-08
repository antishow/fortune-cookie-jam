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

    private Recipe myRecipe = new Recipe();

    public void OnTriggerEnter(Collider other)
    {
        Ingredient i = other.GetComponent<Ingredient>();

        if (i == null)
        {
            i = other.GetComponentInParent<Ingredient>();
        }

        if (i == null)
            return;

        if (i.data.type == IngredientType.BURGER_PATTY)
        {
            return;
        }

        if (i.data.type != IngredientType.BURGER_BUN && !i.data.isChopped)
        {
            return;
        }

        NVRInteractableItem nvrii = i.GetComponent<NVRInteractableItem>();

        if (nvrii.AttachedHand)
        {
            nvrii.AttachedHand.EndInteraction(nvrii);
        }

        nvrii.Rigidbody.isKinematic = true;

        nvrii.transform.parent = unfinishedSalad.transform;

        ingredients.Add(nvrii);
    }

    private void Update()
    {
        for (int i = ingredients.Count - 1; i >= 0; i--)
        {
            if (ingredients[i].AttachedHand != null)
            {
                ingredients[i].transform.parent = null;

                ingredients.RemoveAt(i);
            }
        }

        if (myNVRII.AttachedHand != null)
        {
            float dist = Vector3.Distance(transform.position, lastPos) / (Time.deltaTime * 100);

            shakeAmount += dist;

            if (shakeAmount > 10)
            {
                NVRHand hand = myNVRII.AttachedHand;
                hand.EndInteraction(myNVRII);

                finishedSalad.SetActive(true);
                unfinishedSalad.SetActive(false);

                string recip = "";

                List<IngredientData> ing = new List<IngredientData>();

                for (int j = 0; j < ingredients.Count; j++)
                {
                    IngredientData i = ingredients[j].GetComponent<Ingredient>().data;
                    ing.Add(i);

                    if (string.IsNullOrEmpty(recip))
                    {
                        recip = i.name;
                    }
                    else
                    {
                        recip += "\n" + i.name;
                    }
                }

                myRecipe.ingredients = ing;

                text.text = recip;

                GetComponent<NVRInteractableItem>().UpdateColliders();
            }
        }

        lastPos = transform.position;
    }
}