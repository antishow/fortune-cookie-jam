using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This goes on a dish gameobject 
public class Dish : MonoBehaviour {
    public Recipe contentsOfDish;

    public void setDishContent(Recipe rec){
        contentsOfDish = rec;
    }

    public void AddIngredient(IngredientData data){
        contentsOfDish.ingredients.Add(data);
    }
}