using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Order{
    public List<OrderRecipe> orders; //2 Burgers, 1 Salad
}

public struct OrderRecipe{
    public string name; //Burger 
    public List<OrderIngredient> requiredIngredients; //Bun, Patty, Letuice, Tomato
}

public struct OrderIngredient{
    public string name; //Patty
    public IngredientType type;
    public bool needsCooking;
    public bool needsPrepping;
}

public enum IngredientType{
    TOMATO,
    ONION,
    MUSHROOM,
    LETUICE,
    CHEESE,
    BURGER_BUN,
    BURGER_PATTY
}

public enum IngredientCookedRequirement{
    UNCOOKED,
    COOKED
}
public enum IngredientPreppedRequirement{
    UNPREPPED,
    PREPPED,
    EITHER
    
}


[System.Serializable]
public struct CoreIngredientData{
    public string name;
    public IngredientType type;
    public IngredientPreppedRequirement preppedRequirement;
    public bool prepped;
    public IngredientCookedRequirement cookedRequirement;
    public bool cooked;
    public float inclusionChance;
    public int maxInRecipe;
}


[System.Serializable]
public struct IngredientData{
    public string name;
    public IngredientType type;
    public bool prepped;
    public bool cooked;
}

public class Ingredient : MonoBehaviour{
    public IngredientData data;

    void Awake(){

    }

    void Start(){
        
    }

    void Update(){

    }

}