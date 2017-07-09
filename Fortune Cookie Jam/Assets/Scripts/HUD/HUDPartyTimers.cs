using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPartyTimers : MonoBehaviour {
	public OrderGenerator orderController;
	public Text scoreText;
	public static float scoreValue;
	public static int mistakes;
	//public List<HUDTimer> timers;
	public GameObject timerPrefab;
	// Use this for initialization
	void Start () {
		foreach (PartyMemeberStatus type in PartyMemeberStatus.GetValues(typeof(PartyMemeberStatus))){
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "$" + scoreValue;
		foreach(Party par in orderController.parties){
			
			foreach(PartyMember mem in par.partyMembers){
				switch (mem.status)
				{
					case PartyMemeberStatus.DECIDED:
						
					break;
				}
			}
		}
	}
}
