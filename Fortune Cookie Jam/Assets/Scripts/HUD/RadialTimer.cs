using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialTimer : MonoBehaviour {

    public Image cooldown;
    public bool coolingDown;
    public float waitTime = 30.0f;
	// Use this for initialization
	void Start () {
		cooldown.type = Image.Type.Filled;
		cooldown.fillMethod = Image.FillMethod.Radial360;
		cooldown.fillAmount = 0;
	}
	
	// Update is called once per frame
    void Update()
    {
        //Reduce fill amount over 30 seconds
        cooldown.fillAmount += Time.deltaTime * 0.1f;
    }
}