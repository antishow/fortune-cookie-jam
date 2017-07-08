using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This goes on each party member so that if you interact with one of them, you can find the order from any party member, the partyID should be the same across all party members
public class Party : MonoBehaviour{
    public int partyID; 
    public List<PartyMember> partyMembers;
    public Order partyOrder;

    public bool partyFinished;
    public bool partyDeparted;
    public float partyPaidAmount;

    public void SeatParty(){
        foreach(PartyMember pm in partyMembers){
            pm.seated = true;
        }
    }
    
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
        //Insert comparison code here
        foreach(PartyMember pm in partyMembers){
            pm.eating = true;
        }
    }

    public void CheckFinished(){
        //Insert comparison code here
        bool allFinished = true;
        foreach(PartyMember pm in partyMembers){
            if(!pm.finished){
                allFinished = false;
            }
        }
        if(allFinished){
            foreach(OrderRecipe or in partyOrder.orders){
                //partyPaidAmount += or.price;
                //TODO: Multiply by time wait scaler.
                partyPaidAmount += Preferences.flatPrice;
            }
            foreach(PartyMember pm in partyMembers){
                pm.leaving = true;
            }
            //Tell party members to leave
        }
    }
}