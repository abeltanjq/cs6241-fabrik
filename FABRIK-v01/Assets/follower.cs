using UnityEngine;
using System.Collections;

public class follower : MonoBehaviour {

	GameObject des;
	Transform target;
	public float speed = 0.2f;
	// Use this for initialization
	void Start () {
		
	}

	void reachForDes () {
		des = GameObject.Find ("Destination");
		target = des.transform;
	}

	// Update is called once per frame
	void Update () {
		reachForDes ();
		Vector3 moveToDes = target.position - this.transform.position;
	//	Debug.Log ("target pos = " + target.position);
		if (moveToDes.magnitude > 0.1) {
			float distance = speed + Time.deltaTime;
			// also means this.transform.Translate();
			transform.Translate (moveToDes.normalized * distance, Space.World);
			//	The Quaternion rotation below is correct but needs some fine tuning.
			//	Quaternion targetRotation = Quaternion.LookRotation (moveToDes);
			//	this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime);
		}
	}
}
