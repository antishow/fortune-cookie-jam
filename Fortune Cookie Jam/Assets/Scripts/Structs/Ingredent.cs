using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType{
    TOMATO,
    ONION,
    MUSHROOM,
    LETUICE,
    CHEESE,
    BURGER_BUN,
    BURGER_PATTY
}

public enum IngredientStatus{
    RAW,
    CHOPPED,
    COOKED
}

[System.Serializable]
public struct IngredientData{
    public string name;
    public IngredientType type;
    public IngredientStatus status;
    public bool cooked;
    public float inclusionChance;
    public int maxInRecipe;
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