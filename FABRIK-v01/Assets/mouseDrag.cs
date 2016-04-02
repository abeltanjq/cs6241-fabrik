using UnityEngine;
using System.Collections;

public class mouseDrag : MonoBehaviour {
	float distance = 10;
	// Use this for initialization
	void OnMouseDrag () {
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = objPosition;
		Debug.Log("mousePosition x = " + Input.mousePosition.x + ", y = "+ Input.mousePosition.y);
	}
}
