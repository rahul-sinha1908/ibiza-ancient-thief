using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MyGame;
public struct IPS{
	public string ip;
	public string name;
};
public class NetworkDisc : NetworkDiscovery
{
	private MyNetworkScript netScript;
	[SerializeField]
	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		if(netScript==null)
			netScript=GameRunningScript.getInstance().myNetworkScript;
		netScript.addPlayer(fromAddress, data);
	}
	
	
}