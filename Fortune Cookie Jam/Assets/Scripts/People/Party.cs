using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This goes on each party member so that if you interact with one of them, you can find the order from any party member, the partyID should be the same across all party members
public class Party : MonoBehaviour{
    public int partyID; 
    public int tableNum; 
    public List<PartyMember> partyMembers;
    public Order partyOrder;

	public Dictionary<PartyMemeberStatus, HUDTimer> timers;
    public bool partyFinished;
    public bool partyDeparted;
    public float partyPaidAmount;

    public void SeatParty(){
        foreach(PartyMember pm in partyMembers){
            pm.status = PartyMemeberStatus.SEATED;
        }
    }
    
    public Order GetOrder(){
        if(partyOrder != null){
            return partyOrder;
        }
        bool allDecided = true;
        foreach(PartyMember pm in partyMembers){
            if(pm.status != PartyMemeberStatus.DECIDED){
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
            pm.status = PartyMemeberStatus.EATING;
        }
    }

    public void CheckFinished(){
        //Insert comparison code here
        bool allFinished = true;
        foreach(PartyMember pm in partyMembers){
            if(pm.status != PartyMemeberStatus.FINISHED){
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