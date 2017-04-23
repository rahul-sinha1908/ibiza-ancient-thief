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
		//TODO Reduce Money and send RPCs calls
	}

	public void askDirection(){
		//TODO Reduce Money and send RPCs calls1
	}

	public void addThiefMoves(TransportType type){
		//TODO Add the button in the scroll Pane.
	}
}
