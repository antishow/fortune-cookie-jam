using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    public List<DefaultRecipe> recipeList;
    public int pageSet;
    public Text pageOne;
    public Text pageTwo;
    private bool _pagesDirty;

    void Awake(){

    }

    public void AddNewRecipie(){
        recipeList.Add(RecipeGenerator.CreateRandomDefaultRecipe());
        _pagesDirty = true;
    }


    public void Update(){
        if(_pagesDirty){
            if(pageSet < recipeList.Count && recipeList[pageSet] != null){
                pageOne.text = recipeList[pageSet].RecipieDescription();
            }
            if(pageSet+1 < recipeList.Count && recipeList[pageSet+1] != null){
                pageTwo.text = recipeList[pageSet+1].RecipieDescription();
            }
        }
    }
}