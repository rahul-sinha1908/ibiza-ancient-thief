using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class PlayerControlScript : MonoBehaviour {
	private bool isPolice;
	private CheckPoints currentCheck;
	private int index;
	private Character character;
	private bool isClickable, isMoving, isDisabled;
	private int coolDownTime, currentTime;
	private int money;
	// Use this for initialization
	void Start () {
		isDisabled=true;
		isClickable=false;
		isMoving=false;
		coolDownTime=60;
		money=getMyMoney();
	}
	private int getMyMoney(){
		//TODO Write Segments to implement my cash logic
		return 2000;
	}
	// Update is called oce per frame
	void Update () {
		
	}

	public void initMyPlayer(CheckPoints ch, Character c, int ind){
		isDisabled=false;
		character=c;
		index=ind;
		currentCheck=ch;
		transform.position=currentCheck.transform.position;
		isClickable=true;
		setSelected(false);
	}

	public void moveMyPlayer(CheckPoints ch, TransportType type){
		currentCheck.setSelected(false);
		StartCoroutine(moveToPlace(currentCheck, ch, type));
	}
	private IEnumerator moveToPlace(CheckPoints orig, CheckPoints next, TransportType type){
		while(true){
			//TODO Move at a given rate(tranport)
			if(true)
				break;
		}
		currentTime=coolDownTime;
		while(currentTime>0){
			yield return new WaitForSeconds(1);
			currentTime--;
		}
		currentCheck=next;
		yield break;
	}
	public void setSelected(bool b){
		if(isDisabled)
			return;
		if(b){
			if(GameRunningScript.getInstance().selectedPlayer!=this){
				if(GameRunningScript.getInstance().selectedPlayer!=null)
					GameRunningScript.getInstance().selectedPlayer.setSelected(false);
			}else
				return;
			GameRunningScript.getInstance().selectedPlayer=this;
			if(isClickable){
				//TODO Do operation to highlight
			}else{
				if(isMoving){
					//TODO Show the timeleft for cool down
				}else{
					//TODO Show the timeleft for cool down with the highlighting
				}
			}
		}else{
			//TODO Do operation to De highlight
			//TOOD Remove all cooldown and time left Window
		}
	}
	public CheckPoints getCurrentCheck(){
		return currentCheck;
	}
	public int removeCoolDown(){
		int temp = currentTime;
		currentTime=0;
		return temp;
	}
}
