using UnityEngine;
using System.Collections;

public class mouseDrag : MonoBehaviour {
	Vector3 distanceFromCamera;
	Vector3 posOfCamera;
	float zDesFromCam;
	GameObject des;
	// Use this for initialization
	void Start () {
		des = GameObject.Find ("Destination");
		posOfCamera = Camera.main.gameObject.transform.position;
		// fix the distance of object to camera unless there is a change in its position
		zDesFromCam = findZdisFromCam(posOfCamera, des.transform.position);
	}

	void Update () {
		// check if the position of camera has changed
		// **NOT TESTED YET** Camera now cannot move
		if (posOfCamera != Camera.main.gameObject.transform.position) {
			posOfCamera = Camera.main.gameObject.transform.position;
			zDesFromCam = findZdisFromCam(posOfCamera, des.transform.position);
			Debug.Log("Camera Pos Shifted. New pos = " + posOfCamera);
		}

	}

	void OnMouseDrag () {
		// Destination object will only move x and y axis when the mouse drags as seen from the camera.
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDesFromCam);
		//..ScreenToWorldPoint(x,y, distanceFromCamera)
		Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = objPosition;
	//	Debug.Log("mousePosition x = " + Input.mousePosition.x + ", y = "+ Input.mousePosition.y);
	}

	float findZdisFromCam (Vector3 cameraVec, Vector3 objVec) {
		Vector3 temp = cameraVec - objVec;
		return temp.magnitude;
	}
}
