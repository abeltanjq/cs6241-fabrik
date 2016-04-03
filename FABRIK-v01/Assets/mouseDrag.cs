using UnityEngine;
using System.Collections;

public class mouseDrag : MonoBehaviour {
	Vector3 distanceFromCamera;
	float zDesFromCam;
	GameObject des;
	// Use this for initialization
	void OnMouseDrag () {
		des = GameObject.Find ("Destination");
		//assume des is z position is always >= 0
		zDesFromCam = Mathf.Abs(des.transform.position.z) + Mathf.Abs(Camera.main.gameObject.transform.position.z);
	//	distanceFromCamera = Camera.main.gameObject.transform.position - des.transform.position;
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDesFromCam);
		//..ScreenToWorldPoint(x,y, distanceFromCamera)
		Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = objPosition;
		Debug.Log("mousePosition x = " + Input.mousePosition.x + ", y = "+ Input.mousePosition.y);
	}
}
