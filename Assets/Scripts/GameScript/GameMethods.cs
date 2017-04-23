using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public class GameMethods{
		public static float getSqrDist(Vector3 v1, Vector3 v2){
			v1 = v1-v2;
			return v1.sqrMagnitude;
		}

		public static Vector3 getProjectionWithSpeed(Vector3 dest, Vector3 current, float speed, float timeLag){
			Vector3 change = (dest-current).normalized * speed * timeLag;
			Vector3 finalPos;
			if(change.sqrMagnitude < (dest-current).sqrMagnitude)
				finalPos = current + change;
			else
				finalPos=dest;
			return finalPos;
		}
	}
}