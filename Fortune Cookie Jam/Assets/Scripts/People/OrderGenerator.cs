using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    const float PARTY_SPAWN_PERIOD = 300.0f;
    const float PARTY_SPAWN_PERIOD_DECAY_RATE = 0.95f;
    const float MIN_PARTY_SPAWN_PERIOD = 120.0f;
    private float _spawnPeriod = PARTY_SPAWN_PERIOD;



    public static OrderGenerator instance;
    public GameObject partyPrefab;
    public GameObject partyMemberPrefab;
    public int numberOfParties; //The total number of parties in the resaraunt
    public int numberOfPeople; //The total number of people in all parties
    public List<Party> parties;
    public void Awake(){
        if(OrderGenerator.instance != null){
            Destroy(this.gameObject);
        } else {
            OrderGenerator.instance = this;
            _spawnPeriod = PARTY_SPAWN_PERIOD;
            CreateParty();
            CreateParty();
            CreateParty();
            Invoke("OnSpawnPeriodComplete", _spawnPeriod);
        }
    }

    public void OnSpawnPeriodComplete(){
        CreateParty();
        _spawnPeriod *= Mathf.Max(MIN_PARTY_SPAWN_PERIOD, _spawnPeriod * PARTY_SPAWN_PERIOD_DECAY_RATE);
            Invoke("OnSpawnPeriodComplete", _spawnPeriod);
    }


    public void Update(){
        // foreach(Party p in partyOrders){
        //     if(p.partyFinished){

        //     }
        // }
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
        party.GetComponent<Party>().GetOrder();
        parties.Add(party.GetComponent<Party>());
    }

    public List<Party> GetParties(){
        return parties;
    }
}