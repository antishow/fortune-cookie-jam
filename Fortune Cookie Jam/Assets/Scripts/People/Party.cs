using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This goes on each party member so that if you interact with one of them, you can find the order from any party member, the partyID should be the same across all party members
public class Party : MonoBehaviour{
    public int partyID; 
    public List<PartyMember> partyMembers;
    public Order partyOrder;

    public Order GetOrder(){
        bool allDecided = true;
        foreach(PartyMember pm in partyMembers){
            if(!pm.decided){
                allDecided = false;
            }
        }
        if(allDecided){
            foreach(PartyMember pm in partyMembers){
                partyOrder.orders.AddRange(pm.GetOrder());
            }
            return partyOrder;
        }
        return null;
    }

    public void DeliverOrder(){
        
    }
}