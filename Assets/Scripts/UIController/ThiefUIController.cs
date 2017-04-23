using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGame;

public class ThiefUIController : MonoBehaviour, SupremeController {
	public PlayerControlScript thief;
	public GameObject displayMessage;
	public Text displayText;
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
		Dev.log(Tag.PoliceUIController, "Showing the time left Window");
		displayMessage.SetActive(true);
		displayText.text="Travelling.. ";
	}
	public void showCooldownWindow(){
		Dev.log(Tag.PoliceUIController, "Showing the Cooldowm Window");
		displayMessage.SetActive(true);
		displayText.text="Cool Down Time.. ";
	}
	public void hideAllWindows(){
		Dev.log(Tag.PoliceUIController, "Removing all Windows");
		displayMessage.SetActive(false);
	}
	public void setCooldownTime(int time){
		displayMessage.SetActive(true);
		displayText.text="Cool Down.. "+time+" sec";
	}
}
