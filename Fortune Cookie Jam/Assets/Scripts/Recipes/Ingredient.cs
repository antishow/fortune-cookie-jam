using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum IngredientType{
    TOMATO,
    ONION,
    MUSHROOM,
    LETTUCE,
    CHEESE,
    BURGER_BUN,
    BURGER_PATTY,
    CELERY,
    CARROT,
    NOODLES,
    BROTH,
}

[System.Serializable]
public struct IngredientData{
    public string name; //Patty
    public IngredientType type;
    public bool isCooked;
    public bool isChopped;
    public bool isTenderized;
}

public class Ingredient : MonoBehaviour{
    public IngredientData data;

    void Awake(){

    }

    void Start(){
        
    }

    void Update(){

    }

    public void Cook(){
        data.isCooked = true;
    }

    public void Chop(){
        data.isChopped = true;
    }

    public void Tenderize(){
        data.isTenderized = true;
    }


}