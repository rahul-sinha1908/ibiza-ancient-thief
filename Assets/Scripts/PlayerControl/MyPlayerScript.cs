using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

[RequireComponent(typeof(NetworkView))]
public class MyPlayerScript : MonoBehaviour {
	public List<PlayerControlScript> myPolices;
	public PlayerControlScript myThief;
	public PoliceUIController policeController;
	public ThiefUIController thiefController;
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

		if(GameRunningScript.getInstance().myPlayerChar==Character.Police)
			policeController.gameObject.SetActive(true);
		else
			thiefController.gameObject.SetActive(true);
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







	/*
	RPC's For Polices.......................................................
	*/
	[RPC]
	private void policeAskRAdius(int ind, int radius){
		CheckPoints check=gameClickHandler.allChecks[ind];
		Vector3 playerPos = check.transform.position;
		Vector3 myPos = myThief.transform.position;
		float dist2 = GameMethods.getSqrDist(playerPos, myPos);
		if(radius*radius<dist2*dist2){
			netView.RPC("thiefAskRadiusAnswer" , RPCMode.OthersBuffered, new object[]{false});
		}else{
			netView.RPC("thiefAskRadiusAnswer" , RPCMode.OthersBuffered, new object[]{true});
		}
	}

	[RPC]
	private void policeAskDirection(int ind, int radius){
		CheckPoints check=gameClickHandler.allChecks[ind];
		Vector3 playerPos = check.transform.position;
		Vector3 myPos = myThief.transform.position;
		float dist2 = GameMethods.getSqrDist(playerPos, myPos);
		if(radius*radius<dist2*dist2){
			netView.RPC("thiefAskDirectionAnswer", RPCMode.OthersBuffered, new object[]{null});
		}else{
			//TODO Get the direction and send
		}
	}

	[RPC]
	private void policeSendMove(int ind, int ch, int tType){
		TransportType type = (TransportType)tType;
		myPolices[ind].moveMyPlayer(gameClickHandler.allChecks[ch],type);
	}










	/*
	RPC's For Thief..........................................................
	*/
	[RPC]
	private void thiefAskRadiusAnswer(bool b){
		if(true){

		}else{

		}
	}

	[RPC]
	private void thiefAskDirectionAnswer(int[] inds){
		if(inds==null){
			//TODO No direction Found.
			return;
		}
	}

	[RPC]
	private void thiefSendMoves(int move){
		//TODO Display the move on the police panel
	}
}
