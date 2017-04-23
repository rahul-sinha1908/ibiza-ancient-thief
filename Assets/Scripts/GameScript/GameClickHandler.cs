using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using MyGame;

public class GameClickHandler : MonoBehaviour {

	public LayerMask layerMask;
	public int checkTemp=-1;
	public GameObject timerWindow;
	public Text timerText;
	private Camera cam;
	private CheckPoints selectedPoints;
	public List<CheckPoints> allChecks;
	public List<CheckPoints> spawning1, spawning2, spawning3, spawning4, spawning5, spawningX;
	public MyPlayerScript myPlayerScript;
	// Use this for initialization
	void Start () {
		cam=Camera.main;
		GameRunningScript.getInstance().gameClickHandler=this;
		if(GameRunningScript.getInstance().myPlayerChar==Character.Thief){
			myPlayerScript.getPolices();
		}
		//myPlayerScript.initiateChecks(allChecks);
	}
	
	public void SingleClick(Vector2 vect){
		if(GameRunningScript.getInstance().isClickActive==false)
			return;
		Ray ray = cam.ScreenPointToRay(vect);
		RaycastHit point;
		if(Physics.Raycast(ray,out point, 20000, layerMask)){
			if(point.collider.gameObject.GetComponent<PlayerControlScript>()!=null){
				PlayerControlScript player=point.collider.gameObject.GetComponent<PlayerControlScript>();
				player.setSelected(true);
			}else if(point.collider.gameObject.GetComponent<CheckPoints>()!=null){
				CheckPoints check =point.collider.gameObject.GetComponent<CheckPoints>();
				if(!check.getClickable())
					return;
				if(GameRunningScript.getInstance().selectedPlayer==null)
					return;
				//TODO Do a method to decide the transport method
				GameRunningScript.getInstance().myPlayer.sendMove(check, TransportType.Cycle);
				GameRunningScript.getInstance().selectedPlayer.moveMyPlayer(check, TransportType.Cycle);
				Dev.log(Tag.GameClickListener,"Hit : "+check.name);
			}
			
		}
	}

	public void DoubleClick(Vector2 vect){
		if(GameRunningScript.getInstance().isClickActive==false)
			return;
	}

	// Update is called once per frame
	void Update () {
		showCountDown();
	}

    private void showCountDown()
    {
        if(GameRunningScript.getInstance().countDown>0){
			timerWindow.SetActive(true);
			timerText.text=""+GameRunningScript.getInstance().countDown;
		}else{
			timerWindow.SetActive(false);
		}
    }

    public CheckPoints selectRandomPolice(int i){
		System.Random random=new System.Random();
		if(i==1){
			return spawning1[random.Next(0, spawning1.Count)];
		}else if(i==2){
			return spawning2[random.Next(0, spawning2.Count)];
		}else if(i==3){
			return spawning3[random.Next(0, spawning3.Count)];
		}else if(i==4){
			return spawning4[random.Next(0, spawning4.Count)];
		}else if(i==5){
			return spawning5[random.Next(0, spawning5.Count)];
		}
		return null;
	}
	public bool checkExistence(List<PlayerControlScript> list, CheckPoints check){
		foreach(PlayerControlScript ch in list){
			if(ch.getCurrentCheck().name == check.name){
				return true;
			}
		}
		return false;
	}
	public CheckPoints selectThiefLoc(List<PlayerControlScript> list){
		System.Random random = new System.Random();
		int r=random.Next(0, spawningX.Count);
		
		while(checkExistence(list, spawningX[r])){
			r=random.Next(0, spawningX.Count);
		}
		return spawningX[r];
	}
	public List<CheckPoints> getAllCheks(){
		return allChecks;
	}
}