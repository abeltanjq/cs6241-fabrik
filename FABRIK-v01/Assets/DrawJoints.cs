using UnityEngine;
using System.Collections;

public class DrawJoints : MonoBehaviour {
	// Place this script on root node

//	private int totalChild;
	// Use this for initialization
	void Start () {
	//	totalChild = this.gameObject.transform.childCount;
	}

	// Update is called once per frame
	void Update () {
		drawLineToChild (this.gameObject.transform);
	}
	void drawLineToChild (Transform me) {
		if (me.childCount != 0) {
			for (int i = 0; i < me.childCount; i++) {
				Debug.DrawLine (me.position, me.GetChild (i).position, Color.red, 0f, true);
				drawLineToChild (me.GetChild(i));
			}
		}
	}
}
