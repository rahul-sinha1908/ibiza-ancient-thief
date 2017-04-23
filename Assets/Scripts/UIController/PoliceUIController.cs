using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class PoliceUIController : MonoBehaviour, SupremeController {
	public List<PlayerControlScript> polices;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void selectPolice(int i){
		polices[i].setSelected(true);
	}

	public void askRadius(){
		//TODO Reduce Money and send RPCs calls
	}

	public void askDirection(){
		//TODO Reduce Money and send RPCs calls
	}

	public void addThiefMoves(TransportType type){
		//TODO Add the button in the scroll Pane.
		Dev.log(Tag.PoliceUIController, "Added thief Button : "+type);
	}

	public void showTravellingWindow(){
		Dev.log(Tag.PoliceUIController, "Showing the time left Window");
	}
	public void showCooldownWindow(){
		Dev.log(Tag.PoliceUIController, "Showing the Cooldowm Window");
	}
	public void hideAllWindows(){
		Dev.log(Tag.PoliceUIController, "Removing all Windows");
	}
	public void setCooldownTime(int time){
		
	}

}
