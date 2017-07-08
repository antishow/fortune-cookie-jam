using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDPartyTimers : MonoBehaviour {
	public OrderGenerator orderController;
	//public List<HUDTimer> timers;
	public List<HUDTimer> timers;
	public GameObject timerPrefab;
	// Use this for initialization
	void Start () {
		foreach (PartyMemeberStatus type in PartyMemeberStatus.GetValues(typeof(PartyMemeberStatus))){

		}
	}
	
	// Update is called once per frame
	void Update () {
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
