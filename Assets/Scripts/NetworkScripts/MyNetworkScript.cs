using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MyGame;

[RequireComponent(typeof(NetworkView))]
public class MyNetworkScript : MonoBehaviour {
	public class IpAddress{
		public string ip, name;
		public IpAddress(string ip, string name){
			this.ip=ip;
			this.name=name;
		}
	}
	public NetworkDiscovery networkDisc;
	private NetworkView netView;
	private List<IpAddress> ipList;
	private string registeredName="infinito_divertido_ibiza_thief_19081997";
	private int portNum=190897;


//{start}Temp Section Delete After Testing is done.
	public void startOnlineServer(){
		Dev.log(Tag.Network,"You Clicked to initiate Server1");
		GameRunningScript.getInstance().myChar=Character.Police;
		Network.InitializeServer(2,portNum,true);
		Dev.log(Tag.Network,"You Clicked to initiate Server3");
	}
	public void searchOnline(){
		Dev.log(Tag.Network,"You Clicked to Search Server 1");
		GameRunningScript.getInstance().myChar=Character.Thief;
		Network.Connect("127.0.0.1",portNum);
		Dev.log(Tag.Network,"You Clicked to Search Server 2");
	}
//{close}Temp Section Delete After Testing is done.



	// Use this for initialization
	void Start () {
		GameRunningScript.getInstance().myNetworkScript=this;
		netView=GetComponent<NetworkView>();
	}
	
	
	private void offlineBroadcast(string gameName){
		networkDisc.broadcastData=gameName;
		networkDisc.Initialize();
		networkDisc.broadcastData=gameName;
		networkDisc.StartAsServer();
	}
	private void onlineBroadcast(string gameName){
		MasterServer.RegisterHost(registeredName, gameName, "Fuck the whole Universe");
	}

	/// <summary>
	/// Called on the server whenever a Network.InitializeServer was invoked and has completed.
	/// </summary>
	void OnServerInitialized()
	{
		Dev.log(Tag.Network, "Now you are the server.");
		GameRunningScript.getInstance().isServer=true;
	}
	
	
	/// <summary>
	/// Called on clients or servers when reporting events from the MasterServer.
	/// </summary>
	/// <param name="msEvent">The MasterServerEvent which ocurred.</param>
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if(msEvent == MasterServerEvent.RegistrationSucceeded){
			Dev.log(Tag.Network,"Successfully Registered");
			//MasterServer.RequestHostList(registeredName);
		}else if(msEvent == MasterServerEvent.HostListReceived){
			HostData[] host=MasterServer.PollHostList();
			Dev.log(Tag.Network,"Got the Hosts : "+host.Length);
			foreach(HostData h in host){
				Dev.log(Tag.Network,h.gameName+" : "+h.comment);
				addButtons(h);
			}
			//MasterServer.RequestHostList(registeredName);
		}else{

		}
	}

	/// <summary>
	/// Called on the server whenever a new player has successfully connected.
	/// </summary>
	/// <param name="player">The NetworkPlayer which just connected.</param>
	void OnPlayerConnected(NetworkPlayer player)
	{
		Dev.log(Tag.Network, "Player is connected : "+ player.externalIP);

		if(networkDisc.isClient || networkDisc.isClient)
			networkDisc.StopBroadcast();
		//TODO Change the Scene.
		NetworkView netView = GetComponent<NetworkView>();
		netView.RPC("openScene",RPCMode.All, null);
	}

	/// <summary>
	/// Called on the client when you have successfully connected to a server.
	/// </summary>
	void OnConnectedToServer()
	{
		Dev.log(Tag.Network, "Connected to the Server");
		
	}

	/// <summary>
	/// Called on the client when the connection was lost or you disconnected from the server.
	/// </summary>
	/// <param name="info">NetworkDisconnection data associated with this disconnect.</param>
	void OnDisconnectedFromServer(NetworkDisconnection info) {
        if (Network.isServer){
            Debug.Log("Local server connection disconnected");
		}else{
            if (info == NetworkDisconnection.LostConnection){
                Debug.Log("Lost connection to the server");
			}else{
                Debug.Log("Successfully diconnected from the server");
			}
		}
    }


//----------------------------Non Network Calls ----------------------------------

	public void disconnectAndGoBack(){
		//TODO Check the things initialised and shut it off.
		Network.Disconnect();
		if(networkDisc.isClient || networkDisc.isServer)
			networkDisc.StopBroadcast();
	}

	private void onlineSearch(){
		MasterServer.RequestHostList(registeredName);
	}

	private void offlineSearch(){
		networkDisc.Initialize();
		networkDisc.StartAsClient();
	}
	private void addButtons(IpAddress ip){


	}
	private void addButtons(HostData ip){
		
		
	}

	public void addPlayer(string ip, string name){
		ip=ip.Trim().Substring(7);
		foreach(IpAddress i in ipList){
			if(i.ip==ip)
				return;
		}
		Dev.log(Tag.Network, "Local  : "+ip+" : "+name);
		IpAddress ipp=new IpAddress(ip, name);
		ipList.Add(ipp);
		addButtons(ipp);
	}
	private void connectToHost(string s){
		networkDisc.StopBroadcast();
		Network.Connect(s,portNum);
	} 
	private void connectToHost(HostData s){
		Network.Connect(s);
	}
	void Update () {
		
	}

	private void destroyAllChilds(Transform t){
		foreach(Transform child in t){
			Destroy(child.gameObject);
		}
	}

	[RPC]
	void openScene(){
		//TODO Open your own Scene
		SceneManager.LoadScene("Level");
	}
}

