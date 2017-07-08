using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CoreIngredientCookedStatus{
    ANY,
    UNCOOKED,
    COOKED,
    // OVERCOOKED,
    // BURNT,
}

[System.Serializable]
public enum CoreIngredientChoppedStatus{
    ANY,
    UNCHOPPED,
    CHOPPED,
    // DICED,
    // MINCED,
}

[System.Serializable]
public enum CoreIngredientTenderizedStatus{
    ANY,
    UNTENDERIZED,
    TENDERIZED,
    // PULP,
}

[System.Serializable]
public struct CoreRecipe{
    public string name; //Burger 
    public List<CoreRecipeIngredient> requiredIngredients; //Bun, Patty, Lettuce, Tomato
}

[System.Serializable]
public struct CoreRecipeIngredient{
    public string name; //Patty
    public IngredientType type;
    public float percentageChance;
    public int maxInRecipe;
    public CoreIngredientCookedStatus possibleCookedStatus; //Can/should this be cooked
    public CoreIngredientChoppedStatus possibleChoppedStatus; //Can/should this be chopped
    public CoreIngredientTenderizedStatus possibleTenderizedStatus; //Can/should this be tenderized
}

