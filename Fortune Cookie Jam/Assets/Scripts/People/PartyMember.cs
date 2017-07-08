using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HungerLevel{
    FULL,
    HUNGRY,
    STARVING
}

//This goes on each party member so that if you interact with one of them, you can find the order from any party member, the partyID should be the same across all party members
public class PartyMember : MonoBehaviour{
    public int partyID; 
    public HungerLevel hunger;

    //Meal status'
    public bool seated;
    public float decideTime;
    public bool decided; //has this person decided yet?
    public float orderWaitTime;
    public bool ordered;

    public float foodWaitTime;
    public float eatingTime;
    public bool eating;
    public bool finished;
    public bool checkDelivered;

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
    }

    public void Update(){
        //They only start deciding once seated
        if(!seated){
            //Wait to be seated
        } else if(!decided){
            if(decideTime < 0){
                decided = true;
            } else {
                decideTime -= Time.deltaTime;
            }
        } else if(!ordered){
            //Wait to have order taken
            orderWaitTime += Time.deltaTime;
        } else if(!eating){
            //Wait to have food delivered
            foodWaitTime += Time.deltaTime;
        } else if(!finished){
            if(eatingTime < 0){
                finished = true;
            } else {
                eatingTime -= Time.deltaTime;
            }
        } else if(!checkDelivered){
            //Make them dissapear or something.
            //Possibly do some scoring here or something
        }
    }

    public List<OrderRecipe> GetOrder(){
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
        return personOrder;
    }

    public Party GetParty(){
        return OrderGenerator.partyOrders[partyID];
    }
}