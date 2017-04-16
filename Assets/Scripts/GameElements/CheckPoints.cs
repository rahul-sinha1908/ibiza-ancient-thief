using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGame;

public class CheckPoints : MonoBehaviour {

	public bool bCycle, bBullock, bHorse, bBoat, bMoney;
	public List<Transform> cycle, bullock, horse, boat;
	public MeshRenderer myMesh;
	public TextMesh myText;
	public List<Material> materials;
	private bool isClickable;

	// Use this for initialization
	void Start () {
		isClickable=false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSelected(bool b){
		if(b){
			//TODO Write Code to select the current 
		}else{
			//TODO Write Code to disselect the current checkBox
		}
	}
	private void setType(CheckTypes type){
		myMesh.material=materials[(int)type];
	}
	public void setClickabe(bool b){
		isClickable=b;
	}
	public bool getClickable(){
		return isClickable;
	}
}
