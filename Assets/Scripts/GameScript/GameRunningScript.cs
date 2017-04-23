using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public enum Character{
		Police, Thief
	}
	public enum TransportType{
		Cycle, BullockCart, Horse, Boat, HiddenMove
	}
	public class GameRunningScript{
		private static GameRunningScript instance;
		public bool isClickActive, playSound;
		public MyNetworkScript myNetworkScript;
		public MyPlayerScript myPlayer;
		public bool gamePlayable;
		public GameClickListener gameClickListener;
		public GameClickHandler gameClickHandler;
		public Character myPlayerChar;
		public List<PlayerControlScript> policePos;
		public PlayerControlScript thiefPos;
		public PlayerControlScript selectedPlayer;
		public int countDown;
		public bool isServer;
		private GameRunningScript(){
			policePos=new List<PlayerControlScript>();
			isClickActive=true;
			countDown=-1;
		}
		public static GameRunningScript getInstance(){
			if(instance==null)
				instance=new GameRunningScript();
			return instance;
		}
	}
}