using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

[RequireComponent(typeof(NetworkView))]
public class MyPlayerScript : MonoBehaviour {
	public List<PlayerControlScript> myPolices;
	public PlayerControlScript myThief;
	NetworkView netView;
	private bool isLocalPlayer, isServer;
	public GameClickHandler gameClickHandler;
	//private List<CheckPoints> myPolices;
	//private CheckPoints myThief;
	private Character myChar;
	void Awake()
	{
		netView = GetComponent<NetworkView>();
		isLocalPlayer=netView.isMine;
		isServer=GameRunningScript.getInstance().isServer;
	}
	void Start () {
		myPolices=GameRunningScript.getInstance().policePos;
		GameRunningScript.getInstance().myPlayer=this;
		myChar=GameRunningScript.getInstance().myPlayerChar;
	}
	
	public void getPolices(){
		myPolices.Clear();
		for(int i=0;i<5;i++){
			myPolices[i].initMyPlayer(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(i+1), Character.Police, i);
		}
		getThief();
	}
	private void getThief(){
		myThief.initMyPlayer(GameRunningScript.getInstance().gameClickHandler.selectThiefLoc(myPolices), Character.Thief, -1);
		GameRunningScript.getInstance().thiefPos=myThief;
		formInitialMessage();
	}
	private void formInitialMessage(){
		int[] police= new int[]{myPolices[0].getCurrentCheck().getIndex(), myPolices[1].getCurrentCheck().getIndex(), myPolices[2].getCurrentCheck().getIndex()
								, myPolices[3].getCurrentCheck().getIndex(), myPolices[4].getCurrentCheck().getIndex()};
		netView.RPC("sendDetailsToNetwork", RPCMode.OthersBuffered, new object[]{police});
	}

	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	private void sendDetailsToNetwork(int[] polices){
		//TODO this function should initiate the thief as well
		for(int i=0;i<5;i++){
			myPolices[i].initMyPlayer(gameClickHandler.allChecks[polices[i]], Character.Police, i);
		}
	}
}
