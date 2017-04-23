using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class PlayerControlScript : MonoBehaviour {

	public Material selectedMat, unselectedMat;
	public MeshRenderer meshRenderer;
	private bool isPolice;
	private CheckPoints currentCheck;
	private int index;
	private Character character;
	private bool isClickable, isMoving, isDisabled;
	private int coolDownTime, currentTime;
	private PoliceUIController policeController;
	private ThiefUIController thiefController;
	private SupremeController controller;
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

	public void initPolicePlayer(CheckPoints ch, PoliceUIController c, int ind){
		policeController=c;
		controller=policeController;
		initMyPlayer(ch, Character.Police, ind);
	}
	public void initThiefPlayer(CheckPoints ch, ThiefUIController c, int ind){
		thiefController=c;
		controller=thiefController;
		initMyPlayer(ch, Character.Thief, ind);
	}
	private void initMyPlayer(CheckPoints ch, Character c, int ind){
		isDisabled=false;
		character=c;
		index=ind;
		currentCheck=ch;
		Dev.log(Tag.PlayerControllerScript, "Initiatin the player at "+currentCheck.transform.position+" : "+currentCheck.getIndex());
		transform.position=currentCheck.transform.position;
		meshRenderer.material=unselectedMat;
		isClickable=true;
		setSelected(false);
	}

	public void moveMyPlayer(CheckPoints ch, TransportType type){
		currentCheck.setSelected(false);
		StartCoroutine(moveToPlace(currentCheck, ch, type));
	}
	private float getSpeed(TransportType type){
		//TODo Decide the speed of the user here.
		return 50;
	}
	private IEnumerator moveToPlace(CheckPoints orig, CheckPoints next, TransportType type){
		isMoving=true;
		isClickable=false;
		Vector3 finalPos=next.transform.position;
		float speed=getSpeed(type);
		while(true){
			//TODO Move at a given rate(tranport)
			Vector3 cur=GameMethods.getProjectionWithSpeed(finalPos, transform.position, speed, Time.deltaTime);
			transform.position=cur;
			if(cur==finalPos)
				break;
		}
		isMoving=false;
		if(GameRunningScript.getInstance().selectedPlayer==this){
			controller.showCooldownWindow();
		}
		currentTime=coolDownTime;
		while(currentTime>0){
			if(GameRunningScript.getInstance().selectedPlayer==this){
				controller.setCooldownTime(currentTime);
			}
			yield return new WaitForSeconds(1);
			currentTime--;
		}
		currentCheck=next;
		isClickable=true;
		if(GameRunningScript.getInstance().selectedPlayer==this){
			controller.hideAllWindows();
		}
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
			meshRenderer.material=selectedMat;
			if(isClickable){
				//TODO Do operation to highlight
			}else{
				if(isMoving){
					//TODO Show the timeleft for cool down
					controller.showTravellingWindow();
					
				}else{
					//TODO Show the timeleft for cool down with the highlighting
					controller.showCooldownWindow();
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
