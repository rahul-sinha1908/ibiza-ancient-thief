using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class GameClickHandler : MonoBehaviour {

	public LayerMask layerMask;
	private Camera cam;
	// Use this for initialization
	void Start () {
		cam=Camera.main;
	}
	
	public void SingleClick(Vector2 vect){
		Ray ray = cam.ScreenPointToRay(vect);
		RaycastHit point;
		if(Physics.Raycast(ray,out point, 20000, layerMask)){
			if(point.collider.gameObject.GetComponent<CheckPoints>()==null)
				return;
			CheckPoints check =point.collider.gameObject.GetComponent<CheckPoints>();
			Dev.log(Tag.GameClickListener,"Hit : "+check.name);
		}
	}

	public void DoubleClick(Vector2 vect){
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
