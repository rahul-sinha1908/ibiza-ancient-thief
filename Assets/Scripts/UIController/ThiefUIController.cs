using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class ThiefUIController : MonoBehaviour, SupremeController {
	public PlayerControlScript thief;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void hideMoves(){

	}

	public void removeCooldown(){

	}

	public void bribePerson(){

	}

	public void showTravellingWindow(){
		Dev.log(Tag.ThiefUIController, "Showing the time left Window");
	}
	public void showCooldownWindow(){
		Dev.log(Tag.ThiefUIController, "Showing the Cooldowm Window");
	}
	public void hideAllWindows(){
		Dev.log(Tag.ThiefUIController, "Removing all Windows");
	}
	public void setCooldownTime(int time){
		
	}

}
