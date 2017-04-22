using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class PoliceUIController : MonoBehaviour {
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

	}

	public void askDirection(){

	}

	public void addThiefMoves(TransportType type){
		//TODO Add the button in the scroll Pane.
	}
}
