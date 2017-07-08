using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AllCoreRecipies{
    public List<string> coreRecipes;
}

[System.Serializable]
public struct IngredientName{
    public IngredientType type;
    public string name;
}

[System.Serializable]
public struct RecipeNames{
    public string[] nouns;
    public string[] adjectives;
    public List<IngredientName> ingredientNameList;
    public Dictionary<IngredientType, string> ingredientNames;
}

public class RecipeGenerator : MonoBehaviour
{
    public static List<CoreRecipe> coreRecipes;
    public List<CoreRecipe> coreRecipesPublic;
    public static RecipeGenerator instance;
    // public DefaultRecipe testRecipe;
    // public List<OrderRecipe> testOrderList;
    public static RecipeNames nameList;
    public void Awake(){
        if(RecipeGenerator.instance != null){
            Destroy(this.gameObject);
        } else {
            RecipeGenerator.instance = this;
            //Load core recipes
            coreRecipes = new List<CoreRecipe>();
            coreRecipesPublic = new List<CoreRecipe>();
            AllCoreRecipies loadedRecipes = JsonUtility.FromJson<AllCoreRecipies>(FileUtility.LoadJsonFromFile("CoreRecipes/AllRecipes.json", true));
            foreach(string s in loadedRecipes.coreRecipes){
                CoreRecipe temp = JsonUtility.FromJson<CoreRecipe>(FileUtility.LoadJsonFromFile(s, true));
                //Debug.Log(temp);
                coreRecipes.Add(temp);
                coreRecipesPublic.Add(temp);
            }
            
            //Load all the names
            nameList = JsonUtility.FromJson<RecipeNames>(FileUtility.LoadJsonFromFile("Names.json", true));
            nameList.ingredientNames = new Dictionary<IngredientType, string>();
            foreach(IngredientName n in nameList.ingredientNameList){
                nameList.ingredientNames.Add(n.type, n.name);
            }
            RecipeBook.AddNewRecipie();
        }
    }

    //Make a recipe for the book
    public static DefaultRecipe CreateRandomDefaultRecipe(){
        DefaultRecipe defaultRecipe = new DefaultRecipe();
        defaultRecipe.requiredIngredients = new List<OrderIngredient>();
        //Choose a core recipe
        int recipeChosen = Random.Range(0, coreRecipes.Count);
        CoreRecipe core = coreRecipes[recipeChosen];
        string randomName = nameList.nouns[Random.Range(0,nameList.nouns.Length)];
        string randomAdjective = nameList.adjectives[Random.Range(0,nameList.adjectives.Length)];
        defaultRecipe.name = randomAdjective + " " + randomName + " " + core.name;
        defaultRecipe.parentRecipe = core;
        defaultRecipe.price = Random.Range(Preferences.minRecipeCost, Preferences.maxRecipeCost);
        //For each ingredient in the core recipe
        foreach(CoreRecipeIngredient cri in core.requiredIngredients){
            //Determine if the ingredient is being added
            float isAddedPercent = Random.Range(0.0f, 100.0f);
            //Debug.Log("Checking to see if adding: " + cri.name);
            if(cri.percentageChance > isAddedPercent){
                //Debug.Log("[" + cri.name + "]" + "Adding  with a chance of: " + isAddedPercent + "/" + cri.percentageChance);
                //If so, how many are we adding
                int maxInRecipe = Random.Range(1, cri.maxInRecipe+1);
                //Debug.Log("Adding  " + maxInRecipe + " of the: " + cri.name + " ");
                for(int i = 0; i < maxInRecipe; i++){
                    OrderIngredient ingredient = new OrderIngredient();
                    ingredient.name = cri.name;
                    ingredient.type = cri.type;
                    switch(cri.possibleCookedStatus){
                        case CoreIngredientCookedStatus.UNCOOKED:
                            ingredient.needsCooking = false;
                        break;
                        case CoreIngredientCookedStatus.COOKED:
                            ingredient.needsCooking = true;
                        break;
                        case CoreIngredientCookedStatus.ANY:
                            int r = Random.Range(0,2);
                            if(r == 1){
                                ingredient.needsCooking = false;
                            } else {
                                ingredient.needsCooking = true;
                            }
                        break;
                    }
                    
                    switch(cri.possibleChoppedStatus){
                        case CoreIngredientChoppedStatus.UNCHOPPED:
                            ingredient.needsChopping = false;
                        break;
                        case CoreIngredientChoppedStatus.CHOPPED:
                            ingredient.needsChopping = true;
                        break;
                        case CoreIngredientChoppedStatus.ANY:
                            int r = Random.Range(0,2);
                            if(r == 1){
                                ingredient.needsChopping = false;
                            } else {
                                ingredient.needsChopping = true;
                            }
                        break;
                    }
                    
                    switch(cri.possibleTenderizedStatus){
                        case CoreIngredientTenderizedStatus.UNTENDERIZED:
                            ingredient.needsTenderizing = false;
                        break;
                        case CoreIngredientTenderizedStatus.TENDERIZED:
                            ingredient.needsTenderizing = true;
                        break;
                        case CoreIngredientTenderizedStatus.ANY:
                            int r = Random.Range(0,2);
                            if(r == 1){
                                ingredient.needsTenderizing = false;
                            } else {
                                ingredient.needsTenderizing = true;
                            }
                        break;
                    }
                    //Debug.Log("Adding  " +  ingredient.name);
                    defaultRecipe.requiredIngredients.Add(ingredient);
                }
            }
        }
        return defaultRecipe;
    }

