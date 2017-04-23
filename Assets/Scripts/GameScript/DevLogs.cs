using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public class UserPrefs{
		public static string scrollSpeed="ScrollSpeed", mobPanSensitivity="MovePanSensitivity", moveSensitivity="NormalMoveSensitivity";
	}
	public enum CheckTypes{
		Normal, Selected, PoliceUnselected, Cycle, Cart, Horse, Boat, CCart, CHorse, CBoat, CCartHorse, CCartBoat, CHorseBoat, CCartHorseBoat
	}
	public enum Tag{
		UnOrdered, GameClickListener,MyPlayerScript, Network, PoliceUIController, ThiefUIController, PlayerControllerScript
	}
	public class DevTag{
		public const int MAX=100;
		public bool[] list;
		private static DevTag instance; 
		private DevTag(){
			list =new bool[MAX];
			list[(int)Tag.GameClickListener]=true;
			list[(int)Tag.UnOrdered]=false;
			list[(int)Tag.MyPlayerScript]=true;
			list[(int)Tag.Network]=true;
			list[(int)Tag.PoliceUIController]=true;
			list[(int)Tag.ThiefUIController]=true;
			list[(int)Tag.PlayerControllerScript]=true;
			//list[(int)Tag.]=true;
		}
		public bool isAllowed(Tag tag){
			if(list[(int)tag])
				return true;
			else
				return false;
		}
		public static DevTag getInstance(){
			if(instance==null)
				instance=new DevTag();
			return instance;
		}
	}
	public class Dev{
		public static void log(Tag tag, object s){
			if(DevTag.getInstance().isAllowed(tag))
				Debug.Log(tag+" : "+s);
		}
		public static void error(Tag tag, object s){
			if(DevTag.getInstance().isAllowed(tag))
				Debug.LogError(tag+" : "+s);
		}
	}
}