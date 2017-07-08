using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Order{
    public List<OrderRecipe> orders; //2 Burgers, 1 Salad
    public List<Recipe> completedOrders;
}

[System.Serializable]
public struct OrderRecipe{
    public string name; //Burger 
    public List<OrderIngredient> requiredIngredients; //Bun, Patty, Lettuce, Tomato
    public float price;
    public List<string> variance; 
}

[System.Serializable]
public struct OrderIngredient{
    public string name; //Patty
    public IngredientType type;
    public bool needsCooking;
    public bool needsChopping;
    public bool needsTenderizing;
}

