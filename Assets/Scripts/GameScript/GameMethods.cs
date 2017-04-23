using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public class GameMethods{
		public static float getSqrDist(Vector3 v1, Vector3 v2){
			v1 = v1-v2;
			return v1.sqrMagnitude;
		}
	}
}