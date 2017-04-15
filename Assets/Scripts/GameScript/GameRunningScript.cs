using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public class GameRunningScript{
		private static GameRunningScript instance;
		public bool isClickActive, playSound;
		public MyNetworkScript myNetworkScript;
		public MyPlayerScript localPlayer, networkPlayer; 
		public bool gamePlayable;
		public GameClickListener gameClickListener;
		public bool isServer;
		private GameRunningScript(){

		}
		public static GameRunningScript getInstance(){
			if(instance==null)
				instance=new GameRunningScript();
			return instance;
		}
	}
}