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
	private CheckPoints myThief;
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
		myPolices.Add(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(1));
		myPolices.Add(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(2));
		myPolices.Add(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(3));
		myPolices.Add(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(4));
		myPolices.Add(GameRunningScript.getInstance().gameClickHandler.selectRandomPolice(5));
		foreach(CheckPoints ch in myPolices){
			ch.setType(CheckTypes.PoliceUnselected);
		}
		getThief();
	}
	private void getThief(){
		myThief = GameRunningScript.getInstance().gameClickHandler.selectThiefLoc(myPolices);
		GameRunningScript.getInstance().thiefPos=myThief;
		formInitialMessage();
	}
	private void formInitialMessage(){
		int[] police= new int[]{myPolices[0].getIndex(), myPolices[1].getIndex(), myPolices[2].getIndex()
								, myPolices[3].getIndex(), myPolices[4].getIndex()};
		int thief=myThief.getIndex();		
		netView.RPC("sendDetailsToNetwork", RPCMode.OthersBuffered, new object[]{police, thief});
	}
	public void initiateChecks(List<CheckPoints> checks){
		allChecks=checks;
	}
	// Update is called once per frame
	void Update () {
		
	}
	private void initThiefSystem(){
		myThief.setSelected(true);
	}

	[RPC]
	private void sendDetailsToNetwork(int[] polices, int thief){
		//TODO this function should initiate the thief as well
		GameRunningScript.getInstance().policePos = new List<CheckPoints>(){allChecks[polices[0]], allChecks[polices[1]]
						,allChecks[polices[2]],allChecks[polices[3]],allChecks[polices[4]]};
		GameRunningScript.getInstance().thiefPos=allChecks[thief];
		myThief=GameRunningScript.getInstance().thiefPos;
		myPolices=GameRunningScript.getInstance().policePos;

		initThiefSystem();
	}

	public void sendMoves(){
		Dev.log(Tag.MyPlayerScript, "Came to send moves");
		if(GameRunningScript.getInstance().countDown>-1){
			netView.RPC("changeTimeLeft", RPCMode.AllBuffered, new object[]{-5});
		}else{
			StartCoroutine(countDown());
		}
	}

	private IEnumerator countDown(){
		int t=10;
		Dev.log(Tag.MyPlayerScript, "Init the count down service.");
		while(true){
			if(GameRunningScript.getInstance().countDown <=-5){
				t=0;
			}
			if(t<-1)
				t=-1;
			else{
				netView.RPC("changeTimeLeft", RPCMode.AllBuffered, new object[]{t});
				if(t==-1)
					yield break;
			}
			t--;
			yield return new WaitForSeconds(1);
		}
	}
	private void sendYourRespectiveMoves(){
		GameRunningScript.getInstance().isClickActive=true;
		Dev.log(Tag.MyPlayerScript, "Moves Send !!");
		if(myChar==Character.Police){
			//TODO Send all police moves
		}else{
			//TODO Send all thief moves
		}
	}
	
	[RPC]
	public void changeTimeLeft(int t){
		GameRunningScript.getInstance().countDown=t;
		if(t==0)
			sendYourRespectiveMoves();
	}
}
