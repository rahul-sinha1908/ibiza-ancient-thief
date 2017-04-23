using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGame;

public class PoliceUIController : MonoBehaviour, SupremeController {
	public List<PlayerControlScript> polices;
	public GameObject displayMessage, listofMOves, movePrefab;
	public Text displayText;
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
		GameObject go=GameObject.Instantiate(movePrefab, Vector3.zero, Quaternion.identity, listofMOves.transform);
		go.transform.FindChild("Text").GetComponent<Text>().text=""+type;
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
		//displayMessage.SetActive(true);
		displayText.text="Cool Down.. "+time+" sec";
	}

}
