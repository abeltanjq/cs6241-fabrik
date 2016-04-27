using UnityEngine;
using System.Collections;

public class EndE_unityIK : MonoBehaviour {

	public Transform target;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().position = target.position;	
	}
}
