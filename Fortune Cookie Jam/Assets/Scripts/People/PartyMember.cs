using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HungerLevel{
    FULL,
    HUNGRY,
    STARVING
}

public enum PartyMemeberStatus{
    WAITING_SEATED,
    SEATED,
    DECIDED,
    ORDERED,
    EATING,
    FINISHED,
    LEAVING
}

//This goes on each party member so that if you interact with one of them, you can find the order from any party member, the partyID should be the same across all party members
public class PartyMember : MonoBehaviour{
    public Party parentParty; 
    public int IDInParty; 
    public HungerLevel hunger;

    public PartyMemeberStatus status;
    //Meal status'
    // public bool eating;
    // public bool finished;
    // public bool seated;
    // public bool decided; //has this person decided yet?
    // public bool ordered;

    public float seatedWaitTime;
    public float decideTime;
    public float orderWaitTime;
    public float foodWaitTime;
    public float eatingTime;
    public bool leaving;
    // public bool checkDelivered;
    // public float checkDeliveredWaitTime;
    public List<OrderRecipe> partyMemeberOrder;

    public void Start(){
        //Randomly choose how hungry the person is
        float hungerLevel =  Random.Range(0.0f, 100.0f);
        if(hungerLevel > Preferences.fullThreshold){
            hunger = HungerLevel.FULL;
        } else if(hungerLevel > Preferences.hungryThreshold){
            hunger = HungerLevel.HUNGRY;
        } else if(hungerLevel > Preferences.starvingThreshold){
            hunger = HungerLevel.STARVING;
        }

        //it will take the person 20-40 seconds to decide on what they want.
        decideTime = Random.Range(Preferences.partyMemberMinDecideTime, Preferences.partyMemberMaxDecideTime);
        eatingTime = Random.Range(Preferences.partyMemberMinEatTime, Preferences.partyMemberMaxEatTime);
        GetOrder();
    }

    public void Update(){
        switch (status)
        {
            case PartyMemeberStatus.WAITING_SEATED:
                //Wait to be seated
                seatedWaitTime += Time.deltaTime;
            break;
            case PartyMemeberStatus.SEATED:
                //Deciding
                if(decideTime < 0){
                    status = PartyMemeberStatus.DECIDED;
                } else {
                    decideTime -= Time.deltaTime;
                }
            break;
            case PartyMemeberStatus.DECIDED:
                //Wait to have order taken
                orderWaitTime += Time.deltaTime;
            break;
            case PartyMemeberStatus.ORDERED:
                //Wait to have food delivered
                foodWaitTime += Time.deltaTime;
            break;
            case PartyMemeberStatus.EATING:
                //Eating
                if(eatingTime < 0){
                    status = PartyMemeberStatus.FINISHED;
                } else {
                    eatingTime -= Time.deltaTime;
                }
            break;
            case PartyMemeberStatus.FINISHED:
                //Make them dissapear or something.
                if(IDInParty == 0){
                    //Only do this on the party leader
                    parentParty.CheckFinished();
                }
            break;
            case PartyMemeberStatus.LEAVING:
            break;
        }
    }

    public List<OrderRecipe> GetOrder(){
        if(partyMemeberOrder != null && partyMemeberOrder.Count != 0){
            return partyMemeberOrder;
        }
        int itemsOrdered = 0;
        switch(hunger){
            case HungerLevel.FULL:
                itemsOrdered = Random.Range(0,2); //0-1 exclusive
            break;
            case HungerLevel.HUNGRY:
                itemsOrdered = Random.Range(1,3); //1-2 exclusive
            break;
            case HungerLevel.STARVING:
                itemsOrdered = Random.Range(1,4); //1-3 exclusive
            break;
        }
        List<OrderRecipe> personOrder = new List<OrderRecipe>();
        for (int i = 0; i < itemsOrdered; i++){
            personOrder.Add(RecipeGenerator.CreateOrderFromMenuItem(RecipeBook.GetRandomRecipe()));
        }
        partyMemeberOrder = personOrder;
        return personOrder;
    }

    public Party GetParty(){
        return parentParty;
    }
}