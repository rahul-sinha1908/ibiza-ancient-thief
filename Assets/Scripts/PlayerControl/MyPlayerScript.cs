using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

[RequireComponent(typeof(NetworkView))]
public class MyPlayerScript : MonoBehaviour {
	NetworkView netView;
	private bool isLocalPlayer, isServer;
	void Awake()
	{
		netView = GetComponent<NetworkView>();
		isLocalPlayer=netView.isMine;
		isServer=GameRunningScript.getInstance().isServer;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
