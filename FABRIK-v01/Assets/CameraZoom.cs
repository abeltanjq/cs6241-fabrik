using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {
	public GameObject targetLookAt = null;
	private bool rotateR = false;
	private bool rotateL = false;

	void Start() {
		if (targetLookAt != null) {
			transform.LookAt (targetLookAt.transform);
		}
	}

	void  Update (){
		// -------------------Code for Zooming Out------------
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (Camera.main.fieldOfView<=125)
				Camera.main.fieldOfView +=2;
			if (Camera.main.orthographicSize<=20)
				Camera.main.orthographicSize +=0.5f;

		}
		// ---------------Code for Zooming In------------------------
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (Camera.main.fieldOfView>2)
				Camera.main.fieldOfView -=2;
			if (Camera.main.orthographicSize>=1)
				Camera.main.orthographicSize -=0.5f;
		}

		// -------Code to switch camera between Perspective and Orthographic--------
		if (Input.GetKeyUp(KeyCode.B ))
		{
			if (Camera.main.orthographic==true)
				Camera.main.orthographic=false;
			else
				Camera.main.orthographic=true;
		}

		if (rotateR) {			
			transform.RotateAround (targetLookAt.transform.position, Vector3.up, Time.deltaTime * 30);
		}

		if (rotateL) {			
			transform.RotateAround (targetLookAt.transform.position, Vector3.down, Time.deltaTime * 30);
		}

		if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
			if (targetLookAt != null) {
				rotateR = true;
				rotateL = false;

			}
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
			if (targetLookAt != null) {
				rotateL = true;
				rotateR = false;
			}
		}

		if (Input.GetKeyDown(KeyCode.S)){
			if (targetLookAt != null) {
				rotateL = false;
				rotateR = false;
			}
		}
	}


}