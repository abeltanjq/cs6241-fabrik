using UnityEngine;
using System.Collections;

public class follower : MonoBehaviour {

	GameObject des;
	Transform target;
	// Use this for initialization
	void Start () {
		des = GameObject.Find ("Destination");
	}

	void reachForDes () {
		target = des.transform;
	}

	// Update is called once per frame
	void Update () {
		Vector3 moveToDes = target.position - this.transform.LocalPosition;

	}
}
