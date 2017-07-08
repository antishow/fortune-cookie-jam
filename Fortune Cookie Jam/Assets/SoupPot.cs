using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Recipe{

}

public class SoupPot : MonoBehaviour {
	public List<GameObject> contents;
	public float heat = 0.001f;
	private float _temperature = 72.0f;
	public float Temperature {
		get { return _temperature; }
	}

	void Update(){
		_temperature = Mathf.Min(212.0f, _temperature + heat);
	}

	void OnTriggerEnter(Collider other){
		AddIngredient(other.gameObject);
	}

	void AddIngredient(GameObject ingredient){
		contents.Add(ingredient);
		ingredient.SetActive(false);
		Debug.LogFormat("You put {0} in the soup!", ingredient.gameObject.name);

		List<string> ingredients = new List<string>();
		int i = 0;
		for(i=0; i<contents.Count; i++){
			ingredients.Add(contents[i].name);
		}

		Debug.LogFormat("Soup now contains: {0}", string.Join(", ", ingredients.ToArray()));
	}


}
