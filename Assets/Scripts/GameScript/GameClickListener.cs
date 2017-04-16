using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public class GameClickListener : MonoBehaviour {

	public GameClickHandler clickHandler;
	private Vector3 trackDragVect, trackClickVect;
	private Camera cam;
	public GameObject playerprefab;

	private bool trackClicks;
	private float trackClickCount, camPanSpeed=10f, doubleClickTimeLimit=0.1f;
	// Use this for initialization
	void Start () {
		cam=Camera.main;
		Network.Instantiate(playerprefab,Vector3.zero, Quaternion.identity, 0);
	}
	
	private void detectZoom(float wheel){
		// Dev.log(Tag.GameMoveListener,"Touch Count : "+Input.touchCount);

		float deltaMagnitudeDiff=0f;
		
		if(wheel==0){
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			//deltaMagnitudeDiff = Mathf.Sqrt(deltaMagnitudeDiff);
		}else{
			deltaMagnitudeDiff=-wheel* PlayerPrefs.GetInt(UserPrefs.scrollSpeed, 400);
		}
		
		if(!(cam.transform.position.y<5 && cam.transform.position.y+deltaMagnitudeDiff<cam.transform.position.y))
			cam.transform.Translate(-cam.transform.forward*deltaMagnitudeDiff, Space.World);
	}
	
	private void SingleClick(Vector2 vect)
	{
		clickHandler.SingleClick(vect);
	}
	private void DoubleClick(Vector2 vect)
	{
		clickHandler.DoubleClick(vect);	
	}

	Vector3 intC,finC,accC;
	float rotspeed=10f;
	float angleDown(Vector3 v){
		Vector3 b = Vector3.down;
		v.Normalize ();
		return Mathf.Abs( Mathf.Acos(Vector3.Dot (v, b)) *Mathf.Rad2Deg);
	}
	void rotate(){
		if (Input.GetButtonDown ("Fire2")) {
			intC = Input.mousePosition;
		} else if (Input.GetButton ("Fire2")) {
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - intC);
			//Vector3 pos2 = new Vector3 (Input.GetAxisRaw("Mouse Y"),0f,0f)*rotspeed;

			//if (transform.up.y > 0 && transform.forward.y<0) {
			//Debug.Log(angleDown(transform.forward));
			float Vangle = angleDown (transform.forward);
			if (Vangle >30  && Vangle<90) {
				transform.RotateAround (transform.position, transform.right, -pos.y * rotspeed);
			} else {
				transform.RotateAround (transform.position, transform.right, -pos.y * rotspeed);
				//if (transform.up.y < 0 || transform.forward.y>0)
				float cV=angleDown(transform.forward);
				if((Vangle < 30 && cV<Vangle) || (Vangle>90 && cV>Vangle))
				//if (Vangle < eV || Vangle > eV)
					transform.RotateAround (transform.position, transform.right, pos.y * rotspeed);
			}
			transform.RotateAround(transform.position, Vector3.up, pos.x * rotspeed);
		}
	}
	private void checkClickListener(){
		// if(!isClicksActive)
		// 	return;

		if(trackClicks==false){
			//TODO DO the function for assigning the initial values
			if(Input.touchCount==1 && Input.GetTouch(0).phase==TouchPhase.Began){
				trackClickVect=Input.GetTouch(0).position;
				trackClicks=true;
				trackClickCount=0f;
			}
			else if(Input.GetButtonDown("Fire1")){
				trackClickVect=Input.mousePosition;
				trackClicks=true;
				trackClickCount=0f;
			}
		}
		else if(trackClickCount>doubleClickTimeLimit){
			SingleClick(trackClickVect);
			trackClicks=false;
			trackClickCount=0f;
		}
		else{
			if(Input.touchCount==1 && Input.GetTouch(0).phase==TouchPhase.Began){
				//if(GameMethods.sqrDist(trackClickVect, Input.GetTouch(0).position)<25)
				if(Vector3.Distance(trackClickVect, Input.GetTouch(0).position)<5)
					DoubleClick(trackClickVect);
				trackClicks=false;
			}else if(Input.touchCount==1 && Input.GetTouch(0).phase==TouchPhase.Moved){
				trackClicks=false;
			}
			else if(Input.GetButtonDown("Fire1"))
			{
				//Dev.error(Tag.UnOrdered,"It Entered Here3 : "+CrossPlatformInputManager.GetButtonDown("Fire1"));
				if(Vector2.Distance(trackClickVect, Input.mousePosition)<5)
					DoubleClick(trackClickVect);
				trackClicks=false;
			}else if(Input.GetButton("Fire1")){
				if(Vector2.Distance(trackClickVect, Input.mousePosition)>5){
					trackClicks=false;
				}
			}
			trackClickCount += Time.deltaTime;
		}
	}
	private void Pan(Vector2 touchDeltaPosition){
		// if(!isServer)
			// touchDeltaPosition=-touchDeltaPosition;
		// if(Mathf.Abs(cam.transform.position.x)> GameContants.getInstance().sizeOfBoardX*GameContants.getInstance().boxSize/2 && Mathf.Abs(cam.transform.position.x-touchDeltaPosition.x)>Mathf.Abs(cam.transform.position.x))
		// 	touchDeltaPosition.x=0;
		// //TODO Take care of this constant value if ever camera position is changed
		// if(Mathf.Abs(cam.transform.position.z)> 33*GameContants.getInstance().boxSize && Mathf.Abs(cam.transform.position.z-touchDeltaPosition.y)>Mathf.Abs(cam.transform.position.z))
		// 	touchDeltaPosition.y=0;
		Vector3 camF=cam.transform.forward;
		camF=Vector3.ProjectOnPlane(camF,Vector3.up).normalized;
		Vector3 camL=Vector3.Cross(Vector3.up, camF);
		Vector3 tot = camF*touchDeltaPosition.y + camL*touchDeltaPosition.x;
		//cam.transform.Translate(camF.x*touchDeltaPosition.x * camPanSpeed, 0, camF.z*touchDeltaPosition.y * camPanSpeed, Space.World);
		cam.transform.Translate(-tot *camPanSpeed, Space.World);
	}
	// Update is called once per frame
	void Update () {
		// applyeRestrictions=true;
		// if(applyeRestrictions)
		// 	return;

		// if(.disableClicks)
		// 	return;
		
		checkClickListener();
		rotate();
		if(Application.isMobilePlatform){
			if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				touchDeltaPosition=touchDeltaPosition*PlayerPrefs.GetFloat(UserPrefs.mobPanSensitivity, 0.6f);
				Pan(touchDeltaPosition);
			}else if (Input.touchCount == 2){
				detectZoom(0);
			}
		}else{
			if(Input.GetAxis("Mouse ScrollWheel")!=0){
				// Dev.log(Tag.GameMoveListener,Input.GetAxis("Mouse ScrollWheel"));
				detectZoom(Input.GetAxis("Mouse ScrollWheel"));
			}else if(Input.GetButtonDown("Fire1")){
				trackDragVect=Input.mousePosition;
			}else if(Input.GetButton("Fire1")){
				if(Input.mousePosition!=trackDragVect){
					Vector2 delta=Input.mousePosition-trackDragVect;
					trackDragVect=Input.mousePosition;
					delta = delta * PlayerPrefs.GetFloat(UserPrefs.moveSensitivity, 0.2f);
					Pan(delta);
				}
			}
		}
	}
}
