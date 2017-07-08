using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupPot : CookingStation {
	const float START_TEMP = 72.0f;
	public List<Ingredient> contents;
	public float heat = 0.001f;

	private float _temperature;
	public float Temperature {
		get { return _temperature; }
	}

	void Update(){
		_temperature = Mathf.Min(212.0f, _temperature + heat);
	}

	void OnTriggerEnter(Collider other){
		Ingredient ingredient = other.GetComponent<Ingredient>();
		if(ingredient != null){
			AddIngredient(ingredient);
		}
	}

	void AddIngredient(Ingredient ingredient){
		contents.Add(ingredient);
		Debug.LogFormat("You put {0} in the soup!", ingredient.data.type);
	}

	private void Reset(){
		_temperature = START_TEMP;
		contents = new List<Ingredient>();


	}

	private Recipe GetSoup(){
		Recipe Soup = new Recipe();
		Soup.ingredients = new List<IngredientData>();

		contents.ForEach(i => {
			Ingredient ing = i.GetComponent<Ingredient>();
			if(ing != null){
				Soup.ingredients.Add(ing.data);
			}
		});

		return Soup;
	}

	override public Recipe Plate(){
		Recipe Soup = GetSoup();
		Reset();

		return Soup;
	}
}
