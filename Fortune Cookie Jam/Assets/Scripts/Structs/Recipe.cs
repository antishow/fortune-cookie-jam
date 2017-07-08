using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CoreRecipe{
    public string name; //Burger, Soup, Salad
    public List<IngredientData> ingredients;//all possible ingredients
}

public class Recipe
{
    public List<IngredientData> ingredients;
}