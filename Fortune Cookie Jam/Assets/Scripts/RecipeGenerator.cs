using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AllCoreRecipies{
    public List<CoreRecipe> coreRecipes;
}


public class RecipeGenerator : MonoBehaviour
{
    public static List<CoreRecipe> coreRecipes;
    public static RecipeGenerator instance;

    public RecipeGenerator(){
        if(RecipeGenerator.instance != null){
            Destroy(this.gameObject);
        } else {
            RecipeGenerator.instance = this;
            //Load core recipes
        }
    }

    public static Recipe CreateRandomRecipe(){
        //Choose the random ingredients
        Recipe response = new Recipe();

        //Choose a core recipe
        int recipeChosen = Random.Range(0, coreRecipes.Count);
        CoreRecipe core = coreRecipes[recipeChosen];
        foreach(IngredientData type in core.ingredients){
            float isAddedPercent = Random.Range(0.0f, 100.0f);
            int maxInRecipe = Random.Range(1, type.maxInRecipe);
            if(type.inclusionChance > isAddedPercent){
                for(int i = 0; i <= maxInRecipe; i++){
                    response.ingredients.Add(type);
                }
            }
        }

        return response;
    }
}