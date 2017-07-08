using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    public static OrderGenerator instance;
    public GameObject partyPrefab;
    public GameObject partyMemberPrefab;
    public int numberOfParties; //The total number of parties in the resaraunt
    public int numberOfPeople; //The total number of people in all parties
    public static Dictionary<int, Party> partyOrders;
    public void Awake(){
        if(OrderGenerator.instance != null){
            Destroy(this.gameObject);
        } else {
            OrderGenerator.instance = this;
            CreateParty();
        }
    }

    public void Update(){
        
    }

    public void CreateParty(){
        //Create a party
        GameObject party = (GameObject)Instantiate(partyPrefab);
        party.transform.parent = this.transform;
        //Create the party members
        int partyCount = Random.Range(Preferences.minPartyMemebers, Preferences.maxPartyMemebers+1);
        for (int i = 0; i < partyCount; i++)
        {
            //TODO: put the players somewhere
            GameObject player = (GameObject)Instantiate(partyMemberPrefab);
            player.transform.parent = party.transform;
            //Link them
            party.GetComponent<Party>().partyMembers.Add(player.GetComponent<PartyMember>());
        }
    }
}