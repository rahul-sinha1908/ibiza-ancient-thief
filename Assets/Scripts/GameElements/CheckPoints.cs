using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGame;

public class CheckPoints : MonoBehaviour {

	public bool bCycle, bBullock, bHorse, bBoat, bMoney;
	public bool bShop;
	public List<Transform> cycle, bullock, horse, boat;
	public MeshRenderer myMesh;
	public TextMesh myText;
	public List<Material> materials;
	private bool isClickable;

	// Use this for initialization
	void Start () {
		isClickable=false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSelected(bool b){
		if(b){
			//TODO Write Code to select the current
			setType(CheckTypes.Selected);
			foreach(Transform t in cycle){
				if(t==null)
					continue;
				CheckPoints ch = t.GetComponent<CheckPoints>();
				ch.setType(CheckTypes.Cycle);
			}
			foreach(Transform t in bullock){
				if(t==null)
					continue;
				CheckPoints ch = t.GetComponent<CheckPoints>();
				ch.setType(CheckTypes.Cart);
			}
			foreach(Transform t in horse){
				if(t==null)
					continue;
				CheckPoints ch = t.GetComponent<CheckPoints>();
				ch.setType(CheckTypes.Horse);
			}
			foreach(Transform t in boat){
				if(t==null)
					continue;
				CheckPoints ch = t.GetComponent<CheckPoints>();
				ch.setType(CheckTypes.Boat);
			}
		}else{
			//TODO Write Code to disselect the current checkBox
			setType(CheckTypes.Normal);
			foreach(Transform t in cycle){
				if(t==null)
					continue;
				CheckPoints ch = t.GetComponent<CheckPoints>();
				ch.setType(CheckTypes.Normal);
			}
			foreach(Transform t in bullock){
				if(t==null)
					continue;
				CheckPoints ch = t.GetComponent<CheckPoints>();
				ch.setType(CheckTypes.Normal);
			}
			foreach(Transform t in horse){
				if(t==null)
					continue;
				CheckPoints ch = t.GetComponent<CheckPoints>();
				ch.setType(CheckTypes.Normal);
			}
			foreach(Transform t in boat){
				if(t==null)
					continue;
				CheckPoints ch = t.GetComponent<CheckPoints>();
				ch.setType(CheckTypes.Normal);
			}
		}
	}
	private void setType(CheckTypes type){
		if(type==CheckTypes.Normal)
			isClickable=false;
		else
			isClickable=true;
		myMesh.material=materials[(int)type];
	}
	public void setClickable(bool b){
		isClickable=b;
	}
	public bool getClickable(){
		return isClickable;
	}
}
