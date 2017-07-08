using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VRSoupBowl : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro text;

    private Recipe myRecipe;

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(text.text))
        {
            return;
        }

        SoupPot sp = other.GetComponent<SoupPot>();

        if (sp != null)
        {
            myRecipe = sp.Plate();

            string recip = "";

            for (int i = 0; i < myRecipe.ingredients.Count; i++)
            {
                if (string.IsNullOrEmpty(recip))
                {
                    recip = myRecipe.ingredients[i].name;
                }
                else
                {
                    recip += "\n" + myRecipe.ingredients[i].name;
                }
            }

            text.text = recip;
        }
    }
}
