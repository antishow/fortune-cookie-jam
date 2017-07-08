using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    public static RecipeBook instance;
    public static List<DefaultRecipe> recipeList;
    public static bool pagesDirty;
    public int pageNumber = 0;
    public Text pageOne;
    public Text pageTwo;
    public void Start(){
        pageOne.text = "";
        pageTwo.text = "";
    }

    public void Update(){
        if(RecipeBook.pagesDirty){
            if(pageNumber < recipeList.Count && recipeList[pageNumber] != null){
                pageOne.text = recipeList[pageNumber].RecipieDescription();
            } else {
                pageOne.text = "";
            }
            
            if(pageNumber+1 < recipeList.Count && recipeList[pageNumber+1] != null){
                pageTwo.text = recipeList[pageNumber+1].RecipieDescription();
            } else {
                pageTwo.text = "";
            }
            RecipeBook.pagesDirty = false;
        }
    }

    public static void AddNewRecipie(){
        if(recipeList ==  null){
            recipeList = new List<DefaultRecipe>();
        }
        recipeList.Add(RecipeGenerator.CreateRandomDefaultRecipe());
        pagesDirty = true;
    }

    public static DefaultRecipe GetRandomRecipe(){
        return recipeList[Random.Range(0, recipeList.Count+1)];
    }

    public void AddRecipe(){
        RecipeBook.AddNewRecipie();
    }

    public void NextPage(){
        if(pageNumber + 2 < recipeList.Count){
            pageNumber += 2;
            RecipeBook.pagesDirty = true;
        }
    }

    public void PreviousPage(){
        if(pageNumber - 2 >= 0){
            pageNumber -= 2;
            RecipeBook.pagesDirty = true;
        }
    }
}