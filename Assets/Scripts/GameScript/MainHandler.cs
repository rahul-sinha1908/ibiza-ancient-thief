using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHandler : MonoBehaviour {

	public Transform mainLoc, leftLoc, rightLoc;
	public Transform mainMenu, searchMenu, lobby, waitMenu, eventSystem;
	public Text waitText;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void createServer(){

	}


	public void searchForServer(){

	}
	
	public void exit(){
		Application.Quit();
	}
}
