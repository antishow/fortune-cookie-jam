using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    public static OrderGenerator instance;
    public GameObject partyPrefab;
    public int numberOfParties; //The total number of parties in the resaraunt
    public int numberOfPeople; //The total number of people in all parties
    public static Dictionary<int, Party> partyOrders;
    public void Awake(){
        if(OrderGenerator.instance != null){
            Destroy(this.gameObject);
        } else {
            OrderGenerator.instance = this;
        }
    }

    public void Update(){
        
    }

    public void CreateParty(){
        //Create a party

        //Create the party members
        
        //Link them

        //Let the party memebers do whatever
    }
}