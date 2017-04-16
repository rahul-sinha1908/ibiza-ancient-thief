using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class GameClickHandler : MonoBehaviour {

	public LayerMask layerMask;
	public int checkTemp=-1;
	private Camera cam;
	private CheckPoints selectedPoints;
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
			if(!check.getClickable())
				return;
			if(selectedPoints!=null)
				selectedPoints.setSelected(false);
			check.setSelected(true);
			selectedPoints = check;
			Dev.log(Tag.GameClickListener,"Hit : "+check.name);
		}
	}

	public void DoubleClick(Vector2 vect){

	}

	// Update is called once per frame
	void Update () {
		
	}
}
