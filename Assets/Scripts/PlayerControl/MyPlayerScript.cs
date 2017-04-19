using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

[RequireComponent(typeof(NetworkView))]
public class MyPlayerScript : MonoBehaviour {
	NetworkView netView;	
	private bool isLocalPlayer, isServer;
	private List<CheckPoints> allChecks;
	private List<CheckPoints> myPolices;
	private CheckPoints thief;
	private Character myChar;
	void Awake()
	{
		netView = GetComponent<NetworkView>();
		isLocalPlayer=netView.isMine;
		isServer=GameRunningScript.getInstance().isServer;
	}
	void Start () {
		if(isLocalPlayer){
			GameRunningScript.getInstance().localPlayer=this;
			myChar=GameRunningScript.getInstance().localPlayerChar;
			if(myChar==Character.Police){
				getPolices();
			}
		}else{
			GameRunningScript.getInstance().networkPlayer=this;
			myChar=GameRunningScript.getInstance().networkPlayerChar;
		}			
	}
	private void getPolices(){
		myPolices=new List<CheckPoints>();
		myPolices.Add(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(1));
		myPolices.Add(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(2));
		myPolices.Add(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(3));
		myPolices.Add(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(4));
		myPolices.Add(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(5));
		getThief();
	}
	private void getThief(){
		thief = GameRunningScript.getInstance().gameClickHandler.selectThiefLoc(myPolices);
	}

	public void initiateChecks(List<CheckPoints> checks){
		allChecks=checks;
	}
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	private void sendDetailsToNetwork(){
		//TODO this function should initiate the thief as well
	}
}
