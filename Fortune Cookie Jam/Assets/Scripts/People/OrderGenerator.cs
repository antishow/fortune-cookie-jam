using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    public static OrderGenerator instance;
    public GameObject partyPrefab;
    public int numberOfParties; //The total number of parties in the resaraunt
    public int numberOfPeople; //The total number of people in all parties
    public void Awake(){
        if(RecipeGenerator.instance != null){
            Destroy(this.gameObject);
        } else {
            OrderGenerator.instance = this;
        }
    }

    public void Update(){
        
    }
}