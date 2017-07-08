using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupPot : CookingStation
{
    const float START_TEMP = 72.0f;
    const float DONE_TEMP = 120.0f;
    const float BURN_TEMP = 200.0f;
    const float MAX_TEMP = 212.0f;

    private Gradient colorGradient;


    public List<Ingredient> contents;
    public float heat = 0.001f;
    public GameObject Broth;
    private Material brothMaterial;

    private float _temperature;
    public float Temperature
    {
        get { return _temperature; }
    }

    public Color GetBrothColor()
    {
        float doneInterval = Mathf.Clamp01((_temperature - START_TEMP) / (MAX_TEMP - START_TEMP));
        return colorGradient.Evaluate(doneInterval);
    }

    void Awake()
    {
        colorGradient = new Gradient();
        GradientColorKey[] gck = {
            new GradientColorKey(new Color32(255, 255, 116, 255), 0),
            new GradientColorKey(new Color32(255, 105, 0, 255), (DONE_TEMP - START_TEMP) / (MAX_TEMP - START_TEMP)),
            new GradientColorKey(new Color32(141, 122, 14, 255), (DONE_TEMP - START_TEMP) / (MAX_TEMP - START_TEMP) + 0.01f),
            new GradientColorKey(new Color32(0, 135, 0, 255), 1)
        };
        GradientAlphaKey[] gak = {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(1.0f, 1.0f)
        };

        colorGradient.SetKeys(gck, gak);

        Reset();
        brothMaterial = Broth.GetComponent<Renderer>().material;
    }

    void Update()
    {
        if(!Broth.activeInHierarchy)
        {
            return;
        }

        _temperature = Mathf.Min(MAX_TEMP, _temperature + heat);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Plate();
        }

        brothMaterial.color = GetBrothColor();
    }

    void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();

        if(ingredient == null)
        {
            ingredient = other.GetComponentInParent<Ingredient>();
        }

        if (ingredient != null)
        {
            if(!Broth.activeInHierarchy)
            {
                Broth.SetActive(true);
            }

            AddIngredient(ingredient);
            other.gameObject.SetActive(false);
        }
    }

    void AddIngredient(Ingredient ingredient)
    {
        contents.Add(ingredient);
        _temperature -= 5.0f;
        Debug.LogFormat("You put {0} in the soup!", ingredient.data.type);
    }

    private void Reset()
    {
        _temperature = START_TEMP;
        contents = new List<Ingredient>();
        Broth.SetActive(false);

    }

    private Recipe GetSoup()
    {
        Recipe Soup = new Recipe();
        Soup.ingredients = new List<IngredientData>();

        contents.ForEach(i =>
        {
            Ingredient ing = i.GetComponent<Ingredient>();
            if (ing != null)
            {
                Soup.ingredients.Add(ing.data);
            }
        });

        return Soup;
    }

    override public Recipe Plate()
    {
        Debug.Log("Put the Soup in a bowl");
        Recipe Soup = GetSoup();
        Reset();

        return Soup;
    }
}
