using UnityEngine;
using System.Collections;

public class DrawJoints : MonoBehaviour {

	private int totalChild;
	// Use this for initialization
	void Start () {
		totalChild = this.gameObject.transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < (totalChild - 1); i++) {
			Debug.DrawLine (this.gameObject.transform.GetChild (i).position, this.gameObject.transform.GetChild (i + 1).position, Color.red, 0f, true);
		}
	}
}