    //Make a recipe for the book
    public static OrderRecipe CreateOrderFromMenuItem(DefaultRecipe def){
        OrderRecipe newOrder = new OrderRecipe();
        newOrder.requiredIngredients = new List<OrderIngredient>();
        CoreRecipe core = def.parentRecipe;
        newOrder.parentDefaultRecipe = def;
        newOrder.price = def.price;
        newOrder.name = def.name;
        //For each ingredient in the core recipe
        foreach(CoreRecipeIngredient cri in core.requiredIngredients){
            //Determine if the ingredient is being added
            float isAddedPercent = Random.Range(0.0f, 100.0f);
            //Debug.Log("Checking to see if adding: " + cri.name);
            if(cri.percentageChance > isAddedPercent){
                //Debug.Log("[" + cri.name + "]" + "Adding  with a chance of: " + isAddedPercent + "/" + cri.percentageChance);
                //If so, how many are we adding
                int maxInRecipe = Random.Range(1, cri.maxInRecipe+1);
                //Debug.Log("Adding  " + maxInRecipe + " of the: " + cri.name + " ");
                OrderIngredient ingredient = new OrderIngredient();
                ingredient.name = cri.name;
                ingredient.type = cri.type;
                switch(cri.possibleCookedStatus){
                    case CoreIngredientCookedStatus.UNCOOKED:
                        ingredient.needsCooking = false;
                    break;
                    case CoreIngredientCookedStatus.COOKED:
                        ingredient.needsCooking = true;
                    break;
                    case CoreIngredientCookedStatus.ANY:
                        int r = Random.Range(0,2);
                        if(r == 1){
                            ingredient.needsCooking = false;
                        } else {
                            ingredient.needsCooking = true;
                        }
                    break;
                }
                
                switch(cri.possibleChoppedStatus){
                    case CoreIngredientChoppedStatus.UNCHOPPED:
                        ingredient.needsChopping = false;
                    break;
                    case CoreIngredientChoppedStatus.CHOPPED:
                        ingredient.needsChopping = true;
                    break;
                    case CoreIngredientChoppedStatus.ANY:
                        int r = Random.Range(0,2);
                        if(r == 1){
                            ingredient.needsChopping = false;
                        } else {
                            ingredient.needsChopping = true;
                        }
                    break;
                }
                
                switch(cri.possibleTenderizedStatus){
                    case CoreIngredientTenderizedStatus.UNTENDERIZED:
                        ingredient.needsTenderizing = false;
                    break;
                    case CoreIngredientTenderizedStatus.TENDERIZED:
                        ingredient.needsTenderizing = true;
                    break;
                    case CoreIngredientTenderizedStatus.ANY:
                        int r = Random.Range(0,2);
                        if(r == 1){
                            ingredient.needsTenderizing = false;
                        } else {
                            ingredient.needsTenderizing = true;
                        }
                    break;
                }
                for(int i = 0; i < maxInRecipe; i++){
                    //Debug.Log("Adding  " +  ingredient.name);
                    newOrder.requiredIngredients.Add(ingredient);
                }
            }
        }
        return newOrder;
    }

    public static List<string> GetVarrianceBetweenOrders(DefaultRecipe def, OrderRecipe ord){
        List<string> orderVariance = new List<string>();
        foreach (IngredientType type in IngredientType.GetValues(typeof(IngredientType)))
        {
            bool isPresent = false;
            int defaultCount = 0;
            foreach(OrderIngredient defaultIn in def.requiredIngredients){
                if(defaultIn.type == type){
                    isPresent = true;
                    defaultCount++;
                }
            }
            bool inOrder = false;
            int orderCount = 0;
            foreach(OrderIngredient orderIn in ord.requiredIngredients){
                if(orderIn.type == type){
                    inOrder = true;
                    orderCount++;
                }
            }

            if(isPresent && inOrder){
                //If in both but different counts
                if(defaultCount != orderCount){
                    orderVariance.Add("Only" + orderCount + nameList.ingredientNames[type]);
                }
            } else if(!isPresent && inOrder){
                 orderVariance.Add("Add " + nameList.ingredientNames[type]);
            } else if(isPresent && !inOrder){
                 orderVariance.Add("No " + nameList.ingredientNames[type]);
            }
        }
        return orderVariance;
    }

    //Returns the number of mistakes
    public static int CompareOrderToRecipe(OrderRecipe ord, Recipe recipe){
        int mistakes = 0;
        //We check every possible ingredient
        foreach (IngredientType type in IngredientType.GetValues(typeof(IngredientType)))
        {
            bool isPresent = false;
            int orderCount = 0;  
            foreach(OrderIngredient orderIn in ord.requiredIngredients){
                if(orderIn.type == type){
                    isPresent = true;
                    orderCount++;
                }
            }

            bool inRecipe = false;
            int recipeCount = 0;
            foreach(IngredientData inData in recipe.ingredients){
                if(inData.type == type){
                    inRecipe = true;
                    recipeCount++;
                }
            }

            if(isPresent && inRecipe){
                //If in both but different counts
                if(orderCount != recipeCount){
                    mistakes += Mathf.Abs(orderCount - recipeCount);
                }
            } else if(!isPresent && inRecipe){
                mistakes += recipeCount;
            } else if(isPresent && !inRecipe){
                mistakes += orderCount;
            }
        }

        return mistakes;
    }
}