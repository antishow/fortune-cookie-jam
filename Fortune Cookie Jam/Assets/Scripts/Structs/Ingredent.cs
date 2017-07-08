using UnityEngine;
using System;

public enum IngredientType{
    TOMATO,
    ONION,
    MUSHROOM,
    TOMATO,
    LETUICE,
    CHEESE,
    BURGER_BUN,
    BURGER_PATTY
}

public enum IngredientStatus{
    RAW,
    COOKED
}

public struct IngredientData{
    IngredientType type;
    IngredientStatus status;
}

public class Ingredient : MonoBehaviour{

    void Awake(){

    }

    void Start(){

    }

    void Update(){

    }

}