using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DefaultRecipe{
    public string name;
    public CoreRecipe parentRecipe;
    public List<OrderIngredient> requiredIngredients; //Bun, Patty, Letuice, Tomato

    public string RecipieDescription(){
        string fullRecipe = name + "\n";
        foreach (IngredientType type in IngredientType.GetValues(typeof(IngredientType)))
        {
            bool hasIngredient = false;
            int ingredientCount = 0;
            foreach(OrderIngredient ord in requiredIngredients){
                if(ord.type == type){
                    hasIngredient = true;
                    ingredientCount++;
                }
            }
            if(hasIngredient){
                fullRecipe += ingredientCount + " - " + RecipeGenerator.nameList.ingredientNames[type] + "\n";
            }
        }
        return fullRecipe;
    }
}


